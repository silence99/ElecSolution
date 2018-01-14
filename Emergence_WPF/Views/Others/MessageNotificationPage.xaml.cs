using Business.Services;
using Busniess.Services;
using Busniess.ViewModel;
using Emergence.Common.Model;
using Framework;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;
using Utils;
using System.Linq;
using System.Collections.Generic;
using OfficeOpenXml;

namespace Emergence_WPF
{
	/// <summary>
	/// MessageNotificationPage.xaml 的交互逻辑
	/// </summary>
	public partial class MessageNotificationPage : Page
	{
		public MessageNotificationViewModel ViewModel { get; set; }
		public MessageNotificationPage()
		{
			InitializeComponent();
			ViewModel = new MessageNotificationViewModel().CreateAopProxy();
			DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, object e)
		{

		}

		private void NavigateToMessageHistory_Handler(object sender, MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new MessageHistoryPage(), UriKind.Relative);
		}

		private void Template_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ViewModel != null && ViewModel.MessageTemplates != null)
			{
				var obj = sender as System.Windows.Controls.ComboBox;
				var item = obj.SelectedItem as MessageTemplateModel;
				Content.Text = item == null ? "" : item.TemplateContent;
			}
		}

		private void PopupEditButton_Click(object sender, MouseButtonEventArgs e)
		{
			var obj = sender as Image;
			if (obj != null)
			{
				var person = obj.Tag as PersonModel;
				if (person != null)
				{
					ViewModel.CurrentMember = person;
					ViewModel.IsPopupOpen = true;
					ViewModel.IsEditMode = true;
				}
			}
		}

        private void ImportButton_Click(object sender, System.Windows.RoutedEventArgs e)
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


            List<PersonModel> teamMembers = new List<PersonModel>();
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
                if (!sheet.Cells[1, 1].Value.Equals("姓名") ||
                     !sheet.Cells[1, 2].Value.Equals("手机号"))
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
                    if (value != null && CheckString(value.ToString(), 0, 28))
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

                    teamMembers.Add(pm);
                }

                if (teamMembers.Count() < 1)
                {
                    System.Windows.MessageBox.Show("文档内没有有效的数据，请检查后上传!");
                    return;
                }
                else
                {
                    this.ViewModel.AddPerson(teamMembers);
                    System.Windows.MessageBox.Show("上传成功!");
                }


                #endregion

            }
            #endregion
        }

        private bool CheckPhoneNumber(string Number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Number, @"^[1]+[3,5,7,8,9]+\d{9}");
        }

        private bool CheckString(string str, int minStrLength, int maxStrLength)
        {
            return !string.IsNullOrEmpty(str) && str.Length >= minStrLength && str.Length <= maxStrLength;
        }
    }
}
