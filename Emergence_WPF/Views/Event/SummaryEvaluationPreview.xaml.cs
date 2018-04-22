﻿using Business.Services;
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
        public string eventID = "";
        public int type = 0;

        public SummaryEvaluationPreview(int seType)
        {
            InitializeComponent();
            this.KeyDown += SummaryEvaluationPreview_KeyDown;
            browser = new CefSharp.Wpf.ChromiumWebBrowser();
            this.type = seType;
        }

        void SummaryEvaluationPreview_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                browser.Name = "CefSharpBrowser";
                this.Content = browser;
                string loadUrlName = ConfigurationManager.AppSettings["SummaryEvaluationPreviewURL"] ?? "http://emgr-long.tsicent.com";
                loadUrlName += "?id=" + eventID + ";type=" + type;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
