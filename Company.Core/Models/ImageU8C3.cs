using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Models
{
    /// <summary>
    /// 三通道8位位图
    /// </summary>
    public struct ImageU8C3
    {
        public int Width;
        public int Height;

        public IntPtr IntPtr;
        public ImageU8C3(int width, int height, IntPtr intPtr)
        {
            Width = width;
            Height = height;
            IntPtr = intPtr;
        }
        /// <summary>
        /// 创建一张三通道8位位图
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ImageU8C3 Create(int width, int height)
        {
            return new ImageU8C3(width, height, Marshal.AllocHGlobal(width * height*3));
        }
    }
}
