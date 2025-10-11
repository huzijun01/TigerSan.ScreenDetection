using System.Windows.Media;

namespace TigerSan.Test.Models
{
    public class PointModel : BindableBase
    {
        #region 【Properties】
        /// <summary>
        /// Left
        /// </summary>
        public double Left
        {
            get { return _Left; }
            set { SetProperty(ref _Left, value); }
        }
        private double _Left;

        /// <summary>
        /// Top
        /// </summary>
        public double Top
        {
            get { return _Top; }
            set { SetProperty(ref _Top, value); }
        }
        private double _Top;

        /// <summary>
        /// 笔刷
        /// </summary>
        public Brush Stroke
        {
            get { return _Stroke; }
            private set { SetProperty(ref _Stroke, value); }
        }
        private Brush _Stroke = new SolidColorBrush(Colors.White);

        /// <summary>
        /// 笔刷线宽
        /// </summary>
        public double StrokeThickness
        {
            get { return _StrokeThickness; }
            set { SetProperty(ref _StrokeThickness, value); }
        }
        private double _StrokeThickness = 1;

        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color
        {
            get { return ((SolidColorBrush)Stroke).Color; }
            set { Stroke = new SolidColorBrush(value); }
        }
        #endregion 【Properties】
    }
}
