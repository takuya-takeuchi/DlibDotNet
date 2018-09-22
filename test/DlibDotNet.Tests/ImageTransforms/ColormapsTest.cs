using System;
using System.IO;
using DlibDotNet.Tests.Array2D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class ColormapsTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        private const int LoadTargetWidth = 256;

        private const int LoadTargetHeight = 256;

        #region Heatmap

        [TestMethod]
        public void Heatmap()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false },
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false },
                new { Type = ImageTypes.UInt8,         ExpectResult = true },
                new { Type = ImageTypes.UInt16,        ExpectResult = true },
                new { Type = ImageTypes.UInt32,        ExpectResult = true },
                new { Type = ImageTypes.Int8,          ExpectResult = true },
                new { Type = ImageTypes.Int16,         ExpectResult = true },
                new { Type = ImageTypes.Int32,         ExpectResult = true },
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false },
                new { Type = ImageTypes.Float,         ExpectResult = true },
                new { Type = ImageTypes.Double,        ExpectResult = true }
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                Array2DBase imageObj = null;
                Array2DBase horzObj = null;
                Array2DBase vertObj = null;
                Array2DBase outputImageObj = null;
                MatrixOp matrix = null;
                DlibObject windowObj = null;

                try
                {
                    const ImageTypes inputType = ImageTypes.Float;

                    var image = DlibTest.LoadImage(test.Type, path);
                    imageObj = image;
                    var horz = Array2DTest.CreateArray2D(inputType);
                    horzObj = horz;
                    var vert = Array2DTest.CreateArray2D(inputType);
                    vertObj = vert;
                    var outputImage = Array2DTest.CreateArray2D(test.Type);
                    outputImageObj = outputImage;

                    Dlib.SobelEdgeDetector(image, horz, vert);
                    Dlib.SuppressNonMaximumEdges(horz, vert,outputImage);

                    try
                    {
                        matrix = Heatmap(test.Type, outputImage);

                        if (test.ExpectResult)
                        {
                            Assert.AreEqual(matrix.Columns, LoadTargetWidth);
                            Assert.AreEqual(matrix.Rows, LoadTargetHeight);

                            if (this.CanGuiDebug)
                            {
                                var window = new ImageWindow(matrix, $"{test.Type}");
                                windowObj = window;
                            }

                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "Heatmap"), $"{LoadTarget}_{test.Type}.bmp")}");
                        }
                        else
                        {
                            Assert.Fail($"Failed to execute Heatmap to Type: {test.Type}");
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
                    Console.WriteLine($"Failed to execute Heatmap to Type: {test.Type}");
                    throw;
                }
                finally
                {
                    if (outputImageObj != null)
                        this.DisposeAndCheckDisposedState(outputImageObj);
                    if (vertObj != null)
                        this.DisposeAndCheckDisposedState(vertObj);
                    if (horzObj != null)
                        this.DisposeAndCheckDisposedState(horzObj);
                    if (windowObj != null)
                        this.DisposeAndCheckDisposedState(windowObj);
                    if (matrix != null)
                        this.DisposeAndCheckDisposedState(matrix);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                }
            }
        }

        [TestMethod]
        public void Heatmap2()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false,  Max = 255,  Min = 0},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false,  Max = 255,  Min = 0},
                new { Type = ImageTypes.UInt8,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.UInt16,        ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.UInt32,        ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Int8,          ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Int16,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Int32,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false,  Max = 255,  Min = 0},
                new { Type = ImageTypes.Float,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Double,        ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false,  Max = 75,   Min = 50},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false,  Max = 75,   Min = 50},
                new { Type = ImageTypes.UInt8,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.UInt16,        ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.UInt32,        ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Int8,          ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Int16,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Int32,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false,  Max = 75,   Min = 50},
                new { Type = ImageTypes.Float,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Double,        ExpectResult = true,   Max = 75,   Min = 50}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                Array2DBase imageObj = null;
                Array2DBase horzObj = null;
                Array2DBase vertObj = null;
                Array2DBase outputImageObj = null;
                MatrixOp matrix = null;
                DlibObject windowObj = null;

                try
                {
                    const ImageTypes inputType = ImageTypes.Float;

                    var image = DlibTest.LoadImage(test.Type, path);
                    imageObj = image;
                    var horz = Array2DTest.CreateArray2D(inputType);
                    horzObj = horz;
                    var vert = Array2DTest.CreateArray2D(inputType);
                    vertObj = vert;
                    var outputImage = Array2DTest.CreateArray2D(test.Type);
                    outputImageObj = outputImage;

                    Dlib.SobelEdgeDetector(image, horz, vert);
                    Dlib.SuppressNonMaximumEdges(horz, vert, outputImage);

                    try
                    {
                        matrix = Heatmap(test.Type, outputImage, test.Max, test.Min);

                        if (test.ExpectResult)
                        {
                            if (this.CanGuiDebug)
                            {
                                var window = new ImageWindow(matrix, $"{test.Type} - Max: {test.Max}, Min : {test.Min}");
                                windowObj = window;
                            }

                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "Heatmap2"), $"{LoadTarget}_{test.Type}_{test.Max}_{test.Min}.bmp")}");
                        }
                        else
                        {
                            Assert.Fail($"Failed to execute Heatmap2 to Type: {test.Type}");
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
                    Console.WriteLine($"Failed to execute Heatmap2 to Type: {test.Type}");
                    throw;
                }
                finally
                {
                    if (outputImageObj != null)
                        this.DisposeAndCheckDisposedState(outputImageObj);
                    if (vertObj != null)
                        this.DisposeAndCheckDisposedState(vertObj);
                    if (horzObj != null)
                        this.DisposeAndCheckDisposedState(horzObj);
                    if (windowObj != null)
                        this.DisposeAndCheckDisposedState(windowObj);
                    if (matrix != null)
                        this.DisposeAndCheckDisposedState(matrix);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                }
            }
        }

        #endregion

        #region Jet

        [TestMethod]
        public void Jet()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false },
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false },
                new { Type = ImageTypes.UInt8,         ExpectResult = true },
                new { Type = ImageTypes.UInt16,        ExpectResult = true },
                new { Type = ImageTypes.UInt32,        ExpectResult = true },
                new { Type = ImageTypes.Int8,          ExpectResult = true },
                new { Type = ImageTypes.Int16,         ExpectResult = true },
                new { Type = ImageTypes.Int32,         ExpectResult = true },
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false },
                new { Type = ImageTypes.Float,         ExpectResult = true },
                new { Type = ImageTypes.Double,        ExpectResult = true }
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                Array2DBase imageObj = null;
                Array2DBase horzObj = null;
                Array2DBase vertObj = null;
                Array2DBase outputImageObj = null;
                MatrixOp matrix = null;
                DlibObject windowObj = null;

                try
                {
                    const ImageTypes inputType = ImageTypes.Float;

                    var image = DlibTest.LoadImage(test.Type, path);
                    imageObj = image;
                    var horz = Array2DTest.CreateArray2D(inputType);
                    horzObj = horz;
                    var vert = Array2DTest.CreateArray2D(inputType);
                    vertObj = vert;
                    var outputImage = Array2DTest.CreateArray2D(test.Type);
                    outputImageObj = outputImage;

                    Dlib.SobelEdgeDetector(image, horz, vert);
                    Dlib.SuppressNonMaximumEdges(horz, vert, outputImage);

                    try
                    {
                        matrix = Jet(test.Type, outputImage);

                        if (test.ExpectResult)
                        {
                            Assert.AreEqual(matrix.Columns, LoadTargetWidth);
                            Assert.AreEqual(matrix.Rows, LoadTargetHeight);

                            if (this.CanGuiDebug)
                            {
                                var window = new ImageWindow(matrix, $"{test.Type}");
                                windowObj = window;
                            }

                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "Jet"), $"{LoadTarget}_{test.Type}.bmp")}");
                        }
                        else
                        {
                            Assert.Fail($"Failed to execute Jet to Type: {test.Type}");
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
                    Console.WriteLine($"Failed to execute Jet to Type: {test.Type}");
                    throw;
                }
                finally
                {
                    if (outputImageObj != null)
                        this.DisposeAndCheckDisposedState(outputImageObj);
                    if (vertObj != null)
                        this.DisposeAndCheckDisposedState(vertObj);
                    if (horzObj != null)
                        this.DisposeAndCheckDisposedState(horzObj);
                    if (windowObj != null)
                        this.DisposeAndCheckDisposedState(windowObj);
                    if (matrix != null)
                        this.DisposeAndCheckDisposedState(matrix);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                }
            }
        }

        [TestMethod]
        public void Jet2()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false,  Max = 255,  Min = 0},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false,  Max = 255,  Min = 0},
                new { Type = ImageTypes.UInt8,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.UInt16,        ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.UInt32,        ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Int8,          ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Int16,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Int32,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false,  Max = 255,  Min = 0},
                new { Type = ImageTypes.Float,         ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.Double,        ExpectResult = true,   Max = 255,  Min = 0},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false,  Max = 75,   Min = 50},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false,  Max = 75,   Min = 50},
                new { Type = ImageTypes.UInt8,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.UInt16,        ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.UInt32,        ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Int8,          ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Int16,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Int32,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false,  Max = 75,   Min = 50},
                new { Type = ImageTypes.Float,         ExpectResult = true,   Max = 75,   Min = 50},
                new { Type = ImageTypes.Double,        ExpectResult = true,   Max = 75,   Min = 50}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                Array2DBase imageObj = null;
                Array2DBase horzObj = null;
                Array2DBase vertObj = null;
                Array2DBase outputImageObj = null;
                MatrixOp matrix = null;
                DlibObject windowObj = null;

                try
                {
                    const ImageTypes inputType = ImageTypes.Float;

                    var image = DlibTest.LoadImage(test.Type, path);
                    imageObj = image;
                    var horz = Array2DTest.CreateArray2D(inputType);
                    horzObj = horz;
                    var vert = Array2DTest.CreateArray2D(inputType);
                    vertObj = vert;
                    var outputImage = Array2DTest.CreateArray2D(test.Type);
                    outputImageObj = outputImage;

                    Dlib.SobelEdgeDetector(image, horz, vert);
                    Dlib.SuppressNonMaximumEdges(horz, vert, outputImage);

                    try
                    {
                        matrix = Jet(test.Type, outputImage, test.Max, test.Min);

                        if (test.ExpectResult)
                        {
                            if (this.CanGuiDebug)
                            {
                                var window = new ImageWindow(matrix, $"{test.Type} - Max: {test.Max}, Min : {test.Min}");
                                windowObj = window;
                            }

                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "Jet2"), $"{LoadTarget}_{test.Type}_{test.Max}_{test.Min}.bmp")}");
                        }
                        else
                        {
                            Assert.Fail($"Failed to execute Jet2 to Type: {test.Type}");
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
                    Console.WriteLine($"Failed to execute Jet2 to Type: {test.Type}");
                    throw;
                }
                finally
                {
                    if (outputImageObj != null)
                        this.DisposeAndCheckDisposedState(outputImageObj);
                    if (vertObj != null)
                        this.DisposeAndCheckDisposedState(vertObj);
                    if (horzObj != null)
                        this.DisposeAndCheckDisposedState(horzObj);
                    if (windowObj != null)
                        this.DisposeAndCheckDisposedState(windowObj);
                    if (matrix != null)
                        this.DisposeAndCheckDisposedState(matrix);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                }
            }
        }

        #endregion

        internal static MatrixOp Heatmap(ImageTypes type, TwoDimensionObjectBase obj)
        {
            MatrixOp matrixOp;
            switch (type)
            {
                case ImageTypes.RgbPixel:
                    matrixOp = Dlib.Heatmap(obj as Array2D<RgbPixel>);
                    break;
                case ImageTypes.RgbAlphaPixel:
                    matrixOp = Dlib.Heatmap(obj as Array2D<RgbAlphaPixel>);
                    break;
                case ImageTypes.UInt8:
                    matrixOp = Dlib.Heatmap(obj as Array2D<byte>);
                    break;
                case ImageTypes.UInt16:
                    matrixOp = Dlib.Heatmap(obj as Array2D<ushort>);
                    break;
                case ImageTypes.UInt32:
                    matrixOp = Dlib.Heatmap(obj as Array2D<uint>);
                    break;
                case ImageTypes.Int8:
                    matrixOp = Dlib.Heatmap(obj as Array2D<sbyte>);
                    break;
                case ImageTypes.Int16:
                    matrixOp = Dlib.Heatmap(obj as Array2D<short>);
                    break;
                case ImageTypes.Int32:
                    matrixOp = Dlib.Heatmap(obj as Array2D<int>);
                    break;
                case ImageTypes.HsiPixel:
                    matrixOp = Dlib.Heatmap(obj as Array2D<HsiPixel>);
                    break;
                case ImageTypes.Float:
                    matrixOp = Dlib.Heatmap(obj as Array2D<float>);
                    break;
                case ImageTypes.Double:
                    matrixOp = Dlib.Heatmap(obj as Array2D<double>);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return matrixOp;
        }

        internal static MatrixOp Heatmap(ImageTypes type, TwoDimensionObjectBase obj, double max, double min = 0)
        {
            MatrixOp matrixOp;
            switch (type)
            {
                case ImageTypes.RgbPixel:
                    matrixOp = Dlib.Heatmap(obj as Array2D<RgbPixel>, max, min);
                    break;
                case ImageTypes.RgbAlphaPixel:
                    matrixOp = Dlib.Heatmap(obj as Array2D<RgbAlphaPixel>, max, min);
                    break;
                case ImageTypes.UInt8:
                    matrixOp = Dlib.Heatmap(obj as Array2D<byte>, max, min);
                    break;
                case ImageTypes.UInt16:
                    matrixOp = Dlib.Heatmap(obj as Array2D<ushort>, max, min);
                    break;
                case ImageTypes.UInt32:
                    matrixOp = Dlib.Heatmap(obj as Array2D<uint>, max, min);
                    break;
                case ImageTypes.Int8:
                    matrixOp = Dlib.Heatmap(obj as Array2D<sbyte>, max, min);
                    break;
                case ImageTypes.Int16:
                    matrixOp = Dlib.Heatmap(obj as Array2D<short>, max, min);
                    break;
                case ImageTypes.Int32:
                    matrixOp = Dlib.Heatmap(obj as Array2D<int>, max, min);
                    break;
                case ImageTypes.HsiPixel:
                    matrixOp = Dlib.Heatmap(obj as Array2D<HsiPixel>, max, min);
                    break;
                case ImageTypes.Float:
                    matrixOp = Dlib.Heatmap(obj as Array2D<float>, max, min);
                    break;
                case ImageTypes.Double:
                    matrixOp = Dlib.Heatmap(obj as Array2D<double>, max, min);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return matrixOp;
        }

        internal static MatrixOp Jet(ImageTypes type, TwoDimensionObjectBase obj)
        {
            MatrixOp matrixOp;
            switch (type)
            {
                case ImageTypes.RgbPixel:
                    matrixOp = Dlib.Jet(obj as Array2D<RgbPixel>);
                    break;
                case ImageTypes.RgbAlphaPixel:
                    matrixOp = Dlib.Jet(obj as Array2D<RgbAlphaPixel>);
                    break;
                case ImageTypes.UInt8:
                    matrixOp = Dlib.Jet(obj as Array2D<byte>);
                    break;
                case ImageTypes.UInt16:
                    matrixOp = Dlib.Jet(obj as Array2D<ushort>);
                    break;
                case ImageTypes.UInt32:
                    matrixOp = Dlib.Jet(obj as Array2D<uint>);
                    break;
                case ImageTypes.Int8:
                    matrixOp = Dlib.Jet(obj as Array2D<sbyte>);
                    break;
                case ImageTypes.Int16:
                    matrixOp = Dlib.Jet(obj as Array2D<short>);
                    break;
                case ImageTypes.Int32:
                    matrixOp = Dlib.Jet(obj as Array2D<int>);
                    break;
                case ImageTypes.HsiPixel:
                    matrixOp = Dlib.Jet(obj as Array2D<HsiPixel>);
                    break;
                case ImageTypes.Float:
                    matrixOp = Dlib.Jet(obj as Array2D<float>);
                    break;
                case ImageTypes.Double:
                    matrixOp = Dlib.Jet(obj as Array2D<double>);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return matrixOp;
        }

        internal static MatrixOp Jet(ImageTypes type, TwoDimensionObjectBase obj, double max, double min = 0)
        {
            MatrixOp matrixOp;
            switch (type)
            {
                case ImageTypes.RgbPixel:
                    matrixOp = Dlib.Jet(obj as Array2D<RgbPixel>, max, min);
                    break;
                case ImageTypes.RgbAlphaPixel:
                    matrixOp = Dlib.Jet(obj as Array2D<RgbAlphaPixel>, max, min);
                    break;
                case ImageTypes.UInt8:
                    matrixOp = Dlib.Jet(obj as Array2D<byte>, max, min);
                    break;
                case ImageTypes.UInt16:
                    matrixOp = Dlib.Jet(obj as Array2D<ushort>, max, min);
                    break;
                case ImageTypes.UInt32:
                    matrixOp = Dlib.Jet(obj as Array2D<uint>, max, min);
                    break;
                case ImageTypes.Int8:
                    matrixOp = Dlib.Jet(obj as Array2D<sbyte>, max, min);
                    break;
                case ImageTypes.Int16:
                    matrixOp = Dlib.Jet(obj as Array2D<short>, max, min);
                    break;
                case ImageTypes.Int32:
                    matrixOp = Dlib.Jet(obj as Array2D<int>, max, min);
                    break;
                case ImageTypes.HsiPixel:
                    matrixOp = Dlib.Jet(obj as Array2D<HsiPixel>, max, min);
                    break;
                case ImageTypes.Float:
                    matrixOp = Dlib.Jet(obj as Array2D<float>, max, min);
                    break;
                case ImageTypes.Double:
                    matrixOp = Dlib.Jet(obj as Array2D<double>, max, min);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return matrixOp;
        }

    }

}
