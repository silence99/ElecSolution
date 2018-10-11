using Business.Services;
using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Framework;
using log4net;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
	public partial class TeamListPage : Page
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private TeamListPageViewModel ViewModel { get; set; }
		private TeamService TeamService = new TeamService();
		public TeamListPage()
		{
			InitializeComponent();

		}

		private void Delete_Handler(object sender, RoutedEventArgs e)
		{
			var teams = ViewModel.Teams.Where(team => team.IsChecked).ToList();
			if (teams != null && teams.Count != 0)
			{
				var result = TeamService.DeleteTeamMembers(teams.Select(t => t.TeamMemberId.ToString()).ToList());

				if (result)
				{
					System.Windows.MessageBox.Show("删除成功！");
					GetTeams();
				}
				else
				{
					System.Windows.MessageBox.Show("删除失败！");
				}
			}
			else
			{
				System.Windows.MessageBox.Show("没有选择要删除的队员");
			}
		}


        private void Import_Handler(object sender, RoutedEventArgs e)
        {
            try
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
                //stream = new FileStream(dialog.FileName, FileMode.Open);
                stream = File.OpenRead(dialog.FileName);
                #endregion


                ICollection<TeamMemberModel> teamMembers = new List<TeamMemberModel>();
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
                    if ((sheet.Cells[1, 2].Value != null && sheet.Cells[1, 3].Value != null && sheet.Cells[1, 4].Value != null && sheet.Cells[1, 5].Value != null && sheet.Cells[1, 6].Value != null) &&
                        (!sheet.Cells[1, 2].Value.Equals("姓名") ||
                         !sheet.Cells[1, 3].Value.Equals("移动电话") ||
                         !sheet.Cells[1, 4].Value.Equals("所属单位") ||
                         !sheet.Cells[1, 5].Value.Equals("队伍名称") ||
                         !sheet.Cells[1, 6].Value.Equals("队长队员")))
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
                    var teamDepart = MetaDataService.TeamDepartments;
                    var teamList = MetaDataService.TeamIds;
                    bool uploadFailed = false;
                    string uploadFailedStr = "上传失败，以下行数据有误：";

                    #region read datas
                    for (int i = 2; i <= lastRow; i++)
                    {
                        object value;
                        TeamMemberModel pm = new TeamMemberModel();
                        value = sheet.Cells[i, 2].Value;
                        if (value != null && CheckString(value.ToString(), 0, 28))
                        {
                            pm.TeamMemberName = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 3].Value;
                        if (value == null)
                        {
                            pm.PhoneNumber = "";
                        }
                        else
                        {
                            pm.PhoneNumber = value.ToString();
                        }
                        //if (value != null && CheckPhoneNumber(value.ToString()))
                        //{
                        //    pm.PhoneNumber = value.ToString();
                        //}
                        //else
                        //{
                        //    uploadFailedStr += i.ToString() + ",";
                        //    uploadFailed = true;
                        //    continue;
                        //}

                        value = sheet.Cells[i, 4].Value;
                        if (value != null && teamDepart.Where(a => a.Name.Trim() == value.ToString().Trim()).Count() > 0)
                        {
                            pm.TeamDept = teamDepart.Where(a => a.Name.Trim() == value.ToString().Trim()).Select(a => a.Value).FirstOrDefault();
                            pm.TeamDeptName = value.ToString();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                pm.TeamDept = "-1";
                                pm.TeamDeptName = value.ToString();
                            }
                            else
                            { 
                                uploadFailedStr += i.ToString() + ",";
                                uploadFailed = true;
                                continue;
                            }
                        }


                        value = sheet.Cells[i, 5].Value;
                        if (value != null && teamList.Where(a => a.Name.Trim() == value.ToString().Trim()).Count() > 0)
                        {
                            pm.TeamId = (teamList.Where(a => a.Name.Trim() == value.ToString().Trim()).Select(a => a.Value).FirstOrDefault().ToString());
                            pm.TeamName = value.ToString();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                pm.TeamId = "-1";
                                pm.TeamName = value.ToString();
                            }
                            else
                            {
                                uploadFailedStr += i.ToString() + ",";
                                uploadFailed = true;
                                continue;
                            }
                        }

                        value = sheet.Cells[i, 6].Value;
                        if (value != null && CheckString(value.ToString(), 0, 28))
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

                    if (lastRow <= 1)
                    {
                        System.Windows.MessageBox.Show("文档内没有有效的数据，请检查后上传!");
                        return;
                    }

                    if (uploadFailed)
                    {
                        System.Windows.MessageBox.Show(uploadFailedStr + "请检查后上传!");
                        return;
                    }

                    var uploadString = JSONHelper.ToJsonString(teamMembers.Select(a => new
                    {
                        teamName = a.TeamName,
                        //teamID = a.TeamId,
                        //teamDept = a.TeamDept,
                        teamDeptName = a.TeamDeptName,
                        place = a.PlaceName,
                        phoneNumber = a.PhoneNumber,
                        name = a.TeamMemberName
                    }).ToArray());
                    #endregion
                    var uploadResult = TeamService.ImportTeamMembersV2(uploadString);
                    if (!uploadResult)
                    {
                        System.Windows.MessageBox.Show("上传失败，请联系管理员!");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("上传成功!");
                    }
                    GetTeams();
                }
                #endregion
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show("上传失败，请联系管理员！");
                return;
            }
        }


        private void Search_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.PageIndex = 1;
			GetTeams();
		}

		private void GetTeams()
		{
			var teams = TeamService.GetTeamMembersDataV2(ViewModel.PageIndex, ViewModel.PageSize, ViewModel.SerachInfo);
			if (teams != null)
			{
				ViewModel.Teams = new ObservableCollection<TeamMemberModel>(teams.Result.Data.Select(o => o.CreateAopProxy()));
				ViewModel.PageIndex = teams.Result.PageIndex;
				ViewModel.PageSize = teams.Result.PageSize;
				ViewModel.TotalCount = teams.Result.Count;
				ViewModel.TotalPage = teams.Result.PageSize == 0 ? 0 : (int)Math.Ceiling((double)teams.Result.Count / teams.Result.PageSize);
			}
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			GetTeams();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			if (ViewModel == null)
			{
				ViewModel = new TeamListPageViewModel().CreateAopProxy();
				ViewModel.PageIndex = 1;
				ViewModel.PageSize = 10;
				DataContext = ViewModel;
				ViewModel.SetPopupEditToFullScreen += this.FullPageEditPopup;
			}
			GetTeams();
		}

        private void Add_Handler(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentTeam = new TeamMemberModel().CreateAopProxy();
            ViewModel.CurrentTeam.TeamDept = ViewModel.TeamDepts == null || ViewModel.TeamDepts.Count == 0 ? null : ViewModel.TeamDepts[0].Value;
            ViewModel.CurrentTeam.TeamId = ViewModel.TeamList == null || ViewModel.TeamList.Count == 0 ? null : ViewModel.TeamList[0].Value;
            ViewModel.CurrentTeam.Place = ViewModel.TeamMemberPlace == null || ViewModel.TeamMemberPlace.Count == 0 ? "" : ViewModel.TeamMemberPlace[0].Value;

            DependencyObject parent = this.PopupEditTeam.Child;
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
            ViewModel.CanSelectCaptain = "Visible";
            ViewModel.PopupHeader = "新增队员";
            ViewModel.PopupTeamEdit();
            FullPageEditPopup();
        }

        private void Edit_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ViewModel.CurrentTeam = (sender as Image).DataContext as TeamMemberModel;
            ViewModel.CurrentTeam.TeamDept = ViewModel.TeamDepts == null || ViewModel.TeamDepts.Count == 0 ? null : ViewModel.TeamDepts[0].Value;
            ViewModel.CurrentTeam.TeamId = ViewModel.TeamList == null || ViewModel.TeamList.Count == 0 ? null : ViewModel.TeamList[0].Value;
            ViewModel.CurrentTeam.Place = ViewModel.TeamMemberPlace == null || ViewModel.TeamMemberPlace.Count == 0 ? "" : ViewModel.TeamMemberPlace[0].Value;

            DependencyObject parent = this.PopupEditTeam.Child;
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
			ViewModel.CanSelectCaptain = "Visible";
			ViewModel.PopupHeader = "编辑队员";
			ViewModel.PopupTeamEdit();
            //ViewModel.CurrentTeam.TeamMemberId = ViewModel.TeamMembers == null || ViewModel.TeamMembers.Count == 0 ? 0 : Convert.ToInt64(ViewModel.TeamMembers[0].Value);
            //ViewModel.CurrentTeam.TeamMemberId = ((ViewModel.CurrentTeam.TeamMemberId <= 0 || !ViewModel.TeamMembers.Select(a => Convert.ToInt64( a.Value)).Contains(ViewModel.CurrentTeam.TeamMemberId)) && ViewModel.Members != null && ViewModel.Members.Count != 0) ? ViewModel.Members[0].ID : ViewModel.CurrentTeam.TeamMemberId;
			FullPageEditPopup();
		}

		private void NavigateToMaterialPage_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new Uri("./Views/Others/MaterialListPage.xaml", UriKind.Relative));
		}

		private void Update_Handler(object sender, RoutedEventArgs e)
		{
			try
			{
				var resultValidation = false;
				foreach (var item in TLPPopupBindingGroup.BindingExpressions)
				{
					item.UpdateSource();
					if (item.HasValidationError)
					{
						resultValidation = true;
					}
				}
				if (!resultValidation)
				{
                    if (string.IsNullOrEmpty(ViewModel.CurrentTeam.TeamMemberId))
                    {
                        ViewModel.CurrentTeam.TeamDeptName = this.comboDept.Text;
                        ViewModel.CurrentTeam.TeamName = this.comboTeam.Text;
                        ViewModel.CurrentTeam.TeamMemberId = null;
                        ViewModel.CurrentTeam.TeamDept = ViewModel.CurrentTeam.TeamDept == null ? null : ViewModel.CurrentTeam.TeamDept;
                        ViewModel.CurrentTeam.TeamId = ViewModel.CurrentTeam.TeamId == null ? null : ViewModel.CurrentTeam.TeamId;

                        var result = TeamService.CreateTeamMemberV2(ViewModel.CurrentTeam);
                        if (result)
                        {
                            ViewModel.ClosePopup();
                            System.Windows.MessageBox.Show("添加成功！");
                            GetTeams();
                        }
                        else
                        {
                            ViewModel.ClosePopup();
                            System.Windows.MessageBox.Show("添加失败！");
                        }
                    }
                    else
                    {
                        //var captain = ViewModel.Members.First(a => a.team == ViewModel.CurrentTeam.TeamMemberId);
                        //if (captain != null)
                        //{
                        //	ViewModel.CurrentTeam.PersonCharge = captain.Name;
                        //	ViewModel.CurrentTeam.PersonChargePhone = captain.PhoneNumber;
                        //}

                        ViewModel.CurrentTeam.TeamDeptName = this.comboDept.Text;
                        ViewModel.CurrentTeam.TeamName = this.comboTeam.Text;
                        ViewModel.CurrentTeam.TeamDept = ViewModel.CurrentTeam.TeamDept == null ? null : ViewModel.CurrentTeam.TeamDept;
                        ViewModel.CurrentTeam.TeamId = ViewModel.CurrentTeam.TeamId == null ? null : ViewModel.CurrentTeam.TeamId;
                        var result = TeamService.UpdateTeamMemberV2(ViewModel.CurrentTeam);
                        if (result)
                        {
                            ViewModel.ClosePopup();
                            System.Windows.MessageBox.Show("编辑成功！");
                            GetTeams();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("编辑失败！");
                        }
                    }
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("编辑队员异常，请重新编辑。");
				Logger.Error("编辑队员异常", ex);
			}
		}

        private void ExportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog m_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            MemoryStream templeteStream = MetaDataService.DownloadTeamMemberTempleteFile();
            if (templeteStream != null)
            {
                //var splits = aopWraper.SummaryEvaluationPopupDownloadUrl.Split('/', '\\');
                string fileName = "队员信息导入模板" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
                string DownloadFullPath = System.IO.Path.Combine(m_Dialog.SelectedPath, fileName);

                using (Stream fileStream = new FileStream(DownloadFullPath, FileMode.Create))
                {
                    fileStream.Write(templeteStream.ToArray(), 0, Convert.ToInt32(templeteStream.Length));
                }
                System.Windows.MessageBox.Show("下载成功！");
            }
            else
            {
                System.Windows.MessageBox.Show("下载失败！");
            }
        }

        private void NavigateToTeamDetailPage_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //var team = GridTeamList.SelectedItem as TeamMemberModel;
            //if (team != null)
            //{
            //    NavigationService.Navigate(new TeamDetailPage(team));
            //}
        }

        private void ClosePopup_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.ClosePopup();
		}
		private void FullPageEditPopup()
		{
			DependencyObject parent = this.PopupEditTeam.Child;
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

        private bool CheckString(string str, int minStrLength, int maxStrLength)
        {
            return !string.IsNullOrEmpty(str) && str.Length >= minStrLength && str.Length <= maxStrLength;
        }
        private bool CheckPhoneNumber(string Number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Number, @"^[1]+[3,5,7,8,9]+\d{9}");
        }

    }
}
