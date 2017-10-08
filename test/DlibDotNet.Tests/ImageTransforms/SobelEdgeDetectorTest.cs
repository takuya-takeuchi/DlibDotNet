using System;
using System.IO;
using DlibDotNet.Tests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class SobelEdgeDetectorTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        #region SobelEdgeDetector

        [TestMethod]
        public void SobelEdgeDetector()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.UInt8,         ExpectResult = false},
                new { Type = ImageTypes.UInt16,        ExpectResult = false},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (ImageTypes inputType in Enum.GetValues(typeof(ImageTypes)))
            {
                if (inputType == ImageTypes.Matrix)
                    continue;

                foreach (var test in tests)
                {
                    TwoDimentionObjectBase imageObj = null;
                    TwoDimentionObjectBase horzObj = null;
                    TwoDimentionObjectBase vertObj = null;

                    try
                    {
                        var image = DlibTest.LoadImage(inputType, path);
                        imageObj = image;
                        var horz = Array2DTest.CreateArray2D(test.Type);
                        horzObj = horz;
                        var vert = Array2DTest.CreateArray2D(test.Type);
                        vertObj = vert;

                        try
                        {
                            Dlib.SobelEdgeDetector(image, horz, vert);

                            if (!test.ExpectResult)
                            {
                                Assert.Fail($"SobelEdgeDetector should throw excption for InputType: {inputType}, Type: {test.Type}.");
                            }
                            else
                            {
                                Dlib.SaveBmp(horz, $"{Path.Combine(this.GetOutDir(type, "SobelEdgeDetector"), $"{LoadTarget}_{inputType}_{test.Type}_horz.bmp")}");
                                Dlib.SaveBmp(vert, $"{Path.Combine(this.GetOutDir(type, "SobelEdgeDetector"), $"{LoadTarget}_{inputType}_{test.Type}_vert.bmp")}");
                            }
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
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to execute SobelEdgeDetector to InputType: {inputType}, Type: {test.Type}.");
                        throw;
                    }
                    finally
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                        if (horzObj != null)
                            this.DisposeAndCheckDisposedState(horzObj);
                        if (vertObj != null)
                            this.DisposeAndCheckDisposedState(vertObj);
                    }
                }
            }
        }

        #endregion

        #region SuppressNonMaximumEdges

        [TestMethod]
        public void SuppressNonMaximumEdges()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var inputType in new[] { ImageTypes.Float, ImageTypes.Double })
            {
                foreach (var test in tests)
                {
                    Array2DBase inputObj = null;
                    Array2DBase horzObj = null;
                    Array2DBase vertObj = null;
                    Array2DBase outputObj = null;

                    try
                    {
                        var inputImage = DlibTest.LoadImage(inputType, path);
                        inputObj = inputImage;
                        var horz = Array2DTest.CreateArray2D(inputType);
                        horzObj = horz;
                        var vert = Array2DTest.CreateArray2D(inputType);
                        vertObj = vert;
                        var outputImage = Array2DTest.CreateArray2D(test.Type);
                        outputObj = outputImage;

                        Dlib.SobelEdgeDetector(inputImage, horz, vert);

                        try
                        {
                            Dlib.SuppressNonMaximumEdges(horz, vert, outputImage);

                            if (!test.ExpectResult)
                            {
                                Assert.Fail($"SuppressNonMaximumEdges should throw excption for InputType: {inputType}, Type: {test.Type}.");
                            }
                            else
                            {
                                Dlib.SaveBmp(outputImage, $"{Path.Combine(this.GetOutDir(type, "SuppressNonMaximumEdges"), $"{LoadTarget}_{inputType}_{test.Type}.bmp")}");
                            }
                        }
                        catch (ArgumentException)
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
                        catch (NotSupportedException)
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
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to execute SuppressNonMaximumEdges to InputType: {inputType}, Type: {test.Type}.");
                        throw;
                    }
                    finally
                    {
                        if (inputObj != null)
                            this.DisposeAndCheckDisposedState(inputObj);
                        if (horzObj != null)
                            this.DisposeAndCheckDisposedState(horzObj);
                        if (vertObj != null)
                            this.DisposeAndCheckDisposedState(vertObj);
                        if (outputObj != null)
                            this.DisposeAndCheckDisposedState(outputObj);
                    }
                }
            }
        }

        #endregion

    }

}
