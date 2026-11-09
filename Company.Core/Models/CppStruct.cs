using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Models
{
    public class CppStruct
    {
       
        public struct CppRectangle
        {
            public double X;
            public double Y;
            public double Width;
            public double Height;
        };
        public struct CppSetting
        {
            /// <summary>
            /// 图片TOP向下偏移ROI的像素值
            /// </summary>
            public int OffSetup;
            /// <summary>
            /// 图片BOTTOM向上偏移ROI的像素值
            /// </summary>
            public int OffSetdown;
            /// <summary>
            /// 是否保存中间结果，0表示不保存，1表示保存
            /// </summary>
            public  int Output;
            /// <summary>
            /// 元件的二值化阈值
            /// </summary>
            public int threshold_element;
            /// <summary>
            /// 齿轮孔的二值化阈值
            /// </summary>
            public int threshold_circle;
        };
    }
}
