using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace DlibDotNet.Tests.Matrix
{

    public class MatrixTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        [Fact]
        public void Create()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = true},
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

        [Fact]
        public void Create1()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = true},
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
                    TwoDimensionObjectBase matrix = null;

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
                                Assert.True(false, $"{matrix.GetType().Name} should throw exception for Type: {test.Type}, Row: {r.Row}, Column: {r.Column}.");
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

        [Fact]
        public void Create2()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                //new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                //new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                //new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                //new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                //new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                //new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                //new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                switch (input.Type)
                {
                    case MatrixElementTypes.UInt8:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<byte>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<byte>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt16:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<ushort>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<ushort>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt32:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<uint>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<uint>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int8:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<sbyte>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<sbyte>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int16:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<short>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<short>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int32:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<int>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<int>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.Float:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<float>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<float>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (Math.Abs(a - m) > float.Epsilon)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.Double:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<double>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<double>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (Math.Abs(a - m) > double.Epsilon)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbPixel:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<RgbPixel>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<RgbPixel>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is [{a.Red}, {a.Green}, {a.Blue}], matrix[{r}, {c}] is [{m.Red}, {m.Green}, {m.Blue}]");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbAlphaPixel:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<RgbAlphaPixel>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<RgbAlphaPixel>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is [{a.Red}, {a.Green}, {a.Blue}, {a.Alpha}], matrix[{r}, {c}] is [{m.Red}, {m.Green}, {m.Blue}, {m.Alpha}]");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.HsiPixel:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<HsiPixel>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<HsiPixel>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is [{a.H}, {a.S}, {a.I}], matrix[{r}, {c}] is [{m.H}, {m.S}, {m.I}]");
                                        }
                            }
                        }
                        break;
                    case MatrixElementTypes.LabPixel:
                        {
                            using (var matrix = DlibTest.LoadImageAsMatrixHelp(input.Type, path) as Matrix<LabPixel>)
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                using (var tmp = new Matrix<LabPixel>(array, row, column))
                                    for (var r = 0; r < row; r++)
                                        for (var c = 0; c < column; c++)
                                        {
                                            var a = tmp[r, c];
                                            var m = matrix[r, c];
                                            if (a != m)
                                                Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is [{a.B}, {a.A}, {a.B}], matrix[{r}, {c}] is [{m.B}, {m.A}, {m.B}]");
                                        }
                            }
                        }
                        break;
                }
            }
        }

        [Fact]
        public void Create3()
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
                new { Type = MatrixElementTypes.BgrPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            const int row1 = 10;
            const int column1 = 10;

            foreach (var input in tests)
            {
                switch (input.Type)
                {
                    case MatrixElementTypes.UInt8:
                        {
                            using (var matrix = FillMatrixByNonZero<byte>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt16:
                        {
                            using (var matrix = FillMatrixByNonZero<ushort>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt32:
                        {
                            using (var matrix = FillMatrixByNonZero<uint>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int8:
                        {
                            using (var matrix = FillMatrixByNonZero<sbyte>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int16:
                        {
                            using (var matrix = FillMatrixByNonZero<short>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int32:
                        {
                            using (var matrix = FillMatrixByNonZero<int>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Float:
                        {
                            using (var matrix = FillMatrixByNonZero<float>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (Math.Abs(a - m) > float.Epsilon)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Double:
                        {
                            using (var matrix = FillMatrixByNonZero<double>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (Math.Abs(a - m) > double.Epsilon)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<RgbPixel>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbAlphaPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<RgbAlphaPixel>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.HsiPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<HsiPixel>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.LabPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<LabPixel>(row1, column1, out var result, out _))
                            {
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (var c = 0; c < column; c++)
                                    {
                                        var a = result[r * column1 + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                }
            }
        }

        [Fact]
        public void Create4()
        {
            var testName = nameof(this.Create4);
            var path = this.GetDataFile($"{LoadTarget}_padding2.png");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.BgrPixel,      ExpectResult = true},
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
            {
                switch (input.Type)
                {
                    case MatrixElementTypes.RgbPixel:
                        {
                            using (var bitmap = (Bitmap)Image.FromFile(path.FullName))
                            {
                                BitmapData bitmapData = null;

                                try
                                {
                                    var rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                                    bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

                                    var array = bitmapData.Scan0;
                                    var stride = bitmapData.Stride;

                                    using (var matrix = new Matrix<RgbPixel>(array, bitmap.Height, bitmap.Width, stride))
                                        Dlib.SavePng(matrix, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}.png")}");
                                }
                                finally
                                {
                                    if (bitmapData != null)
                                        bitmap.UnlockBits(bitmapData);
                                }
                            }
                        }
                        break;
                    case MatrixElementTypes.BgrPixel:
                        {
                            using (var bitmap = (Bitmap)Image.FromFile(path.FullName))
                            {
                                BitmapData bitmapData = null;

                                try
                                {
                                    var rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                                    bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

                                    var array = bitmapData.Scan0;
                                    var stride = bitmapData.Stride;

                                    using (var matrix = new Matrix<BgrPixel>(array, bitmap.Height, bitmap.Width, stride))
                                        Dlib.SavePng(matrix, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}.png")}");
                                }
                                finally
                                {
                                    if (bitmapData != null)
                                        bitmap.UnlockBits(bitmapData);
                                }
                            }
                        }
                        break;
                }
            }
        }

        [Fact]
        public void ToArrayTest()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

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
                new { Type = MatrixElementTypes.BgrPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                switch (input.Type)
                {
                    case MatrixElementTypes.UInt8:
                        {
                            using (var matrix = (Matrix<byte>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt16:
                        {
                            using (var matrix = (Matrix<ushort>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt32:
                        {
                            using (var matrix = (Matrix<uint>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int8:
                        {
                            using (var matrix = (Matrix<sbyte>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int16:
                        {
                            using (var matrix = (Matrix<short>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Int32:
                        {
                            using (var matrix = (Matrix<int>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Float:
                        {
                            using (var matrix = (Matrix<float>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.Double:
                        {
                            using (var matrix = (Matrix<double>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{step} + {c}] is {a}, matrix[{r}, {c}] is {m}");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbPixel:
                        {
                            using (var matrix = (Matrix<RgbPixel>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: tmp[{r}, {c}] is [{a.Red}, {a.Green}, {a.Blue}], matrix[{r}, {c}] is [{m.Red}, {m.Green}, {m.Blue}]");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbAlphaPixel:
                        {
                            using (var matrix = (Matrix<RgbAlphaPixel>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{r}, {c}] is [{a.Red}, {a.Green}, {a.Blue}, {a.Alpha}], matrix[{r}, {c}] is [{m.Red}, {m.Green}, {m.Blue}, {m.Alpha}]");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.HsiPixel:
                        {
                            using (var matrix = (Matrix<HsiPixel>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{r}, {c}] is [{a.H}, {a.S}, {a.I}], matrix[{r}, {c}] is [{m.H}, {m.S}, {m.I}]");
                                    }
                            }
                        }
                        break;
                    case MatrixElementTypes.LabPixel:
                        {
                            using (var matrix = (Matrix<LabPixel>)DlibTest.LoadImageAsMatrixHelp(input.Type, path))
                            {
                                var array = matrix.ToArray();
                                var column = matrix.Columns;
                                var row = matrix.Rows;
                                for (var r = 0; r < row; r++)
                                    for (int c = 0, step = r * column; c < column; c++)
                                    {
                                        var a = array[step + c];
                                        var m = matrix[r, c];
                                        if (a != m)
                                            Assert.True(false, $"{input.Type}: array[{r}, {c}] is [{a.L}, {a.A}, {a.B}], matrix[{r}, {c}] is [{m.L}, {m.A}, {m.B}]");
                                    }
                            }
                        }
                        break;
                }
            }
        }

        [Fact]
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
                new { Type = MatrixElementTypes.BgrPixel,      ExpectResult = false, Answer = "" },
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = false, Answer = "" },
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false, Answer = "" },
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = false, Answer = "" },
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = false, Answer = "" },
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
                TwoDimensionObjectBase matrix = null;

                try
                {
                    if (test.ExpectResult)
                    {
                        matrix = CreateMatrix(test.Type, 3, 3);
                        this.AssignHelp(matrix, array);
                        var str = matrix.ToString();
                        Assert.Equal(test.Answer, str);
                    }
                    else
                    {
                        try
                        {
                            matrix = CreateMatrix(test.Type, 3, 3);

                            var str = matrix.ToString();
                            Assert.True(false, $"{matrix.GetType().Name} should throw exception for Type: {test.Type}.");
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

        [Fact]
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
                Assert.True(false, $"Failed to set array for Type: {typeof(byte)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(ushort)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(uint)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(sbyte)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(short)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(int)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(float)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(double)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(RgbPixel)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(RgbAlphaPixel)}");
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
                Assert.True(false, $"Failed to set array for Type: {typeof(RgbAlphaPixel)}");
            }
        }

        [Fact]
        public void Enumerator()
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
                new { Type = MatrixElementTypes.BgrPixel,      ExpectResult = true},
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
                            using (var matrix = FillMatrixByNonZero<byte>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt16:
                        {
                            using (var matrix = FillMatrixByNonZero<ushort>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.UInt32:
                        {
                            using (var matrix = FillMatrixByNonZero<uint>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.Int8:
                        {
                            using (var matrix = FillMatrixByNonZero<sbyte>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.Int16:
                        {
                            using (var matrix = FillMatrixByNonZero<short>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.Int32:
                        {
                            using (var matrix = FillMatrixByNonZero<int>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.Float:
                        {
                            using (var matrix = FillMatrixByNonZero<float>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.Double:
                        {
                            using (var matrix = FillMatrixByNonZero<double>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<RgbPixel>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.RgbAlphaPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<RgbAlphaPixel>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.HsiPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<HsiPixel>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                    case MatrixElementTypes.LabPixel:
                        {
                            using (var matrix = FillMatrixByNonZero<LabPixel>(row, column, out var array, out _))
                            {
                                var index = 0;
                                foreach (var b in matrix)
                                {
                                    Assert.True(b == array[index], $"Fail: {input.Type}. index: {index}");
                                    index++;
                                }
                                Assert.Equal(matrix.Size, index);
                            }
                        }
                        break;
                }
            }
        }

        [Fact]
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
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(byte)}");
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
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(ushort)}");
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
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(uint)}");
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
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(sbyte)}");
            }

            try
            {
                using (var matrix = new Matrix<ulong>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (ulong)(r + c);
                            matrix[r, c] = v;
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(ulong)}");
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
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(short)}");
            }

            try
            {
                using (var matrix = new Matrix<int>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = r + c;
                            matrix[r, c] = v;
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(int)}");
            }

            try
            {
                using (var matrix = new Matrix<long>(3, 3))
                {
                    for (var r = 0; r < 3; r++)
                        for (var c = 0; c < 3; c++)
                        {
                            var v = (long)(r + c);
                            matrix[r, c] = v;
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(long)}");
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
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(float)}");
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
                            Assert.Equal(v, matrix[r, c]);
                        }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(double)}");
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
                            Assert.Equal(v.Red, matrix[r, c].Red);
                            Assert.Equal(v.Blue, matrix[r, c].Blue);
                            Assert.Equal(v.Green, matrix[r, c].Green);
                        }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(RgbPixel)}");
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
                            Assert.Equal(v.Red, matrix[r, c].Red);
                            Assert.Equal(v.Blue, matrix[r, c].Blue);
                            Assert.Equal(v.Green, matrix[r, c].Green);
                        }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(RgbPixel)}");
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
                            Assert.Equal(v.H, matrix[r, c].H);
                            Assert.Equal(v.S, matrix[r, c].S);
                            Assert.Equal(v.I, matrix[r, c].I);
                        }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(HsiPixel)}");
            }
        }

        [Fact]
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
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(byte)}");
            }
            try
            {
                using (var matrix = new Matrix<ushort>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (ushort)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(ushort)}");
            }

            try
            {
                using (var matrix = new Matrix<uint>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (uint)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(uint)}");
            }

            try
            {
                using (var matrix = new Matrix<ulong>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (ulong)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(ulong)}");
            }

            try
            {
                using (var matrix = new Matrix<sbyte>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (sbyte)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(sbyte)}");
            }

            try
            {
                using (var matrix = new Matrix<short>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (short)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(short)}");
            }

            try
            {
                using (var matrix = new Matrix<int>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = index;
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(int)}");
            }

            try
            {
                using (var matrix = new Matrix<long>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (long)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(long)}");
            }

            try
            {
                using (var matrix = new Matrix<float>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (float)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(float)}");
            }

            try
            {
                using (var matrix = new Matrix<double>(3, 1))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (double)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(double)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbPixel>(3, 1))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.Equal(v.Red, matrix[index].Red);
                        Assert.Equal(v.Blue, matrix[index].Blue);
                        Assert.Equal(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbAlphaPixel>(3, 1))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbAlphaPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.Equal(v.Red, matrix[index].Red);
                        Assert.Equal(v.Blue, matrix[index].Blue);
                        Assert.Equal(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<HsiPixel>(3, 1))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new HsiPixel { H = b, S = b, I = b };
                        matrix[index] = v;
                        Assert.Equal(v.H, matrix[index].H);
                        Assert.Equal(v.S, matrix[index].S);
                        Assert.Equal(v.I, matrix[index].I);
                    }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(HsiPixel)}");
            }
        }

        [Fact]
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
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(byte)}");
            }
            try
            {
                using (var matrix = new Matrix<ushort>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (ushort)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(ushort)}");
            }

            try
            {
                using (var matrix = new Matrix<uint>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (uint)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(uint)}");
            }

            try
            {
                using (var matrix = new Matrix<ulong>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (ulong)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(ulong)}");
            }

            try
            {
                using (var matrix = new Matrix<sbyte>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (sbyte)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(sbyte)}");
            }

            try
            {
                using (var matrix = new Matrix<short>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (short)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(short)}");
            }

            try
            {
                using (var matrix = new Matrix<int>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = index;
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(int)}");
            }

            try
            {
                using (var matrix = new Matrix<long>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (long)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(long)}");
            }

            try
            {
                using (var matrix = new Matrix<float>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (float)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(float)}");
            }

            try
            {
                using (var matrix = new Matrix<double>(1, 3))
                {
                    for (var index = 0; index < 3; index++)
                    {
                        var v = (double)(index);
                        matrix[index] = v;
                        Assert.Equal(v, matrix[index]);
                    }
                }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(double)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbPixel>(1, 3))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.Equal(v.Red, matrix[index].Red);
                        Assert.Equal(v.Blue, matrix[index].Blue);
                        Assert.Equal(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<RgbAlphaPixel>(1, 3))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new RgbAlphaPixel { Red = b, Blue = b, Green = b };
                        matrix[index] = v;
                        Assert.Equal(v.Red, matrix[index].Red);
                        Assert.Equal(v.Blue, matrix[index].Blue);
                        Assert.Equal(v.Green, matrix[index].Green);
                    }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(RgbPixel)}");
            }

            try
            {
                using (var matrix = new Matrix<HsiPixel>(1, 3))
                    for (var index = 0; index < 3; index++)
                    {
                        var b = (byte)(index);
                        var v = new HsiPixel { H = b, S = b, I = b };
                        matrix[index] = v;
                        Assert.Equal(v.H, matrix[index].H);
                        Assert.Equal(v.S, matrix[index].S);
                        Assert.Equal(v.I, matrix[index].I);
                    }
            }
            catch (Exception)
            {
                Assert.True(false, $"Failed to access for Type: {typeof(HsiPixel)}");
            }
        }

        [Fact]
        public void RowsColumnsSize()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.BgrPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.LabPixel,      ExpectResult = true},
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

        [Fact]
        public void Addition()
        {
            this.AdditionSub<byte>();
            this.AdditionSub<ushort>();
            this.AdditionSub<uint>();
            this.AdditionSub<sbyte>();
            this.AdditionSub<short>();
            this.AdditionSub<int>();
            this.AdditionSub<float>();
            this.AdditionSub<double>();
            this.AdditionSub<RgbPixel>(false);
            this.AdditionSub<RgbAlphaPixel>(false);
            this.AdditionSub<HsiPixel>(false);
        }

        [Fact]
        public void Addition2()
        {
            const int rows = 3;
            const int columns = 4;
            using (var lhs = new Matrix<double>(rows, columns))
            using (var rhs = new Matrix<double>(rows, columns))
            {
                var tmpLeft = new double[rows * columns];
                var tmpRight = new double[rows * columns];

                var index = 0;
                for (var r = 0; r < rows; r++)
                    for (var c = 0; c < columns; c++)
                    {
                        var v1 = this.NextInt32Random();
                        lhs[r, c] = v1;
                        tmpLeft[index] = v1;

                        var v2 = this.NextInt32Random();
                        rhs[r, c] = v2;
                        tmpRight[index] = v2;

                        index++;
                    }

                using (var ret = lhs + rhs)
                {
                    index = 0;
                    for (var r = 0; r < rows; r++)
                        for (var c = 0; c < columns; c++)
                        {
                            var mv = ret[r, c];
                            var av = tmpLeft[index] + tmpRight[index];
                            Assert.True(Math.Abs(mv - av) < double.Epsilon);
                            index++;
                        }
                }

                Assert.True(index == rows * columns);
            }
        }

        [Fact]
        public void Addition3()
        {
            const int rows = 31;
            const int columns = 1;
            using (var lhs = new Matrix<double>(rows, columns))
            using (var rhs = Matrix<double>.CreateTemplateParameterizeMatrix(rows, columns))
            {
                var tmpLeft = new double[rows * columns];
                var tmpRight = new double[rows * columns];

                var index = 0;
                for (var r = 0; r < rows; r++)
                    for (var c = 0; c < columns; c++)
                    {
                        var v1 = this.NextInt32Random();
                        lhs[r, c] = v1;
                        tmpLeft[index] = v1;

                        var v2 = this.NextInt32Random();
                        rhs[r, c] = v2;
                        tmpRight[index] = v2;

                        index++;
                    }

                using (var ret = lhs + rhs)
                {
                    index = 0;
                    for (var r = 0; r < rows; r++)
                        for (var c = 0; c < columns; c++)
                        {
                            var mv = ret[r, c];
                            var av = tmpLeft[index] + tmpRight[index];
                            Assert.True(Math.Abs(mv - av) < double.Epsilon);
                            index++;
                        }
                }

                Assert.True(index == rows * columns);
            }
        }

        [Fact]
        public void Addition4()
        {
            const int rows = 31;
            const int columns = 1;
            using (var lhs = Matrix<double>.CreateTemplateParameterizeMatrix(rows, columns))
            using (var rhs = new Matrix<double>(rows, columns))
            {
                var tmpLeft = new double[rows * columns];
                var tmpRight = new double[rows * columns];

                var index = 0;
                for (var r = 0; r < rows; r++)
                    for (var c = 0; c < columns; c++)
                    {
                        var v1 = this.NextInt32Random();
                        lhs[r, c] = v1;
                        tmpLeft[index] = v1;

                        var v2 = this.NextInt32Random();
                        rhs[r, c] = v2;
                        tmpRight[index] = v2;

                        index++;
                    }

                using (var ret = lhs + rhs)
                {
                    index = 0;
                    for (var r = 0; r < rows; r++)
                        for (var c = 0; c < columns; c++)
                        {
                            var mv = ret[r, c];
                            var av = tmpLeft[index] + tmpRight[index];
                            Assert.True(Math.Abs(mv - av) < double.Epsilon);
                            index++;
                        }
                }

                Assert.True(index == rows * columns);
            }
        }

        private void AdditionSub<T>(bool expectResult = true)
            where T : struct
        {
            var rules = new[]
            {
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 10, ExpectResult = expectResult},
                new { LeftColumn =  1,  LeftRow =  10, RightColumn = 10, RightRow = 10, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  1,  RightColumn = 10, RightRow = 10, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow = 10, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 1,  ExpectResult = false}
            };

            foreach (var r in rules)
            {
                Matrix<T> lhs = null;
                Matrix<T> rhs = null;
                Matrix<T> ret = null;

                try
                {
                    if (r.ExpectResult)
                    {
                        lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                        rhs = new Matrix<T>(r.RightRow, r.RightColumn);
                        ret = lhs + rhs;
                    }
                    else
                    {
                        try
                        {
                            lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                            rhs = new Matrix<T>(r.RightRow, r.RightColumn);
                            ret = lhs + rhs;

                            Assert.True(false, $"{lhs.GetType().Name} should throw exception for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
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
                    Console.WriteLine($"Failed to create for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
                    throw;
                }
                finally
                {
                    if (lhs != null)
                        this.DisposeAndCheckDisposedState(lhs);
                    if (rhs != null)
                        this.DisposeAndCheckDisposedState(rhs);
                    if (ret != null)
                        this.DisposeAndCheckDisposedState(ret);
                }
            }
        }

        [Fact]
        public void Subtraction()
        {
            this.SubtractionSub<byte>();
            this.SubtractionSub<ushort>();
            this.SubtractionSub<uint>();
            this.SubtractionSub<sbyte>();
            this.SubtractionSub<short>();
            this.SubtractionSub<int>();
            this.SubtractionSub<float>();
            this.SubtractionSub<double>();
            this.SubtractionSub<RgbPixel>(false);
            this.SubtractionSub<RgbAlphaPixel>(false);
            this.SubtractionSub<HsiPixel>(false);
        }
        [Fact]
        public void Subtraction2()
        {
            const int rows = 3;
            const int columns = 4;
            using (var lhs = new Matrix<double>(rows, columns))
            using (var rhs = new Matrix<double>(rows, columns))
            {
                var tmpLeft = new double[rows * columns];
                var tmpRight = new double[rows * columns];

                var index = 0;
                for (var r = 0; r < rows; r++)
                    for (var c = 0; c < columns; c++)
                    {
                        var v1 = this.NextInt32Random();
                        lhs[r, c] = v1;
                        tmpLeft[index] = v1;

                        var v2 = this.NextInt32Random();
                        rhs[r, c] = v2;
                        tmpRight[index] = v2;

                        index++;
                    }

                using (var ret = lhs - rhs)
                {
                    index = 0;
                    for (var r = 0; r < rows; r++)
                        for (var c = 0; c < columns; c++)
                        {
                            var mv = ret[r, c];
                            var av = tmpLeft[index] - tmpRight[index];
                            Assert.True(Math.Abs(mv - av) < double.Epsilon);
                            index++;
                        }
                }

                Assert.True(index == rows * columns);
            }
        }

        [Fact]
        public void Subtraction3()
        {
            const int rows = 31;
            const int columns = 1;
            using (var lhs = new Matrix<double>(rows, columns))
            using (var rhs = Matrix<double>.CreateTemplateParameterizeMatrix(rows, columns))
            {
                var tmpLeft = new double[rows * columns];
                var tmpRight = new double[rows * columns];

                var index = 0;
                for (var r = 0; r < rows; r++)
                    for (var c = 0; c < columns; c++)
                    {
                        var v1 = this.NextInt32Random();
                        lhs[r, c] = v1;
                        tmpLeft[index] = v1;

                        var v2 = this.NextInt32Random();
                        rhs[r, c] = v2;
                        tmpRight[index] = v2;

                        index++;
                    }

                using (var ret = lhs - rhs)
                {
                    index = 0;
                    for (var r = 0; r < rows; r++)
                        for (var c = 0; c < columns; c++)
                        {
                            var mv = ret[r, c];
                            var av = tmpLeft[index] - tmpRight[index];
                            Assert.True(Math.Abs(mv - av) < double.Epsilon);
                            index++;
                        }
                }

                Assert.True(index == rows * columns);
            }
        }

        [Fact]
        public void Subtraction4()
        {
            const int rows = 31;
            const int columns = 1;
            using (var lhs = Matrix<double>.CreateTemplateParameterizeMatrix(rows, columns))
            using (var rhs = new Matrix<double>(rows, columns))
            {
                var tmpLeft = new double[rows * columns];
                var tmpRight = new double[rows * columns];

                var index = 0;
                for (var r = 0; r < rows; r++)
                    for (var c = 0; c < columns; c++)
                    {
                        var v1 = this.NextInt32Random();
                        lhs[r, c] = v1;
                        tmpLeft[index] = v1;

                        var v2 = this.NextInt32Random();
                        rhs[r, c] = v2;
                        tmpRight[index] = v2;

                        index++;
                    }

                using (var ret = lhs - rhs)
                {
                    index = 0;
                    for (var r = 0; r < rows; r++)
                        for (var c = 0; c < columns; c++)
                        {
                            var mv = ret[r, c];
                            var av = tmpLeft[index] - tmpRight[index];
                            Assert.True(Math.Abs(mv - av) < double.Epsilon);
                            index++;
                        }
                }

                Assert.True(index == rows * columns);
            }
        }

        private void SubtractionSub<T>(bool expectResult = true)
            where T : struct
        {
            var rules = new[]
            {
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 10, ExpectResult = expectResult},
                new { LeftColumn =  1,  LeftRow =  10, RightColumn = 10, RightRow = 10, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  1,  RightColumn = 10, RightRow = 10, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow = 10, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 1,  ExpectResult = false}
            };

            foreach (var r in rules)
            {
                Matrix<T> lhs = null;
                Matrix<T> rhs = null;
                Matrix<T> ret = null;

                try
                {
                    if (r.ExpectResult)
                    {
                        lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                        rhs = new Matrix<T>(r.RightRow, r.RightColumn);
                        ret = lhs - rhs;
                    }
                    else
                    {
                        try
                        {
                            lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                            rhs = new Matrix<T>(r.RightRow, r.RightColumn);
                            ret = lhs - rhs;

                            Assert.True(false, $"{lhs.GetType().Name} should throw exception for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
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
                    Console.WriteLine($"Failed to create for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
                    throw;
                }
                finally
                {
                    if (lhs != null)
                        this.DisposeAndCheckDisposedState(lhs);
                    if (rhs != null)
                        this.DisposeAndCheckDisposedState(rhs);
                    if (ret != null)
                        this.DisposeAndCheckDisposedState(ret);
                }
            }
        }

        [Fact]
        public void Multiply()
        {
            this.MultiplySub<byte>();
            this.MultiplySub<ushort>();
            this.MultiplySub<uint>();
            this.MultiplySub<sbyte>();
            this.MultiplySub<short>();
            this.MultiplySub<int>();
            this.MultiplySub<float>();
            this.MultiplySub<double>();
            this.MultiplySub<RgbPixel>(false);
            this.MultiplySub<RgbAlphaPixel>(false);
            this.MultiplySub<HsiPixel>(false);
        }

        private void MultiplySub<T>(bool expectResult = true)
            where T : struct
        {
            var rules = new[]
            {
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 10, ExpectResult = expectResult},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 0,  ExpectResult = false},
                new { LeftColumn =  10, LeftRow =   0, RightColumn = 10, RightRow = 10, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 5,  RightRow = 10, ExpectResult = false}
            };

            foreach (var r in rules)
            {
                Matrix<T> lhs = null;
                Matrix<T> rhs = null;
                Matrix<T> ret = null;

                try
                {
                    if (r.ExpectResult)
                    {
                        lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                        rhs = new Matrix<T>(r.RightRow, r.RightColumn);
                        ret = lhs * rhs;
                    }
                    else
                    {
                        try
                        {
                            lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                            rhs = new Matrix<T>(r.RightRow, r.RightColumn);
                            ret = lhs * rhs;

                            Assert.True(false, $"{lhs.GetType().Name} should throw exception for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
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
                    Console.WriteLine($"Failed to create for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
                    throw;
                }
                finally
                {
                    if (lhs != null)
                        this.DisposeAndCheckDisposedState(lhs);
                    if (rhs != null)
                        this.DisposeAndCheckDisposedState(rhs);
                    if (ret != null)
                        this.DisposeAndCheckDisposedState(ret);
                }
            }
        }

        [Fact]
        public void Division()
        {
            this.DivisionSub<byte>();
            this.DivisionSub<ushort>();
            this.DivisionSub<uint>();
            this.DivisionSub<sbyte>();
            this.DivisionSub<short>();
            this.DivisionSub<int>();
            this.DivisionSub<float>();
            this.DivisionSub<double>();
            this.DivisionSub<RgbPixel>(false);
            this.DivisionSub<RgbAlphaPixel>(false);
            this.DivisionSub<HsiPixel>(false);
        }

        private void DivisionSub<T>(bool expectResult = true)
            where T : struct
        {
            var rules = new[]
            {
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow = 1,  Fill = true,  ExpectResult = expectResult},
                new { LeftColumn =  1,  LeftRow =  1,  RightColumn = 1,  RightRow = 1,  Fill = true,  ExpectResult = expectResult},
                new { LeftColumn =  1,  LeftRow =  0,  RightColumn = 1,  RightRow = 1,  Fill = true,  ExpectResult = expectResult},
                new { LeftColumn =  0,  LeftRow =  1,  RightColumn = 1,  RightRow = 1,  Fill = true,  ExpectResult = expectResult},
                new { LeftColumn =  0,  LeftRow =  0,  RightColumn = 1,  RightRow = 1,  Fill = true,  ExpectResult = expectResult},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 1,  Fill = true,  ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow = 10, Fill = true,  ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 0,  RightRow =  1, Fill = true,  ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow =  0, Fill = true,  ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow = 1,  Fill = false, ExpectResult = false},
                new { LeftColumn =  1,  LeftRow =  1,  RightColumn = 1,  RightRow = 1,  Fill = false, ExpectResult = false},
                new { LeftColumn =  1,  LeftRow =  0,  RightColumn = 1,  RightRow = 1,  Fill = false, ExpectResult = false},
                new { LeftColumn =  0,  LeftRow =  1,  RightColumn = 1,  RightRow = 1,  Fill = false, ExpectResult = false},
                new { LeftColumn =  0,  LeftRow =  0,  RightColumn = 1,  RightRow = 1,  Fill = false, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 10, RightRow = 1,  Fill = false, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow = 10, Fill = false, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 0,  RightRow =  1, Fill = false, ExpectResult = false},
                new { LeftColumn =  10, LeftRow =  10, RightColumn = 1,  RightRow =  0, Fill = false, ExpectResult = false}
            };

            foreach (var r in rules)
            {
                Matrix<T> lhs = null;
                Matrix<T> rhs = null;
                Matrix<T> ret = null;

                try
                {
                    if (r.ExpectResult)
                    {
                        lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                        rhs = new Matrix<T>(r.RightRow, r.RightColumn);

                        if (r.Fill)
                            FillMatrixByNonZero(rhs, r.RightRow * r.RightColumn);

                        ret = lhs / rhs;
                    }
                    else
                    {
                        try
                        {
                            lhs = new Matrix<T>(r.LeftRow, r.LeftColumn);
                            rhs = new Matrix<T>(r.RightRow, r.RightColumn);

                            if (r.Fill)
                                FillMatrixByNonZero(rhs, r.RightRow * r.RightColumn);

                            ret = lhs / rhs;

                            Assert.True(false, $"{lhs.GetType().Name} should throw exception for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
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
                    Console.WriteLine($"Failed to create for Type: {lhs.MatrixElementType}, LeftRow: {r.LeftRow}, LeftColumn: {r.LeftColumn}, RightRow: {r.RightRow}, RightColumn: {r.RightColumn}.");
                    throw;
                }
                finally
                {
                    if (lhs != null)
                        this.DisposeAndCheckDisposedState(lhs);
                    if (rhs != null)
                        this.DisposeAndCheckDisposedState(rhs);
                    if (ret != null)
                        this.DisposeAndCheckDisposedState(ret);
                }
            }
        }

        private static void FillMatrixByNonZero<T>(Matrix<T> matrix, int length)
            where T : struct
        {
            var rand = new Random();
            switch (matrix.MatrixElementType)
            {
                case MatrixElementTypes.UInt8:
                    {
                        var tmp = matrix as Matrix<byte>;
                        var array = new byte[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = (byte)rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
                case MatrixElementTypes.UInt16:
                    {
                        var tmp = matrix as Matrix<ushort>;
                        var array = new ushort[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = (ushort)rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
                case MatrixElementTypes.UInt32:
                    {
                        var tmp = matrix as Matrix<uint>;
                        var array = new uint[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = (uint)rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
                case MatrixElementTypes.Int8:
                    {
                        var tmp = matrix as Matrix<sbyte>;
                        var array = new sbyte[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = (sbyte)rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
                case MatrixElementTypes.Int16:
                    {
                        var tmp = matrix as Matrix<short>;
                        var array = new short[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = (short)rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
                case MatrixElementTypes.Int32:
                    {
                        var tmp = matrix as Matrix<int>;
                        var array = new int[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
                case MatrixElementTypes.Float:
                    {
                        var tmp = matrix as Matrix<float>;
                        var array = new float[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
                case MatrixElementTypes.Double:
                    {
                        var tmp = matrix as Matrix<double>;
                        var array = new double[length];
                        for (var index = 0; index < array.Length; index++)
                            array[index] = rand.Next(1, 100);
                        tmp.Assign(array);
                    }
                    break;
            }
        }

        internal static unsafe Matrix<T> FillMatrixByNonZero<T>(int row, int column, out T[] result, out byte[] bytes)
            where T : struct
        {
            var rand = new Random();
            var length = row * column;
            using (var matrix = new Matrix<T>())
                switch (matrix.MatrixElementType)
                {
                    case MatrixElementTypes.UInt8:
                        {
                            var array = new byte[length];
                            for (var index = 0; index < array.Length; index++)
                                array[index] = (byte)rand.Next(1, 100);
                            result = array as T[];
                            bytes = array;
                            return new Matrix<byte>(array, row, column, 1) as Matrix<T>;
                        }
                    case MatrixElementTypes.UInt16:
                        {
                            var tmp = new ushort[length];
                            for (var index = 0; index < tmp.Length; index++)
                                tmp[index] = (ushort)rand.Next(1, 100);

                            var array = new byte[length * sizeof(ushort)];
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                var t = BitConverter.GetBytes(tmp[i]);
                                for (var j = 0; j < t.Length; j++)
                                    array[i * t.Length + j] = t[j];
                            }

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<ushort>(array, row, column, sizeof(ushort)) as Matrix<T>;
                        }
                    case MatrixElementTypes.UInt32:
                        {
                            var tmp = new uint[length];
                            for (var index = 0; index < tmp.Length; index++)
                                tmp[index] = (uint)rand.Next(1, 100);

                            var array = new byte[length * sizeof(uint)];
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                var t = BitConverter.GetBytes(tmp[i]);
                                for (var j = 0; j < t.Length; j++)
                                    array[i * t.Length + j] = t[j];
                            }

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<uint>(array, row, column, sizeof(uint)) as Matrix<T>;
                        }
                    case MatrixElementTypes.Int8:
                        {
                            var tmp = new sbyte[length];
                            var array = new byte[length];
                            for (var index = 0; index < array.Length; index++)
                            {
                                array[index] = (byte)rand.Next(1, 100);
                                tmp[index] = (sbyte)array[index];
                            }

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<sbyte>(array, row, column, 1) as Matrix<T>;
                        }
                    case MatrixElementTypes.Int16:
                        {
                            var tmp = new short[length];
                            for (var index = 0; index < tmp.Length; index++)
                                tmp[index] = (short)rand.Next(1, 100);

                            var array = new byte[length * sizeof(short)];
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                var t = BitConverter.GetBytes(tmp[i]);
                                for (var j = 0; j < t.Length; j++)
                                    array[i * t.Length + j] = t[j];
                            }

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<short>(array, row, column, sizeof(short)) as Matrix<T>;
                        }
                    case MatrixElementTypes.Int32:
                        {
                            var tmp = new int[length];
                            for (var index = 0; index < tmp.Length; index++)
                                tmp[index] = rand.Next(1, 100);

                            var array = new byte[length * sizeof(int)];
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                var t = BitConverter.GetBytes(tmp[i]);
                                for (var j = 0; j < t.Length; j++)
                                    array[i * t.Length + j] = t[j];
                            }

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<int>(array, row, column, sizeof(int)) as Matrix<T>;
                        }
                    case MatrixElementTypes.Float:
                        {
                            var tmp = new float[length];
                            for (var index = 0; index < tmp.Length; index++)
                                tmp[index] = rand.Next(1, 100);

                            var array = new byte[length * sizeof(float)];
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                var t = BitConverter.GetBytes(tmp[i]);
                                for (var j = 0; j < t.Length; j++)
                                    array[i * t.Length + j] = t[j];
                            }

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<float>(array, row, column, sizeof(float)) as Matrix<T>;
                        }
                    case MatrixElementTypes.Double:
                        {
                            var tmp = new double[length];
                            for (var index = 0; index < tmp.Length; index++)
                                tmp[index] = rand.Next(1, 100);

                            var array = new byte[length * sizeof(double)];
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                var t = BitConverter.GetBytes(tmp[i]);
                                for (var j = 0; j < t.Length; j++)
                                    array[i * t.Length + j] = t[j];
                            }

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<double>(array, row, column, sizeof(double)) as Matrix<T>;
                        }
                    case MatrixElementTypes.RgbPixel:
                        {
                            var tmp = new RgbPixel[length];
                            for (var index = 0; index < tmp.Length; index++)
                            {
                                tmp[index].Blue = (byte)rand.Next(1, 100);
                                tmp[index].Green = (byte)rand.Next(1, 100);
                                tmp[index].Red = (byte)rand.Next(1, 100);
                            }

                            var array = new byte[length * sizeof(RgbPixel)];
                            var buffer = Marshal.AllocHGlobal(sizeof(RgbPixel));
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                Marshal.StructureToPtr(tmp[i], buffer, false);
                                Marshal.Copy(buffer, array, i * sizeof(RgbPixel), sizeof(RgbPixel));
                            }
                            Marshal.FreeHGlobal(buffer);

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<RgbPixel>(array, row, column, sizeof(RgbPixel)) as Matrix<T>;
                        }
                    case MatrixElementTypes.RgbAlphaPixel:
                        {
                            var tmp = new RgbAlphaPixel[length];
                            for (var index = 0; index < tmp.Length; index++)
                            {
                                tmp[index].Blue = (byte)rand.Next(1, 100);
                                tmp[index].Green = (byte)rand.Next(1, 100);
                                tmp[index].Red = (byte)rand.Next(1, 100);
                                tmp[index].Alpha = (byte)rand.Next(1, 100);
                            }

                            var array = new byte[length * sizeof(RgbAlphaPixel)];
                            var buffer = Marshal.AllocHGlobal(sizeof(RgbAlphaPixel));
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                Marshal.StructureToPtr(tmp[i], buffer, false);
                                Marshal.Copy(buffer, array, i * sizeof(RgbAlphaPixel), sizeof(RgbAlphaPixel));
                            }
                            Marshal.FreeHGlobal(buffer);

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<RgbAlphaPixel>(array, row, column, sizeof(RgbAlphaPixel)) as Matrix<T>;
                        }
                    case MatrixElementTypes.HsiPixel:
                        {
                            var tmp = new HsiPixel[length];
                            for (var index = 0; index < tmp.Length; index++)
                            {
                                tmp[index].H = (byte)rand.Next(1, 100);
                                tmp[index].S = (byte)rand.Next(1, 100);
                                tmp[index].I = (byte)rand.Next(1, 100);
                            }

                            var array = new byte[length * sizeof(HsiPixel)];
                            var buffer = Marshal.AllocHGlobal(sizeof(HsiPixel));
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                Marshal.StructureToPtr(tmp[i], buffer, false);
                                Marshal.Copy(buffer, array, i * sizeof(HsiPixel), sizeof(HsiPixel));
                            }
                            Marshal.FreeHGlobal(buffer);

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<HsiPixel>(array, row, column, sizeof(HsiPixel)) as Matrix<T>;
                        }
                    case MatrixElementTypes.LabPixel:
                        {
                            var tmp = new LabPixel[length];
                            for (var index = 0; index < tmp.Length; index++)
                            {
                                tmp[index].L = (byte)rand.Next(1, 100);
                                tmp[index].A = (byte)rand.Next(1, 100);
                                tmp[index].B = (byte)rand.Next(1, 100);
                            }

                            var array = new byte[length * sizeof(LabPixel)];
                            var buffer = Marshal.AllocHGlobal(sizeof(LabPixel));
                            for (var i = 0; i < tmp.Length; i++)
                            {
                                Marshal.StructureToPtr(tmp[i], buffer, false);
                                Marshal.Copy(buffer, array, i * sizeof(LabPixel), sizeof(LabPixel));
                            }
                            Marshal.FreeHGlobal(buffer);

                            result = tmp as T[];
                            bytes = array;
                            return new Matrix<LabPixel>(array, row, column, sizeof(LabPixel)) as Matrix<T>;
                        }
                }

            result = null;
            bytes = null;
            return null;
        }

        private void AssignHelp(TwoDimensionObjectBase obj, int[] array)
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
                intMatrix.Assign(array.Select(i => i).ToArray());
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

        private void CheckRowsColumnsSize(TwoDimensionObjectBase obj, int row, int column)
        {
            if (obj is Matrix<sbyte> sbyteMatrix)
            {
                Assert.Equal(sbyteMatrix.Rows, row);
                Assert.Equal(sbyteMatrix.Columns, column);
                Assert.Equal(sbyteMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<short> shortMatrix)
            {
                Assert.Equal(shortMatrix.Rows, row);
                Assert.Equal(shortMatrix.Columns, column);
                Assert.Equal(shortMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<int> intMatrix)
            {
                Assert.Equal(intMatrix.Rows, row);
                Assert.Equal(intMatrix.Columns, column);
                Assert.Equal(intMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<byte> byteMatrix)
            {
                Assert.Equal(byteMatrix.Rows, row);
                Assert.Equal(byteMatrix.Columns, column);
                Assert.Equal(byteMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<ushort> ushortMatrix)
            {
                Assert.Equal(ushortMatrix.Rows, row);
                Assert.Equal(ushortMatrix.Columns, column);
                Assert.Equal(ushortMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<uint> uintMatrix)
            {
                Assert.Equal(uintMatrix.Rows, row);
                Assert.Equal(uintMatrix.Columns, column);
                Assert.Equal(uintMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<float> floatMatrix)
            {
                Assert.Equal(floatMatrix.Rows, row);
                Assert.Equal(floatMatrix.Columns, column);
                Assert.Equal(floatMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<double> doubleMatrix)
            {
                Assert.Equal(doubleMatrix.Rows, row);
                Assert.Equal(doubleMatrix.Columns, column);
                Assert.Equal(doubleMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<RgbPixel> rgbPixelMatrix)
            {
                Assert.Equal(rgbPixelMatrix.Rows, row);
                Assert.Equal(rgbPixelMatrix.Columns, column);
                Assert.Equal(rgbPixelMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<BgrPixel> bgrPixelMatrix)
            {
                Assert.Equal(bgrPixelMatrix.Rows, row);
                Assert.Equal(bgrPixelMatrix.Columns, column);
                Assert.Equal(bgrPixelMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<RgbAlphaPixel> rgbAlphaPixelMatrix)
            {
                Assert.Equal(rgbAlphaPixelMatrix.Rows, row);
                Assert.Equal(rgbAlphaPixelMatrix.Columns, column);
                Assert.Equal(rgbAlphaPixelMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<HsiPixel> hsiPixelMatrix)
            {
                Assert.Equal(hsiPixelMatrix.Rows, row);
                Assert.Equal(hsiPixelMatrix.Columns, column);
                Assert.Equal(hsiPixelMatrix.Size, row * column);
                return;
            }

            if (obj is Matrix<LabPixel> labPixelMatrix)
            {
                Assert.Equal(labPixelMatrix.Rows, row);
                Assert.Equal(labPixelMatrix.Columns, column);
                Assert.Equal(labPixelMatrix.Size, row * column);
                return;
            }

            throw new NotSupportedException();
        }

        internal static TwoDimensionObjectBase CreateMatrix(MatrixElementTypes elementTypes)
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
                case MatrixElementTypes.LabPixel:
                    return new Matrix<HsiPixel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementTypes), elementTypes, null);
            }
        }

        internal static TwoDimensionObjectBase CreateMatrix(MatrixElementTypes elementTypes, int rows = 0, int columns = 0)
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
                case MatrixElementTypes.BgrPixel:
                    return new Matrix<BgrPixel>(rows, columns);
                case MatrixElementTypes.RgbPixel:
                    return new Matrix<RgbPixel>(rows, columns);
                case MatrixElementTypes.RgbAlphaPixel:
                    return new Matrix<RgbAlphaPixel>(rows, columns);
                case MatrixElementTypes.HsiPixel:
                    return new Matrix<HsiPixel>(rows, columns);
                case MatrixElementTypes.LabPixel:
                    return new Matrix<LabPixel>(rows, columns);
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementTypes), elementTypes, null);
            }
        }

    }

}
