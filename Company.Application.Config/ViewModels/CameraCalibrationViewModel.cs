using Company.Application.Share.Config;
using Company.Application.Share.Init;
using Company.Application.UI.Controls;
using Company.Application.UI.Extensions;
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
using System.Windows;
using System.Windows.Input;

namespace Company.Application.Config.ViewModels
{
    public class CameraCalibrationViewModel:ReactiveObject
    {
        [Reactive]
        public double LeftPixelToMillimeter { get; set; }           //左相机像素转毫米的比例
        [Reactive]
        public double RightPixelToMillimeter { get; set; }          //右相机像素转毫米的比例

        public Rect LeftSelectRect  { get; set; }        //左相机选择的标定区域
        public Rect RightSelectRect { get; set; }        //右相机选择的标定区域
        public BitmapGDI RightBitmap    { get; set; }        //右相机标定图片
        public BitmapGDI LeftBitmap     { get; set; }        //左相机标定图片
        public ICamera LeftCamera { get; set; }        //左相机
        public ICamera RightCamera { get; set; }       //右相机

        private IHardwareManager HardwareManager { get; set; }

        public ICommand LeftRectChangedCommand { get; set; }        //左相机选择区域改变命令
        public ICommand RightRectChangedCommand { get; set; }       //右相机选择区域改变命令
        public ICommand CameraCalibrationCommand    { get; set; }        //相机标定命令

        public ICommand SaveCommand { get; set; }        //保存标定结果命令
        private ISystemConfigManager SystemConfigManager { get; set; }
        public ICalibrationManager CalibrationManager { get; set; }

        public CameraCalibrationViewModel(IHardwareManager hardwareManager, ISystemConfigManager systemConfigManager, ICalibrationManager calibrationManager)
        {
            HardwareManager = hardwareManager;
            SystemConfigManager = systemConfigManager;
            CalibrationManager = calibrationManager;
            LeftCamera = HardwareManager.leftCamera;
            RightCamera = HardwareManager.rightCamera;
            LeftBitmap = new BitmapGDI(LeftCamera.Width, LeftCamera.Height);
            RightBitmap = new BitmapGDI(RightCamera.Width, RightCamera.Height);
            LeftCamera.ImageU8C1Observable.ObserveOn(RxApp.MainThreadScheduler).Subscribe(LeftCamera_OnGrabbed);
            RightCamera.ImageU8C1Observable.ObserveOn(RxApp.MainThreadScheduler).Subscribe(RightCamera_OnGrabbed);
            LeftRectChangedCommand = ReactiveCommand.Create<RectEventArgs>(OnLeftRectChangedCommand);
            RightRectChangedCommand = ReactiveCommand.Create<RectEventArgs>(OnRightRectChangedCommand);
            CameraCalibrationCommand = ReactiveCommand.CreateFromTask<CameraType>(OnCameraCalibrationCommand);
            SaveCommand = ReactiveCommand.Create(OnSaveCommand);

        }

        private void OnSaveCommand()
        {
            SystemConfigManager.CalibrationParameter.LeftPixelToMillimeter = LeftPixelToMillimeter;
            SystemConfigManager.CalibrationParameter.RightPixelToMillimeter = RightPixelToMillimeter;
            SystemConfigManager.Save();
            MessageBox.Show("标定参数保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// 相机标定命令
        /// </summary>
        /// <param name="cameraType"></param>
        /// <returns></returns>
        private async Task OnCameraCalibrationCommand(CameraType cameraType)
        {
            if(cameraType == CameraType.left)
            {
                LeftPixelToMillimeter = await CalibrationManager.CalibrateCameraAsync(CameraType.left, LeftSelectRect);
            }
            else
            {
                RightPixelToMillimeter = await CalibrationManager.CalibrateCameraAsync(CameraType.right, RightSelectRect);
            }
        }
        /// <summary>
        /// 右相机选择区域改变事件
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private void OnRightRectChangedCommand(RectEventArgs args)
        {
            RightSelectRect = args.GetSelectRect(RightCamera.Width, RightCamera.Height);
        }
        /// <summary>
        /// 左相机选择区域改变事件
        /// </summary>
        /// <returns></returns>
        private void OnLeftRectChangedCommand(RectEventArgs args)
        {
           LeftSelectRect = args.GetSelectRect(LeftCamera.Width, LeftCamera.Height);
        }

        /// <summary>
        /// 右相机抓取图像事件
        /// </summary>
        /// <param name="photo"></param>
        private void RightCamera_OnGrabbed(ImageU8C1 photo)
        {
             RightBitmap.WritePixels(photo, 0,0);
        }
        /// <summary>
        /// 左相机抓取图像事件
        /// </summary>
        /// <param name="photo"></param>
        private void LeftCamera_OnGrabbed(ImageU8C1 photo)
        {
            LeftBitmap.WritePixels(photo, 0,0);
        }
    }
}
