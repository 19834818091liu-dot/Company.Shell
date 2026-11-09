using Company.Hardware.Camera.Base;
using Company.Hardware.ControlCard.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Company.Application.Share.Config
{
    
    public interface ISystemConfigManager
    {
        void Save();
        SoftwareConfig SoftwareConfig { get; }
        /// <summary>
        /// 左相机配置参数
        /// </summary>
        CameraConfig leftCameraConfig { get; }

        CameraConfig rightCameraConfig { get;}

        ControlCardConfig ControlCardConfig { get; }
        XAxisParameter XAxisParameter { get; }
        YAxisParameter YAxisParameter { get; }

        ZAxisParameter ZAxisParameter{ get; }
        MainAxisParameter MainAxisParameter { get; }

        KnifeAxisParameter KnifeAxisParameter { get; }
         TrackAxisParameter TrackAxisParameter { get; }
        CalibrationParameter CalibrationParameter { get; }

    }
}
