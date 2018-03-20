using Business.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Emergence_WPF.Views
{
    /// <summary>
    /// Interaction logic for SummaryEvaluationPreview.xaml
    /// </summary>
    public partial class SummaryEvaluationPreview : Window
    {
        CefSharp.Wpf.ChromiumWebBrowser browser;

        public SummaryEvaluationPreview()
        {
            InitializeComponent();
            this.KeyDown += SummaryEvaluationPreview_KeyDown;
            browser = new CefSharp.Wpf.ChromiumWebBrowser();
        }

        void SummaryEvaluationPreview_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                browser.Name = "CefSharpBrowser";
                this.Content = browser;
                string loadUrlName = ConfigurationManager.AppSettings["SummaryEvaluationPreviewURL"] ?? "http://www.baidu.com";
                browser.Address = loadUrlName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //var setting = new CefSharp.CefSettings();
            //CefSharp.Cef.Initialize();//setting, true, null);

            //var webView = new CefSharp.Wpf.ChromiumWebBrowser();
            //this.Content = webView;

            
        }

        private void SummaryEvaluationPreview_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)//Esc键  
                {
                    this.Close();
                }
                if (e.Key == Key.C)
                {
                    browser.Load("http://www.csdn.net");
                }
                if (e.Key == Key.V)
                {
                    browser.Load("http://www.taobao.com");
                }
                if (e.Key == Key.B)
                {
                    browser.Load("http://www.163.com");
                }
                if (e.Key == Key.N)
                {
                    browser.Load("http://www.sohu.com");
                }
                if (e.Key == Key.M)
                {
                    browser.Load("http://www.qq.com");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
