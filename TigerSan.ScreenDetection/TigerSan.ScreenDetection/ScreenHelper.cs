using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using TigerSan.ScreenDetection.Models;

namespace TigerSan.ScreenDetection
{
    public static class ScreenHelper
    {
        #region 【平台调用】
        #region [Fields]
        //private const int MONITOR_DEFAULTTOPRIMARY = 1;
        private const int LOGPIXELSX = 88;
        private const int LOGPIXELSY = 90;
        public delegate bool MonitorEnumProc(
            IntPtr hMonitor,
            IntPtr hdcMonitor,
            ref RECT lprcMonitor,
            IntPtr dwData);
        #endregion [Fields]

        #region [Structs]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            #region 【Ctor】
            public RECT(Rectangle rectangle)
                : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
            {
            }

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
            #endregion
        }

        private struct MONITORINFOEX
        {
            public uint cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szDevice;
        }
        #endregion [Structs]

        #region  [Windows APIs]
        [DllImport("Shcore.dll")]
        private static extern int GetDpiForMonitor(
            IntPtr hmonitor,
            int dpiType,
            out uint dpiX,
            out uint dpiY);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDisplayMonitors(
            IntPtr hdc,
            IntPtr lprcClip,
            MonitorEnumProc lpfnEnum,
            IntPtr dwData);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromRect([In] ref RECT lprc, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDC(
            string lpszDriver,
            string? lpszDevice,
            string? lpszOutput,
            IntPtr lpszInitData);

        [DllImport("gdi32.dll")]
        static extern bool DeleteDC(IntPtr hdc);
        #endregion [Windows APIs]
        #endregion 【平台调用】

        #region 【Functions】
        #region 获取“屏幕信息集合”
        /// <summary>
        /// 获取“屏幕信息集合”
        /// </summary>
        public static List<ScreenInfo> GetScreenInfos()
        {
            var screenInfos = new List<ScreenInfo>();
            int index = 0;

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, Fn_MonitorEnumProc, IntPtr.Zero);

            bool Fn_MonitorEnumProc(
                IntPtr hMonitor,
                IntPtr hdcMonitor,
                ref RECT lprcMonitor,
                IntPtr dwData)
            {
                var info = new MONITORINFOEX();
                info.cbSize = (uint)Marshal.SizeOf<MONITORINFOEX>();
                GetMonitorInfo(hMonitor, ref info);

                // 直接从MONITORINFOEX的rcMonitor计算像素尺寸:
                int width = info.rcMonitor.Right - info.rcMonitor.Left;
                int height = info.rcMonitor.Bottom - info.rcMonitor.Top;

                #region 添加
                var screenInfo = new ScreenInfo
                {
                    Index = index++,
                    DeviceName = info.szDevice,
                    // 尺寸：
                    Width = width,
                    Height = height,
                    // 边框：
                    BoundRect = GetRectangle2D(info.rcMonitor),
                    // 工作区：
                    WorkingAreaTop = info.rcWork.Top,
                    WorkingAreaBottom = info.rcWork.Bottom,
                    WorkingAreaLeft = info.rcWork.Left,
                    WorkingAreaRight = info.rcWork.Right,
                    WorkingAreaRect = GetRectangle2D(info.rcWork),
                };

                // 设置“DPI”和“缩放比”:
                var hdc = CreateDC(info.szDevice, null, null, IntPtr.Zero);
                screenInfo.DpiX = GetDeviceCaps(hdc, LOGPIXELSX);
                screenInfo.DpiY = GetDeviceCaps(hdc, LOGPIXELSY);
                screenInfo.Scale = screenInfo.DpiX / 96.0;
                DeleteDC(hdc);

                #region [虚拟尺寸]
                // 尺寸：
                screenInfo.VirtualWidth = (int)Math.Round(width / screenInfo.Scale);
                screenInfo.VirtualHeight = (int)Math.Round(height / screenInfo.Scale);
                // 边框：
                screenInfo.VirtualBoundRect = GetRectangle2D(info.rcMonitor);
                screenInfo.VirtualBoundRect.Divide(screenInfo.Scale);
                // 工作区：
                screenInfo.VirtualWorkingAreaTop = info.rcWork.Top / screenInfo.Scale;
                screenInfo.VirtualWorkingAreaBottom = info.rcWork.Bottom / screenInfo.Scale;
                screenInfo.VirtualWorkingAreaLeft = info.rcWork.Left / screenInfo.Scale;
                screenInfo.VirtualWorkingAreaRight = info.rcWork.Right / screenInfo.Scale;
                screenInfo.VirtualWorkingAreaRect = GetRectangle2D(info.rcWork);
                screenInfo.VirtualWorkingAreaRect.Divide(screenInfo.Scale);
                #endregion [虚拟尺寸]

                screenInfos.Add(screenInfo);
                #endregion 添加

                return true;
            }

            return screenInfos;
        }
        #endregion

        #region 获取最小点
        public static Point2D GetMinPoint()
        {
            var screenInfos = GetScreenInfos();
            var boundRects = ScreenInfo.GetBoundRects(screenInfos);
            return Rectangle2D.GetMinPoint(boundRects);
        }
        #endregion

        #region 获取“矩形”
        public static Rectangle2D GetRectangle2D(RECT rect)
        {
            return new Rectangle2D(
                new Point2D(rect.Left, rect.Top),
                new Point2D(rect.Right, rect.Top),
                new Point2D(rect.Left, rect.Bottom),
                new Point2D(rect.Right, rect.Bottom));
        }
        #endregion

        #region 获取屏幕缩放比
        /// <summary>
        /// 获取屏幕缩放比
        /// </summary>
        public static double GetDpiScale(int screenIndex = 0)
        {
            var screenInfos = GetScreenInfos();
            return screenIndex >= 0 && screenIndex < screenInfos.Count
                ? screenInfos[screenIndex].Scale : 1;
        }
        #endregion

        #region 判断系统版本是否高于Windows8.1
        public static bool IsWindows81OrLater()
        {
            var os = Environment.OSVersion;
            return os.Platform == PlatformID.Win32NT &&
                   os.Version.Major >= 6 &&
                   os.Version.Minor >= 3;
        }
        #endregion

        #region 获取“点”所在屏幕的序号
        /// <summary>
        /// 获取“点”所在屏幕的序号
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="expend">扩大量</param>
        /// <returns>屏幕序号</returns>
        public static int GetIndexOfScreen(Point2D point, double expend = 0)
        {
            var screenInfos = GetScreenInfos();

            for (int i = 0; i < screenInfos.Count; i++)
            {
                var screenInfo = screenInfos[i];
                screenInfo.BoundRect.Expand(expend);

                if (screenInfo.BoundRect.IsIn(point))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region 获取“坐标”所在屏幕的序号
        /// <summary>
        /// 获取“坐标”所在屏幕的序号
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="expend">扩大量</param>
        /// <returns>屏幕序号</returns>
        public static int GetIndexOfScreen(double x, double y, double expend = 0)
        {
            var screenInfos = GetScreenInfos();

            for (int i = 0; i < screenInfos.Count; i++)
            {
                var screenInfo = screenInfos[i];
                screenInfo.BoundRect.Expand(expend);

                if (screenInfo.BoundRect.IsIn(x, y))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region 获取“矩形”所在屏幕的序号
        /// <summary>
        /// 获取“矩形”所在屏幕的序号
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="expend">扩大量</param>
        /// <returns>屏幕序号</returns>
        public static int GetIndexOfScreen(Rectangle2D rect, double expend = 0)
        {
            var screenInfos = GetScreenInfos();

            // 左上：
            var screenIndex = GetIndexOfScreen(new Point2D(rect.pLT), expend);

            // 右上：
            if (screenIndex < 0)
            {
                screenIndex = GetIndexOfScreen(new Point2D(rect.pRT), expend);
            }

            // 左下：
            if (screenIndex < 0)
            {
                screenIndex = GetIndexOfScreen(new Point2D(rect.pLB), expend);
            }

            // 右下：
            if (screenIndex < 0)
            {
                screenIndex = GetIndexOfScreen(new Point2D(rect.pRB), expend);
            }

            return screenIndex;
        }
        #endregion

        #region 获取控件相对于屏幕的位置
        /// <summary>
        /// 获取控件相对于屏幕的位置
        /// </summary>
        public static Point2D? GetScreenPosition(Control control)
        {
            var strError = "The screen position of the control cannot be obtained!";

            if (control == null)
            {
                Console.WriteLine($"The {nameof(control)} is null!");
                return null;
            }

            if (!control.IsLoaded)
            {
                return null;
            }

            // 获取屏幕坐标：
            try
            {
                var window = Window.GetWindow(control);
                if (window != null)
                {
                    // 正确获取窗口在屏幕中的位置：
                    var windowLeft = window.Left;
                    var windowTop = window.Top;

                    // 获取控件在窗口中的位置：
                    var elementInWindow = control.TransformToVisual(window)
                                         .Transform(new System.Windows.Point(0, 0));

                    // 计算屏幕绝对坐标：
                    return new Point2D(
                        windowLeft + elementInWindow.X,
                        windowTop + elementInWindow.Y
                    );
                }

                Console.WriteLine(strError);
                return null;
            }
            catch
            {
                Console.WriteLine(strError);
                return null;
            }
        }
        #endregion
        #endregion 【Functions】
    }
}
