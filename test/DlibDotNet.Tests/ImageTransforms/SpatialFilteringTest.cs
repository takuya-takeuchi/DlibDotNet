using System;
using System.IO;
using DlibDotNet.Tests.Array2D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class SpatialFilteringTest : TestBase
    {
        private const string LoadTarget = "Lenna_mini";

        #region GaussianBlur

        [TestMethod]
        public void GaussianBlur()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
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
                    TwoDimentionObjectBase outObj = null;
                    TwoDimentionObjectBase inObj = null;

                    var expect = input.ExpectResult && output.ExpectResult;

                    try
                    {
                        var inIage = DlibTest.LoadImage(input.Type, path);
                        inObj = inIage;

                        try
                        {
                            var outImage = Array2DTest.CreateArray2D(output.Type);
                            outObj = outImage;

                            Dlib.GaussianBlur(inIage, outImage);

                            if (!expect)
                            {
                                Assert.Fail($"GaussianBlur should throw excption for InputType: {input.Type}, OutputType: {output.Type}");
                            }
                            else
                            {
                                Assert.AreEqual(inIage.Columns, outImage.Columns);
                                Assert.AreEqual(inIage.Rows, outImage.Rows);

                                Dlib.SaveBmp(outImage, $"{Path.Combine(this.GetOutDir(type, "GaussianBlur"), $"{LoadTarget}_{input.Type}_{output.Type}.bmp")}");
                            }
                        }
                        catch (ArgumentException)
                        {
                            if (!expect)
                            {
                                Console.WriteLine("OK");
                            }
                            else
                            {
                                throw;
                            }
                        }
                        catch (NotSupportedException)
                        {
                            if (!expect)
                            {
                                Console.WriteLine("OK");
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to execute GaussianBlur to InputType: {input.Type}, OutputType: {output.Type}");
                        throw;
                    }
                    finally
                    {
                        if (outObj != null)
                            this.DisposeAndCheckDisposedState(outObj);
                        if (inObj != null)
                            this.DisposeAndCheckDisposedState(inObj);
                    }
                }
        }

        [TestMethod]
        public void GaussianBlurThrowException()
        {
            var path = this.GetDataFile($"{ LoadTarget}.bmp");
            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.UInt8,         ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.UInt16,        ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.UInt32,        ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.Int8,          ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.Int16,         ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.Int32,         ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.Float,         ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.Double,        ExpectResult = false,  Sigma = 0, MaxSize = 1},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.UInt8,         ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.UInt16,        ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.UInt32,        ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.Int8,          ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.Int16,         ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.Int32,         ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.Float,         ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.Double,        ExpectResult = false,  Sigma = 10, MaxSize = 0},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.UInt8,         ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.UInt16,        ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.UInt32,        ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.Int8,          ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.Int16,         ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.Int32,         ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.Float,         ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.Double,        ExpectResult = false,  Sigma = 10, MaxSize = 2},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true, Sigma = 10, MaxSize = 1001},
                //new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.UInt8,         ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.UInt16,        ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.UInt32,        ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.Int8,          ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.Int16,         ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.Int32,         ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.Float,         ExpectResult = true, Sigma = 10, MaxSize = 1001},
                new { Type = ImageTypes.Double,        ExpectResult = true, Sigma = 10, MaxSize = 1001}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                var outImage = Array2DTest.CreateArray2D(test.Type);
                var inIage = DlibTest.LoadImage(test.Type, path);

                try
                {
                    Dlib.GaussianBlur(inIage, outImage, test.Sigma, test.MaxSize);

                    if (!test.ExpectResult)
                    {
                        Assert.Fail($"GaussianBlur should throw excption for Type: {test.Type}, Sigma: {test.Sigma}, MaxSize: {test.MaxSize}.");
                    }

                    Dlib.SaveJpeg(outImage, $"{Path.Combine(this.GetOutDir(type, "GaussianBlurThrowException"), $"{LoadTarget}_{test.Type}_{test.Sigma}_{test.MaxSize}.jpg")}");
                }
                catch (Exception)
                {
                    if (!test.ExpectResult)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    if (outImage != null)
                        this.DisposeAndCheckDisposedState(outImage);
                    if (inIage != null)
                        this.DisposeAndCheckDisposedState(inIage);
                }
            }
        }

        #endregion

        #region SumFilter

        [TestMethod]
        public void SumFilter()
        {
            const string testName = "SumFilter";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false}
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
                foreach (var output in tests)
                {
                    TwoDimentionObjectBase outObj = null;
                    TwoDimentionObjectBase inObj = null;
                    var rect = new Rectangle(3, 3);

                    var expect = input.ExpectResult && output.ExpectResult;

                    try
                    {
                        var inImage = DlibTest.LoadImage(input.Type, path);
                        inObj = inImage;

                        try
                        {
                            var outImage = Array2DTest.CreateArray2D(output.Type, inImage.Rows, inImage.Columns);
                            outObj = outImage;

                            Dlib.SumFilter(inImage, outImage, rect);

                            if (!expect)
                            {
                                Assert.Fail($"{testName} should throw excption for InputType: {input.Type}, OutputType: {output.Type}");
                            }
                            else
                            {
                                Assert.AreEqual(inImage.Columns, outImage.Columns);
                                Assert.AreEqual(inImage.Rows, outImage.Rows);

                                Dlib.SaveBmp(outImage, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}.bmp")}");
                            }
                        }
                        catch (ArgumentException)
                        {
                            if (!expect)
                            {
                                Console.WriteLine("OK");
                            }
                            else
                            {
                                throw;
                            }
                        }
                        catch (NotSupportedException)
                        {
                            if (!expect)
                            {
                                Console.WriteLine("OK");
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, OutputType: {output.Type}");
                        throw;
                    }
                    finally
                    {
                        if (outObj != null)
                            this.DisposeAndCheckDisposedState(outObj);
                        if (inObj != null)
                            this.DisposeAndCheckDisposedState(inObj);
                    }
                }
        }

        #endregion

    }

}
