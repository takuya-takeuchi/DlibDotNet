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

        #region ExtractImageChip

        [TestMethod]
        public void ExtractImageChip()
        {
            const string testName = nameof(ExtractImageChip);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
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
            using (var dims = new ChipDims(227, 227))
            using (var chip = new ChipDetails(new Rectangle(0, 0, 100, 100), dims))
                foreach (var input in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImage(input.Type, path);

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        switch (input.Type)
                        {
                            case ImageTypes.RgbPixel:
                                return Dlib.ExtractImageChip<RgbPixel>(imageObj, chip);
                            case ImageTypes.RgbAlphaPixel:
                                return Dlib.ExtractImageChip<RgbAlphaPixel>(imageObj, chip);
                            case ImageTypes.UInt8:
                                return Dlib.ExtractImageChip<byte>(imageObj, chip);
                            case ImageTypes.UInt16:
                                return Dlib.ExtractImageChip<ushort>(imageObj, chip);
                            case ImageTypes.UInt32:
                                return Dlib.ExtractImageChip<uint>(imageObj, chip);
                            case ImageTypes.Int8:
                                return Dlib.ExtractImageChip<sbyte>(imageObj, chip);
                            case ImageTypes.Int16:
                                return Dlib.ExtractImageChip<short>(imageObj, chip);
                            case ImageTypes.Int32:
                                return Dlib.ExtractImageChip<int>(imageObj, chip);
                            case ImageTypes.HsiPixel:
                                return Dlib.ExtractImageChip<HsiPixel>(imageObj, chip);
                            case ImageTypes.Float:
                                return Dlib.ExtractImageChip<float>(imageObj, chip);
                            case ImageTypes.Double:
                                return Dlib.ExtractImageChip<double>(imageObj, chip);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
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

        #endregion

        #region FlipImageLeftRight

        [TestMethod]
        public void FlipImageLeftRight()
        {
            const string testName = "FlipImageLeftRight";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
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
            const string testName = "PyramidUp";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
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

        #endregion

        #region ResizeImage

        [TestMethod]
        public void ResizeImage()
        {
            const string testName = "ResizeImage";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
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
                foreach (var size in new[] { 128, 512 })
                    foreach (var input in tests)
                    {
                        var expectResult = input.ExpectResult;
                        var imageObj = DlibTest.LoadImage(input.Type, path);
                        var outputObj = Array2DTest.CreateArray2D(input.Type, size, size);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            Dlib.ResizeImage(imageObj, outputObj, itype);
                            return outputObj;
                        });

                        var successAction = new Action<Array2DBase>(image =>
                        {
                            Dlib.SaveJpeg(outputObj, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{input.Type}_{size}_{itype}.jpg")}");
                        });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw exception for InputType: {input.Type}, OutputType: {input.Type}, Size: {size}, InterpolationType: {itype}.");
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
                            Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {input.Type}, Size: {size}, InterpolationType: {itype}.");
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
