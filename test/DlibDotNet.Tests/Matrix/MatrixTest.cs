using System;
using System.Linq;
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
        public void ToStringTest()
        {
            var array = new[]
            {
                1, 2, 6,
                5, 3, 6,
                4, 5, 0
            };

            const string answer = "1 2 6 \n5 3 6 \n4 5 0 \n";
            const string answer1 = "\u0001 \u0002 \u0006 \n\u0005 \u0003 \u0006 \n\u0004 \u0005 ";

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = false, Answer = "" },
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false, Answer = "" },
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = false, Answer = "" },
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true,  Answer = answer1},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true,  Answer = answer },
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true,  Answer = answer },
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true,  Answer = answer1},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true,  Answer = answer },
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true,  Answer = answer },
                new { Type = MatrixElementTypes.Float,         ExpectResult = true,  Answer = answer },
                new { Type = MatrixElementTypes.Double,        ExpectResult = true,  Answer = answer }
            };

            foreach (var test in tests)
            {
                TwoDimentionObjectBase matrix = null;

                try
                {
                    if (test.ExpectResult)
                    {
                        matrix = CreateMatrix(test.Type, 3, 3);
                        this.Assign(matrix, array);
                        var str = matrix.ToString();
                        Assert.AreEqual(test.Answer, str);
                    }
                    else
                    {
                        try
                        {
                            matrix = CreateMatrix(test.Type, 3, 3);

                            var str = matrix.ToString();
                            Assert.Fail($"{matrix.GetType().Name} should throw excption for Type: {test.Type}.");
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
                    Console.WriteLine($"Failed to create for Type: {test.Type}.");
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
                var array = new[]
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
                var array = new[]
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
        public void Indexer()
        {
            try
            {
                using (var matrix = new Matrix<byte>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (byte)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(byte)}");
            }
            try
            {
                using (var matrix = new Matrix<ushort>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (ushort)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(ushort)}");
            }

            try
            {
                using (var matrix = new Matrix<uint>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (uint)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(uint)}");
            }

            try
            {
                using (var matrix = new Matrix<sbyte>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (sbyte)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(sbyte)}");
            }

            try
            {
                using (var matrix = new Matrix<short>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (short)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(short)}");
            }

            try
            {
                using (var matrix = new Matrix<int>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (int)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(int)}");
            }

            try
            {
                using (var matrix = new Matrix<float>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (float)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(float)}");
            }

            try
            {
                using (var matrix = new Matrix<double>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (double)(r + c);
                            matrix[r, c] = v;
                            Assert.AreEqual(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(double)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbPixel>(3, 3))
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var b = (byte)(r + c);
                            var v = new RgbPixel { Red = b, Blue = b, Green = b };
                            matrix[r, c] = v;
                            Assert.AreEqual(v.Red, matrix[r, c].Red);
                            Assert.AreEqual(v.Blue, matrix[r, c].Blue);
                            Assert.AreEqual(v.Green, matrix[r, c].Green);
                        }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbAlphaPixel>(3, 3))
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var b = (byte)(r + c);
                            var v = new RgbAlphaPixel { Red = b, Blue = b, Green = b };
                            matrix[r, c] = v;
                            Assert.AreEqual(v.Red, matrix[r, c].Red);
                            Assert.AreEqual(v.Blue, matrix[r, c].Blue);
                            Assert.AreEqual(v.Green, matrix[r, c].Green);
                        }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<HsiPixel>(3, 3))
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var b = (byte)(r + c);
                            var v = new HsiPixel { H = b, S = b, I = b };
                            matrix[r, c] = v;
                            Assert.AreEqual(v.H, matrix[r, c].H);
                            Assert.AreEqual(v.S, matrix[r, c].S);
                            Assert.AreEqual(v.I, matrix[r, c].I);
                        }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(HsiPixel)}");
            }
        }

        [TestMethod]
        public void Indexer2()
        {
            try
            {
                using (var matrix = new Matrix<byte>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (byte)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(byte)}");
            }
            try
            {
                using (var matrix = new Matrix<ushort>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (ushort)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(ushort)}");
            }

            try
            {
                using (var matrix = new Matrix<uint>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (uint)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(uint)}");
            }

            try
            {
                using (var matrix = new Matrix<sbyte>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (sbyte)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(sbyte)}");
            }

            try
            {
                using (var matrix = new Matrix<short>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (short)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(short)}");
            }

            try
            {
                using (var matrix = new Matrix<int>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (int)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(int)}");
            }

            try
            {
                using (var matrix = new Matrix<float>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (float)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(float)}");
            }

            try
            {
                using (var matrix = new Matrix<double>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (double)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(double)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbPixel>(3, 1))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.AreEqual(v.Red, matrix[index].Red);
                        Assert.AreEqual(v.Blue, matrix[index].Blue);
                        Assert.AreEqual(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbAlphaPixel>(3, 1))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbAlphaPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.AreEqual(v.Red, matrix[index].Red);
                        Assert.AreEqual(v.Blue, matrix[index].Blue);
                        Assert.AreEqual(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<HsiPixel>(3, 1))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new HsiPixel { H = b, S = b, I = b };
                        matrix[index] = v;
                        Assert.AreEqual(v.H, matrix[index].H);
                        Assert.AreEqual(v.S, matrix[index].S);
                        Assert.AreEqual(v.I, matrix[index].I);
                    }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(HsiPixel)}");
            }
        }

        [TestMethod]
        public void Indexer3()
        {
            try
            {
                using (var matrix = new Matrix<byte>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (byte)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(byte)}");
            }
            try
            {
                using (var matrix = new Matrix<ushort>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (ushort)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(ushort)}");
            }

            try
            {
                using (var matrix = new Matrix<uint>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (uint)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(uint)}");
            }

            try
            {
                using (var matrix = new Matrix<sbyte>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (sbyte)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(sbyte)}");
            }

            try
            {
                using (var matrix = new Matrix<short>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (short)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(short)}");
            }

            try
            {
                using (var matrix = new Matrix<int>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (int)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(int)}");
            }

            try
            {
                using (var matrix = new Matrix<float>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (float)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(float)}");
            }

            try
            {
                using (var matrix = new Matrix<double>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (double)(index);
                        matrix[index] = v;
                        Assert.AreEqual(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(double)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbPixel>(1, 3))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.AreEqual(v.Red, matrix[index].Red);
                        Assert.AreEqual(v.Blue, matrix[index].Blue);
                        Assert.AreEqual(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbAlphaPixel>(1, 3))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbAlphaPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.AreEqual(v.Red, matrix[index].Red);
                        Assert.AreEqual(v.Blue, matrix[index].Blue);
                        Assert.AreEqual(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<HsiPixel>(1, 3))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new HsiPixel { H = b, S = b, I = b };
                        matrix[index] = v;
                        Assert.AreEqual(v.H, matrix[index].H);
                        Assert.AreEqual(v.S, matrix[index].S);
                        Assert.AreEqual(v.I, matrix[index].I);
                    }
            }
            catch (Exception)
            {
                Assert.Fail($"Failed to access for Type: {typeof(HsiPixel)}");
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

        private void Assign(TwoDimentionObjectBase obj, int[] array)
        {
            if (obj is Matrix<sbyte> sbyteMatrix)
            {
                sbyteMatrix.Assign(array.Select(i => (sbyte)i).ToArray());
                return;
            }

            if (obj is Matrix<short> shortMatrix)
            {
                shortMatrix.Assign(array.Select(i => (short)i).ToArray());
                return;
            }

            if (obj is Matrix<int> intMatrix)
            {
                intMatrix.Assign(array.Select(i => (int)i).ToArray());
                return;
            }

            if (obj is Matrix<byte> byteMatrix)
            {
                byteMatrix.Assign(array.Select(i => (byte)i).ToArray());
                return;
            }

            if (obj is Matrix<ushort> ushortMatrix)
            {
                ushortMatrix.Assign(array.Select(i => (ushort)i).ToArray());
                return;
            }

            if (obj is Matrix<uint> uintMatrix)
            {
                uintMatrix.Assign(array.Select(i => (uint)i).ToArray());
                return;
            }

            if (obj is Matrix<float> floatMatrix)
            {
                floatMatrix.Assign(array.Select(i => (float)i).ToArray());
                return;
            }

            if (obj is Matrix<double> doubleMatrix)
            {
                doubleMatrix.Assign(array.Select(i => (double)i).ToArray());
                return;
            }

            throw new NotSupportedException();
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

        internal static TwoDimentionObjectBase CreateMatrix(MatrixElementTypes elementTypes)
        {
            switch (elementTypes)
            {
                case MatrixElementTypes.UInt8:
                    return new Matrix<byte>();
                case MatrixElementTypes.UInt16:
                    return new Matrix<ushort>();
                case MatrixElementTypes.UInt32:
                    return new Matrix<uint>();
                case MatrixElementTypes.Int8:
                    return new Matrix<sbyte>();
                case MatrixElementTypes.Int16:
                    return new Matrix<short>();
                case MatrixElementTypes.Int32:
                    return new Matrix<int>();
                case MatrixElementTypes.Float:
                    return new Matrix<float>();
                case MatrixElementTypes.Double:
                    return new Matrix<double>();
                case MatrixElementTypes.RgbPixel:
                    return new Matrix<RgbPixel>();
                case MatrixElementTypes.RgbAlphaPixel:
                    return new Matrix<RgbAlphaPixel>();
                case MatrixElementTypes.HsiPixel:
                    return new Matrix<HsiPixel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementTypes), elementTypes, null);
            }
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
