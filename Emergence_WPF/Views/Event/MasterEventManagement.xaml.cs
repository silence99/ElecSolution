﻿using Business.Services;
using Busniess.Services;
using Busniess.Strategies;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Emergence_WPF.Views;
using Framework;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //SetPopupToFullScreen(this.PopupItem);
            this.ViewModel.SetPopupToFullScreen += ViewModel_SetPopupToFullScreen;
        }

        private void ViewModel_SetPopupToFullScreen()
        {
            DependencyObject parent = this.PopupItem.Child;
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

        private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			ViewModel.SyncData();
        }

		private void Grid_MasterEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var dg = e.Source as DataGrid;
			MasterEvent me = dg.SelectedItem as MasterEvent;
            if (me != null && me.ID >= 0)
            {
                //if (ResolutionService.Width < 1366)
                //{
                //	this.NavigationService.Navigate(new MasterEventDetail_1024(me));
                //}
                //else
                //{
                this.NavigationService.Navigate(new MasterEventDetail(me));
                //}
            }
		}

		private void masterEventSearchButton2_Click(object sender, RoutedEventArgs e)
		{
			Login lg = new Login();
			lg.Show();
		}

		private void AddressPopupOpen_Click(object sender, RoutedEventArgs e)
		{
			AddressPickerV2 addressPicker = new AddressPickerV2();
			addressPicker.Width = 600;
			addressPicker.Height = 500;
			addressPicker.AddressPickedEvent += PickedAddress;
			addressPicker.ShowDialog();
		}

		private void PickedAddress(AddressPickedEventArgs args)
		{
			ViewModel.Current.Locale = args.Address;
			ViewModel.Current.Longitude = args.Coordinate.X.ToString();
			ViewModel.Current.Latitude = args.Coordinate.Y.ToString();
		}
		public void SetPopupToFullScreen()
        {
            DependencyObject parent = this.PopupItem.Child;
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
