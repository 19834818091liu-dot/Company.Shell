using Company.Hardware.Camera.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Init
{
    /// <summary>
    /// 硬件加载及管理类接口
    /// </summary>
    public interface IHardwareManager
    {
        ICamera leftCamera { get; set; }
        ICamera rightCamera { get; set; }
        bool Initialized { get; set; }

        Task<InitResult> InitAsync();

    }
}
