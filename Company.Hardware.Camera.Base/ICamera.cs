using Company.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Camera.Base
{
    public interface ICamera
    {
        event Action<ImageU8C1> OnGrabbed;

        ImageU8C1 ImageU8C1 { get; }

        IObservable<ImageU8C1> ImageU8C1Observable { get; }

        ImageU8C3 ImageU8C3 { get; }

        IObservable<ImageU8C3> ImageU8C3Observable { get; }
         int Width { get; set; }

        int Height { get; set; }

         bool Initialized { get; set; }

        bool Connected { get; set; }

        string Message  { get; set; }
        bool Init(CameraConfig cameraConfig);

        bool SetExplosureTime(float value);

        bool SetGain(float value);

        float GetExplosureTime();

        float GetGain();

        bool Trigger();

        void Close();

    }
}
