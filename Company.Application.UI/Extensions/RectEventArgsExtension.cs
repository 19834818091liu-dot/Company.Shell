using Company.Application.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.Application.UI.Extensions
{
    public static class RectEventArgsExtension
    {
        /// <summary>
        /// 根据选框计算图像中的实际选择区域
        /// </summary>
        /// <param name="args"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Rect GetSelectRect(this RectEventArgs args,int width,int height)
        {
            var scale = 1.0 / args.Zoom;
            //计算缩放
            Rect selectRect = args.SelectRect;    //获取选框矩形
            Rect rect1 = new Rect();
            rect1.X = selectRect.X;
            rect1.Y = selectRect.Y;
            rect1.Width = selectRect.Width;
            rect1.Height = selectRect.Height;
            Rect imageRect = new Rect(args.X, args.Y, width, height);     //获取图像矩形
            Rect rect2 = new Rect();
            rect2.X = imageRect.X;
            rect2.Y = imageRect.Y;
            rect2.Width = imageRect.Width * args.Zoom;
            rect2.Height = imageRect.Height * args.Zoom;

            //计算交集矩形左上角和右下角的坐标
            double left = Math.Max(rect1.X, rect2.X);
            double top = Math.Max(rect1.Y, rect2.Y);
            double right = Math.Min(rect1.X + rect1.Width, rect2.X + rect2.Width);
            double bottom = Math.Min(rect1.Y + rect1.Height, rect2.Y + rect2.Height);
            //说明有真实的选择区域，选框在图像区域内
            if (right > left && bottom > top)
            {
                //计算平移
                Rect rect3 = new Rect(left, top, right - left, bottom - top);    //计算平移后的矩形 
                rect3.X -= rect2.X;
                rect3.Y -= rect2.Y;
                rect3.X *= scale;
                rect3.Y *= scale;
                rect3.Width *= scale;
                rect3.Height *= scale;
                rect3.X = Math.Round(rect3.X);
                rect3.Y = Math.Round(rect3.Y);
                rect3.Width = Math.Round(rect3.Width);
                rect3.Height = Math.Round(rect3.Height);
                return rect3;
            }
            else
            {
                //如果只是在图像区域点击了鼠标，则返回该点的坐标
                Rect rect3 = new Rect(left, top, 0, 0);
                rect3.X -= rect2.X;
                rect3.Y -= rect2.Y;
                rect3.X *= scale;
                rect3.Y *= scale;
                rect3.Width *= scale;
                rect3.Height *= scale;
                rect3.X = Math.Round(rect3.X);
                rect3.Y = Math.Round(rect3.Y);
                rect3.Width = Math.Round(rect3.Width);
                rect3.Height = Math.Round(rect3.Height);
                return rect3;
            }
        }
    }
}
