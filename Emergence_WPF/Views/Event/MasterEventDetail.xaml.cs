using Business.Services;
using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Views;
using Emergence_WPF.Views.Event;
using Framework;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
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
			ViewModel.SetPopupSubEventEditToFullScreen += this.FullPageSubEventEditPopup;
			ViewModel.SetPopupTeamToFullScreen += this.FullPageTeamPopup;
            ViewModel.SetPopupMaterialToFullScreen += this.FullPageMaterialPopup;
            ViewModel.SetPopupSummaryEvaluationToFullScreen += this.FullPageSummaryEvaluationlPopup;
            ViewModel.ResetSubEventSelectedValue += this.ResetSubEventDetailSelected; 

        }
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			ResetSubEventDetailSelected();
		}

		private void ResetSubEventDetailSelected()
		{
			int count = this.Grid_SubEventList.SelectedItems.Count;
			if (count <= 0)
			{
				if (ViewModel.SubEventList != null && ViewModel.SubEventList.Count > 0)
				{
					if (this.Grid_SubEventList.ItemContainerGenerator.Items.Count > 0)
					{
						DataGridRow row = (DataGridRow)this.Grid_SubEventList.ItemContainerGenerator.ContainerFromIndex(0);
						if (row != null)
						{
							row.IsSelected = true;
							var item = row.Item as SubEvent;
							ViewModel.SelectSubEventAction(item.Id.ToString());
						}
					}
					else
					{
						ViewModel.CleanSubEventDetailBlock();
						ViewModel.SetSubEventDetailBlockStatus(false);
					}
				}
				else
				{
					ViewModel.CleanSubEventDetailBlock();
					ViewModel.SetSubEventDetailBlockStatus(false);
				}
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
				ViewModel.SetSubEventDetailBlockStatus(true);
			}
		}

		private void Btn_PublishSubEvent_Click(object sender, RoutedEventArgs e)
		{
			if (this.ViewModel != null && this.ViewModel.SubEventDetail != null && this.ViewModel.SubEventDetail.Id != null)
			{
				MessageNotificationPage mnp = new MessageNotificationPage(this.ViewModel.SubEventDetail.Id.ToString());
				NavigationService.Navigate(mnp);
			}
			else
			{
				System.Windows.MessageBox.Show("请选择有效的子事件！");
			}
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
			this.ViewModel.ThreePopupSelectCloseAction();
			List<Tuple<Point, string, string>> data = new List<Tuple<Point, string, string>>();
			var subeventService = new SubeventService();
			var cameraService = new CameraService();

			data.Add(new Tuple<Point, string, string>(new Point(ViewModel.MasterEventInfo.Longitude, ViewModel.MasterEventInfo.Latitude), string.Format("主事件:{0}", ViewModel.MasterEventInfo.Title), ViewModel.MasterEventInfo.Remarks));
			var subevents = subeventService.GetAllSubevents(ViewModel.MasterEventInfo.ID);

			var camerasData = cameraService.GetCameraByMasterEvent(1, 10000, ViewModel.MasterEventInfo.Latitude, ViewModel.MasterEventInfo.Longitude);
			CameraModel[] cameras = new CameraModel[0];
			if (camerasData != null && camerasData.Data != null)
			{
				cameras = camerasData.Data;
			}

			foreach (var item in subevents)
			{
				data.Add(new Tuple<Point, string, string>(new Point(item.ChildLongitude, item.ChildLatitude), string.Format("子事件:{0}", item.ChildTitle), item.ChildRemarks));
			}

			foreach (var item in cameras)
			{
				data.Add(new Tuple<Point, string, string>(new Point(item.Longitude, item.Latitude), string.Format("摄像头:{0}", item.VideoNumber), item.Remarks));
			}


			var win = new SubEventsMapV2()
			{
				WindowStyle = WindowStyle.None,
				WindowState = WindowState.Maximized
			};
			win.MoveToCenter(ViewModel.MasterEventInfo.Longitude, ViewModel.MasterEventInfo.Latitude);
			win.BindingData(data);
			win.Show();
		}

		private void ThreePopupVideoButton_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.ThreePopupSelectCloseAction();
			SubEventPopup_VideoV2 video = new SubEventPopup_VideoV2();
			video.SetCoordinate(ViewModel.MasterEventInfo.Latitude, ViewModel.MasterEventInfo.Longitude);
			video.Show();
		}

		private void FullPageSubEventEditPopup()
		{
			DependencyObject parent = this.PopupEditSubEvent.Child;
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
		private void FullPageTeamPopup()
		{
			DependencyObject parent = this.PopupSelectTeam.Child;
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
        private void FullPageMaterialPopup()
        {
            DependencyObject parent = this.PopupSelectMaterial.Child;
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
        private void FullPageSummaryEvaluationlPopup()
        {
            DependencyObject parent = this.PopupSummaryEvaluation.Child;
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
        

        private void Grid_SubEventList_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
		{

		}

		private void PopupOpen_Click(object sender, RoutedEventArgs e)
		{
			AddressPickerV2 addressPicker = new AddressPickerV2();
			addressPicker.Width = 600;
			addressPicker.Height = 550;
			addressPicker.AddressPickedEvent += PickedAddress;
			addressPicker.ShowDialog();
		}

		private void PickedAddress(AddressPickedEventArgs args)
		{
			ViewModel.SubEventEdit.ChildLocale = args.Address;
			ViewModel.SubEventEdit.ChildLongitude = args.Coordinate.X;
			ViewModel.SubEventEdit.ChildLatitude = args.Coordinate.Y;
		}

        private void Button_SubEventEdit_Click(object sender, RoutedEventArgs e)
        {
            var result = false;
            foreach (var item in MEDPopupBindingGroup.BindingExpressions)
            {
                item.UpdateSource();
                if (item.HasValidationError)
                {
                    result = true;
                }
            }
            if (!result)
            {
                this.ViewModel.CreateSubEventCommand.Execute();
            }
            
        }
    }
}
