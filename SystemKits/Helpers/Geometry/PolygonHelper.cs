using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 多边形帮助类
    /// </summary>
    public class PolygonHelper
    {
        #region CalculateClockDirection
        /// <summary>
        /// 判断多边形是顺时针还是逆时针.
        /// </summary>
        /// <param name="points">所有的点</param>
        /// <param name="isYAxixToDown">true:Y轴向下为正(屏幕坐标系),false:Y轴向上为正(一般的坐标系)</param>
        /// <returns></returns>
        public static ClockDirection CalculateClockDirection(List<PointF> points, bool isYAxixToDown)
        {
            int i, j, k;
            int count = 0;
            double z;
            int yTrans = isYAxixToDown ? (-1) : (1);
            if (points == null || points.Count < 3)
            {
                return (0);
            }
            int n = points.Count;
            for (i = 0; i < n; i++)
            {
                j = (i + 1) % n;
                k = (i + 2) % n;
                z = (points[j].X - points[i].X) * (points[k].Y * yTrans - points[j].Y * yTrans);
                z -= (points[j].Y * yTrans - points[i].Y * yTrans) * (points[k].X - points[j].X);
                if (z < 0)
                {
                    count--;
                }
                else if (z > 0)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                return (ClockDirection.Counterclockwise);
            }
            else if (count < 0)
            {
                return (ClockDirection.Clockwise);
            }
            else
            {
                return (ClockDirection.None);
            }
        }
        #endregion

        #region CalculatePolygonType
        /// <summary>      
        ///判断多边形是凸多边形还是凹多边形.
        ///假定该多边形是简单的多边形，即没有横穿也没有洞的多边形。
        /// </summary>
        /// <param name="points"></param>
        /// <param name="isYAxixToDown">true:Y轴向下为正(屏幕坐标系),false:Y轴向上为正(一般的坐标系)</param>
        /// <returns></returns>
        public static PolygonType CalculatePolygonType(List<PointF> points, bool isYAxixToDown)
        {
            int i, j, k;
            int flag = 0;
            double z;

            if (points == null || points.Count < 3)
            {
                return (0);
            }
            int n = points.Count;
            int yTrans = isYAxixToDown ? (-1) : (1);
            for (i = 0; i < n; i++)
            {
                j = (i + 1) % n;
                k = (i + 2) % n;
                z = (points[j].X - points[i].X) * (points[k].Y * yTrans - points[j].Y * yTrans);
                z -= (points[j].Y * yTrans - points[i].Y * yTrans) * (points[k].X - points[j].X);
                if (z < 0)
                {
                    flag |= 1;
                }
                else if (z > 0)
                {
                    flag |= 2;
                }
                if (flag == 3)
                {
                    return (PolygonType.Concave);
                }
            }
            if (flag != 0)
            {
                return (PolygonType.Convex);
            }
            else
            {
                return (PolygonType.None);
            }
        }
        #endregion

        #region CalculateArea Function
        /// <summary>
        /// 计算多边形面积的函数
        /// (以原点为基准点,分割为多个三角形)
        /// 定理：任意多边形的面积可由任意一点与多边形上依次两点连线构成的三角形矢量面积求和得出。矢量面积=三角形两边矢量的叉乘。
        /// </summary>
        /// <param name="vectorPoints"></param>
        /// <returns></returns>
        public static float CalculateArea(List<PointF> vectorPoints)
        {
            int index, count;
            index = 0;
            float area = 0;
            count = vectorPoints.Count;

            for (index = 0; index < count; index++)
            {
                area = area + (vectorPoints[index].X * vectorPoints[(index + 1) % count].Y - vectorPoints[(index + 1) % count].X * vectorPoints[index].Y);
            }

            return (float)Math.Abs(0.5 * area);
        }
        #endregion

        #region IsInPolygon
        /// <summary>
        /// 判断点是否在多边形内.
        /// ----------原理----------
        /// 注意到如果从P作水平向左的射线的话，如果P在多边形内部，那么这条射线与多边形的交点必为奇数，
        /// 如果P在多边形外部，则交点个数必为偶数(0也在内)。
        /// </summary>
        /// <param name="checkPoint">要判断的点</param>
        /// <param name="polygonPoints">多边形的顶点</param>
        /// <returns></returns>
        public static bool IsInPolygon(PointF checkPoint, List<PointF> polygonPoints)
        {
            bool inside = false;
            int pointCount = polygonPoints.Count;
            PointF p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)
            {//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if (checkPoint.Y < p2.Y)
                {//p2在射线之上
                    if (p1.Y <= checkPoint.Y)
                    {//p1正好在射线中或者射线下方
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                        //斜率判断,checkPoint在P1和P2之间且在P1P2右侧
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。
                            //由于inside初始值为false，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，此时为inside=(!inside)
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为inside=(!inside)
                            inside = (!inside);
                        }
                    }
                }
                else if (checkPoint.Y < p1.Y)
                {
                    //p2正好在射线中或者在射线下方，p1在射线上
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                    //斜率判断,checkPoint在P1和P2之间且在P1P2右侧
                    {
                        inside = (!inside);
                    }
                }
            }
            return inside;
        }
        #endregion

        #region IsEvenIntersectionOfVertexYUpRayToEdges Function
        /// <summary>
        /// 判断多边形顶点的垂直向上射线与多边形的边是否有偶数个交点
        /// </summary>
        /// <param name="checkPoint"></param>
        /// <param name="polygonPoints"></param>
        /// <returns></returns>
        public static bool IsEvenIntersectionOfVertexYUpRayToEdges(PointF checkPoint, List<PointF> polygonPoints)
        {
            bool isEvenIntersection = true;//零个交点
            int pointCount = polygonPoints.Count;
            PointF p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)
            {//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if (checkPoint.X < p2.X)
                {//p2在垂直射线左侧
                    if (p1.X <= checkPoint.X)
                    {//p1正好在射线中或者射线左侧
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                        //斜率判断,checkPoint在P1和P2之间且在P1P2下方
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。
                            //由于isEvenIntersection初始值为true，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，
                            //此时为isEvenIntersection=(!isEvenIntersection)
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为isEvenIntersection=(!isEvenIntersection)
                            isEvenIntersection = (!isEvenIntersection);
                        }
                    }
                }
                else if (checkPoint.X < p1.X)
                {
                    //p2正好在射线中或者在射线右侧，p1在射线上
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                    //斜率判断,checkPoint在P1和P2之间且在P1P2下方
                    {
                        isEvenIntersection = (!isEvenIntersection);
                    }
                }
            }
            return isEvenIntersection;
        }
        #endregion

        #region IsEvenIntersectionOfVertexXLeftRayToEdges Function
        /// <summary>
        /// 判断多边形顶点的水平向左射线与多边形的边是否有偶数个交点
        /// </summary>
        /// <param name="checkPoint"></param>
        /// <param name="polygonPoints"></param>
        /// <returns></returns>
        public static bool IsEvenIntersectionOfVertexXLeftRayToEdges(PointF checkPoint, List<PointF> polygonPoints,
                           PointF[] filterLinePoints)
        {
            bool isEvenIntersection = true;//零个交点
            int pointCount = polygonPoints.Count;
            PointF p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)
            {//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if ((filterLinePoints[0] == p1 && filterLinePoints[1] == p2) || (filterLinePoints[0] == p2 && filterLinePoints[1] == p1))
                {
                    continue;
                }
                if (checkPoint.Y < p2.Y)
                {//p2在水平射线上方
                    if (p1.Y <= checkPoint.Y)
                    {//p1正好在射线中或者射线下方
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                        //斜率判断,checkPoint在P1和P2之间且在P1P2右侧
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。
                            //由于isEvenIntersection初始值为true，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，
                            //此时为isEvenIntersection=(!isEvenIntersection)
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为isEvenIntersection=(!isEvenIntersection)
                            isEvenIntersection = (!isEvenIntersection);
                        }
                    }
                }
                else if (checkPoint.Y < p1.Y)
                {
                    //p2正好在射线中或者在射线下方
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                    //斜率判断,checkPoint在P1和P2之间且在P1P2下方
                    {
                        isEvenIntersection = (!isEvenIntersection);
                    }
                }
            }
            return isEvenIntersection;
        }
        #endregion

        #region IsEvenIntersectionOfVertexXRightRayToEdges Function
        /// <summary>
        /// 判断多边形顶点的水平向右射线与多边形的边是否有偶数个交点
        /// </summary>
        /// <param name="checkPoint"></param>
        /// <param name="polygonPoints"></param>
        /// <returns></returns>
        public static bool IsEvenIntersectionOfVertexXRightRayToEdges(PointF checkPoint, List<PointF> polygonPoints, PointF[] filterLinePoints)
        {
            bool isEvenIntersection = true;//零个交点
            int pointCount = polygonPoints.Count;
            PointF p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)
            {//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if ((filterLinePoints[0] == p1 && filterLinePoints[1] == p2) || (filterLinePoints[0] == p2 && filterLinePoints[1] == p1))
                {
                    continue;
                }
                if (checkPoint.Y < p2.Y)
                {//p2在水平射线上方
                    if (p1.Y <= checkPoint.Y)
                    {//p1正好在射线中或者射线下方
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                        //斜率判断,checkPoint在P1和P2之间且在P1P2右侧
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。
                            //由于isEvenIntersection初始值为true，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，
                            //此时为isEvenIntersection=(!isEvenIntersection)
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为isEvenIntersection=(!isEvenIntersection)
                            isEvenIntersection = (!isEvenIntersection);
                        }
                    }
                }
                else if (checkPoint.Y < p1.Y)
                {
                    //p2正好在射线中或者在射线下方
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))
                    //斜率判断,checkPoint在P1和P2之间且在P1P2下方
                    {
                        isEvenIntersection = (!isEvenIntersection);
                    }
                }
            }
            return isEvenIntersection;
        }
        #endregion
    }

    #region PolygonType
    /// <summary>
    /// 多边形的类型
    /// </summary>
    public enum PolygonType
    {
        /// <summary>
        /// 无.不可计算的多边形(比如多点共线)
        /// </summary>
        None,

        /// <summary>
        /// 凸多边形
        /// </summary>
        Convex,

        /// <summary>
        /// 凹多边形
        /// </summary>
        Concave

    }
    #endregion

    #region ClockDirection
    /// <summary>
    /// 时钟方向
    /// </summary>
    public enum ClockDirection
    {
        /// <summary>
        /// 无.可能是不可计算的图形，比如多点共线
        /// </summary>
        None,

        /// <summary>
        /// 顺时针方向
        /// </summary>
        Clockwise,

        /// <summary>
        /// 逆时针方向
        /// </summary>
        Counterclockwise
    }
    #endregion
}
