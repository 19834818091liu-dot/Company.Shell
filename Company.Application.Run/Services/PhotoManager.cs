using Company.Application.Share.Init;
using Company.Application.Share.Run;
using Company.Core.Models;
using Company.Hardware.Camera.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Run.Services
{
    class PhotoManager :ReactiveObject, IPhotoManager
    {
        [Reactive]
        public BitmapGDI Currentphoto { get; set; }

        public BitmapGDI LeftPhoto { get; set; }

        public BitmapGDI RightPhoto { get; set; }

        public ICamera Camera { get; set; }
        private IHardwareManager HardwareManager { get; set; }
        public PhotoManager(IHardwareManager hardwareManager)
        {
            HardwareManager = hardwareManager;
            LeftPhoto = new BitmapGDI(HardwareManager.leftCamera.Width,HardwareManager.leftCamera.Height);
            RightPhoto = new BitmapGDI(HardwareManager.rightCamera.Width, HardwareManager.rightCamera.Height);
            Currentphoto = LeftPhoto;         //当前相机图片默认为左相机图片
            Camera = HardwareManager.leftCamera;      //当前相机默认为左相机
            HardwareManager.leftCamera.OnGrabbed += LeftCamera_OnGrabbed;
            HardwareManager.leftCamera.ImageU8C1Observable.ObserveOn(RxApp.MainThreadScheduler).Subscribe(LeftCamera_OnGrabbed);

            HardwareManager.rightCamera.OnGrabbed += RightCamera_OnGrabbed;
            HardwareManager.rightCamera.ImageU8C1Observable.ObserveOn(RxApp.MainThreadScheduler).Subscribe(RightCamera_OnGrabbed);
        }

        private void RightCamera_OnGrabbed(ImageU8C1 photo)
        {
            RightPhoto.WritePixels(photo, 0, 0);
        }

        private void LeftCamera_OnGrabbed(ImageU8C1 photo)
        {
            LeftPhoto.WritePixels(photo,0,0);
        }

        public void SetCurrentPhoto(CameraType cameraType)
        {
            switch (cameraType)
            {
                case CameraType.left:
                    Currentphoto = LeftPhoto;
                    Camera = HardwareManager.leftCamera;
                    break;
                case CameraType.right:
                    Currentphoto = RightPhoto;
                    Camera = HardwareManager.rightCamera;
                    break;
                default:
                    break;
            }
        }
    }
}
