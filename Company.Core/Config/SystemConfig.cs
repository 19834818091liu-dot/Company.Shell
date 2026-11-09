using Company.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Config
{
    public class SystemConfig : ISystemConfig
    {
        public static bool HasHardware = false;
        private const string root = "Config";

        public string GetFullPath(ValueType key)
        {
            return Path.Combine(root, key.GetType().FullName + "." + key.ToString() + ".json");
        }
        /// <summary>
        /// 读取本地json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Read<T>(ValueType key)
        {
            string fileName = GetFullPath(key);
            return JsonHelper.Load<T>(fileName);
        }
        /// <summary>
        /// 写入json文件到本地
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write<T>(ValueType key, T value)
        {
            Directory.CreateDirectory(root);
            string fileName = GetFullPath(key);
            JsonHelper.Write(value, fileName, true);
        }
    }
}
