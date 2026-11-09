using Company.Core.Models;
using Company.Hardware.Camera.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Run
{
    public interface IPhotoManager
    {
        BitmapGDI Currentphoto { get; }

        ICamera Camera { get; }

        void SetCurrentPhoto(CameraType cameraType);
    }
}
