using Busniess.ViewModel;
using Emergence.Common.Model;
using Framework;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Emergence_WPF
{
	/// <summary>
	/// MessageNotificationPage.xaml 的交互逻辑
	/// </summary>
	public partial class MessageNotificationPage : Page
	{
		public MessageNotificationViewModel ViewModel { get; set; }
		public MessageNotificationPage()
		{
			InitializeComponent();
			ViewModel = new MessageNotificationViewModel().CreateAopProxy();
			DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, object e)
		{

		}

		private void NavigateToMessageHistory_Handler(object sender, MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new MessageHistoryPage(), UriKind.Relative);
		}

		private void Template_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ViewModel != null && ViewModel.MessageTemplates != null)
			{
				var obj = sender as ComboBox;
				var item = obj.SelectedItem as MessageTemplateModel;
				Content.Text = item == null ? "" : item.TemplateContent;
			}
		}

		private void PopupEditButton_Click(object sender, MouseButtonEventArgs e)
		{
			var obj = sender as Image;
			if (obj != null)
			{
				var person = obj.Tag as PersonModel;
				if (person != null)
				{
					ViewModel.CurrentMember = person;
					ViewModel.IsPopupOpen = true;
					ViewModel.IsEditMode = true;
				}
			}
		}
	}
}
