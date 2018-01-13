using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Emergence_WPF.Comm
{
	public class FormControlHost : HwndHost
	{
		internal const uint
				  WS_CHILD = 0x40000000,
				  WS_VISIBLE = 0x10000000,
				  LBS_NOTIFY = 0x00000001,
				  HOST_ID = 0x00000002,
				  LISTBOX_ID = 0x00000001,
				  WS_VSCROLL = 0x00200000,
				  WS_BORDER = 0x00800000;
		public IntPtr hwndControl;
		public IntPtr hwndHost;
		public System.Windows.Forms.Control Control { get; set; }
		
		static FormControlHost()
		{
			WidthProperty.OverrideMetadata(typeof(FormControlHost), new System.Windows.FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var obj = sender as FormControlHost;
				Win32.MoveWindow(obj.hwndHost, 0, 0, (int)obj.Width, (int)obj.Height, true);
				obj.Control.Width = (int)obj.Width;
			}));
			HeightProperty.OverrideMetadata(typeof(FormControlHost), new System.Windows.FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var obj = sender as FormControlHost;
				Win32.MoveWindow(obj.hwndHost, 0, 0, (int)obj.Width, (int)obj.Height, true);
				obj.Control.Height = (int)obj.Height;
			}));
		}

		public FormControlHost(System.Windows.Forms.Control control)
		{
			Control = control;
			hwndControl = control.Handle;
		}

		protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		{
			hwndHost = Win32.CreateWindowEx(0, "static", "",
							WS_CHILD | WS_VISIBLE,
							0, 0,
							(int)Width, (int)Width,
							hwndParent.Handle,
							(IntPtr)HOST_ID,
							IntPtr.Zero,
							IntPtr.Zero);
			Win32.SetParent(hwndControl, hwndHost);
			Win32.MoveWindow(hwndHost, 0, 0, (int)Width, (int)Height, true);
			return new HandleRef(this, hwndHost);
		}

		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			Win32.DestroyWindow(hwnd.Handle);
		}
	}
}
