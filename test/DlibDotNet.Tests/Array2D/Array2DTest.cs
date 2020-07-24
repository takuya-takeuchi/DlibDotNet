﻿using System;
using System.Linq;
using DlibDotNet.Tests.Matrix;
using Xunit;

namespace DlibDotNet.Tests.Array2D
{

    public class Array2DTest : TestBase
    {

        [Fact]
        public void CreateArray2D()
        {
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
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var cols = this.NextRandom(1, 100);
                var rows = this.NextRandom(1, 100);
                var array = CreateArray2DHelp(test.Type, rows, cols);
                Assert.Equal(array.Columns, cols);
                Assert.Equal(array.Rows, rows);
                Assert.Equal(array.Size, cols * rows);
                var rect = array.Rect;
                Assert.Equal(rect.Width, (uint)cols);
                Assert.Equal(rect.Height, (uint)rows);
                Assert.Equal(rect.Left, 0);
                Assert.Equal(rect.Top, 0);
                this.DisposeAndCheckDisposedState(array);
            }
        }

        [Fact]
        public void CreateArray2D2()
        {
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
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var array = CreateArray2DHelp(test.Type);
                this.DisposeAndCheckDisposedState(array);
            }
        }

        [Fact]
        public void CreateArray2DMatrix()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = true}
            };

            foreach (var test in tests.Select(arg => arg.Type))
            {
                var cols = this.NextRandom(1, 100);
                var rows = this.NextRandom(1, 100);
                var array2DMatrix = CreateArray2DMatrixHelp(test, rows, cols);
                Assert.Equal(array2DMatrix.Columns, cols);
                Assert.Equal(array2DMatrix.Rows, rows);
                Assert.Equal(array2DMatrix.Size, cols * rows);
                var rect = array2DMatrix.Rect;
                Assert.Equal(rect.Width, (uint)cols);
                Assert.Equal(rect.Height, (uint)rows);
                Assert.Equal(rect.Left, 0);
                Assert.Equal(rect.Top, 0);
                this.DisposeAndCheckDisposedState(array2DMatrix);
            }
        }

        [Fact]
        public void CreateArray2DMatrix2()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = true}
            };

            foreach (var test in tests.Select(arg => arg.Type))
            {
                var array2DMatrix = CreateArray2DMatrixHelp(test);
                this.DisposeAndCheckDisposedState(array2DMatrix);
            }
        }

        [Fact]
        public void GetRowColumn()
        {
            const int width = 150;
            const int height = 100;

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
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var array2D = CreateArray2DHelp(test.Type, height, width);
                switch (array2D.ImageType)
                {
                    case ImageTypes.UInt8:
                        {
                            var array = (Array2D<byte>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.True(row[0] == 255, "Array<byte> failed");
                        }
                        break;
                    case ImageTypes.UInt16:
                        {
                            var array = (Array2D<ushort>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.True(row[0] == 255, "Array<ushort> failed");
                        }
                        break;
                    case ImageTypes.UInt32:
                        {
                            var array = (Array2D<uint>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.True(row[0] == 255u, "Array<uint> failed");
                        }
                        break;
                    case ImageTypes.Int8:
                        {
                            var array = (Array2D<sbyte>)array2D;
                            Dlib.AssignAllPpixels(array, 127);
                            using (var row = array[0])
                                Assert.True(row[0] == 127, "Array<sbyte> failed");
                        }
                        break;
                    case ImageTypes.Int16:
                        {
                            var array = (Array2D<short>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.True(row[0] == 255, "Array<short> failed");
                        }
                        break;
                    case ImageTypes.Int32:
                        {
                            var array = (Array2D<int>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.True(row[0] == 255, "Array<int> failed");
                        }
                        break;
                    case ImageTypes.Float:
                        {
                            var array = (Array2D<float>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.True(row[0] == 255, "Array<float> failed");
                        }
                        break;
                    case ImageTypes.Double:
                        {
                            var array = (Array2D<double>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                                Assert.True(row[0] == 255, "Array<double> failed");
                        }
                        break;
                    case ImageTypes.BgrPixel:
                        {
                            var array = (Array2D<BgrPixel>)array2D;
                            var pixel = new BgrPixel
                            {
                                Red = 255,
                                Blue = 255,
                                Green = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                var t = row[0];
                                Assert.True(t.Red == 255, "Array<BgrPixel> failed");
                                Assert.True(t.Blue == 255, "Array<BgrPixel> failed");
                                Assert.True(t.Green == 255, "Array<BgrPixel> failed");
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
                                var t = row[0];
                                Assert.True(t.Red == 255, "Array<RgbPixel> failed");
                                Assert.True(t.Blue == 255, "Array<RgbPixel> failed");
                                Assert.True(t.Green == 255, "Array<RgbPixel> failed");
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
                                Assert.True(t.Red == 255, "Array<RgbAlphaPixel> failed");
                                Assert.True(t.Blue == 255, "Array<RgbAlphaPixel> failed");
                                Assert.True(t.Green == 255, "Array<RgbAlphaPixel> failed");
                                Assert.True(t.Alpha == 255, "Array<RgbAlphaPixel> failed");
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
                                Assert.True(t.H == 255, "Array<HsiPixel> failed");
                                Assert.True(t.S == 255, "Array<HsiPixel> failed");
                                Assert.True(t.I == 255, "Array<HsiPixel> failed");
                            }
                        }
                        break;
                    case ImageTypes.LabPixel:
                        {
                            var array = (Array2D<LabPixel>)array2D;
                            var pixel = new LabPixel
                            {
                                L = 255,
                                A = 255,
                                B = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                var t = row[0];
                                Assert.True(t.L == 255, "Array<LabPixel> failed");
                                Assert.True(t.A == 255, "Array<LabPixel> failed");
                                Assert.True(t.B == 255, "Array<LabPixel> failed");
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(array2D.ImageType), array2D.ImageType, null);
                }
                this.DisposeAndCheckDisposedState(array2D);
            }
        }

        [Fact]
        public void SetRowColumn()
        {
            const int width = 150;
            const int height = 100;

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
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var array2D = CreateArray2DHelp(test.Type, height, width);
                switch (array2D.ImageType)
                {
                    case ImageTypes.UInt8:
                        {
                            var array = (Array2D<byte>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.True(row[50] == 0, "Array<byte> failed");
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
                                Assert.True(row[50] == 0, "Array<ushort> failed");
                            }
                        }
                        break;
                    case ImageTypes.UInt32:
                        {
                            var array = (Array2D<uint>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.True(row[50] == 0u, "Array<uint> failed");
                            }
                        }
                        break;
                    case ImageTypes.Int8:
                        {
                            var array = (Array2D<sbyte>)array2D;
                            Dlib.AssignAllPpixels(array, 127);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.True(row[50] == 0, "Array<sbyte> failed");
                            }
                        }
                        break;
                    case ImageTypes.Int16:
                        {
                            var array = (Array2D<short>)array2D;
                            Dlib.AssignAllPpixels(array, 255);
                            using (var row = array[0])
                            {
                                row[50] = 0;
                                Assert.True(row[50] == 0, "Array<short> failed");
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
                                Assert.True(row[50] == 0, "Array<int> failed");
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
                                Assert.True(row[50] == 0, "Array<float> failed");
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
                                Assert.True(row[50] == 0, "Array<double> failed");
                            }
                        }
                        break;
                    case ImageTypes.BgrPixel:
                        {
                            var array = (Array2D<BgrPixel>)array2D;
                            var pixel = new BgrPixel
                            {
                                Red = 255,
                                Blue = 255,
                                Green = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                row[50] = new BgrPixel
                                {
                                    Red = 100,
                                    Green = 128,
                                    Blue = 64
                                };
                                var t = row[50];
                                Assert.True(t.Red == 100, "Array<BgrPixel> failed");
                                Assert.True(t.Green == 128, "Array<BgrPixel> failed");
                                Assert.True(t.Blue == 64, "Array<BgrPixel> failed");
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
                                Assert.True(t.Red == 100, "Array<RgbPixel> failed");
                                Assert.True(t.Green == 128, "Array<RgbPixel> failed");
                                Assert.True(t.Blue == 64, "Array<RgbPixel> failed");
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
                                Assert.True(t.Red == 100, "Array<RgbAlphaPixel> failed");
                                Assert.True(t.Green == 128, "Array<RgbPixel> failed");
                                Assert.True(t.Blue == 64, "Array<RgbPixel> failed");
                                Assert.True(t.Alpha == 0, "Array<RgbAlphaPixel> failed");
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
                                Assert.True(t.H == 100, "Array<HsiPixel> failed");
                                Assert.True(t.S == 128, "Array<HsiPixel> failed");
                                Assert.True(t.I == 64, "Array<HsiPixel> failed");
                            }
                        }
                        break;
                    case ImageTypes.LabPixel:
                        {
                            var array = (Array2D<LabPixel>)array2D;
                            var pixel = new LabPixel
                            {
                                L = 255,
                                A = 255,
                                B = 255
                            };

                            Dlib.AssignAllPpixels(array, pixel);
                            using (var row = array[0])
                            {
                                row[50] = new LabPixel
                                {
                                    L = 100,
                                    A = 128,
                                    B = 64
                                };

                                var t = row[50];
                                Assert.True(t.L == 100, "Array<LabPixel> failed");
                                Assert.True(t.A == 128, "Array<LabPixel> failed");
                                Assert.True(t.B == 64, "Array<LabPixel> failed");
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(array2D.ImageType), array2D.ImageType, null);
                }
                this.DisposeAndCheckDisposedState(array2D);
            }
        }

        [Fact]
        public void GetSetRowColumnMatrix()
        {
            const int width = 150;
            const int height = 100;

            var tests = new[]
            {
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var array2DMatrix = CreateArray2DMatrixHelp(test.Type, height, width);
                switch (array2DMatrix.MatrixElementType)
                {
                    case MatrixElementTypes.UInt8:
                        {
                            var matrix = (Array2DMatrix<byte>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const byte tmp = 80;
                                var mat = new Matrix<byte>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<byte> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt16:
                        {
                            var matrix = (Array2DMatrix<ushort>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const ushort tmp = 80;
                                var mat = new Matrix<ushort>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<ushort> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt32:
                        {
                            var matrix = (Array2DMatrix<uint>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const uint tmp = 80;
                                var mat = new Matrix<uint>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<uint> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.Int8:
                        {
                            var matrix = (Array2DMatrix<sbyte>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const sbyte tmp = 80;
                                var mat = new Matrix<sbyte>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<sbyte> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.Int16:
                        {
                            var matrix = (Array2DMatrix<short>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const short tmp = 80;
                                var mat = new Matrix<short>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<short> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.Int32:
                        {
                            var matrix = (Array2DMatrix<int>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const int tmp = 80;
                                var mat = new Matrix<int>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<int> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.Float:
                        {
                            var matrix = (Array2DMatrix<float>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const float tmp = 80.5f;
                                var mat = new Matrix<float>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<float> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.Double:
                        {
                            var matrix = (Array2DMatrix<double>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                const double tmp = 50.5d;
                                var mat = new Matrix<double>(10, 99)
                                {
                                    [5, 10] = tmp
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t == tmp, "Array2DMatrix<double> failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbPixel:
                        {
                            var matrix = (Array2DMatrix<RgbPixel>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                var mat = new Matrix<RgbPixel>(10, 99)
                                {
                                    [5, 10] = new RgbPixel
                                    {
                                        Red = 13,
                                        Blue = 55,
                                        Green = 124
                                    }
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t.Red == 13, "Array2DMatrix<RgbPixel>.Red failed");
                                Assert.True(t.Blue == 55, "Array2DMatrix<RgbPixel>.Blue failed");
                                Assert.True(t.Green == 124, "Array2DMatrix<RgbPixel>.Green failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbAlphaPixel:
                        {
                            var matrix = (Array2DMatrix<RgbAlphaPixel>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                var mat = new Matrix<RgbAlphaPixel>(10, 99)
                                {
                                    [5, 10] = new RgbAlphaPixel
                                    {
                                        Red = 13,
                                        Blue = 55,
                                        Green = 124,
                                        Alpha = 4
                                    }
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t.Red == 13, "Array2DMatrix<RgbAlphaPixel>.Red failed");
                                Assert.True(t.Blue == 55, "Array2DMatrix<RgbAlphaPixel>.Blue failed");
                                Assert.True(t.Green == 124, "Array2DMatrix<RgbAlphaPixel>.Green failed");
                                Assert.True(t.Alpha == 4, "Array2DMatrix<RgbAlphaPixel>.Alpha failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.HsiPixel:
                        {
                            var matrix = (Array2DMatrix<HsiPixel>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                var mat = new Matrix<HsiPixel>(10, 99)
                                {
                                    [5, 10] = new HsiPixel
                                    {
                                        H = 13,
                                        S = 55,
                                        I = 124
                                    }
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t.H == 13, "Array2DMatrix<HsiPixel>.H failed");
                                Assert.True(t.S == 55, "Array2DMatrix<HsiPixel>.S failed");
                                Assert.True(t.I == 124, "Array2DMatrix<HsiPixel>.I failed");
                            }
                        }
                        break;
                    case MatrixElementTypes.LabPixel:
                        {
                            var matrix = (Array2DMatrix<LabPixel>)array2DMatrix;
                            using (var row = matrix[0])
                            {
                                var mat = new Matrix<LabPixel>(10, 99)
                                {
                                    [5, 10] = new LabPixel
                                    {
                                        L = 13,
                                        A = 55,
                                        B = 99
                                    }
                                };

                                row[10] = mat;

                                var value = row[10];
                                var t = value[5, 10];
                                Assert.True(t.L == 13, "Array2DMatrix<LabPixel>.L failed");
                                Assert.True(t.A == 55, "Array2DMatrix<LabPixel>.A failed");
                                Assert.True(t.B == 99, "Array2DMatrix<LabPixel>.B failed");
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(array2DMatrix.MatrixElementType), array2DMatrix.MatrixElementType, null);
                }
                this.DisposeAndCheckDisposedState(array2DMatrix);
            }
        }

        [Fact]
        public void ToBytes()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            const int row = 10;
            const int column = 10;

            foreach (var input in tests)
            {
                switch (input.Type)
                {
                    case MatrixElementTypes.UInt8:
                        {
                            using (var array2D = new Array2D<byte>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<byte>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt16:
                        {
                            using (var array2D = new Array2D<ushort>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<ushort>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt32:
                        {
                            using (var array2D = new Array2D<uint>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<uint>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.Int8:
                        {
                            using (var array2D = new Array2D<sbyte>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<sbyte>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.Int16:
                        {
                            using (var array2D = new Array2D<short>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<short>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.Int32:
                        {
                            using (var array2D = new Array2D<int>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<int>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.Float:
                        {
                            using (var array2D = new Array2D<float>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<float>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.Double:
                        {
                            using (var array2D = new Array2D<double>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<double>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbPixel:
                        {
                            using (var array2D = new Array2D<RgbPixel>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<RgbPixel>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbAlphaPixel:
                        {
                            using (var array2D = new Array2D<RgbAlphaPixel>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<RgbAlphaPixel>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.HsiPixel:
                        {
                            using (var array2D = new Array2D<HsiPixel>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<HsiPixel>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                    case MatrixElementTypes.LabPixel:
                        {
                            using (var array2D = new Array2D<LabPixel>(row, column))
                            using (var matrix = MatrixTest.FillMatrixByNonZero<LabPixel>(row, column, out _, out var bytes))
                            {
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                        array2D[r][c] = matrix[r, c];

                                var tmp = array2D.ToBytes();
                                if (bytes.Length != tmp.Length)
                                    Assert.True(false, $"Array<{input.Type}>.ToBytes() returns a wrong array");

                                for (var index = 0; index < tmp.Length; index++)
                                    if (bytes[index] != tmp[index])
                                        Assert.True(false, $"{input.Type}");
                            }
                        }
                        break;
                }
            }
        }

        internal static Array2DBase CreateArray2DHelp(ImageTypes elementType, string filepath)
        {
            switch (elementType)
            {
                case ImageTypes.UInt8:
                    return Dlib.LoadImage<byte>(filepath);
                case ImageTypes.UInt16:
                    return Dlib.LoadImage<ushort>(filepath);
                case ImageTypes.UInt32:
                    return Dlib.LoadImage<uint>(filepath);
                case ImageTypes.Int8:
                    return Dlib.LoadImage<sbyte>(filepath);
                case ImageTypes.Int16:
                    return Dlib.LoadImage<short>(filepath);
                case ImageTypes.Int32:
                    return Dlib.LoadImage<int>(filepath);
                case ImageTypes.Float:
                    return Dlib.LoadImage<float>(filepath);
                case ImageTypes.Double:
                    return Dlib.LoadImage<double>(filepath);
                case ImageTypes.RgbPixel:
                    return Dlib.LoadImage<RgbPixel>(filepath);
                case ImageTypes.RgbAlphaPixel:
                    return Dlib.LoadImage<RgbAlphaPixel>(filepath);
                case ImageTypes.HsiPixel:
                    return Dlib.LoadImage<HsiPixel>(filepath);
                case ImageTypes.LabPixel:
                    return Dlib.LoadImage<HsiPixel>(filepath);
                case ImageTypes.BgrPixel:
                    return Dlib.LoadImage<BgrPixel>(filepath);
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        internal static Array2DBase CreateArray2DHelp(ImageTypes elementType)
        {
            switch (elementType)
            {
                case ImageTypes.UInt8:
                    return new Array2D<byte>();
                case ImageTypes.UInt16:
                    return new Array2D<ushort>();
                case ImageTypes.UInt32:
                    return new Array2D<uint>();
                case ImageTypes.Int8:
                    return new Array2D<sbyte>();
                case ImageTypes.Int16:
                    return new Array2D<short>();
                case ImageTypes.Int32:
                    return new Array2D<int>();
                case ImageTypes.Float:
                    return new Array2D<float>();
                case ImageTypes.Double:
                    return new Array2D<double>();
                case ImageTypes.BgrPixel:
                    return new Array2D<BgrPixel>();
                case ImageTypes.RgbPixel:
                    return new Array2D<RgbPixel>();
                case ImageTypes.RgbAlphaPixel:
                    return new Array2D<RgbAlphaPixel>();
                case ImageTypes.HsiPixel:
                    return new Array2D<HsiPixel>();
                case ImageTypes.LabPixel:
                    return new Array2D<LabPixel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        internal static Array2DBase CreateArray2DHelp(ImageTypes elementType, int rows, int columns)
        {
            switch (elementType)
            {
                case ImageTypes.UInt8:
                    return new Array2D<byte>(rows, columns);
                case ImageTypes.UInt16:
                    return new Array2D<ushort>(rows, columns);
                case ImageTypes.UInt32:
                    return new Array2D<uint>(rows, columns);
                case ImageTypes.Int8:
                    return new Array2D<sbyte>(rows, columns);
                case ImageTypes.Int16:
                    return new Array2D<short>(rows, columns);
                case ImageTypes.Int32:
                    return new Array2D<int>(rows, columns);
                case ImageTypes.Float:
                    return new Array2D<float>(rows, columns);
                case ImageTypes.Double:
                    return new Array2D<double>(rows, columns);
                case ImageTypes.BgrPixel:
                    return new Array2D<BgrPixel>(rows, columns);
                case ImageTypes.RgbPixel:
                    return new Array2D<RgbPixel>(rows, columns);
                case ImageTypes.RgbAlphaPixel:
                    return new Array2D<RgbAlphaPixel>(rows, columns);
                case ImageTypes.HsiPixel:
                    return new Array2D<HsiPixel>(rows, columns);
                case ImageTypes.LabPixel:
                    return new Array2D<LabPixel>(rows, columns);
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        internal static Array2DMatrixBase CreateArray2DMatrixHelp(MatrixElementTypes elementType)
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
                case MatrixElementTypes.LabPixel:
                    return new Array2DMatrix<LabPixel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

        internal static Array2DMatrixBase CreateArray2DMatrixHelp(MatrixElementTypes elementType, int rows, int columns)
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
                case MatrixElementTypes.LabPixel:
                    return new Array2DMatrix<LabPixel>(rows, columns);
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }

    }

}
