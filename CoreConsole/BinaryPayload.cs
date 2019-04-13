using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CoreConsole
{
    class BinaryPayload
    {
        private const string DllFilePath = "NativeWinLib.dll";

        // Windows
        [DllImport(DllFilePath)]
        private extern static double GetImageAllY(ImagePayload imagePayload);

        private static readonly string ImagePath = @"C:\data\Image1.jpg";

        public void Test()
        {
            Console.WriteLine("--- BinaryPayload ---");
            var sw = new System.Diagnostics.Stopwatch();

            // 画像の平均輝度を取得(C#)
            sw.Restart();
            double csy = ImagePath.GetAverageY();
            Console.WriteLine($"C#  AverageY={csy:f2}  {sw.Elapsed} sec");

            // 画像の平均輝度を取得(PInvoke)
            sw.Restart();
            double cppy;
            using (var bitmap = new Bitmap(ImagePath))
            {
                var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                try
                {
                    var payload = new ImagePayload(bitmap, bitmapData);
                    cppy = GetImageAllY(payload);
                }
                finally { bitmap.UnlockBits(bitmapData); }
            }
            Console.WriteLine($"C++ AverageY={cppy:f2}  {sw.Elapsed} sec");
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public class ImagePayload
    {
        public readonly int width;
        public readonly int height;
        public readonly int bytesPerPixel;
        public readonly int stride;
        public readonly IntPtr data;

        public ImagePayload(Bitmap bitmap, BitmapData bitmapData)
        {
            //Console.WriteLine($"sizeof(ImagePayload)={Marshal.SizeOf(typeof(ImagePayload))}"); //= 20

            width = bitmap.Width;
            height = bitmap.Height;
            bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            stride = bitmapData.Stride;
            unsafe { data = new IntPtr((void*)bitmapData.Scan0); }
        }
    }

}
