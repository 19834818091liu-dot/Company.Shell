using Company.Application.Config.Models;
using Company.Application.Share.Config;
using Company.Core.Config;
using Company.Core.Enums;
using Company.Hardware.Camera.Base;
using Company.Hardware.ControlCard.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Config.Services
{
    class SystemConfigManager : ISystemConfigManager
    {
        private SystemConfigModel SystemConfigModel { get; set; }
        public SoftwareConfig SoftwareConfig => SystemConfigModel.SoftwareConfig;
        public CameraConfig leftCameraConfig => SystemConfigModel.leftCameraConfig;
        public CameraConfig rightCameraConfig => SystemConfigModel.rightCameraConfig;
        private ISystemConfig SystemConfig {  get; set; }     //里面封装了序列化和反序列化

        public ControlCardConfig ControlCardConfig => SystemConfigModel.ControlCardConfig;

        public XAxisParameter XAxisParameter => SystemConfigModel.XAxisParameter;

        public YAxisParameter YAxisParameter => SystemConfigModel.YAxisParameter;

        public ZAxisParameter ZAxisParameter => SystemConfigModel.ZAxisParameter;

        public MainAxisParameter MainAxisParameter => SystemConfigModel.MainAxisParameter;

        public KnifeAxisParameter KnifeAxisParameter => SystemConfigModel.KnifeAxisParameter;

        public TrackAxisParameter TrackAxisParameter => SystemConfigModel.TrackAxisParameter;

        public CalibrationParameter CalibrationParameter => SystemConfigModel.CalibrationParameter;

   

        public SystemConfigManager(ISystemConfig systemConfig)
        {
            SystemConfig = systemConfig;
            Load();
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Load()
        {
            SystemConfigModel = SystemConfig.Read<SystemConfigModel>(ConfigKey.SystemConfig);
            if(SystemConfigModel == null)
            {
                SystemConfigModel = new SystemConfigModel();
                SystemConfig.Write(ConfigKey.SystemConfig, SystemConfigModel);
            }
        }

        public void Save()
        {
            SystemConfig.Write(ConfigKey.SystemConfig, SystemConfigModel);
        }
    }
}
