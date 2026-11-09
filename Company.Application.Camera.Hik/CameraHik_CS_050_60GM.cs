using Company.Core.Helpers;
using Company.Core.Models;
using Company.Hardware.Camera.Base;
using Company.Logger;
using MvCamCtrl.NET;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Application.Camera.Hik
{
    public class CameraHik_CS_050_60GM : ReactiveObject, ICamera
    {
        private MyCamera MyCamera { get; }
        private MyCamera.MV_CC_DEVICE_INFO MV_CC_DEVICE_INFO;     //完整设备信息
        private MyCamera.cbOutputExdelegate MyCamerahandler;       //图像回调委托
        private CameraConfig CameraConfig;
        private IntPtr IntPtr = IntPtr.Zero;
        private AutoResetEvent AutoResetEvent = new AutoResetEvent(false);
        public ImageU8C1 ImageU8C1 { get; set; }

        public IObservable<ImageU8C1> ImageU8C1Observable { get; set; }

        public ImageU8C3 ImageU8C3 { get; set; }

        public IObservable<ImageU8C3> ImageU8C3Observable { get;set; }

        public int Width { get; set; } = 2448;
        public int Height { get; set; } = 2048;
        public bool Initialized { get; set; }
        public bool Connected { get; set; }
        public string Message { get; set ; }
        public event Action<ImageU8C1> OnGrabbed;
        public CameraHik_CS_050_60GM()
        {
            ImageU8C1Observable = this.WhenAnyValue(t => t.ImageU8C1);
            ImageU8C3Observable = this.WhenAnyValue(t => t.ImageU8C3);
        }

        public void Close()
        {
            try
            {
                int result = MyCamera.MV_CC_CloseDevice_NET();
                if (result != MyCamera.MV_OK)
                {
                    logs.LogError($"IP为{CameraConfig.IpAddress}的海康相机关闭失败");
                }
                else
                {
                    logs.LogInfo($"IP为{CameraConfig.IpAddress}的海康相机关闭成功");
                }
            }
            catch (Exception ex)
            {

                logs.LogError(ex.ToString());
            }
        }

        public float GetExplosureTime()
        {
            throw new NotImplementedException();
        }

        public float GetGain()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 初始化海康相机
        /// </summary>
        /// <param name="cameraConfig"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Init(CameraConfig cameraConfig)
        {
            try
            {
                CameraConfig = cameraConfig;
                if(Initialized)
                {
                    return true;
                }
                MyCamera.MV_CC_DEVICE_INFO_LIST devices = new MyCamera.MV_CC_DEVICE_INFO_LIST();  //相机列表
                var result =  MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref devices);  //枚举海康相机列表
                if(result != MyCamera.MV_OK)
                {
                    logs.LogError($"枚举海康相机失败，错误码为{result}");
                }
                if(devices.nDeviceNum == 0)
                {
                    logs.LogError($"没有发现海康相机");
                }
                bool isFind = false;
                //遍历列表里的所有相机
                for(int i = 0; i < devices.nDeviceNum; i++)
                {
                    MV_CC_DEVICE_INFO = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(devices.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if(MV_CC_DEVICE_INFO.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        var info = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(MV_CC_DEVICE_INFO.SpecialInfo.stGigEInfo,typeof(MyCamera.MV_CC_DEVICE_INFO));
                        uint ip1 = info.nCurrentIp & 0xff000000 >> 24;
                        uint ip2 = info.nCurrentIp & 0x00ff0000 >> 16;
                        uint ip3 = info.nCurrentIp & 0x0000ff00 >> 8;
                        uint ip4 = info.nCurrentIp & 0x000000ff;
                        var ip = $"{ip1}.{ip2}.{ip3}.{ip4}";
                        if(ip != CameraConfig.IpAddress)
                        {
                            logs.LogError($"相机地址{ip}与我的海康相机地址{CameraConfig.IpAddress}不一致");
                            isFind = false ;
                        }
                        else
                        {
                            isFind = true ;
                            break ;
                        }
                    }
                }

                //创建设备
                result = MyCamera.MV_CC_CreateDevice_NET(ref MV_CC_DEVICE_INFO);
                if(result != MyCamera.MV_OK)
                {
                    logs.LogError($"创建IP为{CameraConfig.IpAddress}的海康相机失败");
                }
                else
                {
                    logs.LogInfo($"创建IP为{CameraConfig.IpAddress}的海康相机成功");
                }
                //打开相机
                result = MyCamera.MV_CC_OpenDevice_NET();
                if (result != MyCamera.MV_OK)
                {
                    logs.LogError($"打开IP为{CameraConfig.IpAddress}的海康相机失败");
                }
                else
                {
                    logs.LogInfo($"打开IP为{CameraConfig.IpAddress}的海康相机成功");
                }
                //开启触发模式
                result = MyCamera.MV_CC_SetEnumValue_NET("TriggerMode",(uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                if (result != MyCamera.MV_OK)
                {
                    logs.LogError($"设置IP为{CameraConfig.IpAddress}的海康相机触发模式失败");
                    return false;
                }
                result = MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                if (result != MyCamera.MV_OK)
                {
                    logs.LogError($"设置IP为{CameraConfig.IpAddress}的海康相机软触发模式失败");
                    return false;
                }

                //设置拉流策略为最新一张图片
                MyCamera.MV_CC_SetGrabStrategy_NET(MyCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImages);

                //注册回调
                MyCamerahandler = new MyCamera.cbOutputExdelegate(OnGrab);
                result = MyCamera.MV_CC_RegisterImageCallBackEx_NET(MyCamerahandler,IntPtr);
                if (result != MyCamera.MV_OK)
                {
                    logs.LogError($"IP为{CameraConfig.IpAddress}的海康相机注册回调失败");
                }

                //开始抓图
                result = MyCamera.MV_CC_StartGrabbing_NET();
                if (result != MyCamera.MV_OK)
                {
                    logs.LogError($"IP为{CameraConfig.IpAddress}的海康相机开启抓图失败");
                }
                Initialized = true;
                Connected = true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
                return false;
            }
           return true ;
        }
        /// <summary>
        /// 相机回调函数
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="pUser"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnGrab(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            try
            {
                if(IntPtr == IntPtr.Zero)
                {
                    IntPtr = Marshal.AllocHGlobal(new IntPtr(Width*Height*1));        //从进程的非托管内存中分配一个内存空间
                }
                MemoryHelper.CopyMemory(IntPtr,pData,Width*Height);       //将收到的指针pData拷贝到非托管内存IntPtr中
                ImageU8C1 = new ImageU8C1(Width,Height,IntPtr);
                OnGrabbed ?.Invoke(ImageU8C1);     //把图片抛出去，这样UI层就可以绑定了
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            finally
            {
                AutoResetEvent.Set();          //释放信号 
            }
        }

        public bool SetExplosureTime(float value)
        {
            try
            {
                int result = MyCamera.MV_CC_SetFloatValue_NET("ExplosureTime",value);
                if (result != MyCamera.MV_OK)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logs.LogError(ex.ToString());
                return false;
            }
            return true;
        }

        public bool SetGain(float value)
        {
            try
            {
                int result = MyCamera.MV_CC_SetFloatValue_NET("Gain", value);
                if (result != MyCamera.MV_OK)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logs.LogError(ex.ToString());
                return false;
            }
            return true;
        }

        public bool Trigger()
        {
                Stopwatch stopwatch = Stopwatch.StartNew();
                int result = MyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (result != MyCamera.MV_OK)
                {
                    logs.LogError($"IP为{CameraConfig.IpAddress}的海康相机抓图失败");
                    return false;
                }
                var r = AutoResetEvent.WaitOne(1000);    
                if(!r)
                {
                    logs.LogError($"IP为{CameraConfig.IpAddress}的海康相机回调超时，错误码：{result}");
                }        
                return r;
        }
    }
}
