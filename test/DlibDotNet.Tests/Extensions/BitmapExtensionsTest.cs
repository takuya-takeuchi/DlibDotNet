using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DlibDotNet.Extensions;
using Xunit;

namespace DlibDotNet.Tests.Extensions
{

    public class BitmapExtensionsTest : TestBase
    {

        private const string LoadTarget = "Lenna";

        private static bool Compare(Bitmap bitmap1, Bitmap bitmap2)
        {
            var pixelFormat1 = bitmap1.PixelFormat;
            var pixelFormat2 = bitmap2.PixelFormat;
            if (pixelFormat1 != pixelFormat2)
                return false;

            var rect1 = new System.Drawing.Rectangle(0, 0, bitmap1.Width, bitmap1.Height);
            var rect2 = new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
            if (rect1 != rect2)
                return false;

            var data1 = bitmap1.LockBits(rect1, ImageLockMode.ReadOnly, bitmap1.PixelFormat);
            var data2 = bitmap2.LockBits(rect2, ImageLockMode.ReadOnly, bitmap2.PixelFormat);

            try
            {
                var length = data1.Stride * rect1.Height;

                unsafe
                {
                    for (var i = 0; i < length; i++)
                    {
                        var p1 = ((byte*)data1.Scan0) + i;
                        var p2 = ((byte*)data2.Scan0) + i;
                        if (*p1 != *p2)
                            return false;
                    }
                }

                return true;
            }
            finally
            {
                // create pallet

                if (data1 != null)
                    bitmap1.UnlockBits(data1);
                if (data2 != null)
                    bitmap2.UnlockBits(data2);
            }
        }

        private static Bitmap To32Rgb(Bitmap bitmap, bool withAlpha)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var rect = new System.Drawing.Rectangle(0, 0, width, height);

            BitmapData srcData = null;
            BitmapData dstData = null;
            Bitmap dst = null;

            try
            {
                dst = new Bitmap(width, height, withAlpha ? PixelFormat.Format32bppArgb : PixelFormat.Format32bppRgb);

                srcData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                dstData = dst.LockBits(rect, ImageLockMode.WriteOnly, dst.PixelFormat);

                var srcStride = srcData.Stride;
                var dstStride = dstData.Stride;

                switch (bitmap.PixelFormat)
                {
                    case PixelFormat.Format24bppRgb:
                        unsafe
                        {
                            for (var y = 0; y < height; y++)
                            {
                                var pSrc = ((byte*)srcData.Scan0) + y * srcStride;
                                var pDst = ((byte*)dstData.Scan0) + y * dstStride;
                                for (var x = 0; x < width; x++, pSrc += 3, pDst += 4)
                                {
                                    pDst[0] = pSrc[0];
                                    pDst[1] = pSrc[1];
                                    pDst[2] = pSrc[2];
                                }
                            }
                        }
                        break;
                    case PixelFormat.Format32bppRgb:
                    case PixelFormat.Format32bppArgb:
                        unsafe
                        {
                            for (var y = 0; y < height; y++)
                            {
                                var pSrc = ((byte*)srcData.Scan0) + y * srcStride;
                                var pDst = ((byte*)dstData.Scan0) + y * dstStride;
                                for (var x = 0; x < width; x++, pSrc += 4, pDst += 4)
                                {
                                    pDst[0] = pSrc[0];
                                    pDst[1] = pSrc[1];
                                    pDst[2] = pSrc[2];
                                }
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch
            {
                dst?.Dispose();
                throw;
            }
            finally
            {
                // create pallet

                if (srcData != null)
                    bitmap.UnlockBits(srcData);
                if (dstData != null)
                    dst.UnlockBits(dstData);
            }

            return dst;
        }

        [Fact]
        public void To8bppIndexedGrayscale()
        {
            const string testName = nameof(To8bppIndexedGrayscale);
            var path = this.GetDataFile($"{LoadTarget}.png");

            using (var rgb24 = Image.FromFile(path.FullName) as Bitmap)
            using (var rgb32 = To32Rgb(rgb24, false))
            {
                var tests = new[]
                {
                    new
                    {
                        Source = rgb24, ExpectResult = PixelFormat.Format8bppIndexed
                    },
                    new
                    {
                        Source = rgb32, ExpectResult = PixelFormat.Format8bppIndexed
                    }
                };

                foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
                    foreach (var output in tests)
                    {
                        using (var ret = output.Source.To8bppIndexedGrayscale(value))
                        {
                            if (ret.PixelFormat != output.ExpectResult)
                                Assert.True(false);

                            var format = output.Source.PixelFormat.ToString();
                            var cof = value.ToString();
                            ret.Save(Path.Combine(this.GetOutDir(testName), $"{format}_{cof}.bmp"), ImageFormat.Bmp);
                        }
                    }

                foreach (var output in tests)
                    output.Source.Dispose();
            }
        }

        [Fact]
        public void ToArray2DFrom8bppIndexed()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            using (var rgb = new Bitmap(path.FullName))
            using (var argb = To32Rgb(rgb, false))
            {
                var tests = new[]
                {
                new { Source = rgb,   ExpectResult = PixelFormat.Format8bppIndexed },
                new { Source = argb,  ExpectResult = PixelFormat.Format8bppIndexed }
            };

                foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
                    foreach (var output in tests)
                        using (var ret = output.Source.To8bppIndexedGrayscale(value))
                        {
                            var array = ret.ToArray2D<byte>();
                            if (ret.PixelFormat != output.ExpectResult)
                                Assert.True(false);
                            if (array.ImageType != ImageTypes.UInt8)
                                Assert.True(false);

                            this.DisposeAndCheckDisposedState(array);
                        }

                foreach (var output in tests)
                    output.Source.Dispose();
            }
        }

        [Fact]
        public void ToBitmap()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            using (var rgb = new Bitmap(path.FullName))
            using (var matrix = Dlib.LoadImageAsMatrix<RgbPixel>(path.FullName))
            using (var test = matrix.ToBitmap())
                Assert.True(Compare(rgb, test));
        }

    }

}
