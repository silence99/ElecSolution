using Emergence_WPF.Model;
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
            //Emergency = null;
            //infor = new Information();
            //main.Children.Clear();
            //infor.Bind(this.main.Width, this.main.Height);
            //main.Children.Add(infor);
            MasterEventManagement masterInformation = new MasterEventManagement();
            gridEventMain.Children.Clear();
            gridEventMain.Children.Add(masterInformation);
        }
        

        private void labelPageReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Clear();
            //kkk.Content = "应急信息管理";
           
            //a1.Background = new SolidColorBrush(Color.FromRgb(
            //  0,
            // 123,
            //  120));
            // a2.Background = new SolidColorBrush(Color.FromRgb(
            //  0,
            // 123,
            //  120));
            // a3.Background = new SolidColorBrush(Color.FromRgb(
            //  0,
            // 123,120));

            //  a4.Background = new SolidColorBrush(Color.FromRgb(
            //  0,
            // 123,
            //  120));
            // a1.Background = new SolidColorBrush(Color.FromRgb(
            //  0,
            // 142,
            //  142));
            // infor = new Information();
            // main.Children.Clear();
            // infor.Bind(this.main.Width, this.main.Height);
            // main.Children.Add(infor);
        }

        void Clear()
        {

        }
    }
}
