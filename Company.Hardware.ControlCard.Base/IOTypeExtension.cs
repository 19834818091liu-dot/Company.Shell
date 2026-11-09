using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Base
{
    public static class IOTypeExtension
    {
        /// <summary>
        /// 获取IO点枚举项的下标值,IN0要的就是0
        /// </summary>
        /// <param name="iOType"></param>
        /// <returns></returns>
        public static int GetIndex(this IOType iOType)
        {
            var array = Enum.GetValues(iOType.GetType()) as IList;
            int index = array.IndexOf(iOType);     //获取下标索引值   
            return index;
        }
    }
}
