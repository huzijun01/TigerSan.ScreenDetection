using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using TigerSan.Test.Models;
using TigerSan.ScreenDetection;
using TigerSan.KeyboardMouseHook;
using TigerSan.ScreenDetection.Models;

namespace TigerSan.Test.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region 【Fields】
        /// <summary>
        /// 缩小比例
        /// </summary>
        private static double _reduceScale = 0.2;

        /// <summary>
        /// 副屏缩放比例
        /// </summary>
        private static double _secondaryScreenScale = 0.4 / 0.3;
        #endregion 【Fields】

        #region 【Properties】
        /// <summary>
        /// 序号
        /// </summary>
        public int IndexOfScreen
        {
            get { return _IndexOfScreen; }
            set { SetProperty(ref _IndexOfScreen, value); }
        }
        private int _IndexOfScreen = 0;

        /// <summary>
        /// 鼠标X坐标
        /// </summary>
        public int MouseX
        {
            get { return _MouseX; }
            set { SetProperty(ref _MouseX, value); }
        }
        private int _MouseX = 0;

        /// <summary>
        /// 鼠标Y坐标
        /// </summary>
        public int MouseY
        {
            get { return _MouseY; }
            set { SetProperty(ref _MouseY, value); }
        }
        private int _MouseY = 0;

        /// <summary>
        /// 点
        /// </summary>
        public PointModel Point { get; set; } = new PointModel() { Color = Colors.Red, StrokeThickness = 5 };

        /// <summary>
        /// 线集合
        /// </summary>
        public ObservableCollection<LineModel> Lines { get; } = new ObservableCollection<LineModel>();

        /// <summary>
        /// 屏幕集合
        /// </summary>
        public ObservableCollection<ScreenModel> Screens { get; } = new ObservableCollection<ScreenModel>();
        #endregion 【Properties】

        #region 【Ctor】
        public MainViewModel()
        {
            InitLines();
            InitScreens();
            MouseHook.Start();
            MouseHook._action += MouseHook__action;
        }
        #endregion 【Ctor】

        #region 【Events】
        #region 鼠标钩子执行函数
        private void MouseHook__action(System.Windows.Forms.MouseEventArgs args)
        {
            // 屏幕序号:
            Point2D mousePoint = new Point2D(args.Location.X, args.Location.Y);
            var indexOfScreen = ScreenHelper.GetIndexOfScreen(mousePoint);
            IndexOfScreen = indexOfScreen < 0 ? IndexOfScreen : indexOfScreen;
            // 副屏缩放:
            if (indexOfScreen > 0)
            {
                mousePoint.Multiply(_secondaryScreenScale);
            }
            // 鼠标位置:
            MouseX = (int)Math.Round(mousePoint.X);
            MouseY = (int)Math.Round(mousePoint.Y);
            // 最小点:
            var screenInfo = ScreenHelper.GetScreenInfos();
            var minPoint = ScreenHelper.GetMinPoint();
            // 点位置:
            mousePoint.NoNegative(minPoint);
            mousePoint.Multiply(_reduceScale);
            mousePoint.Divide(ScreenHelper.GetDpiScale());
            Point.Top = mousePoint.Y;
            Point.Left = mousePoint.X;
        }
        #endregion
        #endregion 【Events】

        #region 【Commands】
        #region 刷新
        public ICommand RefreshCommand { get => new DelegateCommand(Refresh); }
        private void Refresh()
        {
            InitLines();
            InitScreens();
        }
        #endregion
        #endregion 【Commands】

        #region 【Functions】
        #region 初始化“线集合”
        private void InitLines()
        {
            // 清空线集合:
            Lines.Clear();

            // 获取屏幕信息集合:
            var screenInfos = ScreenHelper.GetScreenInfos();
            var virtualBoundRects = ScreenInfo.GetVirtualBoundRects(screenInfos);
            var virtualWorkingAreaRects = ScreenInfo.GetVirtualWorkingAreaRects(screenInfos);
            Rectangle2D.NoNegative(virtualBoundRects);
            Rectangle2D.NoNegative(virtualWorkingAreaRects);

            // 添加矩形:
            foreach (var screenInfo in screenInfos)
            {
                // 绘制边界集合:
                screenInfo.VirtualBoundRect.Multiply(_reduceScale);
                AddRectangle(screenInfo.VirtualBoundRect, Colors.White, 2);

                // 绘制工作区集合:
                screenInfo.VirtualWorkingAreaRect.Multiply(_reduceScale);
                AddRectangle(screenInfo.VirtualWorkingAreaRect, Colors.Yellow, 2);
            }
        }
        #endregion

        #region 初始化“屏幕集合”
        private void InitScreens()
        {
            // 清空:
            Screens.Clear();

            var screenInfos = ScreenHelper.GetScreenInfos();
            foreach (var screenInfo in screenInfos)
            {
                AddScreen(screenInfo);
            }
        }
        #endregion

        #region 添加线
        private void AddLine(double x1, double y1, double x2, double y2, Color? Color = null, double thickness = 1)
        {
            var color = Color ?? Colors.White;
            Lines.Add(new LineModel { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Color = color, StrokeThickness = thickness });
        }
        #endregion

        #region 添加矩形
        private void AddRectangle(Rectangle2D rect, Color? Color = null, double thickness = 1)
        {
            var color = Color ?? Colors.White;
            AddLine(rect.pLT.X, rect.pLT.Y, rect.pRT.X, rect.pRT.Y, color, thickness);
            AddLine(rect.pLB.X, rect.pLB.Y, rect.pRB.X, rect.pRB.Y, color, thickness);
            AddLine(rect.pLT.X, rect.pLT.Y, rect.pLB.X, rect.pLB.Y, color, thickness);
            AddLine(rect.pRT.X, rect.pRT.Y, rect.pRB.X, rect.pRB.Y, color, thickness);
        }
        #endregion

        #region 添加屏幕
        private void AddScreen(ScreenInfo screenInfo)
        {
            Screens.Add(new ScreenModel()
            {
                Index = screenInfo.Index,
                DeviceName = screenInfo.DeviceName,
                DpiX = screenInfo.DpiX,
                DpiY = screenInfo.DpiY,
                Scale = screenInfo.Scale,
                ActualWidth = screenInfo.Width,
                ActualHeight = screenInfo.Height,
                VirtualWidth = screenInfo.VirtualWidth,
                VirtualHeight = screenInfo.VirtualHeight,
                Top = screenInfo.BoundTop,
                Bottom = screenInfo.BoundBottom,
                Left = screenInfo.BoundLeft,
                Right = screenInfo.BoundRight,
            });
        }
        #endregion
        #endregion 【Functions】
    }

    #region 设计数据
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel()
        {
        }
    }
    #endregion
}
