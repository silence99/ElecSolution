using Emergence_WPF.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Emergence_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //MainUserControl2 Maincontrol = null;
        UserControl_MainPage Maincontrol = null;
        EmergencyInformation Information = null;
        NotificationNavigation Navigation = null;
        ReportCenter report = null;

        public MainWindow()
        {
            InitializeComponent();
            loginname.Text = "用户" + CommHelp.Name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Information = null;
            Navigation = null;

            //Default set full screen
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            this.ResizeMode = System.Windows.ResizeMode.CanResize;
            //this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;    
            //Maincontrol = new MainUserControl2();
            Maincontrol = new UserControl_MainPage();
            //Maincontrol.bind(this.ActualWidth, this.ActualHeight);

            maingrid.Children.Add(Maincontrol);

        }
        
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Maincontrol != null) 
            {
                Maincontrol.Close();
            }
            clear(); 
            maingrid.Children.Clear();
            //Maincontrol = new MainUserControl2();
            Maincontrol = new UserControl_MainPage();
            //var ff = this.ActualWidth - 1000;
            //var hh = this.ActualHeight - 630;
            //Maincontrol.bind(1000 + ff, 550 + hh);
            maingrid.Children.Add(Maincontrol);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Maincontrol != null)
            {
                Maincontrol.Close();
            }
            Maincontrol = null;
            Navigation = null;
            clear(); 
            maingrid.Children.Clear();
            Information = new EmergencyInformation();
            var ff = this.ActualWidth - 1000;
            var hh = this.ActualHeight - 630;
            Information.bind(1000+ff,550+hh);
            maingrid.Children.Add(Information);
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void maingrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {


            if (Maincontrol != null)
            {
                Maincontrol.Close();
            }
            Maincontrol = null;
            Information = null;
            clear(); 
            maingrid.Children.Clear();
            Navigation = new NotificationNavigation();
            var ff = this.ActualWidth - 1000;
            var hh = this.ActualHeight - 630;
            Navigation.bind(1000 + ff, 550 + hh);
            maingrid.Children.Add(Navigation);
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            EmergencyCommandCenter center = new EmergencyCommandCenter();
            center.ShowDialog();
        }
        void clear() 
        {
            Maincontrol = null;
            Information = null;
            Navigation = null;
        }
        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if (Maincontrol != null)
            {
                Maincontrol.Close();
            }
            Maincontrol = null;
            Information = null;
            clear(); 
            maingrid.Children.Clear();
            report = new ReportCenter();
            var ff = this.ActualWidth - 1000;
            var hh = this.ActualHeight - 630;
            report.bind(1000 + ff, 550 + hh);
            maingrid.Children.Add(report);
        }

    }
}
