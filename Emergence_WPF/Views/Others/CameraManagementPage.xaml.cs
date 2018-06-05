using Business.Services;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using Utils;
using System.Linq;
using Busniess.Services;
using Busniess.ViewModel;
using Framework;
using System;

namespace Emergence_WPF
{
	/// <summary>
	/// CameraManagementPage.xaml 的交互逻辑
	/// </summary>
	public partial class CameraManagementPage : Page
	{
        CameraService cameraService { get; set; }
        CameraManagementViewModel ViewModel { get; set; }

        public CameraManagementPage()
		{
			InitializeComponent();
            cameraService = new CameraService();
            ViewModel = new CameraManagementViewModel().CreateAopProxy();
            DataContext = ViewModel;
            this.ViewModel.SyncCameras();
        }

        private void Pager_OnPageChanged(object sender, Comm.PageChangedEventArg e)
		{
			ViewModel.SyncCameras();
		}

		private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.ViewModel.DeleteCameras();
            this.ViewModel.SyncCameras();
        }

		private void ImportButton_Click(object sender, System.Windows.RoutedEventArgs e)
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


                ICollection<CameraModel> cameras = new List<CameraModel>();
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
                    if (sheet.Cells[1, 1].Value == null ||
                         sheet.Cells[1, 2].Value == null ||
                         sheet.Cells[1, 3].Value == null ||
                         sheet.Cells[1, 4].Value == null ||
                         sheet.Cells[1, 5].Value == null ||
                         sheet.Cells[1, 6].Value == null ||
                         sheet.Cells[1, 7].Value == null ||
                         sheet.Cells[1, 8].Value == null ||
                         sheet.Cells[1, 9].Value == null ||
                         sheet.Cells[1, 10].Value == null ||
                         !sheet.Cells[1, 1].Value.Equals("编号") ||
                         !sheet.Cells[1, 2].Value.Equals("所属单位") ||
                         !sheet.Cells[1, 3].Value.Equals("经度") ||
                         !sheet.Cells[1, 4].Value.Equals("纬度") ||
                         !sheet.Cells[1, 5].Value.Equals("详细地址") ||
                         !sheet.Cells[1, 6].Value.Equals("播放地址") ||
                         !sheet.Cells[1, 7].Value.Equals("生产商") ||
                         !sheet.Cells[1, 8].Value.Equals("播放方式") ||
                         !sheet.Cells[1, 9].Value.Equals("播放属性") ||
                         !sheet.Cells[1, 10].Value.Equals("所属系统"))
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

                    var cameraSystem = MetaDataService.MaterialDepts;
                    var status = new string[] { "是", "否" };
                    bool uploadFailed = false;
                    string uploadFailedStr = "上传失败，以下行数据有误：";

                    #region read datas
                    for (int i = 2; i <= lastRow; i++)
                    {
                        object value;
                        CameraModel mm = new CameraModel();
                        value = sheet.Cells[i, 1].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.VideoNumber = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 2].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.Dept = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 3].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()) && CheckLongitude(value.ToString()))
                        {
                            mm.Longitude = double.Parse(value.ToString());
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 4].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()) && CheckLatitude(value.ToString()))
                        {
                            mm.Latitude = double.Parse(value.ToString());
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }


                        value = sheet.Cells[i, 5].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.Locale = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 6].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.Url = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 7].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.Manufacturer = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }
                        value = sheet.Cells[i, 8].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.PlayMode = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 9].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.PlayAttribute = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        value = sheet.Cells[i, 10].Value;
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            mm.System = value.ToString();
                        }
                        else
                        {
                            uploadFailedStr += i.ToString() + ",";
                            uploadFailed = true;
                            continue;
                        }

                        cameras.Add(mm);
                    }

                    if (cameras.Count < 1)
                    {
                        System.Windows.MessageBox.Show("文档内没有有效的数据，请检查后上传!");
                        return;
                    }

                    if (uploadFailed)
                    {
                        System.Windows.MessageBox.Show(uploadFailedStr + "请检查后上传!");
                        return;
                    }

                    var uploadString = JSONHelper.ToJsonString(cameras.Select(a => new
                    {
                        videoNumber = a.VideoNumber,
                        dept = a.Dept,
                        longitude = a.Longitude,
                        latitude = a.Latitude,
                        locale = a.Locale,
                        url = a.Url,
                        manufacturer = a.Manufacturer,
                        playMode = a.PlayMode,
                        playAttribute = a.PlayAttribute,
                        system = a.System
                    }).ToArray());
                    #endregion
                    var uploadResult = cameraService.ImportCameras(uploadString);
                    if (!uploadResult)
                    {
                        System.Windows.MessageBox.Show("上传失败，请联系管理员!");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("上传成功!");
                    }
                    this.ViewModel.SyncCameras();
                }
                #endregion
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show("上传失败，请联系管理员！");
                return;
            }
        }

        private bool CheckLongitude(string str)
        {
            double checkDouble = 0.0;
            return double.TryParse(str, out checkDouble);
            //return System.Text.RegularExpressions.Regex.IsMatch(str, @"/^[\-\+]?(0?\d{1,2}\.\d{1,5}|1[0-7]?\d{1}\.\d{1,5}|180\.0{1,5})$/");
        }

        private bool CheckLatitude(string str)
        {
            double checkDouble = 0.0;
            return double.TryParse(str, out checkDouble);
            //return System.Text.RegularExpressions.Regex.IsMatch(str, @"/^[\-\+]?([0-8]?\d{1}\.\d{1,5}|90\.0{1,5})$/");
        }

        private void ExportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog m_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            
            MemoryStream templeteStream = MetaDataService.DownloadTempleteFile(0);
            if (templeteStream != null)
            {
                //var splits = aopWraper.SummaryEvaluationPopupDownloadUrl.Split('/', '\\');
                string fileName = "摄像头信息模版" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
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
    }
}
