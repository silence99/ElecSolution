using Business.Services;
using Busniess.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Framework;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Emergence_WPF
{
	/// <summary>
	/// AnnouncementManagementPage.xaml 的交互逻辑
	/// </summary>
	public partial class AnnouncementManagementPage : Page
	{
		AnnouncementManagementPageViewModel ViewModel { get; set; }
		public AnnouncementManagementPage()
		{
			InitializeComponent();
			ViewModel = new AnnouncementManagementPageViewModel().CreateAopProxy();
			DataContext = ViewModel;

            ViewModel.SetPopupEditToFullScreen += this.FullPageEditPopup;
        }

		private void Pager_OnPageChanged(object sender, Comm.PageChangedEventArg e)
		{
		}

		private void PopupEdit_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var obj = sender as Image;
			if (obj != null)
			{
				var current = obj.Tag as AnnouncementModel;
				if (current != null)
				{
					ViewModel.PopupEditAction(current);
				}
			}
		}
        private void FullPageEditPopup()
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

        private void Button_AMEdit_Click(object sender, RoutedEventArgs e)
        {
            var result = false;
            foreach (var item in AMPPopupBindingGroup.BindingExpressions)
            {
                item.UpdateSource();
                if (item.HasValidationError)
                {
                    result = true;
                }
            }
            if (!result)
            {
                this.ViewModel.UpdateCommand.Execute();
            }
            
        }
    }
}
