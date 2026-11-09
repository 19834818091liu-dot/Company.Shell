using Company.Core.Helpers;
using Company.Core.Models;
using Company.Hardware.Camera.Base;
using Company.Logger;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Camera.Emulation
{
    public class CameraEmulation : ReactiveObject, ICamera
    {
        private string filename;
        private IntPtr intPtr = IntPtr.Zero;
        public CameraEmulation()
        {
            ImageU8C1Observable = this.WhenAnyValue(t => t.ImageU8C1);
            ImageU8C3Observable = this.WhenAnyValue(t => t.ImageU8C3);

        }
        public ImageU8C1 ImageU8C1 { get; set; }

        public IObservable<ImageU8C1> ImageU8C1Observable { get; set; }

        public ImageU8C3 ImageU8C3 { get; set; }

        public IObservable<ImageU8C3> ImageU8C3Observable { get; set; }

        public int Width { get; set; } = 2448;
        public int Height { get; set; } = 2048;
        public bool Initialized { get; set; }
        public bool Connected { get; set; }
        public string Message { get; set; }

        public event Action<ImageU8C1> OnGrabbed;

        public void Close()
        {
            logs.LogInfo("关闭仿真相机成功");
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
        /// 初始化仿真相机
        /// </summary>
        /// <param name="cameraConfig"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Init(CameraConfig cameraConfig)
        {
            try
            {
                if(intPtr == IntPtr.Zero)
                {
                    intPtr = Marshal.AllocHGlobal(new IntPtr(Width*Height));
                }
                filename = $"images\\{cameraConfig.direction}.bmp";
                if(File.Exists(filename))
                {
                    Bitmap bitmap = ImageHelper.Load(filename);
                    if(bitmap.Width != Width || bitmap.Height != Height)
                    {
                        logs.LogError("相机尺寸与仿真图尺寸不一致");
                        throw new Exception("相机尺寸与仿真图尺寸不一致");
                    }
                    BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                    MemoryHelper.CopyMemory(intPtr, data.Scan0, Width * Height);
                    bitmap.UnlockBits(data);
                    ImageU8C1 = new ImageU8C1(Width,Height,intPtr);    //封装

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return true;
        }

        public bool SetExplosureTime(float value)
        {
            throw new NotImplementedException();
        }

        public bool SetGain(float value)
        {
            throw new NotImplementedException();
        }

        public bool Trigger()
        {
            throw new NotImplementedException();
        }
    }
}
