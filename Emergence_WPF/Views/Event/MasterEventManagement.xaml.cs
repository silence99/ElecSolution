using Busniess.Services;
using Busniess.Strategies;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Framework;
using Framework.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Emergence.Business.ViewModel;


namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for MasterEventManagement.xaml
	/// </summary>
	public partial class MasterEventManagement : Page
	{
		Emergence.Business.ViewModel.VM_MasterEventManagement ViewModel;
		MasterEventService MasterEventService;
		MasterEventManagementStrategyController StrategyController = null;
		public delegate void GoToDetailHandler(MasterEvent masterEvent);
		public event GoToDetailHandler GoToDetail;


		public MasterEventManagement()
		{
			InitializeComponent();
			StrategyController = new MasterEventManagementStrategyController();
		}


		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel = new VM_MasterEventManagement().CreateAopProxy();
			MasterEventService = new MasterEventService();
			RequestMasterEventList();
			this.DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			RequestMasterEventList();
		}

		private void RequestMasterEventList()
		{
			//var masterEvents = gMasterSvr.GetMasterEvents(ViewModel.PageIndex, ViewModel.PageSize);
			var masterEvents = MasterEventService.GetMasterEvents(ViewModel.PageIndex, 20, ViewModel.TxtTitle, default(DateTime), default(DateTime), string.Empty);
			if (masterEvents != null)
			{
				ViewModel.MasterEvents = masterEvents.MasterEvents;
				ViewModel.TotalCount = masterEvents.Count;
				ViewModel.PageIndex = masterEvents.PageIndex;
				ViewModel.PageSize = masterEvents.PageSize;
				ViewModel.TotalPage = ViewModel.TotalCount == 0 ? 0 : (int)Math.Ceiling((double)ViewModel.TotalCount / ViewModel.PageSize);
			}
		}

		private void masterEventSearchButton_Click(object sender, RoutedEventArgs e)
		{
			RequestMasterEventList();
		}

		private void Grid_MasterEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var dg = e.Source as DataGrid;
			MasterEvent me = dg.SelectedItem as MasterEvent;
            this.NavigationService.Navigate(new MasterEventDetail(me));
			//GoToDetail?.Invoke(me);
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
