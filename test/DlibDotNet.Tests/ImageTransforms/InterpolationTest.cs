using System;
using System.IO;
using DlibDotNet.Tests.Array2D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class InterpolationTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        private readonly uint[] PyramidRates = new[] { 1u, 2u, 3u, 4u, 6u };

        #region ExtractImageChip

        [TestMethod]
        public void ExtractImageChip()
        {
            const string testName = nameof(ExtractImageChip);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.RgbPixel,      SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.RgbAlphaPixel, SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = false }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = false }, new { Type = InterpolationTypes.Quadratic, ExpectResult = false } }},
                new { Type = ImageTypes.UInt8,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.UInt16,        SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.UInt32,        SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.Int8,          SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.Int16,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.Int32,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.HsiPixel,      SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = false } }},
                new { Type = ImageTypes.Float,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = ImageTypes.Double,        SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }}
            };

            var type = this.GetType().Name;
            using (var dims = new ChipDims(227, 227))
            using (var chip = new ChipDetails(new Rectangle(0, 0, 100, 100), dims))
                foreach (var input in tests)
                    foreach (var supportType in input.SupportType)
                    {
                        var expectResult = supportType.ExpectResult;
                        var interpolation = supportType.Type;
                        var imageObj = DlibTest.LoadImage(input.Type, path);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            switch (input.Type)
                            {
                                case ImageTypes.BgrPixel:
                                    return Dlib.ExtractImageChip<BgrPixel>(imageObj, chip, interpolation);
                                case ImageTypes.RgbPixel:
                                    return Dlib.ExtractImageChip<RgbPixel>(imageObj, chip, interpolation);
                                case ImageTypes.RgbAlphaPixel:
                                    return Dlib.ExtractImageChip<RgbAlphaPixel>(imageObj, chip, interpolation);
                                case ImageTypes.UInt8:
                                    return Dlib.ExtractImageChip<byte>(imageObj, chip, interpolation);
                                case ImageTypes.UInt16:
                                    return Dlib.ExtractImageChip<ushort>(imageObj, chip, interpolation);
                                case ImageTypes.UInt32:
                                    return Dlib.ExtractImageChip<uint>(imageObj, chip, interpolation);
                                case ImageTypes.Int8:
                                    return Dlib.ExtractImageChip<sbyte>(imageObj, chip, interpolation);
                                case ImageTypes.Int16:
                                    return Dlib.ExtractImageChip<short>(imageObj, chip, interpolation);
                                case ImageTypes.Int32:
                                    return Dlib.ExtractImageChip<int>(imageObj, chip, interpolation);
                                case ImageTypes.HsiPixel:
                                    return Dlib.ExtractImageChip<HsiPixel>(imageObj, chip, interpolation);
                                case ImageTypes.Float:
                                    return Dlib.ExtractImageChip<float>(imageObj, chip, interpolation);
                                case ImageTypes.Double:
                                    return Dlib.ExtractImageChip<double>(imageObj, chip, interpolation);
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        });

                        var successAction = new Action<Array2DBase>(image =>
                        {
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{interpolation}.bmp")}");
                        });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw exception for InputType: {input.Type}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            if (imageObj != null)
                                this.DisposeAndCheckDisposedState(imageObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, Type: {input.Type}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
        }

        [TestMethod]
        public void ExtractImageChipMatrix()
        {
            const string testName = nameof(ExtractImageChipMatrix);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.RgbAlphaPixel, SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = false }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = false }, new { Type = InterpolationTypes.Quadratic, ExpectResult = false } }},
                new { Type = MatrixElementTypes.UInt8,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.UInt16,        SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.UInt32,        SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.Int8,          SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.Int16,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.Int32,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.HsiPixel,      SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = false } }},
                new { Type = MatrixElementTypes.Float,         SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }},
                new { Type = MatrixElementTypes.Double,        SupportType = new [] { new { Type = InterpolationTypes.Bilinear, ExpectResult = true }, new { Type = InterpolationTypes.NearestNeighbor, ExpectResult = true }, new { Type = InterpolationTypes.Quadratic, ExpectResult = true } }}
            };

            var type = this.GetType().Name;
            using (var dims = new ChipDims(227, 227))
            using (var chip = new ChipDetails(new Rectangle(0, 0, 100, 100), dims))
                foreach (var input in tests)
                    foreach (var supportType in input.SupportType)
                    {
                        var expectResult = supportType.ExpectResult;
                        var interpolation = supportType.Type;
                        var imageObj = DlibTest.LoadImageAsMatrix(input.Type, path);

                        var outputImageAction = new Func<bool, MatrixBase>(expect =>
                        {
                            switch (input.Type)
                            {
                                case MatrixElementTypes.RgbPixel:
                                    return Dlib.ExtractImageChip<RgbPixel>(imageObj, chip, interpolation);
                                case MatrixElementTypes.RgbAlphaPixel:
                                    return Dlib.ExtractImageChip<RgbAlphaPixel>(imageObj, chip, interpolation);
                                case MatrixElementTypes.UInt8:
                                    return Dlib.ExtractImageChip<byte>(imageObj, chip, interpolation);
                                case MatrixElementTypes.UInt16:
                                    return Dlib.ExtractImageChip<ushort>(imageObj, chip, interpolation);
                                case MatrixElementTypes.UInt32:
                                    return Dlib.ExtractImageChip<uint>(imageObj, chip, interpolation);
                                case MatrixElementTypes.Int8:
                                    return Dlib.ExtractImageChip<sbyte>(imageObj, chip, interpolation);
                                case MatrixElementTypes.Int16:
                                    return Dlib.ExtractImageChip<short>(imageObj, chip, interpolation);
                                case MatrixElementTypes.Int32:
                                    return Dlib.ExtractImageChip<int>(imageObj, chip, interpolation);
                                case MatrixElementTypes.HsiPixel:
                                    return Dlib.ExtractImageChip<HsiPixel>(imageObj, chip, interpolation);
                                case MatrixElementTypes.Float:
                                    return Dlib.ExtractImageChip<float>(imageObj, chip, interpolation);
                                case MatrixElementTypes.Double:
                                    return Dlib.ExtractImageChip<double>(imageObj, chip, interpolation);
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        });

                        var successAction = new Action<MatrixBase>(image =>
                        {
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{interpolation}.bmp")}");
                        });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw exception for InputType: {input.Type}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            if (imageObj != null)
                                this.DisposeAndCheckDisposedState(imageObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, Type: {input.Type}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
        }

        #endregion

        #region FlipImageLeftRight

        [TestMethod]
        public void FlipImageLeftRight()
        {
            const string testName = "FlipImageLeftRight";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
            {
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImage(input.Type, path);

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        Dlib.FlipImageLeftRight(imageObj);
                        return imageObj;
                    });

                    var successAction = new Action<Array2DBase>(image =>
                    {
                        Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}.bmp")}");
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {input.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, Type: {input.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
            }
        }

        #endregion

        #region FlipImageLeftRight2

        [TestMethod]
        public void FlipImageLeftRight2()
        {
            const string testName = "FlipImageLeftRight2";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
                foreach (var output in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImage(input.Type, path);
                    var outputObj = Array2DTest.CreateArray2D(output.Type);

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        Dlib.FlipImageLeftRight(imageObj, outputObj);
                        return outputObj;
                    });

                    var successAction = new Action<Array2DBase>(image =>
                    {
                        Dlib.SaveBmp(outputObj, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}.bmp")}");
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {output.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                        if (outputObj != null)
                            this.DisposeAndCheckDisposedState(outputObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        #endregion

        #region FlipImageUpDown

        [TestMethod]
        public void FlipImageUpDown()
        {
            const string testName = "FlipImageUpDown";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
                foreach (var output in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImage(input.Type, path);
                    var outputObj = Array2DTest.CreateArray2D(output.Type);

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        Dlib.FlipImageUpDown(imageObj, outputObj);
                        return outputObj;
                    });

                    var successAction = new Action<Array2DBase>(image =>
                    {
                        Dlib.SaveBmp(outputObj, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}.bmp")}");
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {output.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                        if (outputObj != null)
                            this.DisposeAndCheckDisposedState(outputObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        #endregion

        #region PyramidUp

        [TestMethod]
        public void PyramidUp()
        {
            const string testName = nameof(PyramidUp);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                var expectResult = test.ExpectResult;
                var imageObj = DlibTest.LoadImage(test.Type, path);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    Dlib.PyramidUp(imageObj);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                    Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{test.Type}.bmp")}");
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw exception for InputType: {test.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {test.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void PyramidUpMatrix()
        {
            const string testName = nameof(PyramidUpMatrix);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                var expectResult = test.ExpectResult;
                var imageObj = DlibTest.LoadImageAsMatrix(test.Type, path);

                var outputImageAction = new Func<bool, MatrixBase>(expect =>
                {
                    Dlib.PyramidUp(imageObj);
                    return imageObj;
                });

                var successAction = new Action<MatrixBase>(image =>
                {
                    Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{test.Type}.bmp")}");
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw exception for InputType: {test.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {test.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void PyramidUpMatrix2()
        {
            const string testName = nameof(PyramidUpMatrix2);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var pyramidRate in this.PyramidRates)
                foreach (var test in tests)
                {
                    var expectResult = test.ExpectResult;
                    var imageObj = DlibTest.LoadImageAsMatrix(test.Type, path);

                    var outputImageAction = new Func<bool, MatrixBase>(expect =>
                    {
                        using (var pyr = new PyramidDown(pyramidRate))
                        {
                            switch (test.Type)
                            {
                                case MatrixElementTypes.UInt8:
                                    {
                                        Dlib.PyramidUp((Matrix<byte>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.UInt16:
                                    {
                                        Dlib.PyramidUp((Matrix<ushort>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.UInt32:
                                    {
                                        Dlib.PyramidUp((Matrix<uint>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.UInt64:
                                    {
                                        Dlib.PyramidUp((Matrix<ulong>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.Int8:
                                    {
                                        Dlib.PyramidUp((Matrix<sbyte>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.Int16:
                                    {
                                        Dlib.PyramidUp((Matrix<short>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.Int32:
                                    {
                                        Dlib.PyramidUp((Matrix<int>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.Int64:
                                    {
                                        Dlib.PyramidUp((Matrix<long>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.Float:
                                    {
                                        Dlib.PyramidUp((Matrix<float>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.Double:
                                    {
                                        Dlib.PyramidUp((Matrix<double>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.RgbPixel:
                                    {
                                        Dlib.PyramidUp((Matrix<RgbPixel>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.RgbAlphaPixel:
                                    {
                                        Dlib.PyramidUp((Matrix<RgbAlphaPixel>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                case MatrixElementTypes.HsiPixel:
                                    {
                                        Dlib.PyramidUp((Matrix<HsiPixel>)imageObj, pyr, out var ret);
                                        return ret;
                                    }
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    });

                    var successAction = new Action<MatrixBase>(image =>
                    {
                        Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{test.Type}_{pyramidRate}.bmp")}");
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {test.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {test.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        [TestMethod]
        public void PyramidUpMatrix3()
        {
            const string testName = nameof(PyramidUpMatrix3);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var pyramidRate in this.PyramidRates)
                foreach (var test in tests)
                {
                    var expectResult = test.ExpectResult;
                    var imageObj = DlibTest.LoadImageAsMatrix(test.Type, path);

                    var outputImageAction = new Func<bool, MatrixBase>(expect =>
                    {
                        Dlib.PyramidUp<PyramidDown>(imageObj, pyramidRate);
                        return imageObj;
                    });

                    var successAction = new Action<MatrixBase>(image =>
                    {
                        Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{test.Type}_{pyramidRate}.bmp")}");
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {test.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {test.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        #endregion

        #region ResizeImage

        [TestMethod]
        public void ResizeImage()
        {
            const string testName = "ResizeImage";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var scale in new[] { 0.25d, 0.50d, 2.0d })
                foreach (var input in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImage(input.Type, path);

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        Dlib.ResizeImage(imageObj, scale);
                        return imageObj;
                    });

                    var successAction = new Action<Array2DBase>(image =>
                    {
                        Dlib.SaveJpeg(imageObj, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{scale}.jpg")}");
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, Scale: {scale}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, Scale: {scale}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        [TestMethod]
        public void ResizeImage2()
        {
            const string testName = "ResizeImage2";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (InterpolationTypes interpolationTypes in Enum.GetValues(typeof(InterpolationTypes)))
                foreach (var size in new[] { 128, 512 })
                    foreach (var input in tests)
                    {
                        var expectResult = input.ExpectResult;
                        var imageObj = DlibTest.LoadImage(input.Type, path);
                        var outputObj = Array2DTest.CreateArray2D(input.Type, size, size);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            Dlib.ResizeImage(imageObj, outputObj, interpolationTypes);
                            return outputObj;
                        });

                        var successAction = new Action<Array2DBase>(image =>
                        {
                            Dlib.SaveJpeg(outputObj, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{input.Type}_{size}_{interpolationTypes}.jpg")}");
                        });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {input.Type}, Size: {size}, InterpolationType: {interpolationTypes}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            if (imageObj != null)
                                this.DisposeAndCheckDisposedState(imageObj);
                            if (outputObj != null)
                                this.DisposeAndCheckDisposedState(outputObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {input.Type}, Size: {size}, InterpolationType: {interpolationTypes}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
        }

        #endregion

        #region RotateImage

        [TestMethod]
        public void RotateImage()
        {
            const string testName = "RotateImage";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var angle in new[] { 30, 60, 90, 270 })
                foreach (var input in tests)
                    foreach (var output in tests)
                    {
                        var expectResult = input.ExpectResult && output.ExpectResult;
                        var imageObj = DlibTest.LoadImage(input.Type, path);
                        var outputObj = Array2DTest.CreateArray2D(output.Type);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            Dlib.RotateImage(imageObj, outputObj, angle);
                            return outputObj;
                        });

                        var successAction = new Action<Array2DBase>(image =>
                        {
                            Dlib.SaveJpeg(outputObj, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}_{angle}.jpg")}");
                        });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {output.Type}, Angle: {angle}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            if (imageObj != null)
                                this.DisposeAndCheckDisposedState(imageObj);
                            if (outputObj != null)
                                this.DisposeAndCheckDisposedState(outputObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}, Angle: {angle}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
        }

        [TestMethod]
        public void RotateImage2()
        {
            const string testName = "RotateImage2";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (InterpolationTypes itype in Enum.GetValues(typeof(InterpolationTypes)))
                foreach (var angle in new[] { 30, 60, 90 })
                    foreach (var input in tests)
                    {
                        var expectResult = input.ExpectResult;
                        var imageObj = DlibTest.LoadImage(input.Type, path);
                        var outputObj = Array2DTest.CreateArray2D(input.Type);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            Dlib.RotateImage(imageObj, outputObj, angle, itype);
                            return outputObj;
                        });

                        var successAction = new Action<Array2DBase>(image =>
                        {
                            Dlib.SaveJpeg(outputObj, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{input.Type}_{angle}_{itype}.jpg")}");
                        });

                        var failAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {input.Type}, Angle: {angle}, InterpolationType: {itype}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            if (imageObj != null)
                                this.DisposeAndCheckDisposedState(imageObj);
                            if (outputObj != null)
                                this.DisposeAndCheckDisposedState(outputObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {input.Type}, Angle: {angle}, InterpolationType: {itype}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
        }

        #endregion

        #region TransformImage

        #region PointRotator

        [TestMethod]
        public void TransformImagePointRotator()
        {
            const string testName = "TransformImagePointRotator";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var angleCases = new[]
            {
                45d, -45d, 90d, 180d
            };

            var type = this.GetType().Name;
            foreach (var angle in angleCases)
                foreach (var input in tests)
                    foreach (var output in tests)
                    {
                        var expectResult = input.ExpectResult && output.ExpectResult;
                        var imageObj = DlibTest.LoadImage(input.Type, path);
                        var outputObj = Array2DTest.CreateArray2D(output.Type, imageObj.Rows, imageObj.Columns);
                        var transform = new PointRotator(angle);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            Dlib.TransformImage(imageObj, outputObj, transform);
                            return outputObj;
                        });

                        var successAction = new Action<Array2DBase>(image =>
                         {
                             Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}_{angle}.jpg")}");
                         });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            this.DisposeAndCheckDisposedState(transform);
                            this.DisposeAndCheckDisposedState(imageObj);
                            if (outputObj != null)
                                this.DisposeAndCheckDisposedState(outputObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
        }

        #endregion

        #region PointTransform

        [TestMethod]
        public void TransformImagePointTransform()
        {
            const string testName = "TransformImagePointTransform";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var angleCases = new[]
            {
                45d, -45d, 90d, 180d
            };

            var type = this.GetType().Name;
            foreach (var angle in angleCases)
                foreach (var input in tests)
                    foreach (var output in tests)
                    {
                        var expectResult = input.ExpectResult && output.ExpectResult;
                        var imageObj = DlibTest.LoadImage(input.Type, path);
                        var outputObj = Array2DTest.CreateArray2D(output.Type, imageObj.Rows, imageObj.Columns);

                        // 中心で回転
                        var x = imageObj.Columns / 2;
                        var y = imageObj.Rows / 2;
                        var transform = new PointTransform(angle, x, y);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            Dlib.TransformImage(imageObj, outputObj, transform);
                            return outputObj;
                        });

                        var successAction = new Action<Array2DBase>(image =>
                        {
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}_{angle}_{x}_{y}.jpg")}");
                        });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}, X: {x}, Y: {y}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            this.DisposeAndCheckDisposedState(transform);
                            this.DisposeAndCheckDisposedState(imageObj);
                            if (outputObj != null)
                                this.DisposeAndCheckDisposedState(outputObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}, X: {x}, Y: {y}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
        }

        #endregion

        #region PointTransformAffine

        [TestMethod]
        public void TransformImagePointTransformAffine()
        {
            const string testName = "TransformImagePointTransformAffine";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var angleCases = new[]
            {
                //45d, -45d, 90d, 180d
                0d
            };

            var xCases = new[]
            {
                //10, -10, 50,5, -50,5
                0d
            };

            var yCases = new[]
            {
                //10, -10, 50,5, -50,5
                0d
            };

            var type = this.GetType().Name;
            foreach (var angle in angleCases)
                foreach (var x in xCases)
                    foreach (var y in yCases)
                        foreach (var input in tests)
                            foreach (var output in tests)
                            {
                                var expectResult = input.ExpectResult && output.ExpectResult;
                                var imageObj = DlibTest.LoadImage(input.Type, path);
                                var outputObj = Array2DTest.CreateArray2D(output.Type, imageObj.Rows, imageObj.Columns);
                                var matrix = new Matrix<double>(2, 2);
                                //var rad = Math.PI / (180d / angle);
                                //matrix.Assign(new[] { Math.Cos(rad), -Math.Sin(rad), Math.Sin(rad), Math.Cos(rad) });
                                var transform = new PointTransformAffine(matrix, x, y);

                                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                                {
                                    Dlib.TransformImage(imageObj, outputObj, transform);
                                    return outputObj;
                                });

                                var successAction = new Action<Array2DBase>(image =>
                                {
                                    Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}_{angle}_{x}_{y}.jpg")}");
                                });

                                var failAction = new Action(() =>
                                {
                                    Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}, X: {x}, Y: {y}.");
                                });

                                var finallyAction = new Action(() =>
                                {
                                    this.DisposeAndCheckDisposedState(matrix);
                                    this.DisposeAndCheckDisposedState(transform);
                                    this.DisposeAndCheckDisposedState(imageObj);
                                    if (outputObj != null)
                                        this.DisposeAndCheckDisposedState(outputObj);
                                });

                                var exceptionAction = new Action(() =>
                                {
                                    Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}, X: {x}, Y: {y}.");
                                });

                                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                            }
        }

        #endregion

        #region PointTransformProjective

        [TestMethod]
        public void TransformImagePointTransformProjective()
        {
            const string testName = "TransformImagePointTransformProjective";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var angleCases = new[]
            {
                //45d, -45d, 90d, 180d
                0d
            };

            var xCases = new[]
            {
                //10, -10, 50,5, -50,5
                0d
            };

            var yCases = new[]
            {
                //10, -10, 50,5, -50,5
                0d
            };

            var type = this.GetType().Name;
            foreach (var angle in angleCases)
                foreach (var x in xCases)
                    foreach (var y in yCases)
                        foreach (var input in tests)
                            foreach (var output in tests)
                            {
                                var expectResult = input.ExpectResult && output.ExpectResult;
                                var imageObj = DlibTest.LoadImage(input.Type, path);
                                var outputObj = Array2DTest.CreateArray2D(output.Type, imageObj.Rows, imageObj.Columns);
                                var matrix = new Matrix<double>(3, 3);
                                var transform = new PointTransformProjective(matrix);

                                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                                {
                                    Dlib.TransformImage(imageObj, outputObj, transform);
                                    return outputObj;
                                });

                                var successAction = new Action<Array2DBase>(image =>
                                {
                                    Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}_{angle}_{x}_{y}.jpg")}");
                                });

                                var failAction = new Action(() =>
                                {
                                    Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}, X: {x}, Y: {y}.");
                                });

                                var finallyAction = new Action(() =>
                                {
                                    this.DisposeAndCheckDisposedState(matrix);
                                    this.DisposeAndCheckDisposedState(transform);
                                    this.DisposeAndCheckDisposedState(imageObj);
                                    if (outputObj != null)
                                        this.DisposeAndCheckDisposedState(outputObj);
                                });

                                var exceptionAction = new Action(() =>
                                {
                                    Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}, Angle,: {angle}, X: {x}, Y: {y}.");
                                });

                                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                            }
        }

        #endregion

        #endregion

    }

}
