namespace TigerSan.ScreenDetection.Models
{
    #region 点
    /// <summary>
    /// 点
    /// </summary>
    public class Point2D
    {
        #region 【Fields】
        public double X;
        public double Y;
        #endregion 【Fields】

        #region 【Ctor】
        public Point2D()
        {
        }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point2D(Point2D p)
        {
            X = p.X;
            Y = p.Y;
        }
        #endregion 【Ctor】

        #region 【Functions】
        #region 加
        /// <summary>
        /// 加
        /// </summary>
        public void Add(double value)
        {
            Add(value, value);
        }

        /// <summary>
        /// 加
        /// </summary>
        public void Add(double x, double y)
        {
            X += x;
            Y += y;
        }
        #endregion

        #region 减
        /// <summary>
        /// 减
        /// </summary>
        public void Sub(double value)
        {
            Sub(value, value);
        }

        /// <summary>
        /// 减
        /// </summary>
        public void Sub(double x, double y)
        {
            X -= x;
            Y -= y;
        }
        #endregion

        #region 乘
        /// <summary>
        /// 乘
        /// </summary>
        public void Multiply(double value)
        {
            Multiply(value, value);
        }

        /// <summary>
        /// 乘
        /// </summary>
        public void Multiply(double x, double y)
        {
            X *= x;
            Y *= y;
        }
        #endregion

        #region 除
        /// <summary>
        /// 除
        /// </summary>
        public void Divide(double value)
        {
            Divide(value, value);
        }

        /// <summary>
        /// 除
        /// </summary>
        public void Divide(double x, double y)
        {
            X /= x;
            Y /= y;
        }
        #endregion

        #region 无负数处理
        /// <summary>
        /// 无负数处理
        /// </summary>
        public void NoNegative(Point2D minPoint)
        {
            if (minPoint.X < 0)
            {
                X -= minPoint.X;
            }

            if (minPoint.Y < 0)
            {
                Y -= minPoint.Y;
            }
        }
        #endregion
        #endregion 【Functions】
    }
    #endregion

    #region 线
    /// <summary>
    /// 线
    /// </summary>
    public class Line2D
    {
        #region 【Fields】
        public Point2D point1;
        public Point2D point2;
        #endregion 【Fields】

        #region 【Ctor】
        public Line2D()
        {
            point1 = new Point2D();
            point2 = new Point2D();
        }

        public Line2D(Point2D p1, Point2D p2)
        {
            point1 = new Point2D(p1);
            point2 = new Point2D(p2);
        }
        public Line2D(Line2D line)
        {
            point1 = new Point2D(line.point1);
            point2 = new Point2D(line.point2);
        }
        public Line2D(double L1, double T1, double L2, double T2)
        {
            point1 = new Point2D(L1, T1);
            point2 = new Point2D(L2, T2);
        }
        #endregion 【Ctor】

        #region 【Functions】
        #region 加
        /// <summary>
        /// 加
        /// </summary>
        public void Add(double value)
        {
            Add(value, value);
        }

        /// <summary>
        /// 加
        /// </summary>
        public void Add(double x, double y)
        {
            point1.Add(x, y);
            point2.Add(x, y);
        }
        #endregion

        #region 减
        /// <summary>
        /// 减
        /// </summary>
        public void Sub(double value)
        {
            Sub(value, value);
        }

        /// <summary>
        /// 减
        /// </summary>
        public void Sub(double x, double y)
        {
            point1.Sub(x, y);
            point2.Sub(x, y);
        }
        #endregion

        #region 乘
        /// <summary>
        /// 乘
        /// </summary>
        public void Multiply(double value)
        {
            Multiply(value, value);
        }

        /// <summary>
        /// 乘
        /// </summary>
        public void Multiply(double x, double y)
        {
            point1.Multiply(x, y);
            point2.Multiply(x, y);
        }
        #endregion

        #region 除
        /// <summary>
        /// 除
        /// </summary>
        public void Divide(double value)
        {
            Divide(value, value);
        }

        /// <summary>
        /// 除
        /// </summary>
        public void Divide(double x, double y)
        {
            point1.Divide(x, y);
            point2.Divide(x, y);
        }
        #endregion
        #endregion 【Functions】
    }
    #endregion

    #region 矩形
    /// <summary>
    /// 矩形
    /// </summary>
    public class Rectangle2D
    {
        #region 【Fields】
        public Point2D pLT;
        public Point2D pRT;
        public Point2D pLB;
        public Point2D pRB;
        #endregion 【Fields】

        #region 【Properties】
        public double Width { get => pRB.X - pLT.X; }
        public double Height { get => pRB.Y - pLT.Y; }
        public double Top { get => pLT.Y; }
        public double Bottom { get => pRB.Y; }
        public double Left { get => pLT.X; }
        public double Right { get => pRB.X; }
        #endregion 【Properties】

        #region 【Ctor】
        public Rectangle2D()
        {
            pLT = new Point2D();
            pRT = new Point2D();
            pLB = new Point2D();
            pRB = new Point2D();
        }

        public Rectangle2D(Point2D position, double width, double height)
        {
            pLT = new Point2D(position.X, position.Y);
            pRT = new Point2D(position.X + width, position.Y);
            pLB = new Point2D(position.X, position.Y + height);
            pRB = new Point2D(position.X + width, position.Y + height);
        }

        public Rectangle2D(Point2D p2dLT, Point2D p2dRT, Point2D p2dLB, Point2D p2dRB)
        {
            pLT = p2dLT;
            pRT = p2dRT;
            pLB = p2dLB;
            pRB = p2dRB;
        }

        public Rectangle2D(Rectangle2D rect)
        {
            pLT = new Point2D(rect.pLT);
            pRT = new Point2D(rect.pRT);
            pLB = new Point2D(rect.pLB);
            pRB = new Point2D(rect.pRB);
        }

        public Rectangle2D(double Left, double Top, double Height, double Width)
        {
            pLT = new Point2D(Left, Top);
            pRT = new Point2D(Left + Width, Top);
            pLB = new Point2D(Left, Top + Height);
            pRB = new Point2D(Left + Width, Top + Height);
        }
        #endregion 【Ctor】

        #region 【Functions】
        #region 加
        /// <summary>
        /// 加
        /// </summary>
        public void Add(double value)
        {
            Add(value, value);
        }

        /// <summary>
        /// 加
        /// </summary>
        public void Add(double x, double y)
        {
            pLT.X += x;
            pLT.Y += y;
            pRT.X += x;
            pRT.Y += y;
            pLB.X += x;
            pLB.Y += y;
            pRB.X += x;
            pRB.Y += y;
        }

        /// <summary>
        /// 加
        /// </summary>
        public static void Add(IList<Rectangle2D> rects, double value)
        {
            Add(rects, value, value);
        }

        /// <summary>
        /// 加
        /// </summary>
        public static void Add(IList<Rectangle2D> rects, double x, double y)
        {
            foreach (var rect in rects)
            {
                rect.Add(x, y);
            }
        }
        #endregion

        #region 减
        /// <summary>
        /// 减
        /// </summary>
        public void Sub(double value)
        {
            Sub(value, value);
        }

        /// <summary>
        /// 减
        /// </summary>
        public void Sub(double x, double y)
        {
            pLT.X -= x;
            pLT.Y -= y;
            pRT.X -= x;
            pRT.Y -= y;
            pLB.X -= x;
            pLB.Y -= y;
            pRB.X -= x;
            pRB.Y -= y;
        }

        /// <summary>
        /// 减
        /// </summary>
        public static void Sub(IList<Rectangle2D> rects, double value)
        {
            Sub(rects, value, value);
        }

        /// <summary>
        /// 减
        /// </summary>
        public static void Sub(IList<Rectangle2D> rects, double x, double y)
        {
            foreach (var rect in rects)
            {
                rect.Sub(x, y);
            }
        }
        #endregion

        #region 乘
        /// <summary>
        /// 乘
        /// </summary>
        public void Multiply(double value)
        {
            Multiply(value, value);
        }

        /// <summary>
        /// 乘
        /// </summary>
        public void Multiply(double x, double y)
        {
            pLT.X *= x;
            pLT.Y *= y;
            pRT.X *= x;
            pRT.Y *= y;
            pLB.X *= x;
            pLB.Y *= y;
            pRB.X *= x;
            pRB.Y *= y;
        }

        /// <summary>
        /// 乘
        /// </summary>
        public static void Multiply(IList<Rectangle2D> rects, double value)
        {
            Multiply(rects, value, value);
        }

        /// <summary>
        /// 乘
        /// </summary>
        public static void Multiply(IList<Rectangle2D> rects, double x, double y)
        {
            foreach (var rect in rects)
            {
                rect.Multiply(x, y);
            }
        }
        #endregion

        #region 除
        /// <summary>
        /// 除
        /// </summary>
        public void Divide(double value)
        {
            Divide(value, value);
        }

        /// <summary>
        /// 除
        /// </summary>
        public void Divide(double x, double y)
        {
            pLT.X /= x;
            pLT.Y /= y;
            pRT.X /= x;
            pRT.Y /= y;
            pLB.X /= x;
            pLB.Y /= y;
            pRB.X /= x;
            pRB.Y /= y;
        }

        /// <summary>
        /// 除
        /// </summary>
        public static void Divide(IList<Rectangle2D> rects, double value)
        {
            Divide(rects, value, value);
        }

        /// <summary>
        /// 除
        /// </summary>
        public static void Divide(IList<Rectangle2D> rects, double x, double y)
        {
            foreach (var rect in rects)
            {
                rect.Divide(x, y);
            }
        }
        #endregion

        #region 扩大
        /// <summary>
        /// 扩大
        /// </summary>
        public void Expand(double offset)
        {
            Expand(offset, offset);
        }

        /// <summary>
        /// 扩大
        /// </summary>
        public void Expand(double offsetX, double offsetY)
        {
            pLT.X -= offsetX;
            pLT.Y -= offsetY;
            pRT.X += offsetX;
            pRT.Y -= offsetY;
            pLB.X -= offsetX;
            pLB.Y += offsetY;
            pRB.X += offsetX;
            pRB.Y += offsetY;
        }

        /// <summary>
        /// 扩大
        /// </summary>
        public static void Expand(IList<Rectangle2D> rects, double offset)
        {
            Expand(rects, offset, offset);
        }

        /// <summary>
        /// 扩大
        /// </summary>
        public static void Expand(IList<Rectangle2D> rects, double offsetX, double offsetY)
        {
            foreach (var rect in rects)
            {
                rect.Expand(offsetX, offsetY);
            }
        }
        #endregion

        #region 缩小
        /// <summary>
        /// 缩小
        /// </summary>
        public void Shrink(double offset)
        {
            Shrink(offset, offset);
        }

        /// <summary>
        /// 缩小
        /// </summary>
        public void Shrink(double offsetX, double offsetY)
        {
            pLT.X += offsetX;
            pLT.Y += offsetY;
            pRT.X -= offsetX;
            pRT.Y += offsetY;
            pLB.X += offsetX;
            pLB.Y -= offsetY;
            pRB.X -= offsetX;
            pRB.Y -= offsetY;
        }

        /// <summary>
        /// 缩小
        /// </summary>
        public static void Shrink(IList<Rectangle2D> rects, double offset)
        {
            Shrink(rects, offset, offset);
        }

        /// <summary>
        /// 缩小
        /// </summary>
        public static void Shrink(IList<Rectangle2D> rects, double offsetX, double offsetY)
        {
            foreach (var rect in rects)
            {
                rect.Shrink(offsetX, offsetY);
            }
        }
        #endregion

        #region 原点
        /// <summary>
        /// 原点
        /// </summary>
        public void ToOrigin()
        {
            Sub(pLT.X, pLT.Y);
        }
        #endregion

        #region 顶部
        /// <summary>
        /// 顶部
        /// </summary>
        public void ToTop(Rectangle2D rect)
        {
            ToOrigin();
            Add(
                rect.Left,
                rect.Top - Height);
        }
        #endregion

        #region 底部
        /// <summary>
        /// 底部
        /// </summary>
        public void ToBottom(Rectangle2D rect)
        {
            ToOrigin();
            Add(
                rect.Left,
                rect.Top + rect.Height);
        }
        #endregion

        #region 左侧
        /// <summary>
        /// 左侧
        /// </summary>
        public void ToLeft(Rectangle2D rect)
        {
            ToOrigin();
            Add(
                rect.Left - Width,
                rect.Top + rect.Height / 2 - Height / 2);
        }
        #endregion

        #region 右侧
        /// <summary>
        /// 右侧
        /// </summary>
        public void ToRight(Rectangle2D rect)
        {
            ToOrigin();
            Add(
                rect.Left + rect.Width,
                rect.Top + rect.Height / 2 - Height / 2);
        }
        #endregion

        #region 获取最小点
        /// <summary>
        /// 获取最小点
        /// </summary>
        public static Point2D GetMinPoint(IList<Rectangle2D> rects)
        {
            var minPoint = new Point2D(0, 0);

            foreach (var rect in rects)
            {
                if (rect.pLT.X < minPoint.X)
                {
                    minPoint.X = rect.pLT.X;
                }

                if (rect.pLT.Y < minPoint.Y)
                {
                    minPoint.Y = rect.pLT.Y;
                }
            }

            return minPoint;
        }
        #endregion

        #region 无负数处理
        /// <summary>
        /// 无负数处理
        /// </summary>
        /// <returns>最小点</returns>
        public static Point2D NoNegative(IList<Rectangle2D> rects)
        {
            var minPoint = GetMinPoint(rects);

            if (minPoint.X < 0)
            {
                foreach (var rect in rects)
                {
                    rect.Add(-minPoint.X, 0);
                }
            }

            if (minPoint.Y < 0)
            {
                foreach (var rect in rects)
                {
                    rect.Add(0, -minPoint.Y);
                }
            }

            return minPoint;
        }
        #endregion

        #region 判断“点”是否在“矩形”中
        /// <summary>
        /// 判断“点”是否在“矩形”中
        /// </summary>
        public bool IsIn(Point2D point)
        {
            return point.X >= pLT.X && point.Y >= pLT.Y
                && point.X <= pRB.X && point.Y <= pRB.Y;
        }
        #endregion

        #region 判断“坐标”是否在“矩形”中
        /// <summary>
        /// 判断“坐标”是否在“矩形”中
        /// </summary>
        public bool IsIn(double x, double y)
        {
            return x >= pLT.X && y >= pLT.Y
                && x <= pRB.X && y <= pRB.Y;
        }
        #endregion

        #region 判断“矩形”是否在“矩形”中
        /// <summary>
        /// 判断“矩形”是否在“矩形”中
        /// </summary>
        public bool IsIn(Rectangle2D rect)
        {
            return IsIn(rect.pLT) && IsIn(rect.pRT) && IsIn(rect.pLB) && IsIn(rect.pRB);
        }
        #endregion

        #region 获取点所在那块矩形的序号
        /// <summary>
        /// 获取点所在矩形的序号
        /// </summary>
        /// <param name="rects">矩形集合</param>
        /// <param name="pMouse">鼠标坐标</param>
        /// <param name="expend">扩大量</param>
        /// <returns>矩形序号</returns>
        public static int GetIndexOfRectangle(
            IList<Rectangle2D> rects,
            Point2D pMouse,
            double expend = 0)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                var rect = rects[i];
                rect.Expand(expend);

                if (rect.IsIn(pMouse))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
        #endregion 【Functions】
    }
    #endregion

    #region 矩阵
    /// <summary>
    /// 矩阵
    /// </summary>
    public class Matrix2D
    {
        #region 【Fields】
        public int rows;
        public int cols;
        public List<List<Point2D>> Rows;
        #endregion 【Fields】

        #region 【Ctor】
        public Matrix2D()
        {
            Rows = new List<List<Point2D>>();
        }

        public Matrix2D(List<Point2D> points, int iRows, int iCols)
        {
            rows = iRows;
            cols = iCols;
            Rows = new List<List<Point2D>>();

            for (int ir = 0; ir < rows; ir++)
            {
                List<Point2D> ps = new List<Point2D>();
                for (int ic = 0; ic < cols; ic++)
                {
                    ps.Add(new Point2D(points[ir * cols + ic]));
                }
                Rows.Add(ps);
            }
        }

        public Matrix2D(int iRows, int iCols)
        {
            rows = iRows;
            cols = iCols;
            Rows = new List<List<Point2D>>();

            for (int ir = 0; ir < rows; ir++)
            {
                List<Point2D> ps = new List<Point2D>();
                for (int ic = 0; ic < cols; ic++)
                {
                    ps.Add(new Point2D(0, 0));
                }
                Rows.Add(ps);
            }
        }
        #endregion 【Ctor】
    }
    #endregion
}
