using System;
using System.IO;

using SkiaSharp;
using Xunit;

using DlibDotNet.Tests;

namespace DlibDotNet.Extensions.Skia.Tests.Extensions
{

    public class SkiaExtensionsTest : TestBase
    {

        private const string LoadTarget = "Lenna";

        [Fact]
        public void To8bppIndexedGrayscale()
        {
            const string testName = "To8bppIndexedGrayscale";
            var path = this.GetDataFile($"{LoadTarget}.png");

            using var bitmap = SKBitmap.Decode(path.FullName);

            var tests = new[]
            {
                new
                {
                    Source = bitmap,
                    ExpectResult = SKColorType.Gray8
                }
            };

            foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
                foreach (var output in tests)
                {
                    var ret = output.Source.To8bppIndexedGrayscale(value);
                    if (ret.Info.ColorType != output.ExpectResult)
                        Assert.True(false);

                    var format = output.Source.Info.ColorType.ToString();
                    var cof = value.ToString();

                    // I'm not sure why bitmap encode always return false
                    var fileName = Path.Combine(this.GetOutDir(testName), $"{format}_{cof}.png");
                    using var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    var b = ret.Encode(stream, SKEncodedImageFormat.Png, 100);
                    Assert.True(b, "Failed to encode");
                }
        }

        [Fact]
        public void ToArray2D()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            using var bitmap = SKBitmap.Decode(path.FullName);

            using var rgbArray = bitmap.ToArray2D<RgbPixel>();
            using var bgrArray = bitmap.ToArray2D<BgrPixel>();
        }

        [Fact]
        public void ToArray2DFrom8bppIndexed()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            using var bitmap = SKBitmap.Decode(path.FullName);

            var tests = new[]
            {
                new { Source = bitmap,   ExpectResult = SKColorType.Gray8 }
            };

            foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
                foreach (var output in tests)
                {
                    var ret = output.Source.To8bppIndexedGrayscale(value);
                    var array = ret.ToArray2D<byte>();
                    if (ret.Info.ColorType != output.ExpectResult)
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

            using var bitmap = SKBitmap.Decode(path.FullName);

            using var rgbMatrix = bitmap.ToMatrix<RgbPixel>();
            using var bgrMatrix = bitmap.ToMatrix<BgrPixel>();
        }

        [Fact]
        public void ToMatrixFrom8bppIndexed()
        {
            var path = this.GetDataFile($"{LoadTarget}.png");

            using var bitmap = SKBitmap.Decode(path.FullName);

            var tests = new[]
            {
                new { Source = bitmap,   ExpectResult = SKColorType.Gray8 }
            };

            foreach (GrayscalLumaCoefficients value in Enum.GetValues(typeof(GrayscalLumaCoefficients)))
            foreach (var output in tests)
            {
                var ret = output.Source.To8bppIndexedGrayscale(value);
                var array = ret.ToMatrix<byte>();
                if (ret.Info.ColorType != output.ExpectResult)
                    Assert.True(false);
                if (array.MatrixElementType != MatrixElementTypes.UInt8)
                    Assert.True(false);
                Assert.NotNull(array);

                this.DisposeAndCheckDisposedState(array);
            }
        }

    }

}
