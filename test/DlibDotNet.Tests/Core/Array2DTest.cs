using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Core
{

    [TestClass]
    public class Array2DTest : TestBase
    {

        [TestMethod]
        public void CreateArray2D()
        {
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

            foreach (var test in tests)
            {
                var cols = this.NextRandom(1, 100);
                var rows = this.NextRandom(1, 100);
                var array = CreateArray2D(test.Type, rows, cols);
                Assert.AreEqual(array.Columns, cols);
                Assert.AreEqual(array.Rows, rows);
                Assert.AreEqual(array.Size, cols * rows);
                using (var rect = array.Rect)
                {
                    Assert.AreEqual(rect.Width, (uint)cols);
                    Assert.AreEqual(rect.Height, (uint)rows);
                    Assert.AreEqual(rect.Left, 0);
                    Assert.AreEqual(rect.Top, 0);
                }
                this.DisposeAndCheckDisposedState(array);
            }
        }

        [TestMethod]
        public void CreateArray2D2()
        {
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

            foreach (var test in tests)
            {
                var array = CreateArray2D(test.Type);
                this.DisposeAndCheckDisposedState(array);
            }
        }

        [TestMethod]
        public void CreateArray2DMatrix()
        {
            foreach (MatrixElementTypes test in Enum.GetValues(typeof(MatrixElementTypes)))
            {
                var cols = this.NextRandom(1, 100);
                var rows = this.NextRandom(1, 100);
                var array2DMatrix = CreateArray2DMatrix(test, rows, cols);
                Assert.AreEqual(array2DMatrix.Columns, cols);
                Assert.AreEqual(array2DMatrix.Rows, rows);
                Assert.AreEqual(array2DMatrix.Size, cols * rows);
                using (var rect = array2DMatrix.Rect)
                {
                    Assert.AreEqual(rect.Width, (uint)cols);
                    Assert.AreEqual(rect.Height, (uint)rows);
                    Assert.AreEqual(rect.Left, 0);
                    Assert.AreEqual(rect.Top, 0);
                }
                this.DisposeAndCheckDisposedState(array2DMatrix);
            }
        }

        [TestMethod]
        public void CreateArray2DMatrix2()
        {
            foreach (MatrixElementTypes test in Enum.GetValues(typeof(MatrixElementTypes)))
            {
                var array2DMatrix = CreateArray2DMatrix(test);
                this.DisposeAndCheckDisposedState(array2DMatrix);
            }
        }

        [TestMethod]
        public void GetRowColumn()
        {
            const int width = 150;
            const int height = 100;

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var array2D = CreateArray2D(test.Type, height, width);
                switch (array2D.ImageType)
                {
                    case ImageTypes.UInt8:
                        {
                            var array = (Array2D<byte>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.AreEqual(row[0], 255, "Array<byte> failed");
                        }
                        break;
                    case ImageTypes.UInt16:
                        {
                            var array = (Array2D<ushort>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.AreEqual(row[0], 255, "Array<ushort> failed");
                        }
                        break;
                    case ImageTypes.Int32:
                        {
                            var array = (Array2D<int>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.AreEqual(row[0], 255, "Array<int> failed");
                        }
                        break;
                    case ImageTypes.Float:
                        {
                            var array = (Array2D<float>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.AreEqual(row[0], 255, "Array<float> failed");
                        }
                        break;
                    case ImageTypes.Double:
                        {
                            var array = (Array2D<double>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.AreEqual(row[0], 255, "Array<double> failed");
                        }
                        break;
                    case ImageTypes.RgbPixel:
                        {
                            var array = (Array2D<RgbPixel>)array2D;
                            var pixel = new RgbPixel
                            {
                                Red = 255,
                                Blue = 255,
                                Green = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                var t = row[0];
                                Assert.AreEqual(t.Red, 255, "Array<RgbPixel> failed");
                                Assert.AreEqual(t.Blue, 255, "Array<RgbPixel> failed");
                                Assert.AreEqual(t.Green, 255, "Array<RgbPixel> failed");
                            }
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        {
                            var array = (Array2D<RgbAlphaPixel>)array2D;
                            var pixel = new RgbAlphaPixel
                            {
                                Red = 255,
                                Blue = 255,
                                Green = 255,
                                Alpha = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                var t = row[0];
                                Assert.AreEqual(t.Red, 255, "Array<RgbAlphaPixel> failed");
                                Assert.AreEqual(t.Blue, 255, "Array<RgbAlphaPixel> failed");
                                Assert.AreEqual(t.Green, 255, "Array<RgbAlphaPixel> failed");
                                Assert.AreEqual(t.Alpha, 255, "Array<RgbAlphaPixel> failed");
                            }
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        {
                            var array = (Array2D<HsiPixel>)array2D;
                            var pixel = new HsiPixel
                            {
                                H = 255,
                                S = 255,
                                I = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                var t = row[0];
                                Assert.AreEqual(t.H, 255, "Array<HsiPixel> failed");
                                Assert.AreEqual(t.S, 255, "Array<HsiPixel> failed");
                                Assert.AreEqual(t.I, 255, "Array<HsiPixel> failed");
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(array2D.ImageType), array2D.ImageType, null);
                }
                this.DisposeAndCheckDisposedState(array2D);
            }
        }

        [TestMethod]
        public void SetRowColumn()
        {
            const int width = 150;
            const int height = 100;

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var array2D = CreateArray2D(test.Type, height, width);
                switch (array2D.ImageType)
                {
                    case ImageTypes.UInt8:
                        {
                            var array = (Array2D<byte>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.AreEqual(row[50], 0, "Array<byte> failed");
                            }
                        }
                        break;
                    case ImageTypes.UInt16:
                        {
                            var array = (Array2D<ushort>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.AreEqual(row[50], 0, "Array<ushort> failed");
                            }
                        }
                        break;
                    case ImageTypes.Int32:
                        {
                            var array = (Array2D<int>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.AreEqual(row[50], 0, "Array<int> failed");
                            }
                        }
                        break;
                    case ImageTypes.Float:
                        {
                            var array = (Array2D<float>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.AreEqual(row[50], 0, "Array<float> failed");
                            }
                        }
                        break;
                    case ImageTypes.Double:
                        {
                            var array = (Array2D<double>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.AreEqual(row[50], 0, "Array<double> failed");
                            }
                        }
                        break;
                    case ImageTypes.RgbPixel:
                        {
                            var array = (Array2D<RgbPixel>)array2D;
                            var pixel = new RgbPixel
                            {
                                Red = 255,
                                Blue = 255,
                                Green = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                row[50] = new RgbPixel
                                {
                                    Red = 100,
                                    Green = 128,
                                    Blue = 64
                                };
                                var t = row[50];
                                Assert.AreEqual(t.Red, 100, "Array<RgbPixel> failed");
                                Assert.AreEqual(t.Green, 128, "Array<RgbPixel> failed");
                                Assert.AreEqual(t.Blue, 64, "Array<RgbPixel> failed");
                            }
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        {
                            var array = (Array2D<RgbAlphaPixel>)array2D;
                            var pixel = new RgbAlphaPixel
                            {
                                Red = 255,
                                Blue = 255,
                                Green = 255,
                                Alpha = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                row[50] = new RgbAlphaPixel
                                {
                                    Red = 100,
                                    Green = 128,
                                    Blue = 64,
                                    Alpha = 0
                                };

                                var t = row[50];
                                Assert.AreEqual(t.Red, 100, "Array<RgbAlphaPixel> failed");
                                Assert.AreEqual(t.Green, 128, "Array<RgbPixel> failed");
                                Assert.AreEqual(t.Blue, 64, "Array<RgbPixel> failed");
                                Assert.AreEqual(t.Alpha, 0, "Array<RgbAlphaPixel> failed");
                            }
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        {
                            var array = (Array2D<HsiPixel>)array2D;
                            var pixel = new HsiPixel
                            {
                                H = 255,
                                S = 255,
                                I = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                row[50] = new HsiPixel
                                {
                                    H = 100,
                                    S = 128,
                                    I = 64
                                };

                                var t = row[50];
                                Assert.AreEqual(t.H, 100, "Array<HsiPixel> failed");
                                Assert.AreEqual(t.S, 128, "Array<HsiPixel> failed");
                                Assert.AreEqual(t.I, 64, "Array<HsiPixel> failed");
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(array2D.ImageType), array2D.ImageType, null);
                }
                this.DisposeAndCheckDisposedState(array2D);
            }
        }

        internal static Array2DBase CreateArray2D(ImageTypes elementType)
        {
            switch (elementType)
            {
                case ImageTypes.UInt8:
                    return new Array2D<byte>();
                case ImageTypes.UInt16:
                    return new Array2D<ushort>();
                case ImageTypes.Int32:
                    return new Array2D<int>();
                case ImageTypes.Float:
                    return new Array2D<float>();
                case ImageTypes.Double:
                    return new Array2D<double>();
                case ImageTypes.RgbPixel:
                    return new Array2D<RgbPixel>();
                case ImageTypes.RgbAlphaPixel:
                    return new Array2D<RgbAlphaPixel>();
                case ImageTypes.HsiPixel:
                    return new Array2D<HsiPixel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        internal static Array2DBase CreateArray2D(ImageTypes elementType, int rows, int columns)
        {
            switch (elementType)
            {
                case ImageTypes.UInt8:
                    return new Array2D<byte>(rows, columns);
                case ImageTypes.UInt16:
                    return new Array2D<ushort>(rows, columns);
                case ImageTypes.Int32:
                    return new Array2D<int>(rows, columns);
                case ImageTypes.Float:
                    return new Array2D<float>(rows, columns);
                case ImageTypes.Double:
                    return new Array2D<double>(rows, columns);
                case ImageTypes.RgbPixel:
                    return new Array2D<RgbPixel>(rows, columns);
                case ImageTypes.RgbAlphaPixel:
                    return new Array2D<RgbAlphaPixel>(rows, columns);
                case ImageTypes.HsiPixel:
                    return new Array2D<HsiPixel>(rows, columns);
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        internal static Array2DMatrixBase CreateArray2DMatrix(MatrixElementTypes elementType)
        {
            switch (elementType)
            {
                case MatrixElementTypes.UInt8:
                    return new Array2DMatrix<byte>();
                case MatrixElementTypes.UInt16:
                    return new Array2DMatrix<ushort>();
                case MatrixElementTypes.UInt32:
                    return new Array2DMatrix<uint>();
                case MatrixElementTypes.Int8:
                    return new Array2DMatrix<sbyte>();
                case MatrixElementTypes.Int16:
                    return new Array2DMatrix<short>();
                case MatrixElementTypes.Int32:
                    return new Array2DMatrix<int>();
                case MatrixElementTypes.Float:
                    return new Array2DMatrix<float>();
                case MatrixElementTypes.Double:
                    return new Array2DMatrix<double>();
                case MatrixElementTypes.RgbPixel:
                    return new Array2DMatrix<RgbPixel>();
                case MatrixElementTypes.RgbAlphaPixel:
                    return new Array2DMatrix<RgbAlphaPixel>();
                case MatrixElementTypes.HsiPixel:
                    return new Array2DMatrix<HsiPixel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        internal static Array2DMatrixBase CreateArray2DMatrix(MatrixElementTypes elementType, int rows, int columns)
        {
            switch (elementType)
            {
                case MatrixElementTypes.UInt8:
                    return new Array2DMatrix<byte>(rows, columns);
                case MatrixElementTypes.UInt16:
                    return new Array2DMatrix<ushort>(rows, columns);
                case MatrixElementTypes.UInt32:
                    return new Array2DMatrix<uint>(rows, columns);
                case MatrixElementTypes.Int8:
                    return new Array2DMatrix<sbyte>(rows, columns);
                case MatrixElementTypes.Int16:
                    return new Array2DMatrix<short>(rows, columns);
                case MatrixElementTypes.Int32:
                    return new Array2DMatrix<int>(rows, columns);
                case MatrixElementTypes.Float:
                    return new Array2DMatrix<float>(rows, columns);
                case MatrixElementTypes.Double:
                    return new Array2DMatrix<double>(rows, columns);
                case MatrixElementTypes.RgbPixel:
                    return new Array2DMatrix<RgbPixel>(rows, columns);
                case MatrixElementTypes.RgbAlphaPixel:
                    return new Array2DMatrix<RgbAlphaPixel>(rows, columns);
                case MatrixElementTypes.HsiPixel:
                    return new Array2DMatrix<HsiPixel>(rows, columns);
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

    }

}
