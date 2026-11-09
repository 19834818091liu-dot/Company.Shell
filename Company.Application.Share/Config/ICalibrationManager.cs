using Company.Hardware.Camera.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.Application.Share.Config
{
    public interface ICalibrationManager
    {
        /// <summary>
        /// 相机标定    
        /// </summary>
        /// <param name="cameraType"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        Task<double> CalibrateCameraAsync(CameraType cameraType, Rect rect);
       
    }
}
