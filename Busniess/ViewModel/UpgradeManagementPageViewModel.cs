using Busniess.Services;
using Framework;
using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace Busniess.ViewModel
{
	public class UpgradeManagementPageViewModel : NotificationObject
	{
		public virtual string CurrentVersion { get; set; }
		public virtual string Version { get; set; }
		public virtual string Url { get; set; }
		public virtual DateTime CreatedTime { get; set; }
		public virtual string DownloadFolder { get; set; }
		public virtual string DownloadFullPath { get; set; }
		public virtual long ContentLength { get; set; }
		public virtual long DownloadLength { get; set; }
		public virtual bool DownloadEnabled { get; set; }
		public virtual bool IsDownloading { get; set; }
		public virtual Visibility ProcessBarVisiable { get; set; }

		public UpgradeManagementPageViewModel()
		{
			Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

			ProcessBarVisiable = Visibility.Hidden;
			Init();
			CurrentVersion = ConfigurationManager.AppSettings["Client"] ?? "1.0";
			double verNumber = 1.0;
			double.TryParse(CurrentVersion, out verNumber);
			var latest = 1.0;
			double.TryParse(Version, out latest);
			if (latest > verNumber)
			{
				DownloadEnabled = true;
			}
			else
			{
				DownloadEnabled = false;
			}
		}

		private void Init()
		{
			try
			{
				var service = new UpdateService();
				var data = service.GetCurrentVersionInfo();
				if (data != null)
				{
					Version = data.Version;
					Url = data.Url;
					CreatedTime = data.CreatedTime;
				}
			}
			catch (Exception ex)
			{
				Logger.ErrorFormat("初始化版本管理页面异常", ex);
			}
		}

		public void Download()
		{
			ThreadPool.QueueUserWorkItem((obj) =>
			{
				var aopWraper = IsAopWapper ? this : this.CreateAopProxy();
				try
				{
					if (aopWraper.IsDownloading)
					{
						return;
					}
					else
					{
						aopWraper.IsDownloading = true;
					}
					var splits = aopWraper.Url.Split('/', '\\');
					string fileName = splits != null && splits.Length > 0 ? splits[splits.Length - 1] : "test.txt";
					aopWraper.DownloadFullPath = System.IO.Path.Combine(aopWraper.DownloadFolder, fileName);
					aopWraper.DownloadEnabled = false;
					WebRequest request = WebRequest.Create(aopWraper.Url);
					WebResponse respone = request.GetResponse();
					aopWraper.ProcessBarVisiable = Visibility.Visible;
					aopWraper.ContentLength = respone.ContentLength;

					using (Stream netStream = respone.GetResponseStream())
					{
						using (Stream fileStream = new FileStream(aopWraper.DownloadFullPath, FileMode.Create))
						{
							byte[] read = new byte[1024];
							int realReadLen = netStream.Read(read, 0, read.Length);
							while (realReadLen > 0)
							{
								fileStream.Write(read, 0, realReadLen);
								aopWraper.DownloadLength += realReadLen;
								realReadLen = netStream.Read(read, 0, read.Length);
							}
						}
					}
					System.Windows.MessageBox.Show("下载成功");
				}
				catch (System.UnauthorizedAccessException uex)
				{
					Logger.Error(string.Format("下载失败:{0}", aopWraper.Url), uex);
					System.Windows.MessageBox.Show(string.Format("下载客户端{0}失败, 没有访问\"{1}\"的权限", aopWraper.Version, aopWraper.DownloadFolder));
				}
				catch (Exception ex)
				{
					Logger.Error(string.Format("下载失败:{0}", aopWraper.Url), ex);
					System.Windows.MessageBox.Show(string.Format("下载客户端{0}失败", aopWraper.Version));
				}
				finally
				{
					aopWraper.DownloadEnabled = true;
					aopWraper.IsDownloading = false;
				}
			}, null);
		}
	}
}
