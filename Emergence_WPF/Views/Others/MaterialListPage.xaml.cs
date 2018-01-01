using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
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
			ViewModel = new MaterialListPageViewModel();
			SyncMaterials();
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
			var data = Service.GetMaterials(ViewModel.PageIndex, ViewModel.PageSize, ViewModel.QueryMaterialsName, ViewModel.QueryMaterialsNumber, ViewModel.QueryMaterialsDept, ViewModel.QueryIsConsumable);
			ViewModel.PageIndex = data.PageIndex;
			ViewModel.PageSize = data.PageSize;
			ViewModel.TotalPage = (int)Math.Ceiling((double)data.Count / data.PageSize);
		}

		private void BtnSearchMaterial_Click(object sender, RoutedEventArgs e)
		{
			SyncMaterials();
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			//popu up edit dialog
			ViewModel.CurrentMaterial = new MaterialModel();
			ViewModel.PopupTeamEdit();
		}

		private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			var ids = ViewModel.Materials.Where(m => m.IsChecked).Select(m => m.ID.ToString()).ToList();
			if (ids != null && ids.Count > 0)
			{
				Service.DeleteMaterial(ids);
			}
		}

		private void BtnImport_Click(object sender, RoutedEventArgs e)
		{

		}

		private void EditMaterial_Click(object sender, MouseButtonEventArgs e)
		{
			ViewModel.CurrentMaterial = (sender as Image).DataContext as MaterialModel;
			ViewModel.PopupTeamEdit();
		}

		private void ClosePopup_Click(object sender, MouseButtonEventArgs e)
		{
			ViewModel.ClosePopup();
		}

		private void UpdateMaterial_Click(object sender, RoutedEventArgs e)
		{
			if (ViewModel.IsCreateMaterial)
			{
				Service.CreateMaterial(ViewModel.CurrentMaterial.MaterialsName, ViewModel.CurrentMaterial.MaterialsNumber, ViewModel.CurrentMaterial.MaterialsType, ViewModel.CurrentMaterial.MaterialsDept, ViewModel.CurrentMaterial.IsConsumable, ViewModel.CurrentMaterial.IsBigMaterials, ViewModel.CurrentMaterial.TotalQuantity);
			}
			else
			{
				Service.UpdateMaterial(ViewModel.CurrentMaterial.ID.ToString(), ViewModel.CurrentMaterial.MaterialsName, ViewModel.CurrentMaterial.MaterialsNumber, ViewModel.CurrentMaterial.MaterialsType, ViewModel.CurrentMaterial.MaterialsDept, ViewModel.CurrentMaterial.IsConsumable, ViewModel.CurrentMaterial.IsBigMaterials, ViewModel.CurrentMaterial.TotalQuantity);
			}
		}
	}
}
