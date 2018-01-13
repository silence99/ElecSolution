using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Emergence_WPF.Comm
{
	public class FormControlHost : HwndHost
	{
		IntPtr hwndControl;
		IntPtr hwndHost;
		public System.Windows.Forms.Control Control { get; set; }
		public FormControlHost(System.Windows.Forms.Control control)
		{
			Control = control;
		}

		protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		{
			return new HandleRef(this, Control.Handle);
		}

		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			Win32.DestroyWindow(hwnd.Handle);
		}
	}
}
