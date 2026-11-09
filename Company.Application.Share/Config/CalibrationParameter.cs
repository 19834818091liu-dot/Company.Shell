using Company.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Config
{
    public class CalibrationParameter:ObservableObject
    {
        /// <summary>
        /// 左相机像素与毫米当量
        /// </summary>
        public double LeftPixelToMillimeter { get; set; }
        /// <summary>
        /// 右相机像素与毫米当量
        /// </summary>
        public double RightPixelToMillimeter { get; set; }

        /// <summary>
        /// 左剪切位距离
        /// </summary>
        public double LeftCutmm { get; set; }
        /// <summary>
        /// 右剪切位距离
        /// </summary>
        public double RightCutmm { get; set; }
        /// <summary>
        /// 左剪切位偏移量
        /// </summary>
        public double LeftCutOffsetmm { get; set; }
        /// <summary>
        /// 右剪切位偏移量
        /// </summary>
        public double RightCutOffsetmm { get; set; }
    }
}
