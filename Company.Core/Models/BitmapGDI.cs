using Company.Logger;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Company.Core.Models
{
    /// <summary>
    /// 显示图像到WPF控件上
    /// </summary>
    public class BitmapGDI:ReactiveObject
    {
       [Reactive]
       public  ImageSource ImageSource { get; set; }             //显示在wpf上的图片源
       public  WriteableBitmap _sourceU8C1 { get; set; }        //单通道8位图，接收写的对象

        public WriteableBitmap _sourceU8C3 { get; set; }

        public BitmapGDI(int width,int height)
        {
            _sourceU8C1 = new WriteableBitmap(width,height,96d,96d,PixelFormats.Gray8,null);
            _sourceU8C3 = new WriteableBitmap(width, height, 96d, 96d, PixelFormats.Bgr24, null);
            ImageSource = _sourceU8C1;
        }
        /// <summary>
        /// 显示单通道8位图
        /// </summary>
        /// <param name=""></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void WritePixels(ImageU8C1 photo, int x, int y)
        {
            if (photo.IntPtr == IntPtr.Zero || photo.Width == 0 || photo.Height == 0)
                return;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Int32Rect int32Rect = new Int32Rect(x, y, photo.Width, photo.Height);
                try
                {
                    _sourceU8C1.WritePixels(int32Rect, photo.IntPtr, photo.Width
                        * photo.Height * 1, photo.Width, int32Rect.X, int32Rect.Y);
                }
                catch (Exception ex)
                {

                    logs.LogError(ex);
                }
                finally
                {
                    ImageSource = null;
                    ImageSource = _sourceU8C1;
                }
            });

        }
        /// <summary>
        /// 显示三通道8位图
        /// </summary>
        /// <param name=""></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void WritePixels(ImageU8C3 photo, int x, int y)
        {
            if (photo.IntPtr == IntPtr.Zero || photo.Width == 0 || photo.Height == 0)
                return;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Int32Rect int32Rect = new Int32Rect(x, y, photo.Width, photo.Height);
                try
                {
                    _sourceU8C3.WritePixels(int32Rect, photo.IntPtr, photo.Width
                        * photo.Height * 3, photo.Width*3, int32Rect.X, int32Rect.Y);
                }
                catch (Exception ex)
                {

                    logs.LogError(ex);
                }
                finally
                {
                    ImageSource = null;
                    ImageSource = _sourceU8C3;
                }
            });

        }
    }
}
