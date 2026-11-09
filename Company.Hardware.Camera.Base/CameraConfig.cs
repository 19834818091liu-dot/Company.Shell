using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Camera.Base
{
    public class CameraConfig
    {
        public Cameraparameter Cameraparameter {  get; set; } = new Cameraparameter();
        public string IpAddress { get; set; }

        public int Port { get; set; }

        public string direction { get; set; } 
    }
}
