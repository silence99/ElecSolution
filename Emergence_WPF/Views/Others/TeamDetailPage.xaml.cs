using Business.Services;
using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Framework;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Utils;

namespace Emergence_WPF
{
	/// <summary>
	/// TeamListPage.xaml 的交互逻辑
	/// </summary>
	public partial class TeamDetailPage : Page
	{
		TeamDetailPageViewModel ViewModel { get; set; }
		TeamService TeamService = new TeamService();
		public TeamDetailPage(TeamModel team)
		{
			InitializeComponent();
			InitViewModel(team);
		}

		private void InitViewModel(TeamModel team)
		{
			ViewModel = new TeamDetailPageViewModel().CreateAopProxy();
			DataContext = ViewModel;
			ViewModel.ID = team.ID;
			ViewModel.PersonCharge = team.PersonCharge;
			ViewModel.PersonChargePhone = team.PersonChargePhone;
			ViewModel.TeamDept = team.TeamDept;
			ViewModel.TeamName = team.TeamName;
            ViewModel.TeamLocale = team.TeamLocale;
            ViewModel.TeamMemberId = team.TeamMemberId;
			ViewModel.TotalNumber = team.TotalNumber;

			SyncTeamMembers();
            ViewModel.SetPopupEditToFullScreen += this.FullPageEditPopup;

        }

        private void Delete_Handler(object sender, RoutedEventArgs e)
		{
            var removeList = ViewModel.Members.Where(mbr => mbr.IsChecked).Select(mbr => mbr.ID.ToString()).ToList();
            if (removeList != null && removeList.Count() > 0)
            {
                var result = TeamService.DeleteTeamMembers(ViewModel.Members.Where(mbr => mbr.IsChecked).Select(mbr => mbr.ID.ToString()).ToList());

                if (result)
                {
                    System.Windows.MessageBox.Show("删除成功！");
                    SyncTeamMembers();
                }
                else
                {
                    System.Windows.MessageBox.Show("删除失败！");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("请选择要删除的人员！");
            }

		}

		private void ClosePopup_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.ClosePopup();
		}
        
        private void Import_Handler(object sender, RoutedEventArgs e)
        {
            #region choose import file
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Microsoft Excel 2013|*.xlsx";
            var dlgResult = dialog.ShowDialog();
            if (dlgResult != DialogResult.OK && dlgResult != DialogResult.Yes)
            {
                return;
            }
            #endregion
            #region check file if exists
            FileStream stream = null;
            try
            {
                //stream = new FileStream(dialog.FileName, FileMode.Open);
                stream = File.OpenRead(dialog.FileName);
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show("上传失败，请联系管理员！");
                return;
            }
            #endregion


            ICollection<PersonModel> teamMembers = new List<PersonModel>();
            #region read excel
            using (stream)
            {
                ExcelPackage package = new ExcelPackage(stream);

                ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                #region check excel format
                if (sheet == null)
                {
                    System.Windows.MessageBox.Show("Excel文件格式不正确!");
                    return;
                }
                if ((sheet.Cells[1, 1].Value != null && sheet.Cells[1, 2].Value != null && sheet.Cells[1, 3].Value != null) && 
                    (!sheet.Cells[1, 1].Value.Equals("姓名") ||
                     !sheet.Cells[1, 2].Value.Equals("手机号") ||
                     !sheet.Cells[1, 3].Value.Equals("职位")))
                {
                    System.Windows.MessageBox.Show("Excel文件格式不正确!");
                    return;
                }
                #endregion

                #region get last row index
                int lastRow = sheet.Dimension.End.Row;
                while (sheet.Cells[lastRow, 1].Value == null)
                {
                    lastRow--;
                }
                #endregion

                var teamMemberPlace = MetaDataService.TeamMemberPlaces;
                bool uploadFailed = false;
                string uploadFailedStr = "上传失败，以下行数据有误：";

                #region read datas
                for (int i = 2; i <= lastRow; i++)
                {
                    object value;
                    PersonModel pm = new PersonModel();
                    value = sheet.Cells[i, 1].Value;
                    if (value != null && CheckString(value.ToString(),0,28))
                    {
                        pm.Name = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    value = sheet.Cells[i, 2].Value;
                    if (value != null && CheckPhoneNumber(value.ToString()))
                    {
                        pm.PhoneNumber = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    value = sheet.Cells[i, 3].Value;
                    if (value != null && teamMemberPlace.Where(a => a.Name == value.ToString()).Count() > 0)
                    {
                        pm.PlaceName = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    teamMembers.Add(pm);
                }

                if (teamMembers.Count() < 1)
                {
                    System.Windows.MessageBox.Show("文档内没有有效的数据，请检查后上传!");
                    return;
                }
                
                if (uploadFailed )
                {
                    System.Windows.MessageBox.Show(uploadFailedStr + "请检查后上传!");
                    return;
                }

                var uploadString = JSONHelper.ToJsonString(teamMembers.Select(a => new
                {
                    name = a.Name,
                    phoneNumber = a.PhoneNumber,
                    place = a.PlaceName
                }).ToArray());
                #endregion
                var uploadResult = TeamService.ImportTeamMembers(ViewModel.ID, uploadString);
                if (!uploadResult)
                {
                    System.Windows.MessageBox.Show("上传失败，请联系管理员!");
                }
                SyncTeamMembers();
            }
            #endregion
        }

        private void Add_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.CurrentPerson = new PersonModel().CreateAopProxy();
			ViewModel.CurrentPerson.Place = ViewModel == null || ViewModel.Places.Count == 0 ? "" : ViewModel.Places[0].Value;
			ViewModel.CurrentPerson.PlaceName = ViewModel == null || ViewModel.Places.Count == 0 ? "" : ViewModel.Places[0].Name;
            DependencyObject parent = this.PopupEditTeamMember.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = ResolutionService.Height;
                    element.Width = ResolutionService.Width;
                    break;
                }
            }
            while (parent != null);
            ViewModel.IsAddMember = true;
			ViewModel.PopupTeamEdit();
            SetPopupToFullScreen();
        }

		private void Edit_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.CurrentPerson = (sender as Image).DataContext as PersonModel;
            DependencyObject parent = this.PopupEditTeamMember.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = ResolutionService.Height;
                    element.Width = ResolutionService.Width;
                    break;
                }
            }
            while (parent != null);
            ViewModel.IsAddMember = false;
			ViewModel.PopupTeamEdit();
            SetPopupToFullScreen();
        }

		private void Click_SearchTextBox(object sender, RoutedEventArgs e)
		{
			SyncTeamMembers();
		}

		private void GridTeamList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ViewModel.CurrentPerson = GridTeamMembersList.SelectedItem as PersonModel;
			ViewModel.PopupTeamEdit();
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			SyncTeamMembers();
		}

		private void SyncTeamMembers()
		{
			var result = TeamService.GetTeamPersons(ViewModel.PageIndex, ViewModel.PageSize, ViewModel.ID);
			ViewModel.PageIndex = result.PageIndex;
			ViewModel.PageSize = result.PageSize;
			ViewModel.TotalCount = result.Count;
			ViewModel.TotalPage = (int)Math.Ceiling((double)ViewModel.TotalCount / ViewModel.PageSize);
			ViewModel.Members = new System.Collections.ObjectModel.ObservableCollection<PersonModel>(result.Data);
		}

		//private void EditTeam_Handler(object sender, RoutedEventArgs e)
		//{
		//	if (ViewModel.IsTeamEditModel)
		//	{
		//		CancelEditMode();
		//		TeamService.UpdateTeam(ViewModel.ID, ViewModel.TeamName, ViewModel.PersonCharge, ViewModel.PersonChargePhone, ViewModel.TeamDept);
		//	}
		//	else
		//	{
		//		GotoEditModel();
		//	}
  //      }

		private void EditTeamMember_Handler(object sender, RoutedEventArgs e)
        {
            var result1 = false;
            foreach (var item in TDPPopupBindingGroup.BindingExpressions)
            {
                item.UpdateSource();
                if (item.HasValidationError)
                {
                    result1 = true;
                }
            }
            if (!result1)
            {

                if (ViewModel.IsAddMember)
                {
                    var result = TeamService.CreateTeamMember(ViewModel.ID.ToString(), ViewModel.CurrentPerson.Name, ViewModel.CurrentPerson.PhoneNumber, ViewModel.CurrentPerson.Place);
                    if (result)
                    {
                        ViewModel.ClosePopup();
                        System.Windows.MessageBox.Show("添加成功！");
                        SyncTeamMembers();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("添加失败！");
                    }
                }
                else
                {
                    var result = TeamService.UpdateTeamMember(ViewModel.CurrentPerson.ID, ViewModel.ID.ToString(), ViewModel.CurrentPerson.Name, ViewModel.CurrentPerson.PhoneNumber, ViewModel.CurrentPerson.Place);
                    if (result)
                    {
                        ViewModel.ClosePopup();
                        System.Windows.MessageBox.Show("编辑成功！");
                        SyncTeamMembers();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("编辑失败！");
                    }
                }
            }
		}
        private void FullPageEditPopup()
        {
            DependencyObject parent = this.PopupEditTeamMember.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = ResolutionService.Height;
                    element.Width = ResolutionService.Width;
                    break;
                }
            }
            while (parent != null);
        }

        private void CancelEdit_Handler(object sender, RoutedEventArgs e)
		{
			CancelEditMode();
		}

		private void CancelEditMode()
		{
			ViewModel.CancelEditButtonVisibility = Visibility.Hidden;
			ViewModel.IsTeamEditModel = false;
			ViewModel.EditTeamButtonLabel = "编辑";
		}

		private void GotoEditModel()
		{
			ViewModel.IsTeamEditModel = true;
			ViewModel.CancelEditButtonVisibility = Visibility.Visible;
			ViewModel.EditTeamButtonLabel = "更新";
		}

		private void GoBack(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new Uri(@".\Views\Others\TeamListPage.xaml", UriKind.Relative));
			NavigationService.RemoveBackEntry();
		}

        private bool CheckPhoneNumber(string Number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Number, @"^[1]+[3,5,7,8,9]+\d{9}");
        }

        private bool CheckString(string str, int minStrLength, int maxStrLength)
        {
            return !string.IsNullOrEmpty(str) && str.Length >= minStrLength && str.Length <= maxStrLength;
        }

        public void SetPopupToFullScreen()
        {
            DependencyObject parent = this.PopupEditTeamMember.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = ResolutionService.Height;
                    element.Width = ResolutionService.Width;
                    break;
                }
            }
            while (parent != null);
        }
    }
}
