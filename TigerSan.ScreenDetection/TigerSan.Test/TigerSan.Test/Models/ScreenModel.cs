namespace TigerSan.Test.Models
{
    public class ScreenModel : BindableBase
    {
        #region 【Properties】
        /// <summary>
        /// 序号
        /// </summary>
        public double Index
        {
            get { return _Index; }
            set { SetProperty(ref _Index, value); }
        }
        private double _Index;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get { return _DeviceName; }
            set { SetProperty(ref _DeviceName, value); }
        }
        private string _DeviceName = string.Empty;

        /// <summary>
        /// DpiX
        /// </summary>
        public double DpiX
        {
            get { return _DpiX; }
            set { SetProperty(ref _DpiX, value); }
        }
        private double _DpiX;

        /// <summary>
        /// DpiY
        /// </summary>
        public double DpiY
        {
            get { return _DpiY; }
            set { SetProperty(ref _DpiY, value); }
        }
        private double _DpiY;

        /// <summary>
        /// 缩放比
        /// </summary>
        public double Scale
        {
            get { return _Scale; }
            set { SetProperty(ref _Scale, value); }
        }
        private double _Scale = 1;

        /// <summary>
        /// 缩放百分比
        /// </summary>
        public double Scale100
        {
            get { return Scale * 100; }
        }

        /// <summary>
        /// 实际宽度
        /// </summary>
        public double ActualWidth
        {
            get { return _ActualWidth; }
            set { SetProperty(ref _ActualWidth, value); }
        }
        private double _ActualWidth;

        /// <summary>
        /// 实际高度
        /// </summary>
        public double ActualHeight
        {
            get { return _ActualHeight; }
            set { SetProperty(ref _ActualHeight, value); }
        }
        private double _ActualHeight;

        /// <summary>
        /// 虚拟宽度
        /// </summary>
        public double VirtualWidth
        {
            get { return _VirtualWidth; }
            set { SetProperty(ref _VirtualWidth, value); }
        }
        private double _VirtualWidth;

        /// <summary>
        /// 虚拟高度
        /// </summary>
        public double VirtualHeight
        {
            get { return _VirtualHeight; }
            set { SetProperty(ref _VirtualHeight, value); }
        }
        private double _VirtualHeight;

        /// <summary>
        /// 上
        /// </summary>
        public double Top
        {
            get { return _Top; }
            set { SetProperty(ref _Top, value); }
        }
        private double _Top;

        /// <summary>
        /// 下
        /// </summary>
        public double Bottom
        {
            get { return _Bottom; }
            set { SetProperty(ref _Bottom, value); }
        }
        private double _Bottom;
        /// <summary>
        /// 左
        /// </summary>
        public double Left
        {
            get { return _Left; }
            set { SetProperty(ref _Left, value); }
        }
        private double _Left;

        /// <summary>
        /// 右
        /// </summary>
        public double Right
        {
            get { return _Right; }
            set { SetProperty(ref _Right, value); }
        }
        private double _Right;
        #endregion 【Properties】
    }
}
