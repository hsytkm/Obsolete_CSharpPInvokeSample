using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CoreConsole
{
    static class BitmapExtension
    {
        public static double GetAverageY(this string imagePath)
        {
            if (!File.Exists(imagePath)) throw new FileNotFoundException();

            using (var bitmap = new Bitmap(imagePath))
            {
                var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var (r, g, b) = ProcessUsingLockbitsAndUnsafe(bitmap, ref rect);
                return GetY(R: r, G: g, B: b);
            }
        }

        private static double GetY(double R, double G, double B)
        {
            return 0.299 * R + 0.587 * G + 0.114 * B;
        }

        private static (double R, double G, double B)
            ProcessUsingLockbitsAndUnsafe(Bitmap bitmap, ref Rectangle rect)
        {
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            ulong sumB = 0, sumG = 0, sumR = 0;
            try
            {
                unsafe
                {
                    var stride = bitmapData.Stride;
                    var ptrSt = (byte*)bitmapData.Scan0 + rect.Y * stride;
                    var ptrEd = ptrSt + rect.Height * stride;
                    var xEd = rect.Width * bytesPerPixel;

                    for (byte* pixels = ptrSt; pixels < ptrEd; pixels += stride)
                    {
                        for (int x = 0; x < xEd; x += bytesPerPixel)
                        {
                            sumB += pixels[x];
                            sumG += pixels[x + 1];
                            sumR += pixels[x + 2];
                        }
                    }
                }
            }
            finally { bitmap.UnlockBits(bitmapData); }

            var count = (double)(rect.Width * rect.Height);
            var aveR = sumR / count;
            var aveG = sumG / count;
            var aveB = sumB / count;
            return (aveR, aveG, aveB);
        }

    }
}
