using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Config
{
    /// <summary>
    /// 序列化和反序列化
    /// </summary>
    public interface ISystemConfig
    {
        T Read<T>(ValueType key);

        void Write<T>(ValueType key,T value);
    }
}
