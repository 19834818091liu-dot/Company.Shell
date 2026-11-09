using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Config
{
    public class SoftwareConfig
    {
        public string Title { get; set; } = "视觉对位运动控制系统";
        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }

        public float AxisMoveStepMM { get; set; } = 1f;
    }
}
