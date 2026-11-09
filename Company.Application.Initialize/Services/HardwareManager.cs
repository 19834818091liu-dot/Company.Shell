using Company.Application.Share.Config;
using Company.Application.Share.Init;
using Company.Application.Share.Models;
using Company.Hardware.Camera.Base;
using Company.Hardware.ControlCard.Base;
using Prism.Events;
using Prism.Ioc;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Initialize.Services
{
    public class HardwareManager :ReactiveObject ,IHardwareManager
    {
        [Reactive]
        public bool Initialized { get; set; }
        [Reactive]
        public string Message { get; set; }

        public ICamera leftCamera { get; set; }

        public ICamera rightCamera { get; set; }
        public IControlCard ControlCard { get; set; }
        public ISystemConfigManager SystemConfigManager { get; set; }

        private IEventAggregator EventAggregator { get; set; }

        public HardwareManager(IControlCard controlCard ,IEventAggregator eventAggregator,ISystemConfigManager systemConfigManager ,IContainerProvider containerProvider)
        {
            ControlCard = controlCard;
            EventAggregator = eventAggregator;  
             SystemConfigManager = systemConfigManager;

            leftCamera = containerProvider.Resolve<ICamera>(CameraType.left.ToString());       //从容器中将左相机取出来
            rightCamera = containerProvider.Resolve<ICamera>(CameraType.right.ToString());
        }
        /// <summary>
        /// 初始化硬件
        /// </summary>
        /// <returns></returns>
        public async Task<InitResult> InitAsync()
        {
            if(Initialized) { throw new Exception("重复初始化硬件"); }
            InitResult result = new InitResult();
            result.Message = "初始化硬件失败";
            result.Success = false;

            await Task.Delay(500);    //模拟耗时

            //todo 初始化硬件
            var leftCameraTask = Task.Run(() => leftCamera.Init(SystemConfigManager.leftCameraConfig));
            var rightCameraTask = Task.Run(() => rightCamera.Init(SystemConfigManager.rightCameraConfig));
            var controlcardTask = Task.Run(() => ControlCard.Init(SystemConfigManager.ControlCardConfig));  //这里是为了模拟控制卡的初始化，实际使用中需要替换为控制卡的初始化方法
            var tasks =await Task.WhenAll(leftCameraTask, rightCameraTask, controlcardTask);
            bool temp = tasks.All(p => p);   //这里就是为了判断每一个线程是否都为真
            if (temp)
            {
                Initialized = true;
                result.Success = true;
                result.Message = "初始化硬件成功";
                EventAggregator.GetEvent<InitSuccessEvent>().Publish();  //初始化成功之后发布成功事件
                return result;
            }
            else
            {
                string msg = "以下硬件模块加载失败:\r\n";
                if (leftCameraTask.Result == false)
                {
                    msg += "左相机模块";
                }
                if (rightCameraTask.Result == false)
                {
                    msg += "右相机模块";
                }
                if (controlcardTask.Result == false)
                {
                    msg += "控制卡模块";
                }
                result.Message = msg;
                result.Success = false;
                return result;
            }
        }
    }
}
