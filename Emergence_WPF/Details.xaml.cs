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
    public partial class details : UserControl
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
        public details()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        List<Event> Events = new List<Event>();


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // map.Bind("120.136940", "30.268970");
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ff = this.ActualWidth - 800;
            var hh = this.ActualHeight - 800;
            if (hh < 0) 
            {
                hh = 0;
            }

           // this.Width = 800 + ff;
            this.Height = 800 + hh;
            //a1.Width = 800 + ff;
            //a2.Width = 800 + ff;
            a1.Height = 800 + hh;
            a2.Height = 800 + hh;

            a2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            a1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

        }

       

       
       
       
    }
}
