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
    public partial class NotificationTask : UserControl
    {
       

        public void bind(double width, double height)
        {

            this.Width = width;
            this.Height = height;

        }
        
  
        public NotificationTask()
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
            var hh = this.ActualHeight - 450;
            if (hh < 0) 
            {
                hh = 0;
            }
            if (ff < 0)
            {
                ff = 0;
            }

            this.Width = 800 + ff;
            this.Height = 450 + hh;
            a1.Width = 800 + ff;
            a1.Height = 800 + hh;
            a2.Width = 780 + ff;

            a3.Width = 780 + ff;
            a3.Height = 160 + hh/3; 
            a4.Height = 120 + hh / 3;
            a4.Width = 780 + ff;

            jjj.Height = 160 + hh / 3;
            jjj.Width = 780 + ff;


            txtKeyword.Width = 718 + ff;
            txtKeyword.Height = 60 + hh / 3;


            a5.Width = 780 + ff;
            a5.Height = 165 + hh / 3;

            a6.Width = 780 + ff;

            a10.Width = 718 + ff;
            a10.Height = 60 + hh / 3;

            a11.Width = 780 + ff;
            a11.Height = 100 + hh / 3;

            a12.Width = 780 + ff;
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Window1 win = new Window1();
            win.ShowDialog();
        }

       

       
       
       
    }
}
