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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Utils;

namespace Emergence_WPF
{
	/// <summary>
	/// MaterialListPage.xaml 的交互逻辑
	/// </summary>
	public partial class MaterialListPage : Page
	{
		MaterialListPageViewModel ViewModel { get; set; }
		MaterialService Service { get; set; }
		public MaterialListPage()
		{
			InitializeComponent();
			InitViewModel();
		}

		private void InitViewModel()
		{
			Service = new MaterialService();
			ViewModel = new MaterialListPageViewModel().CreateAopProxy();
			SyncMaterials();
			DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			SyncMaterials();

            ViewModel.CurrentMaterial.MaterialsDept = ViewModel.MaterialDepts == null || ViewModel.MaterialDepts.Count == 0 ?
                "" : ViewModel.MaterialDepts[0].Value;
            ViewModel.CurrentMaterial.MaterialsDeptName = ViewModel.MaterialDepts == null || ViewModel.MaterialDepts.Count == 0 ?
                "" : ViewModel.MaterialDepts[0].Name;

            ViewModel.CurrentMaterial.MaterialsType = ViewModel.MaterialTypes == null || ViewModel.MaterialTypes.Count == 0 ?
                "" : ViewModel.MaterialTypes[0].Value;
            ViewModel.CurrentMaterial.MaterialsTypeName = ViewModel.MaterialTypes == null || ViewModel.MaterialTypes.Count == 0 ?
                "" : ViewModel.MaterialTypes[0].Name;
            ViewModel.SetPopupEditToFullScreen += this.FullPageEditPopup;

        }

        private void NavigateToMaterialPage(object sender, MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new TeamListPage());
		}

        private void SyncMaterials()
        {
            var data = Service.GetMaterials(ViewModel.PageIndex, ViewModel.PageSize, ViewModel.SearchInfo ?? string.Empty);
            if (data != null)
            {
                ViewModel.TotalCount = data.Count;
                ViewModel.PageIndex = data.PageIndex;
                ViewModel.PageSize = data.PageSize;
                ViewModel.TotalPage = (int)Math.Ceiling((double)data.Count / data.PageSize);
                ViewModel.Materials = new System.Collections.ObjectModel.ObservableCollection<MaterialModel>(data.Data ?? new MaterialModel[0]);
            }
        }

        private void BtnSearchMaterial_Click(object sender, RoutedEventArgs e)
		{
			SyncMaterials();
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			//popu up edit dialog
			ViewModel.IsCreateMaterial = true;
			ViewModel.PopupTitle = "添加物资";
			ViewModel.CurrentMaterial = new MaterialModel().CreateAopProxy();
            ViewModel.CurrentMaterial.MaterialsType = ViewModel.MaterialTypes == null || ViewModel.MaterialTypes.Count == 0 ? "" : ViewModel.MaterialTypes[0].Value;
            ViewModel.CurrentMaterial.MaterialsDept = ViewModel.MaterialDepts == null || ViewModel.MaterialDepts.Count == 0 ? "" : ViewModel.MaterialDepts[0].Value;
            ViewModel.CurrentMaterial.IsConsumable = ViewModel.IsConsumableEnums == null || ViewModel.IsConsumableEnums.Count == 0 ? "" : ViewModel.IsConsumableEnums[0].Value;
            ViewModel.CurrentMaterial.IsBigMaterials = ViewModel.IsBigEnums == null || ViewModel.IsBigEnums.Count == 0 ? "" : ViewModel.IsBigEnums[0].Value;
            ViewModel.PopupTeamEdit();
            DependencyObject parent = this.PopupEditMaterial.Child;
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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			var ids = ViewModel.Materials.Where(m => m.IsChecked).Select(m => m.ID.ToString()).ToList();
			if (ids != null && ids.Count > 0)
			{
				if (Service.DeleteMaterial(ids))
				{
					System.Windows.MessageBox.Show("删除成功");
                    SyncMaterials();
                }
				else
				{
					System.Windows.MessageBox.Show("删除失败");
				}
			}
			else
			{
				System.Windows.MessageBox.Show("没有选择要删除的物资");
			}
		}

		private void BtnImport_Click(object sender, RoutedEventArgs e)
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


            ICollection<MaterialModel> materials = new List<MaterialModel>();
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
                if (!sheet.Cells[1, 1].Value.Equals("物资序号") ||
                     !sheet.Cells[1, 2].Value.Equals("物资名称") ||
                     !sheet.Cells[1, 3].Value.Equals("物资类型") ||
                     !sheet.Cells[1, 4].Value.Equals("所属单位") ||
                     !sheet.Cells[1, 5].Value.Equals("是否消耗品") ||
                     !sheet.Cells[1, 6].Value.Equals("是否大物资") ||
                     !sheet.Cells[1, 7].Value.Equals("物资数量"))
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

                var materialDept = MetaDataService.MaterialDepts;
                var materialType = MetaDataService.MaterialTypes;
                var status = new string[] { "是", "否" };
                bool uploadFailed = false;
                string uploadFailedStr = "上传失败，以下行数据有误：";

                #region read datas
                for (int i = 2; i <= lastRow; i++)
                {
                    object value;
                    MaterialModel mm = new MaterialModel();
                    value = sheet.Cells[i, 1].Value;
                    if (value != null)
                    {
                        mm.MaterialsNumber = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    value = sheet.Cells[i, 2].Value;
                    if (value != null)
                    {
                        mm.MaterialsName = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    value = sheet.Cells[i, 3].Value;
                    if (value != null && materialType.Where(a => a.Name == value.ToString()).Count() > 0)
                    {
                        mm.MaterialsTypeName = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    value = sheet.Cells[i, 4].Value;
                    if (value != null && materialDept.Where(a => a.Name == value.ToString()).Count() > 0)
                    {
                        mm.MaterialsDeptName = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }


                    value = sheet.Cells[i, 5].Value;
                    if (value != null && status.Contains(value.ToString()))
                    {
                        mm.IsConsumableName = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    value = sheet.Cells[i, 6].Value;
                    if (value != null && status.Contains(value.ToString()))
                    {
                        mm.IsBigMaterialsName = value.ToString();
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    value = sheet.Cells[i, 7].Value;
                    int materialNumber = 0;
                    if (value != null && int.TryParse(value.ToString(),out materialNumber))
                    {
                        mm.TotalQuantity = materialNumber;
                    }
                    else
                    {
                        uploadFailedStr += i.ToString() + ",";
                        uploadFailed = true;
                        continue;
                    }

                    materials.Add(mm);
                }

                if (materials.Count() < 1)
                {
                    System.Windows.MessageBox.Show("文档内没有有效的数据，请检查后上传!");
                }

                if (uploadFailed)
                {
                    System.Windows.MessageBox.Show(uploadFailedStr + "请检查后上传!");
                    return;
                }

                var uploadString = JSONHelper.ToJsonString(materials.Select(a => new
                {
                    materialsNumber = a.MaterialsNumber,
                    materialsName = a.MaterialsName,
                    materialsType = a.MaterialsTypeName,
                    materialsDept = a.MaterialsDeptName,
                    consumables = a.IsConsumableName,
                    bigMaterials = a.IsBigMaterialsName,
                    totalQuantity = a.TotalQuantity.ToString()
                }).ToArray());
                #endregion
                var uploadResult = Service.ImportMaterials(uploadString);
                if (!uploadResult)
                {
                    System.Windows.MessageBox.Show("上传失败，请联系管理员!");
                }
                else
                {
                    System.Windows.MessageBox.Show("上传成功!");
                }
                SyncMaterials();
            }
            #endregion
        }

        private void EditMaterial_Click(object sender, MouseButtonEventArgs e)
		{
			ViewModel.IsCreateMaterial = false;
			ViewModel.PopupTitle = "更新物资";
            var item = (sender as Image).DataContext as MaterialModel;
            ViewModel.CurrentMaterial = item;
            ViewModel.PopupTeamEdit();
            FullPageEditPopup();
        }

		private void UpdateMaterial_Click(object sender, RoutedEventArgs e)
        {
            var result = false;
            foreach (var item in MLPPopupBindingGroup.BindingExpressions)
            {
                item.UpdateSource();
                if (item.HasValidationError)
                {
                    result = true;
                }
            }
            if (!result)
            {
                ViewModel.ClosePopup();
                if (ViewModel.IsCreateMaterial)
                {
                    var results = Service.CreateMaterial(ViewModel.CurrentMaterial);
                    if (results)
                    {
                        System.Windows.MessageBox.Show("添加成功！");
                        SyncMaterials();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("添加失败！");
                    }
                }
                else
                {
                    var results = Service.UpdateMaterial(ViewModel.CurrentMaterial);
                    if (results)
                    {
                        System.Windows.MessageBox.Show("修改成功！");
                        SyncMaterials();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("修改失败！");
                    }
                }
            }
		}

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClosePopup();
        }
        private void FullPageEditPopup()
        {
            DependencyObject parent = this.PopupEditMaterial.Child;
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
