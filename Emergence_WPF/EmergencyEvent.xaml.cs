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
    public partial class EmergencyEvent : UserControl
    {
        private int _totalCount = 3;
        private int _pageIndex = 1;
        private int _totalPage = 1;
        private int _pageSize = 3;
        public void bind(double width, double height)
        {

            this.Width = width;
            this.Height = height;

        }
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
        public EmergencyEvent()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        List<Event> Events = new List<Event>();

        details deta = null;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            deta = new details();
            deta.bind(this.main.Width, this.main.Height);
            main.Inlines.Add(deta);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // map.Bind("120.136940", "30.268970");
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            a1.Background = new SolidColorBrush(Color.FromRgb(
              0, 142, 142));
            a2.Background = new SolidColorBrush(Color.FromRgb(
             245, 245, 245));
            a3.Background = new SolidColorBrush(Color.FromRgb(
            245, 245, 245));
            a4.Background = new SolidColorBrush(Color.FromRgb(
           245, 245, 245));


            a1.Foreground = new SolidColorBrush(Color.FromRgb(
              255, 255, 255));
            a2.Foreground = new SolidColorBrush(Color.FromRgb(
             51, 122, 183));
            a3.Foreground = new SolidColorBrush(Color.FromRgb(
            51, 122, 183));
            a4.Foreground = new SolidColorBrush(Color.FromRgb(
           51, 122, 183));
         
        }

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            a2.Background = new SolidColorBrush(Color.FromRgb(
              0, 142, 142));
            a1.Background = new SolidColorBrush(Color.FromRgb(
             245, 245, 245));
            a3.Background = new SolidColorBrush(Color.FromRgb(
            245, 245, 245));
            a4.Background = new SolidColorBrush(Color.FromRgb(
           245, 245, 245));

            a2.Foreground = new SolidColorBrush(Color.FromRgb(
              255, 255, 255));
            a1.Foreground = new SolidColorBrush(Color.FromRgb(
             51, 122, 183));
            a3.Foreground = new SolidColorBrush(Color.FromRgb(
            51, 122, 183));
            a4.Foreground = new SolidColorBrush(Color.FromRgb(
           51, 122, 183));
        }

        private void Label_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            a3.Background = new SolidColorBrush(Color.FromRgb(
              0, 142, 142));
            a2.Background = new SolidColorBrush(Color.FromRgb(
             245, 245, 245));
            a1.Background = new SolidColorBrush(Color.FromRgb(
            245, 245, 245));
            a4.Background = new SolidColorBrush(Color.FromRgb(
           245, 245, 245));

            a3.Foreground = new SolidColorBrush(Color.FromRgb(
              255, 255, 255));
            a2.Foreground = new SolidColorBrush(Color.FromRgb(
             51, 122, 183));
            a1.Foreground = new SolidColorBrush(Color.FromRgb(
            51, 122, 183));
            a4.Foreground = new SolidColorBrush(Color.FromRgb(
           51, 122, 183));
        }

        private void Label_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            a4.Background = new SolidColorBrush(Color.FromRgb(
              0, 142, 142));
            a2.Background = new SolidColorBrush(Color.FromRgb(
             245, 245, 245));
            a3.Background = new SolidColorBrush(Color.FromRgb(
            245, 245, 245));
            a1.Background = new SolidColorBrush(Color.FromRgb(
           245, 245, 245));

            a4.Foreground = new SolidColorBrush(Color.FromRgb(
              255, 255, 255));
            a2.Foreground = new SolidColorBrush(Color.FromRgb(
             51, 122, 183));
            a3.Foreground = new SolidColorBrush(Color.FromRgb(
            51, 122, 183));
            a1.Foreground = new SolidColorBrush(Color.FromRgb(
           51, 122, 183));
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ff = this.ActualWidth - 1000;
            var hh = this.ActualHeight - 550;
            this.Width = 1000 + ff;
            this.Height = 550 + hh;
            m1.Width = 1000 + ff;
            m2.Width = 1000 + ff;
            m3.Width = 1000 + ff;
            m3.Width = 1000 + ff;

            m1.Height = 550 + hh;
            m2.Height = 550 + hh;
            m3.Height = 530 + hh;
            m4.Height = 530 + hh;

            //this.main.Width = 800 + ff;
            //this.main.Height = 530 + hh;

            this.jjj.Width = 800 + ff;
            this.jjj.Height = 530 + hh;

            if (deta != null) 
            {
                deta.bind(this.jjj.Width, this.jjj.Height);
                main.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            }


            //if (infor != null)
            //{
            //    infor.Bind(this.main.Width, this.main.Height);
            //}
            //if (Emergency != null)
            //{

            //}
        }

       
       
       
    }
}
