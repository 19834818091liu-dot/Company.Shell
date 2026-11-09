using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Base
{
    public enum StopMode
    {
        取消当前运动,
        取消缓冲的运动,
        取消当前和缓冲的运动,
        立即中断脉冲发送
    }
}
