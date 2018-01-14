using Business.Services;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Views.Event;
using Framework;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for MasterEventDetail.xaml
	/// </summary>
	public partial class MasterEventDetail : Page
	{
		public VM_MasterEventDetail ViewModel { get; set; }

		public SubEventPopup_Video SE_VE { get; set; }
		//{
		//        get { return this.DataContext as VM_MasterEventDetail; }
		//set { this.DataContext = value; }
		//}
		public DelegateCommand<object> ClickCommand { get; private set; }

		public MasterEventDetail(MasterEvent masterEventID)
		{
			InitializeComponent();
			ViewModel = new VM_MasterEventDetail(masterEventID).CreateAopProxy();
			this.DataContext = ViewModel;
			SE_VE = new SubEventPopup_Video();
			this.KeyDown += MasterEventDetail_KeyDown;
		}
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			if (ViewModel.SubEventList != null && ViewModel.SubEventList.Count > 0)
			{
				DataGridRow row = (DataGridRow)this.Grid_SubEventList.ItemContainerGenerator.ContainerFromIndex(0);
				row.IsSelected = true;
				var item = row.Item as SubEvent;
				ViewModel.SelectSubEventAction(item.Id.ToString());
			}
		}
		private void MasterEventDetail_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)//Esc键  
			{
				this.ViewModel.CloseAllPopupWindow();
			}
		}
		private void Grid_SubEventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DataGrid dg = sender as DataGrid;
			var item = dg.CurrentItem as SubEvent;
			//DataRowView item = cell.Item as DataRowView;
			if (item != null)
			{
				ViewModel.SelectSubEventAction(item.Id.ToString());
			}
		}

		private void Btn_PublishSubEvent_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Btn_AmplifySubEvent_Click(object sender, RoutedEventArgs e)
		{
			this.PopupSelectThreeWindow.IsOpen = !this.PopupSelectThreeWindow.IsOpen;
			DependencyObject parent = this.PopupSelectThreeWindow.Child;
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
			this.ViewModel.ThreePopupSelectOpenAction();
		}

		private void ThreePopupSubEventButton_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_SBPopupTeam.Width = ResolutionService.Width - 4;
            this.DataGrid_SBPopupMetairls.Width = ResolutionService.Width - 4;

            this.PopupSubEventAmplify.IsOpen = !this.PopupSubEventAmplify.IsOpen;
			DependencyObject parent = this.PopupSubEventAmplify.Child;
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
			this.ViewModel.ThreePopupSelectCloseAction();
		}

		private void ThreePopupMapButton_Click(object sender, RoutedEventArgs e)
		{
			var win = new SubEventsMapV2()
			{
				WindowStyle = WindowStyle.None,
				WindowState = WindowState.Maximized
			};
			win.Show();
			this.ViewModel.ThreePopupSelectCloseAction();
		}

		private void ThreePopupVideoButton_Click(object sender, RoutedEventArgs e)
		{
			double latitude = 0;
			double longitude = 0;
			if (ViewModel != null && ViewModel.MasterEventInfo != null)
			{
				latitude = string.IsNullOrEmpty(ViewModel.MasterEventInfo.Latitude) ? 0 : double.Parse(ViewModel.MasterEventInfo.Latitude);
				longitude = string.IsNullOrEmpty(ViewModel.MasterEventInfo.Longitude) ? 0 : double.Parse(ViewModel.MasterEventInfo.Longitude);
			}

			SubEventPopup_VideoV2 video = new SubEventPopup_VideoV2();
			video.SetCoordinate(latitude, longitude);
			video.Show();
			this.ViewModel.ThreePopupSelectCloseAction();
		}
	}
}
