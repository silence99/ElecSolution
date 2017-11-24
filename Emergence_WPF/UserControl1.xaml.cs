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
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
          
	//	 [System.Runtime.InteropServices.ComVisible(true)]
    public partial class UserControl1 : UserControl
    {
        private System.Windows.Forms.WebBrowser webBrowser;//= new System.Windows.Forms.WebBrowser();
        WebBrowserOverlayWF wbo = null;
        public UserControl1()
        {
            InitializeComponent();

            try
            {
                this.Loaded += (s, e) =>
                {
                    //1、取得WPF版的WebBrowser的封装引用
                    //WebBrowserOverlay wbo = new WebBrowserOverlay(host);
                    //webBrowser = wbo.WebBrowser;

                    //2、取得Winform版的WebBrower的封闭引用，此版此控件功能强大点
                    wbo = new WebBrowserOverlayWF(host);
                    webBrowser = wbo.WebBrowser;

                    webBrowser.Navigate(new Uri(System.Environment.CurrentDirectory + @"/BMap.html", UriKind.RelativeOrAbsolute));//获取根目录的html文件  
                  //  webBrowser.ObjectForScripting = this;

                //    object[] objs = new object[2] {
                //double.Parse("120.136940"),
                //double.Parse("30.268970")};
                //    webBrowser.Document.InvokeScript("addMarker", objs);
                };
            }
            catch (Exception)
            {
               
              
            }
           
           
        }
        public void close()
        {
            if (wbo != null)
            {
                 this.Visibility = Visibility.Collapsed;
                // webBrowser.Dispose();
                wbo._placementTarget.Visibility = Visibility.Collapsed;
                wbo._placementTarget.Width = 0;
                wbo._placementTarget.Height = 0;
                wbo.OnSizeLocationChanged();

            }
            
        }
        private void Move_Click(object sender, RoutedEventArgs e)
        {
            object[] objs = new object[2] {
                double.Parse(this.jin.Text),
                double.Parse(this.wei.Text)};
            webBrowser.Document.InvokeScript("MoveToPoint", objs);

        }
        public void Bind(string jing,string wei) 
        {
            object[] objs = new object[2] {
                double.Parse(jing),
                double.Parse(wei)};
            webBrowser.Document.InvokeScript("addMarker", objs);
        }
        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            // 119.931298,28.469722
            object[] objs = new object[2] {
                double.Parse("119.931298"),
                double.Parse("28.469722")};
            webBrowser.Document.InvokeScript("addMarker", objs);
            object[] objs1 = new object[2] {
                double.Parse("119.931298"),
                double.Parse("28.469722")};
            webBrowser.Document.InvokeScript("addMarker", objs1);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.Document.InvokeScript("removeOverlay", null);
        }
    }
}
