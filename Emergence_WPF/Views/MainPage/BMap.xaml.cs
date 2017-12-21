using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF
{
	/// <summary>
	/// UserControl1.xaml 的交互逻辑
	/// </summary>
	public partial class BMap : UserControl
	{
		public BMap()
		{
			InitializeComponent();

			//try
			//{
			//    this.Loaded += (s, e) =>
			//    {
			//        //1、取得WPF版的WebBrowser的封装引用
			//        //WebBrowserOverlay wbo = new WebBrowserOverlay(host);
			//        //webBrowser = wbo.WebBrowser;

			//        //2、取得Winform版的WebBrower的封闭引用，此版此控件功能强大点
			//        wbo = new WebBrowserOverlayWF(host);
			//        webBrowser = wbo.WebBrowser;

			//        webBrowser.Navigate(new Uri(Path.GetFullPath(@"Views\MainPage\BMap.html")));
			//        //获取根目录的html文件  
			//        //  webBrowser.ObjectForScripting = this;

			//        //    object[] objs = new object[2] {
			//        //double.Parse("120.136940"),
			//        //double.Parse("30.268970")};
			//        //    webBrowser.Document.InvokeScript("addMarker", objs);
			//    };
			//}
			//catch (Exception)
			//{


			//}

			browser.Navigate(new Uri(Path.GetFullPath(@"BMap.html")));

		}

		private void Move_Click(object sender, RoutedEventArgs e)
		{
			object[] objs = new object[2] {
				double.Parse(this.jin.Text),
				double.Parse(this.wei.Text)};
			browser.Document.InvokeScript("MoveToPoint", objs);

		}
		public void Bind(string jing, string wei)
		{
			object[] objs = new object[2] {
				double.Parse(jing),
				double.Parse(wei)
			};
			browser.Document.InvokeScript("removeOverlay");
			browser.Document.InvokeScript("addMarker", objs);
			objs = new object[2] {
				double.Parse(jing),
				double.Parse(wei)
			};
			browser.Document.InvokeScript("MoveToPoint", objs);
		}
		private void Mark_Click(object sender, RoutedEventArgs e)
		{
			// 119.931298,28.469722
			object[] objs = new object[2] {
				double.Parse("119.931298"),
				double.Parse("28.469722")};
			browser.Document.InvokeScript("addMarker", objs);
			object[] objs1 = new object[2] {
				double.Parse("119.931298"),
				double.Parse("28.469722")};
			browser.Document.InvokeScript("addMarker", objs1);
		}

		private void Clear_Click(object sender, RoutedEventArgs e)
		{
			browser.Document.InvokeScript("removeOverlay", null);
		}

		private void UserControl_Unloaded(object sender, RoutedEventArgs e)
		{
			browser.Dispose();
		}
	}
}
