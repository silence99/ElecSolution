using Busniess.Services;
using Busniess.Strategies;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Framework;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for MasterEventManagement.xaml
	/// </summary>
	public partial class MasterEventManagement : Page
	{
		VM_MasterEventManagement ViewModel { get; set; }
		MasterEventService MasterEventService { get; set; }

		public MasterEventManagement()
		{
			InitializeComponent();
			ViewModel = new VM_MasterEventManagement().CreateAopProxy();
			DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			ViewModel.SyncData();
		}

		private void Grid_MasterEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var dg = e.Source as DataGrid;
			MasterEvent me = dg.SelectedItem as MasterEvent;
			this.NavigationService.Navigate(new MasterEventDetail(me));
		}

		private void Btn_CreateMasterEvent_Click(object sender, RoutedEventArgs e)
		{
			AddMasterEvent ame = new AddMasterEvent();
			ame.ShowDialog();
		}

		private void DeleteMasterEventHandler(object sender, RoutedEventArgs e)
		{
			var events = ViewModel.MasterEvents.Where(ent => ent.IsChecked).Select(ent => (long)ent.ID).ToList();
			MasterEventService.UpdateMasterEventState(events, 9); // state:9 删除, 1 归档, 0 正常
		}

	}
}
