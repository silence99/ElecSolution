using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Emergence_WPF
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			//show messagebox 
			Logger.Error("Unhandled error:", e.Exception);
		}
	}
}
