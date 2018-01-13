using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;

static class Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public int X;
		public int Y;

		public POINT(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
		public POINT(Point pt)
		{
			X = Convert.ToInt32(pt.X);
			Y = Convert.ToInt32(pt.Y);
		}
	};

	[DllImport("user32.dll")]
	internal static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

	[DllImport("user32.dll")]
	internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

	[DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
	internal static extern bool DestroyWindow(IntPtr hwnd);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr CreateWindowEx(
					   uint dwExStyle,
					   [MarshalAs(UnmanagedType.LPStr)] string lpClassName,
					   [MarshalAs(UnmanagedType.LPStr)] string lpWindowName,
					   uint dwStyle,
					   int x,
					   int y,
					   int nWidth,
					   int nHeight,
					   IntPtr hWndParent,
					   IntPtr hMenu,
					   IntPtr hInstance,
					   IntPtr lpParam);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
};

[Flags]
public enum WindowStylesEx : uint
{
	WS_EX_ACCEPTFILES = 0x00000010,
	WS_EX_APPWINDOW = 0x00040000,
	WS_EX_CLIENTEDGE = 0x00000200,
	WS_EX_COMPOSITED = 0x02000000,
	WS_EX_CONTEXTHELP = 0x00000400,
	WS_EX_CONTROLPARENT = 0x00010000,
	WS_EX_DLGMODALFRAME = 0x00000001,
	WS_EX_LAYERED = 0x00080000,
	WS_EX_LAYOUTRTL = 0x00400000,
	WS_EX_LEFT = 0x00000000,
	WS_EX_LEFTSCROLLBAR = 0x00004000,
	WS_EX_LTRREADING = 0x00000000,
	WS_EX_MDICHILD = 0x00000040,
	WS_EX_NOACTIVATE = 0x08000000,
	WS_EX_NOINHERITLAYOUT = 0x00100000,
	WS_EX_NOPARENTNOTIFY = 0x00000004,
	WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
	WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
	WS_EX_RIGHT = 0x00001000,
	WS_EX_RIGHTSCROLLBAR = 0x00000000,
	WS_EX_RTLREADING = 0x00002000,
	WS_EX_STATICEDGE = 0x00020000,
	WS_EX_TOOLWINDOW = 0x00000080,
	WS_EX_TOPMOST = 0x00000008,
	WS_EX_TRANSPARENT = 0x00000020,
	WS_EX_WINDOWEDGE = 0x00000100
}