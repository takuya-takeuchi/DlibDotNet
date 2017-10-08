using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Matrix
{

    [TestClass]
    public class MatrixTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                try
                {
                    var matrix = CreateMatrix(test.Type);
                    this.DisposeAndCheckDisposedState(matrix);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine($"Failed to create for Type: {test.Type}.");
                    throw;
                }
            }
        }

        [TestMethod]
        public void Create1()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            var rules = new[]
            {
                new { Column =  0, Row =  0, ExpectResult = true},
                new { Column =  0, Row = -1, ExpectResult = false},
                new { Column = -1, Row =  0, ExpectResult = false},
                new { Column = -1, Row = -1, ExpectResult = false},
                new { Column =  1, Row =  0, ExpectResult = true},
                new { Column =  0, Row =  1, ExpectResult = true},
                new { Column =  1, Row =  1, ExpectResult = true},
                new { Column = 10, Row =  0, ExpectResult = true},
                new { Column =  0, Row = 10, ExpectResult = true}
            };

            foreach (var r in rules)
                foreach (var test in tests)
                {
                    TwoDimentionObjectBase matrix = null;

                    try
                    {
                        if (r.ExpectResult)
                        {
                            matrix = CreateMatrix(test.Type, r.Row, r.Column);
                        }
                        else
                        {
                            try
                            {
                                matrix = CreateMatrix(test.Type, r.Row, r.Column);
                                Assert.Fail($"{matrix.GetType().Name} should throw excption for Type: {test.Type}, Row: {r.Row}, Column: {r.Column}.");
                            }
                            catch
                            {
                                Console.WriteLine("OK");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to create for Type: {test.Type}, Row: {r.Row}, Column: {r.Column}.");
                        throw;
                    }
                    finally
                    {
                        if (matrix != null)
                            this.DisposeAndCheckDisposedState(matrix);
                    }
                }
        }

        [TestMethod]
        public void Assign()
        {
            try
            {
                var array = new byte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<byte>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(byte)}");
            }

            try
            {
                var array = new ushort[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<ushort>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(ushort)}");
            }

            try
            {
                var array = new uint[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<uint>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(uint)}");
            }

            try
            {
                var array = new sbyte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<sbyte>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(sbyte)}");
            }

            try
            {
                var array = new short[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<short>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(short)}");
            }

            try
            {
                var array = new []
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<int>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(int)}");
            }

            try
            {
                var array = new float[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<float>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(float)}");
            }

            try
            {
                var array = new double[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                };
                using (var matrix = new Matrix<double>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(double)}");
            }

            try
            {
                var array = new []
                {
                    new RgbPixel {Red = 1, Blue = 1, Green = 1},
                    new RgbPixel {Red = 2, Blue = 2, Green = 2},
                    new RgbPixel {Red = 3, Blue = 3, Green = 3},
                    new RgbPixel {Red = 4, Blue = 4, Green = 4},
                    new RgbPixel {Red = 5, Blue = 5, Green = 5},
                    new RgbPixel {Red = 6, Blue = 6, Green = 6},
                    new RgbPixel {Red = 7, Blue = 7, Green = 7},
                    new RgbPixel {Red = 8, Blue = 8, Green = 8},
                    new RgbPixel {Red = 9, Blue = 9, Green = 9}
                };
                using (var matrix = new Matrix<RgbPixel>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(RgbPixel)}");
            }

            try
            {
                var array = new[]
                {
                    new RgbAlphaPixel {Red = 1, Blue = 1, Green = 1},
                    new RgbAlphaPixel {Red = 2, Blue = 2, Green = 2},
                    new RgbAlphaPixel {Red = 3, Blue = 3, Green = 3},
                    new RgbAlphaPixel {Red = 4, Blue = 4, Green = 4},
                    new RgbAlphaPixel {Red = 5, Blue = 5, Green = 5},
                    new RgbAlphaPixel {Red = 6, Blue = 6, Green = 6},
                    new RgbAlphaPixel {Red = 7, Blue = 7, Green = 7},
                    new RgbAlphaPixel {Red = 8, Blue = 8, Green = 8},
                    new RgbAlphaPixel {Red = 9, Blue = 9, Green = 9}
                };
                using (var matrix = new Matrix<RgbAlphaPixel>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(RgbAlphaPixel)}");
            }

            try
            {
                var array = new[]
                {
                    new HsiPixel {H = 1, S = 1, I = 1},
                    new HsiPixel {H = 2, S = 2, I = 2},
                    new HsiPixel {H = 3, S = 3, I = 3},
                    new HsiPixel {H = 4, S = 4, I = 4},
                    new HsiPixel {H = 5, S = 5, I = 5},
                    new HsiPixel {H = 6, S = 6, I = 6},
                    new HsiPixel {H = 7, S = 7, I = 7},
                    new HsiPixel {H = 8, S = 8, I = 8},
                    new HsiPixel {H = 9, S = 9, I = 9}
                };
                using (var matrix = new Matrix<HsiPixel>(3, 3))
                    matrix.Assign(array);
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to set array for Type: {typeof(RgbAlphaPixel)}");
            }
        }

        [TestMethod]
        public void RowsColumnsSize()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                var row = this.NextRandom(0, 100);
                var column = this.NextRandom(101, 200);
                var obj = CreateMatrix(test.Type, row, column);

                try
                {
                    this.CheckRowsColumnsSize(obj, row, column);
                    this.DisposeAndCheckDisposedState(obj);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine($"Failed to check {obj.GetType().Name}");
                    throw;
                }
            }
        }

        private void CheckRowsColumnsSize(TwoDimentionObjectBase obj, int row, int columnn)
        {
            if (obj is Matrix<sbyte> sbyteMatrix)
            {
                Assert.AreEqual(sbyteMatrix.Rows, row);
                Assert.AreEqual(sbyteMatrix.Columns, columnn);
                Assert.AreEqual(sbyteMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<short> shortMatrix)
            {
                Assert.AreEqual(shortMatrix.Rows, row);
                Assert.AreEqual(shortMatrix.Columns, columnn);
                Assert.AreEqual(shortMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<int> intMatrix)
            {
                Assert.AreEqual(intMatrix.Rows, row);
                Assert.AreEqual(intMatrix.Columns, columnn);
                Assert.AreEqual(intMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<byte> byteMatrix)
            {
                Assert.AreEqual(byteMatrix.Rows, row);
                Assert.AreEqual(byteMatrix.Columns, columnn);
                Assert.AreEqual(byteMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<ushort> ushortMatrix)
            {
                Assert.AreEqual(ushortMatrix.Rows, row);
                Assert.AreEqual(ushortMatrix.Columns, columnn);
                Assert.AreEqual(ushortMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<uint> uintMatrix)
            {
                Assert.AreEqual(uintMatrix.Rows, row);
                Assert.AreEqual(uintMatrix.Columns, columnn);
                Assert.AreEqual(uintMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<float> floatMatrix)
            {
                Assert.AreEqual(floatMatrix.Rows, row);
                Assert.AreEqual(floatMatrix.Columns, columnn);
                Assert.AreEqual(floatMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<double> doubleMatrix)
            {
                Assert.AreEqual(doubleMatrix.Rows, row);
                Assert.AreEqual(doubleMatrix.Columns, columnn);
                Assert.AreEqual(doubleMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<RgbPixel> rgbPixelMatrix)
            {
                Assert.AreEqual(rgbPixelMatrix.Rows, row);
                Assert.AreEqual(rgbPixelMatrix.Columns, columnn);
                Assert.AreEqual(rgbPixelMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<RgbAlphaPixel> rgbAlphaPixelMatrix)
            {
                Assert.AreEqual(rgbAlphaPixelMatrix.Rows, row);
                Assert.AreEqual(rgbAlphaPixelMatrix.Columns, columnn);
                Assert.AreEqual(rgbAlphaPixelMatrix.Size, row * columnn);
                return;
            }

            if (obj is Matrix<HsiPixel> hsiPicelMatrix)
            {
                Assert.AreEqual(hsiPicelMatrix.Rows, row);
                Assert.AreEqual(hsiPicelMatrix.Columns, columnn);
                Assert.AreEqual(hsiPicelMatrix.Size, row * columnn);
                return;
            }

            throw new NotSupportedException();
        }

        internal static TwoDimentionObjectBase CreateMatrix(MatrixElementTypes elementTypes, int rows = 0, int columns = 0)
        {
            switch (elementTypes)
            {
                case MatrixElementTypes.UInt8:
                    return new Matrix<byte>(rows, columns);
                case MatrixElementTypes.UInt16:
                    return new Matrix<ushort>(rows, columns);
                case MatrixElementTypes.UInt32:
                    return new Matrix<uint>(rows, columns);
                case MatrixElementTypes.Int8:
                    return new Matrix<sbyte>(rows, columns);
                case MatrixElementTypes.Int16:
                    return new Matrix<short>(rows, columns);
                case MatrixElementTypes.Int32:
                    return new Matrix<int>(rows, columns);
                case MatrixElementTypes.Float:
                    return new Matrix<float>(rows, columns);
                case MatrixElementTypes.Double:
                    return new Matrix<double>(rows, columns);
                case MatrixElementTypes.RgbPixel:
                    return new Matrix<RgbPixel>(rows, columns);
                case MatrixElementTypes.RgbAlphaPixel:
                    return new Matrix<RgbAlphaPixel>(rows, columns);
                case MatrixElementTypes.HsiPixel:
                    return new Matrix<HsiPixel>(rows, columns);
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementTypes), elementTypes, null);
            }
        }

        internal sealed class TestData : TestDataBase
        {

            public TwoDimentionObjectBase Data
            {
                get;
                set;
            }

            public MatrixElementTypes MatrixElementType
            {
                get;
                set;
            }

        }

    }

}
