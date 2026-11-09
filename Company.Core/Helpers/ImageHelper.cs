using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Helpers
{
    /// <summary>
    /// 图片帮助类，将文件中读取到的内容转为图片类
    /// </summary>
    public static class ImageHelper
    {
        public static Bitmap Load(string filename)
        {
            byte[] contents = FileHelper.Load(filename);
            using (MemoryStream ms = new MemoryStream(contents))
            {
                return Image.FromStream(ms) as Bitmap;
            }
        }
    }
}
