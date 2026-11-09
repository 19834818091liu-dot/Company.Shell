using Company.Application.Share.Config;
using Company.Application.Share.Init;
using Company.Application.Share.Run;
using Company.Hardware.Camera.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.Application.Config.Services
{
    public class CalibrationManager : ICalibrationManager
    {
        private IPhotoManager PhotoManager { get; set; }

        private IHardwareManager HardwareManager { get; set; }

        private ISystemConfigManager SystemConfigManager { get; set; }
        public CalibrationManager(IPhotoManager photoManager, IHardwareManager hardwareManager, ISystemConfigManager systemConfigManager)
        {
            PhotoManager = photoManager;
            HardwareManager = hardwareManager;
            SystemConfigManager = systemConfigManager;
        }
        public async Task<double> CalibrateCameraAsync(CameraType cameraType, Rect rect)
        {
            double result = 0;
            Task task;
            if(cameraType == CameraType.left)
            {
                task = Task.Run(() =>
                {
                    //todo 标定左相机
                });
               

            }
            else
            {
                task = Task.Run(() =>
                {
                    //todo 标定右相机
                });
            }
            return result;
        }
    }
}
