using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Helpers
{
    /// <summary>
    /// 文件帮助类，读取文件
    /// </summary>
    public static class FileHelper
    {
        public static byte[] Load(string path)
        {
            if(File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return null;
        }
    }
}
