using Company.Application.Share.Config;
using Company.Hardware.Camera.Base;
using Company.Hardware.ControlCard.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Config.Models
{
    /// <summary>
    /// 系统配置管理类，最终要将这个对象进行序列化和反序列化操作
    /// </summary>
    public class SystemConfigModel
    {
        public SoftwareConfig SoftwareConfig { get; set; } = new SoftwareConfig();
        /// <summary>
        /// 左相机配置参数
        /// </summary>
        public CameraConfig leftCameraConfig { get; set; } = new CameraConfig()
        {
            IpAddress = "192.168.1.0",
            direction = CameraType.left.ToString(),
        };
        /// <summary>
        /// 右相机配置参数
        /// </summary>
        public CameraConfig rightCameraConfig { get; set; } = new CameraConfig()
        {
            IpAddress = "192.168.1.0",
            direction = CameraType.right.ToString(),
        };
        /// <summary>
        /// 控制卡参数
        /// </summary>
        public ControlCardConfig ControlCardConfig { get; set; } = new ControlCardConfig();
        public XAxisParameter XAxisParameter { get; set; } = new XAxisParameter();
        public YAxisParameter YAxisParameter { get;set; } = new YAxisParameter();

        public ZAxisParameter ZAxisParameter { get; set; } = new ZAxisParameter();

        public MainAxisParameter MainAxisParameter { get; set; } = new MainAxisParameter();

        public KnifeAxisParameter KnifeAxisParameter { get; set; } = new KnifeAxisParameter();

        public TrackAxisParameter TrackAxisParameter { get; set; } = new TrackAxisParameter();

        public CalibrationParameter CalibrationParameter { get; set; } = new CalibrationParameter();    //左右棘轮轴标定参数

        
    }
}
