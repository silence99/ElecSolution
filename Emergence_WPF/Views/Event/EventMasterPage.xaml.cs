using Emergence.Common.Model;
using Emergence_WPF.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Emergence_WPF
{
    /// <summary>
    /// MainUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class EventMasterPage : UserControl
    {
        public EventMasterPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MasterEventManagement masterInformation = new MasterEventManagement();
            gridEventMain.Children.Clear();
            gridEventMain.Children.Add(masterInformation);
        }

        public void ChangePageToMasterEventDetails(MasterEvent me)
        {
            MasterEventDetail masterDetails = new MasterEventDetail(me);
            gridEventMain.Children.Clear();
            gridEventMain.Children.Add(masterDetails);
        }
        

        private void labelPageReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Clear();
        }

        public void Clear()
        {

        }

        public void DoItNow()
        { }
    }
}
