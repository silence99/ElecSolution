using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Framework;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Emergence_WPF.Views.Others
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
		}

		private void NavigateToMaterialPage(object sender, MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new TeamListPage());
		}

		private void SyncMaterials()
		{
			var data = Service.GetMaterials(ViewModel.PageIndex, ViewModel.PageSize, string.Empty);
			ViewModel.PageIndex = data.PageIndex;
			ViewModel.PageSize = data.PageSize;
			ViewModel.TotalPage = (int)Math.Ceiling((double)data.Count / data.PageSize);
			ViewModel.Materials = new System.Collections.ObjectModel.ObservableCollection<MaterialModel>(data.Data);
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

			ViewModel.CurrentMaterial.MaterialsDept = ViewModel.MaterialDepts == null || ViewModel.MaterialDepts.Count == 0 ?
				"" : ViewModel.MaterialDepts[0].Value;
			ViewModel.CurrentMaterial.MaterialsDeptName = ViewModel.MaterialDepts == null || ViewModel.MaterialDepts.Count == 0 ?
				"" : ViewModel.MaterialDepts[0].Name;

			ViewModel.CurrentMaterial.MaterialsType = ViewModel.MaterialTypes == null || ViewModel.MaterialTypes.Count == 0 ?
				"" : ViewModel.MaterialTypes[0].Value;
			ViewModel.CurrentMaterial.MaterialsTypeName = ViewModel.MaterialTypes == null || ViewModel.MaterialTypes.Count == 0 ?
				"" : ViewModel.MaterialTypes[0].Name;

			ViewModel.PopupTeamEdit();
		}

		private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			var ids = ViewModel.Materials.Where(m => m.IsChecked).Select(m => m.ID.ToString()).ToList();
			if (ids != null && ids.Count > 0)
			{
				if (Service.DeleteMaterial(ids))
				{
					System.Windows.MessageBox.Show("删除成功");
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
			SyncMaterials();
		}

		private void BtnImport_Click(object sender, RoutedEventArgs e)
		{

		}

		private void EditMaterial_Click(object sender, MouseButtonEventArgs e)
		{
			ViewModel.IsCreateMaterial = false;
			ViewModel.PopupTitle = "更新物资";
			ViewModel.CurrentMaterial = (sender as Image).DataContext as MaterialModel;
			ViewModel.PopupTeamEdit();
		}

		private void ClosePopup_Click(object sender, MouseButtonEventArgs e)
		{
			ViewModel.ClosePopup();
		}

		private void UpdateMaterial_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.ClosePopup();
			if (ViewModel.IsCreateMaterial)
			{
				Service.CreateMaterial(ViewModel.CurrentMaterial);
			}
			else
			{
				Service.UpdateMaterial(ViewModel.CurrentMaterial);
			}

			SyncMaterials();
		}
	}
}
