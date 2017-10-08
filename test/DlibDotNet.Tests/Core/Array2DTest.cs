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

        internal static Array2DBase CreateArray2D(ImageTypes elementType)
        {
            switch (elementType)
            {
                case ImageTypes.UInt8:
                    return new Array2D<byte>();
                case ImageTypes.UInt16:
                    return new Array2D<ushort>();
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
