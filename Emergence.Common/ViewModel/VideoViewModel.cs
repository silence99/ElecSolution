using Framework;
using System;

namespace Emergence.Common.ViewModel
{
	public class VideoViewModel : NotificationObject
	{
		public virtual double Width { get; set; }
		public virtual double Height { get; set; }
		public static string DefaultImageUri { get { return @"DebugImage\11.jpg"; } }
		public virtual Uri Uri { get; set; }
		public virtual string ImageUri { get; set; }		
	}
}
