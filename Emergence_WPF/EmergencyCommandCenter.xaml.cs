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
using System.Windows.Shapes;

namespace Emergence_WPF
{
    /// <summary>
    /// EmergencyCommandCenter.xaml 的交互逻辑
    /// </summary>
    public partial class EmergencyCommandCenter : Window
    {
        public EmergencyCommandCenter()
        {
            InitializeComponent();

        }
        FullScreen1 scr;
        GeographicInformation geo;
        LiveVideo video;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            main.Inlines.Clear();
            scr = new FullScreen1();
            main.Inlines.Add(scr);
        }
        void clear()
        {
            scr = null;
            geo = null;
            video = null;
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
            if (scr != null)
            {
                main.Inlines.Clear();
                clear();
                geo = new GeographicInformation();
                main.Inlines.Add(geo);
                ccc.Text = "应急地理信息";
                this.Title = "应急地理信息";
                return;
            }
            if (geo != null)
            {
                
                main.Inlines.Clear();
                clear();
                video = new LiveVideo();
                main.Inlines.Add(video);
                ccc.Text = "应急现场视频";
                this.Title = "应急现场视频";
                return;
            }
           
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (video != null)
            {
                main.Inlines.Clear();
                clear();
                geo = new GeographicInformation();
                main.Inlines.Add(geo);
                ccc.Text = "应急地理信息";
                this.Title = "应急地理信息";
                return;
            }
            if (geo != null)
            {
                main.Inlines.Clear();
                clear();
              
                scr = new FullScreen1();
                main.Inlines.Add(scr);
                ccc.Text = "应急指挥中心";
                this.Title = "应急指挥中心";
                return;
            }
        }
    }
}
