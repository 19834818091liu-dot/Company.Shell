using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Algorithm.Unwrapper
{
    /// <summary>
    /// 调用C++方法的类
    /// </summary>
    public static class CppMethod
    {
        private const string dll = "Company.Algorithm.Wrapper.dll";
        [DllImport(dll,EntryPoint ="#999",CallingConvention =CallingConvention.Cdecl)]
        public static extern int TestGetNumber(int number);
        //[DllImport(dll,EntryPoint ="#1",CallingConvention =CallingConvention.Cdecl)]

    }
}
