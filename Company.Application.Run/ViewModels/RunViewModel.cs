using Company.Algorithm.Unwrapper;
using Company.Application.Share.Run;
using Company.Hardware.Camera.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Company.Application.Run.ViewModels
{
    public class RunViewModel:ReactiveObject
    {
        [Reactive]
        public bool CanSelect { get; set; } 
        public ICommand SelectCameraCommand { get; set; }
        public ICommand RectChangedCommand { get; set; }
        public IPhotoManager PhotoManager { get; set; }
        public RunViewModel(IPhotoManager photoManager)
        {
            PhotoManager = photoManager;
            SelectCameraCommand = ReactiveCommand.Create<CameraType>(OnSelectCameraCommand);
            RectChangedCommand = ReactiveCommand.Create(OnRectChangedCommand); 
        }
        /// <summary>
        /// 框选区域
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnRectChangedCommand()
        {
            CanSelect = !CanSelect;
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnSelectCameraCommand(CameraType cameraType)
        {
            PhotoManager.SetCurrentPhoto(cameraType); 
        }
    }
}
