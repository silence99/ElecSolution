using Emergence_WPF.Model;
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
using System.Xml;
using Emergence_WPF.Comm;
using System.ComponentModel;

namespace Emergence_WPF
{
    /// <summary>
    /// MainUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationNavigation : UserControl
    {
        private int _totalCount = 3;
        private int _pageIndex = 1;
        private int _totalPage = 1;
        private int _pageSize = 3;
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                _totalCount = value;
              
            }
        }

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
               
            }
        }

        public int TotalPage
        {
            get
            {
                return _totalPage;
            }
            set
            {
                _totalPage = value;
               
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
                
            }
        }
        private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
        {
            var x = e.PageIndex;
        }
        public NotificationNavigation()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        List<Event> Events = new List<Event>();

        NotificationSystem infor;
        NotificationSystem Emergency;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Emergency = null;
            infor = new NotificationSystem();
            main.Children.Clear();
            infor.bind(this.main.Width, this.main.Height);
            main.Children.Add(infor);


        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // map.Bind("120.136940", "30.268970");
        }

        private void a1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Emergency = null;
            a1.Background = new SolidColorBrush(Color.FromRgb(
              0,
             123,
              120));
           
             a1.Background = new SolidColorBrush(Color.FromRgb(
              0,
             142,
              142));
             infor = new NotificationSystem();
             main.Children.Clear();
             infor.bind(this.main.Width, this.main.Height);
             main.Children.Add(infor);
        }

        private void a2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            infor = null;
            Emergency = new NotificationSystem();
a1.Background = new SolidColorBrush(Color.FromRgb(
              0,
             123,
              120));
           
             main.Children.Clear();
             Emergency.bind(this.main.Width, this.main.Height);
             main.Children.Add(Emergency);
        }

        private void a3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            a1.Background = new SolidColorBrush(Color.FromRgb(
              0,
             123,
              120));
            
        }

        private void a4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            a1.Background = new SolidColorBrush(Color.FromRgb(
              0,
             123,
              120));
          
        }
        public void bind(double width, double height)
        {

            this.Width = width;
            this.Height = height;

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ff = this.ActualWidth - 1000;
            var hh = this.ActualHeight - 550;
            this.Width = 1000 + ff;
            this.Height = 550 + hh;
            top1.Width = 1000 + ff;
            top2.Width = 1000 + ff;
            top3.Width = 1000 + ff;
            top4.Width = 1000 + ff;


            top1.Height = 550 + hh;
            top2.Height = 550 + hh;
            this.main.Width = 1000 + ff;
            this.main.Height = 470 + hh;

            if (infor != null) 
            {
                infor.bind(this.main.Width, this.main.Height);
            }
            if (Emergency != null)
            {
                Emergency.bind(this.main.Width, this.main.Height);
            }
        }

       
    }
}
