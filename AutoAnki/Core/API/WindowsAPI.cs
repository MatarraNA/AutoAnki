using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.API
{
    public static class WindowsAPI
    {
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public class WindowInfo
        {
            public IntPtr Hwnd { get; set; }
            public string Title { get; set; } = "";
            public string ProcessName { get; set; } = "";
        }

        public static List<WindowInfo> GetOpenWindows()
        {
            var windows = new List<WindowInfo>();

            EnumWindows((hWnd, lParam) =>
            {
                if (!IsWindowVisible(hWnd))
                    return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0)
                    return true;

                var builder = new StringBuilder(length + 1);
                GetWindowText(hWnd, builder, builder.Capacity);

                uint pid;
                GetWindowThreadProcessId(hWnd, out pid);

                string processName = "";
                try
                {
                    processName = Process.GetProcessById((int)pid).ProcessName;
                }
                catch { }

                windows.Add(new WindowInfo
                {
                    Hwnd = hWnd,
                    Title = builder.ToString(),
                    ProcessName = processName
                });

                return true;

            }, IntPtr.Zero);

            return windows;
        }
    }
}
