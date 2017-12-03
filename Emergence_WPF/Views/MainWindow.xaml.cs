using Business.Strategies;
using Emergence.Common.ViewModel;
using Emergence_WPF.Comm;
using System.Windows;
using System.Windows.Input;

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
        MainPageUiModel _uiModel;
        public MainPageUiModel UiModel
        {
            get
            {
                return _uiModel;
            }
            set
            {
                _uiModel = value;
            }
        }

        MainPageStrategyController StrategyController { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            loginname.Text = "用户" + CommHelp.Name;
            UiModel = new MainPageUiModel();
            StrategyController = new MainPageStrategyController(UiModel);
            UiModel.PropertyChangedEx += (sender, args) => { if (args.PropertyName == "Name") { } };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Information = null;
            Navigation = null;

            //Default set full screen
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.CanResize;
            //this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            //Maincontrol = new MainUserControl2();
            Maincontrol = new UserControl_MainPage();
            //Maincontrol.bind(this.ActualWidth, this.ActualHeight);
            Maincontrol.UiModel = UiModel;
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
            Maincontrol.UiModel = UiModel;
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
            MasterEventManagement information = new MasterEventManagement();
            //Information = new EmergencyInformation();
            //var ff = this.ActualWidth - 1000;
            //var hh = this.ActualHeight - 630;
            //Information.bind(1000 + ff, 550 + hh);
            maingrid.Children.Add(information);

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
