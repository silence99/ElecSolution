using Business.Services;
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
            if (ResolutionService.Width < 1366)
            {
                this.NavigationService.Navigate(new MasterEventDetail_1024(me));
            }
            else
            {
                this.NavigationService.Navigate(new MasterEventDetail(me));
            }
        }

		private void masterEventSearchButton2_Click(object sender, RoutedEventArgs e)
		{
			Login lg = new Login();
			lg.Show();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Login lg = new Login();
            lg.Topmost = true;
			lg.Show();
		}
	}
}
