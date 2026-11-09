using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Core.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// 将json文件反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Load<T>(string json)
        {
            if(!File.Exists(json))
            {
                return default(T);
            }
            string content = File.ReadAllText(json);
            var result = Deserialize<T>(content);
            return result;
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);   
        }
        /// <summary>
        /// 序列化为json文件
        /// </summary>
        public static void Write(object obj,string path,bool indented = false)
        {
            string contents = Serialize(obj,indented);
            File.WriteAllText(path,contents);

        }
        public static string Serialize(object obj,bool indented = false)
        {
            Formatting formatting = indented ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(obj, formatting);
        }
    }
}
