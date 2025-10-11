namespace TigerSan.ScreenDetection.Models
{
    #region 屏幕信息
    /// <summary>
    /// 屏幕信息
    /// </summary>
    public class ScreenInfo
    {
        #region 【Properties】
        #region [基本信息]
        public int Index { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public double DpiX { get; set; }
        public double DpiY { get; set; }
        public double Scale { get; set; } = 1;
        #endregion [基本信息]

        #region [实际尺寸]
        // 尺寸：
        public int Width { get; set; }
        public int Height { get; set; }
        // 边框：
        public double BoundTop { get => BoundRect.pLT.Y; }
        public double BoundBottom { get => BoundRect.pRB.Y; }
        public double BoundLeft { get => BoundRect.pLT.X; }
        public double BoundRight { get => BoundRect.pRB.X; }
        public Rectangle2D BoundRect { get; set; } = new Rectangle2D(0, 0, 0, 0);
        // 工作区：
        public double WorkingAreaTop { get; set; }
        public double WorkingAreaBottom { get; set; }
        public double WorkingAreaLeft { get; set; }
        public double WorkingAreaRight { get; set; }
        public Rectangle2D WorkingAreaRect { get; set; } = new Rectangle2D(0, 0, 0, 0);
        #endregion [实际尺寸]

        #region [虚拟尺寸]
        // 尺寸：
        public int VirtualWidth { get; set; }
        public int VirtualHeight { get; set; }
        // 边框：
        public double VirtualBoundTop { get => VirtualBoundRect.pLT.Y; }
        public double VirtualBoundBottom { get => VirtualBoundRect.pRB.Y; }
        public double VirtualBoundLeft { get => VirtualBoundRect.pLT.X; }
        public double VirtualBoundRight { get => VirtualBoundRect.pRB.X; }
        public Rectangle2D VirtualBoundRect { get; set; } = new Rectangle2D(0, 0, 0, 0);
        // 工作区：
        public double VirtualWorkingAreaTop { get; set; }
        public double VirtualWorkingAreaBottom { get; set; }
        public double VirtualWorkingAreaLeft { get; set; }
        public double VirtualWorkingAreaRight { get; set; }
        public Rectangle2D VirtualWorkingAreaRect { get; set; } = new Rectangle2D(0, 0, 0, 0);
        #endregion [虚拟尺寸]
        #endregion 【Properties】

        #region 【Functions】
        #region 获取“边界集合”
        public static List<Rectangle2D> GetBoundRects(IList<ScreenInfo> screenInfos)
        {
            var boundRects = new List<Rectangle2D>();
            foreach (var screenInfo in screenInfos)
            {
                boundRects.Add(screenInfo.BoundRect);
            }
            return boundRects;
        }
        #endregion

        #region 获取“工作区集合”
        public static List<Rectangle2D> GetWorkingAreaRects(IList<ScreenInfo> screenInfos)
        {
            var workingAreaRects = new List<Rectangle2D>();
            foreach (var screenInfo in screenInfos)
            {
                workingAreaRects.Add(screenInfo.WorkingAreaRect);
            }
            return workingAreaRects;
        }
        #endregion

        #region 获取“虚拟边界集合”
        public static List<Rectangle2D> GetVirtualBoundRects(IList<ScreenInfo> screenInfos)
        {
            var virtualBoundRects = new List<Rectangle2D>();
            foreach (var screenInfo in screenInfos)
            {
                virtualBoundRects.Add(screenInfo.VirtualBoundRect);
            }
            return virtualBoundRects;
        }
        #endregion

        #region 获取“虚拟工作区集合”
        public static List<Rectangle2D> GetVirtualWorkingAreaRects(IList<ScreenInfo> screenInfos)
        {
            var virtualWorkingAreaRects = new List<Rectangle2D>();
            foreach (var screenInfo in screenInfos)
            {
                virtualWorkingAreaRects.Add(screenInfo.VirtualWorkingAreaRect);
            }
            return virtualWorkingAreaRects;
        }
        #endregion
        #endregion 【Functions】
    }
    #endregion
}
