using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Matrix
{

    [TestClass]
    public class MatrixRangeExpTest : TestBase
    {

        [TestMethod]
        public void Craete()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = false},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                MatrixBase matrix = null;

                try
                {
                    matrix = CreateMatrixRangeExp(test.Type, false, false);
                }
                catch (Exception e)
                {
                    if (!test.ExpectResult)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to create MatrixRangeExp for Type: {test.Type}");
                        throw;
                    }
                }
                finally
                {
                    if (matrix != null)
                        this.DisposeAndCheckDisposedState(matrix);
                }
            }
        }

        [TestMethod]
        public void Craete2()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = false},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                MatrixBase matrix = null;

                try
                {
                    matrix = CreateMatrixRangeExp(test.Type, true, false);
                }
                catch (Exception e)
                {
                    if (!test.ExpectResult)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to create MatrixRangeExp2 for Type: {test.Type}");
                        throw;
                    }
                }
                finally
                {
                    if (matrix != null)
                        this.DisposeAndCheckDisposedState(matrix);
                }
            }
        }

        [TestMethod]
        public void Craete3()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = false},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                MatrixBase matrix = null;

                try
                {
                    matrix = CreateMatrixRangeExp(test.Type, false, true);
                }
                catch (Exception e)
                {
                    if (!test.ExpectResult)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to create MatrixRangeExp2 for Type: {test.Type}");
                        throw;
                    }
                }
                finally
                {
                    if (matrix != null)
                        this.DisposeAndCheckDisposedState(matrix);
                }
            }
        }

        internal static MatrixBase CreateMatrixRangeExp(MatrixElementTypes type, bool useInc, bool useNum)
        {
            switch (type)
            {
                case MatrixElementTypes.UInt8:
                    if (useInc)
                        return new MatrixRangeExp<byte>(0, 0, 0);
                    else if (useNum)
                        return new MatrixRangeExp<byte>(0, 0, (uint)0);
                    else
                        return new MatrixRangeExp<byte>(0, 0);
                case MatrixElementTypes.UInt16:
                    if (useInc)
                        return new MatrixRangeExp<ushort>(0, 0, 0);
                    else if (useNum)
                        return new MatrixRangeExp<ushort>(0, 0, (uint)0);
                    else
                        return new MatrixRangeExp<ushort>(0, 0);
                case MatrixElementTypes.UInt32:
                    if (useInc)
                        return new MatrixRangeExp<uint>(0, 0, 0);
                    else if (useNum)
                        return new MatrixRangeExp<uint>(0, 0, 0);
                    else
                        return new MatrixRangeExp<uint>(0, 0);
                case MatrixElementTypes.Int8:
                    if (useInc)
                        return new MatrixRangeExp<sbyte>(0, 0, 0);
                    else if (useNum)
                        return new MatrixRangeExp<sbyte>(0, 0, (uint)0);
                    else
                        return new MatrixRangeExp<sbyte>(0, 0);
                case MatrixElementTypes.Int16:
                    if (useInc)
                        return new MatrixRangeExp<short>(0, 0, 0);
                    else if (useNum)
                        return new MatrixRangeExp<short>(0, 0, (uint)0);
                    else
                        return new MatrixRangeExp<short>(0, 0);
                case MatrixElementTypes.Int32:
                    if (useInc)
                        return new MatrixRangeExp<int>(0, 0, 0);
                    else if (useNum)
                        return new MatrixRangeExp<int>(0, 0, (uint)0);
                    else
                        return new MatrixRangeExp<int>(0, 0);
                case MatrixElementTypes.Float:
                    if (useInc)
                        return new MatrixRangeExp<float>(0f, 0f, 0f);
                    else if (useNum)
                        return new MatrixRangeExp<float>(0, 0, 0);
                    else
                        return new MatrixRangeExp<float>(0, 0);
                case MatrixElementTypes.Double:
                    if (useInc)
                        return new MatrixRangeExp<double>(0d, 0d, 0d);
                    else if (useNum)
                        return new MatrixRangeExp<double>(0, 0, 0);
                    else
                        return new MatrixRangeExp<double>(0, 0);
                case MatrixElementTypes.RgbPixel:
                    if (useInc)
                        return new MatrixRangeExp<RgbPixel>(default(RgbPixel), default(RgbPixel), default(RgbPixel));
                    else if (useNum)
                        return new MatrixRangeExp<RgbPixel>(default(RgbPixel), default(RgbPixel), 0);
                    else
                        return new MatrixRangeExp<RgbPixel>(default(RgbPixel), default(RgbPixel));
                case MatrixElementTypes.RgbAlphaPixel:
                    if (useInc)
                        return new MatrixRangeExp<RgbAlphaPixel>(default(RgbAlphaPixel), default(RgbAlphaPixel), default(RgbAlphaPixel));
                    else if (useNum)
                        return new MatrixRangeExp<RgbAlphaPixel>(default(RgbAlphaPixel), default(RgbAlphaPixel), 0);
                    else
                        return new MatrixRangeExp<RgbAlphaPixel>(default(RgbAlphaPixel), default(RgbAlphaPixel));
                case MatrixElementTypes.HsiPixel:
                    if (useInc)
                        return new MatrixRangeExp<HsiPixel>(default(HsiPixel), default(HsiPixel), default(HsiPixel));
                    else if (useNum)
                        return new MatrixRangeExp<HsiPixel>(default(HsiPixel), default(HsiPixel), 0);
                    else
                        return new MatrixRangeExp<HsiPixel>(default(HsiPixel), default(HsiPixel));
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }

}
