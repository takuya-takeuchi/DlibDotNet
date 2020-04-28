using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Tests.Matrix;
using Xunit;

namespace DlibDotNet.Tests.Optimization
{

    public class MaxCostAssignmentTest : TestBase
    {

        [Fact]
        public void MaxCostAssignment1()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.UInt8,         Rows = 3, Columns = 3, ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        Rows = 3, Columns = 3, ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        Rows = 3, Columns = 3, ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          Rows = 3, Columns = 3, ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         Rows = 3, Columns = 3, ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         Rows = 3, Columns = 3, ExpectResult = true}
            };

            var datas = tests.Select(arg => new TestData
            {
                Data = MatrixTest.CreateMatrix(arg.Type, arg.Rows, arg.Columns),
                ExpectResult = arg.ExpectResult
            });

            this.ExecuteMaxCostAssignments(datas);
        }

        [Fact]
        public void MaxCostAssignment2()
        {
            var matrix = new Matrix<int>(3,3);
            matrix.Assign(new[]{
                1, 2, 6,
                5, 3, 6,
                4, 5, 0});

            var assignments = Dlib.MaxCostAssignment(matrix).ToArray();
            Assert.Equal(assignments.Length, 3);
            foreach (var assignment in assignments)
                Console.WriteLine(assignment);
            Assert.Equal(assignments[0], 2);
            Assert.Equal(assignments[1], 0);
            Assert.Equal(assignments[2], 1);

            this.DisposeAndCheckDisposedState(matrix);
        }

        [Fact]
        public void MaxCostAssignmentThrowException1()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      Rows = 0, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.RgbAlphaPixel, Rows = 0, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.HsiPixel,      Rows = 0, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.Float,         Rows = 0, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.Double,        Rows = 0, Columns = 0, ExpectResult = false}
            };

            var datas = tests.Select(arg => new TestData
            {
                Data = MatrixTest.CreateMatrix(arg.Type, arg.Rows, arg.Columns),
                ExpectResult = arg.ExpectResult
            });

            this.ExecuteMaxCostAssignments(datas);
        }

        [Fact]
        public void MaxCostAssignmentThrowException2()
        {
            var tests = new[]
            {
                new { Type = MatrixElementTypes.Int16,         Rows = 1, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.Int16,         Rows = 0, Columns = 1, ExpectResult = false},
                new { Type = MatrixElementTypes.Int32,         Rows = 1, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.Int32,         Rows = 0, Columns = 1, ExpectResult = false},
                new { Type = MatrixElementTypes.Int8,          Rows = 1, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.Int8,          Rows = 0, Columns = 1, ExpectResult = false},
                new { Type = MatrixElementTypes.UInt16,        Rows = 1, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.UInt16,        Rows = 0, Columns = 1, ExpectResult = false},
                new { Type = MatrixElementTypes.UInt32,        Rows = 1, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.UInt32,        Rows = 0, Columns = 1, ExpectResult = false},
                new { Type = MatrixElementTypes.UInt8,         Rows = 1, Columns = 0, ExpectResult = false},
                new { Type = MatrixElementTypes.UInt8,         Rows = 0, Columns = 1, ExpectResult = false}
            };

            var datas = tests.Select(arg => new TestData
            {
                Data = MatrixTest.CreateMatrix(arg.Type, arg.Rows, arg.Columns),
                ExpectResult = arg.ExpectResult
            });

            this.ExecuteMaxCostAssignments(datas);
        }

        private void ExecuteMaxCostAssignments(IEnumerable<TestData> datas)
        {
            foreach (var test in datas)
            {
                var data = test.Data;
                var matrixElementType = test.MatrixElementType;

                try
                {
                    if (test.ExpectResult)
                    {
                        this.ExecuteMaxCostAssignment(data);
                    }
                    else
                    {
                        try
                        {
                            this.ExecuteMaxCostAssignment(data);
                            Assert.True(false, $"ExecuteMaxCostAssignment should throw exception for Type: {matrixElementType}, Rows: {data.Rows}, Columns: {data.Columns}");
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
                    Console.WriteLine($"Failed to execute ExecuteMaxCostAssignment for Type: {matrixElementType}, Rows: {data.Rows}, Columns: {data.Columns}");
                    throw;
                }
                finally
                {
                    this.DisposeAndCheckDisposedState(data);
                }
            }
        }
        private void ExecuteMaxCostAssignment(TwoDimensionObjectBase obj)
        {
            if (obj is Matrix<sbyte> sbyteMatrix)
            {
                Dlib.MaxCostAssignment(sbyteMatrix);
                return;
            }

            if (obj is Matrix<short> shortMatrix)
            {
                Dlib.MaxCostAssignment(shortMatrix);
                return;
            }

            if (obj is Matrix<int> intMatrix)
            {
                Dlib.MaxCostAssignment(intMatrix);
                return;
            }

            if (obj is Matrix<byte> byteMatrix)
            {
                Dlib.MaxCostAssignment(byteMatrix);
                return;
            }

            if (obj is Matrix<ushort> ushortMatrix)
            {
                Dlib.MaxCostAssignment(ushortMatrix);
                return;
            }

            if (obj is Matrix<uint> uintMatrix)
            {
                Dlib.MaxCostAssignment(uintMatrix);
                return;
            }

            if (obj is Matrix<float> floatMatrix)
            {
                Dlib.MaxCostAssignment(floatMatrix);
                return;
            }

            if (obj is Matrix<double> doubleMatrix)
            {
                Dlib.MaxCostAssignment(doubleMatrix);
                return;
            }

            if (obj is Matrix<RgbPixel> rgbPixelMatrix)
            {
                Dlib.MaxCostAssignment(rgbPixelMatrix);
                return;
            }

            if (obj is Matrix<RgbAlphaPixel> rgbAlphaPixelMatrix)
            {
                Dlib.MaxCostAssignment(rgbAlphaPixelMatrix);
                return;
            }

            if (obj is Matrix<HsiPixel> hsiPicelMatrix)
            {
                Dlib.MaxCostAssignment(hsiPicelMatrix);
                return;
            }

            throw new NotSupportedException();
        }

        internal sealed class TestData : TestDataBase
        {

            public TwoDimensionObjectBase Data
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
