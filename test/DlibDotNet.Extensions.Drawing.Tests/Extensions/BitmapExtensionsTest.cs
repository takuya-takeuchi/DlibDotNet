using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DlibDotNet.Tests;
using Xunit;

namespace DlibDotNet.Extensions.Tests.Extensions
{

    public class BitmapExtensionsTest : TestBase
    {

        private const string LoadTarget = "Lenna";

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
            const string testName = "To8bppIndexedGrayscale";
            var path = this.GetDataFile($"{LoadTarget}.png");

            var rgb24 = Image.FromFile(path.FullName) as Bitmap;
            var rgb32 = To32Rgb(rgb24, false);

            var tests = new[]
            {
                new
                {
                    Source = rgb24,
                    ExpectResult = PixelFormat.Format8bppIndexed
                },
                new
                {
                    Source = rgb32,
                    ExpectResult = PixelFormat.Format8bppIndexed
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

        [Fact]
        public void ToArray2DFrom8bppIndexed()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            var rgb = new Bitmap(path.FullName);
            var argb = To32Rgb(rgb, false);

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
                        Assert.NotNull(array);

                        this.DisposeAndCheckDisposedState(array);
                    }

            foreach (var output in tests)
                output.Source.Dispose();
        }

    }

}
