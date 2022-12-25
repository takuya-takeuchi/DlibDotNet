using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Xunit;

using DlibDotNet.Tests;
using System.Drawing;
using SkiaSharp;

namespace DlibDotNet.Extensions.Tests.Extensions
{

    public class WriteableBitmapExtensionsTest : TestBase
    {

        private const string LoadTarget = "Lenna";

        [Fact]
        public void To8bppIndexedGrayscale()
        {
            const string testName = "To8bppIndexedGrayscale";
            var path = this.GetDataFile($"{LoadTarget}.png");

            var bitmap = new BitmapImage(new Uri(path.FullName, UriKind.Absolute));
            var rgb32 = new WriteableBitmap(bitmap);

            var tests = new[]
            {
                new
                {
                    Source = rgb32,
                    ExpectResult = PixelFormats.Indexed8
                }
            };

            foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
                foreach (var output in tests)
                {
                    var ret = output.Source.To8bppIndexedGrayscale(value);
                    if (ret.Format != output.ExpectResult)
                        Assert.True(false);

                    var format = output.Source.Format.ToString();
                    var cof = value.ToString();

                    var fileName = Path.Combine(this.GetOutDir(testName), $"{format}_{cof}.bmp");
                    using var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    var encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(ret));
                    encoder.Save(stream);
                }
        }

        [Fact]
        public void ToArray2D()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            var bitmap = new BitmapImage(new Uri(path.FullName, UriKind.Absolute));
            var rgb = new WriteableBitmap(bitmap);

            using var rgbArray = rgb.ToArray2D<RgbPixel>();
            using var bgrArray = rgb.ToArray2D<BgrPixel>();
        }

        [Fact]
        public void ToArray2DFrom8bppIndexed()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            var bitmap = new BitmapImage(new Uri(path.FullName, UriKind.Absolute));
            var rgb = new WriteableBitmap(bitmap);

            var tests = new[]
            {
                new { Source = rgb,   ExpectResult = PixelFormats.Indexed8 }
            };

            foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
                foreach (var output in tests)
                {
                    var ret = output.Source.To8bppIndexedGrayscale(value);
                    var array = ret.ToArray2D<byte>();
                    if (ret.Format != output.ExpectResult)
                        Assert.True(false);
                    if (array.ImageType != ImageTypes.UInt8)
                        Assert.True(false);
                    Assert.NotNull(array);

                    this.DisposeAndCheckDisposedState(array);
                }
        }
        
        [Fact]
        public void ToMatrix()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            var bitmap = new BitmapImage(new Uri(path.FullName, UriKind.Absolute));
            var rgb = new WriteableBitmap(bitmap);

            using var rgbMatrix = rgb.ToMatrix<RgbPixel>();
            using var bgrMatrix = rgb.ToMatrix<BgrPixel>();
        }

        [Fact]
        public void ToMatrixFrom8bppIndexed()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            var bitmap = new BitmapImage(new Uri(path.FullName, UriKind.Absolute));
            var rgb = new WriteableBitmap(bitmap);

            var tests = new[]
            {
                new { Source = rgb,   ExpectResult = PixelFormats.Indexed8 }
            };

            foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
            foreach (var output in tests)
            {
                var ret = output.Source.To8bppIndexedGrayscale(value);
                var array = ret.ToMatrix<byte>();
                if (ret.Format != output.ExpectResult)
                    Assert.True(false);
                if (array.MatrixElementType != MatrixElementTypes.UInt8)
                    Assert.True(false);
                Assert.NotNull(array);

                this.DisposeAndCheckDisposedState(array);
            }
        }

    }

}
