using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Base
{
    public class IOEntity : ReactiveObject
    {
        //输入点
        /// <summary>
        /// 左进料感应点
        /// </summary>
        [Reactive]
        public bool IN0 { get; set; }
        /// <summary>
        /// 右进料感应点
        /// </summary>
        [Reactive]
        public bool IN1 { get; set; }
        /// <summary>
        /// 急停感应点
        /// </summary>
        [Reactive]
        public bool IN2 { get; set; }
        [Reactive]
        public bool IN3 { get; set; }

        [Reactive]
        public bool IN4 { get; set; }
        [Reactive]
        public bool IN5 { get; set; }
        [Reactive]
        public bool IN6 { get; set; }
        [Reactive]
        public bool IN7 { get; set; }
        [Reactive]
        public bool IN8 { get; set; }
        [Reactive]
        public bool IN9 { get; set; }
        [Reactive]
        public bool IN10 { get; set; }
        [Reactive]
        public bool IN11 { get; set; }
        [Reactive]
        public bool IN12 { get; set; }
        [Reactive]
        public bool IN13 { get; set; }
        [Reactive]
        public bool IN14 { get; set; }
        [Reactive]
        public bool IN15 { get; set; }
        [Reactive]
        public bool IN16 { get; set; }
        [Reactive]
        public bool IN17 { get; set; }
        [Reactive]
        public bool IN18 { get; set; }
        [Reactive]
        public bool IN19 { get; set; }
        [Reactive]
        public bool IN20 { get; set; }
        [Reactive]
        public bool IN21 { get; set; }
        [Reactive]
        public bool IN22 { get; set; }
        [Reactive]
        public bool IN23 { get; set; }


        //OUT输出点
        [Reactive]
        public bool OUT0 { get; set; } //X轴
        [Reactive]
        public bool OUT1 { get; set; } //Y轴
        [Reactive]
        public bool OUT2 { get; set; } //Z轴
        [Reactive]
        public bool OUT3 { get; set; }
        [Reactive]
        public bool OUT4 { get; set; }
        [Reactive]
        public bool OUT5 { get; set; }
        [Reactive]
        public bool OUT6 { get; set; }
        [Reactive]
        public bool OUT7 { get; set; }//左轴
        [Reactive]
        public bool OUT8 { get; set; }//右轴
        [Reactive]
        public bool OUT9 { get; set; }
        [Reactive]
        public bool OUT10 { get; set; }
        [Reactive]
        public bool OUT11 { get; set; }
        [Reactive]
        public bool OUT12 { get; set; }
        [Reactive]
        public bool OUT13 { get; set; }
        [Reactive]
        public bool OUT14 { get; set; }
        [Reactive]
        public bool OUT15 { get; set; }
        [Reactive]
        public bool OUT16 { get; set; }
        [Reactive]
        public bool OUT17 { get; set; }
        [Reactive]
        public bool OUT18 { get; set; }
        [Reactive]
        public bool OUT19 { get; set; }
        [Reactive]
        public bool OUT20 { get; set; }
        [Reactive]
        public bool OUT21 { get; set; }
        [Reactive]
        public bool OUT22 { get; set; }
        [Reactive]
        public bool OUT23 { get; set; }

    }
}
