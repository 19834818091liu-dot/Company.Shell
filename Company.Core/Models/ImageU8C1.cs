using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Models
{
    /// <summary>
    /// 单通道8位位图,这里是结构体，是值类型，相当于复制数据，而如果是class的话就相当于是共享同一个对象
    /// </summary>
    public struct ImageU8C1
    {
        public int Width;
        public int Height;

        public IntPtr IntPtr;
        public ImageU8C1(int width,int height,IntPtr intPtr)
        {
            Width = width;
            Height = height;
            IntPtr = intPtr;
        }
        /// <summary>
        /// 创建一张单通道8位位图
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ImageU8C1 Create(int width,int height)
        {
            return new ImageU8C1(width, height, Marshal.AllocHGlobal(width * height));
        }
    }
}
