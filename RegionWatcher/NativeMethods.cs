using System;
using System.Runtime.InteropServices;

namespace RegionWatcher
{
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("msvcrt.dll", EntryPoint = "memcmp", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MemCmp(IntPtr b1, IntPtr b2, UIntPtr count);

        public const int WM_NCHITTEST = 132;
        public const int WM_NCLBUTTONDOWN = 161;
        public const int HTCAPTION = 2;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTCLIENT = 1;
    }
}
