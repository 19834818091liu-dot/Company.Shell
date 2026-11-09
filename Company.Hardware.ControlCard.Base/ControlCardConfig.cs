using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Base
{
    public class ControlCardConfig
    {
        public ControlCardParameter ControlCardParameter { get; set; } = new ControlCardParameter();
        public string IpAddress { get; set; } = "192.168.0.001";

        public int Port { get; set; } = 500;
    }
}
