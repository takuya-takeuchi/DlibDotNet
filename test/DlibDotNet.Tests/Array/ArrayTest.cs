using System.Collections.Generic;
using Xunit;

namespace DlibDotNet.Tests.Array
{

    public class ArrayTest : TestBase
    {

        [Fact]
        public void Indexer()
        {
            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            const int length = 100;

            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        using (var array = new Array<RgbPixel>())
                        {
                            var list = new List<RgbPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextRgbPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        using (var array = new Array<RgbAlphaPixel>())
                        {
                            var list = new List<RgbAlphaPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextRgbAlphaPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        using (var array = new Array<HsiPixel>())
                        {
                            var list = new List<HsiPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextHsiPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.LabPixel:
                        using (var array = new Array<LabPixel>())
                        {
                            var list = new List<LabPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextLabPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.UInt8:
                        using (var array = new Array<byte>())
                        {
                            var list = new List<byte>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextByteRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.UInt16:
                        using (var array = new Array<ushort>())
                        {
                            var list = new List<ushort>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextUInt16Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.UInt32:
                        using (var array = new Array<uint>())
                        {
                            var list = new List<uint>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextUInt32Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.Int8:
                        using (var array = new Array<sbyte>())
                        {
                            var list = new List<sbyte>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextSByteRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.Int16:
                        using (var array = new Array<short>())
                        {
                            var list = new List<short>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextInt16Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.Int32:
                        using (var array = new Array<int>())
                        {
                            var list = new List<int>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextInt32Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.Float:
                        using (var array = new Array<float>())
                        {
                            var list = new List<float>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextFloatRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                    case ImageTypes.Double:
                        using (var array = new Array<double>())
                        {
                            var list = new List<double>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextDoubleRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                                Assert.Equal(list[index], array[index]);
                        }
                        break;
                }
            }
        }

        [Fact]
        public void IndexerArray2D()
        {
            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            const int length = 10;
            const int rows = 20;
            const int columns = 30;

            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        using (var array = new Array<Array2D<RgbPixel>>())
                        {
                            var list = new List<Array2D<RgbPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DRgbPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        using (var array = new Array<Array2D<RgbAlphaPixel>>())
                        {
                            var list = new List<Array2D<RgbAlphaPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DRgbAlphaPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        using (var array = new Array<Array2D<HsiPixel>>())
                        {
                            var list = new List<Array2D<HsiPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DHsiPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.LabPixel:
                        using (var array = new Array<Array2D<LabPixel>>())
                        {
                            var list = new List<Array2D<LabPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DLabPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt8:
                        using (var array = new Array<Array2D<byte>>())
                        {
                            var list = new List<Array2D<byte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt16:
                        using (var array = new Array<Array2D<ushort>>())
                        {
                            var list = new List<Array2D<ushort>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DUInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt32:
                        using (var array = new Array<Array2D<uint>>())
                        {
                            var list = new List<Array2D<uint>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DUInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int8:
                        using (var array = new Array<Array2D<sbyte>>())
                        {
                            var list = new List<Array2D<sbyte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DSByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int16:
                        using (var array = new Array<Array2D<short>>())
                        {
                            var list = new List<Array2D<short>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int32:
                        using (var array = new Array<Array2D<int>>())
                        {
                            var list = new List<Array2D<int>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Float:
                        using (var array = new Array<Array2D<float>>())
                        {
                            var list = new List<Array2D<float>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DFloat(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Double:
                        using (var array = new Array<Array2D<double>>())
                        {
                            var list = new List<Array2D<double>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DDouble(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                }
            }
        }

        [Fact]
        public void IndexerMatrix()
        {
            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            const int length = 10;
            const int rows = 20;
            const int columns = 30;

            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        using (var array = new Array<Matrix<RgbPixel>>())
                        {
                            var list = new List<Matrix<RgbPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixRgbPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        using (var array = new Array<Matrix<RgbAlphaPixel>>())
                        {
                            var list = new List<Matrix<RgbAlphaPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixRgbAlphaPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        using (var array = new Array<Matrix<HsiPixel>>())
                        {
                            var list = new List<Matrix<HsiPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixHsiPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.LabPixel:
                        using (var array = new Array<Matrix<LabPixel>>())
                        {
                            var list = new List<Matrix<LabPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixLabPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt8:
                        using (var array = new Array<Matrix<byte>>())
                        {
                            var list = new List<Matrix<byte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt16:
                        using (var array = new Array<Matrix<ushort>>())
                        {
                            var list = new List<Matrix<ushort>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixUInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt32:
                        using (var array = new Array<Matrix<uint>>())
                        {
                            var list = new List<Matrix<uint>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixUInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int8:
                        using (var array = new Array<Matrix<sbyte>>())
                        {
                            var list = new List<Matrix<sbyte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixSByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int16:
                        using (var array = new Array<Matrix<short>>())
                        {
                            var list = new List<Matrix<short>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int32:
                        using (var array = new Array<Matrix<int>>())
                        {
                            var list = new List<Matrix<int>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Float:
                        using (var array = new Array<Matrix<float>>())
                        {
                            var list = new List<Matrix<float>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixFloat(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Double:
                        using (var array = new Array<Matrix<double>>())
                        {
                            var list = new List<Matrix<double>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixDouble(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            for (var index = 0; index < array.Size; index++)
                            {
                                Assert.Equal(rows, array[index].Rows);
                                Assert.Equal(columns, array[index].Columns);
                                array[index].Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                }
            }
        }

        [Fact]
        public void PushBack()
        {
            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            const int length = 100;

            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        using (var array = new Array<RgbPixel>())
                        {
                            var list = new List<RgbPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextRgbPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        using (var array = new Array<RgbAlphaPixel>())
                        {
                            var list = new List<RgbAlphaPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextRgbAlphaPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        using (var array = new Array<HsiPixel>())
                        {
                            var list = new List<HsiPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextHsiPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.LabPixel:
                        using (var array = new Array<LabPixel>())
                        {
                            var list = new List<LabPixel>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextLabPixelRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.UInt8:
                        using (var array = new Array<byte>())
                        {
                            var list = new List<byte>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextByteRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.UInt16:
                        using (var array = new Array<ushort>())
                        {
                            var list = new List<ushort>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextUInt16Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.UInt32:
                        using (var array = new Array<uint>())
                        {
                            var list = new List<uint>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextUInt32Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.Int8:
                        using (var array = new Array<sbyte>())
                        {
                            var list = new List<sbyte>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextSByteRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.Int16:
                        using (var array = new Array<short>())
                        {
                            var list = new List<short>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextInt16Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.Int32:
                        using (var array = new Array<int>())
                        {
                            var list = new List<int>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextInt32Random());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.Float:
                        using (var array = new Array<float>())
                        {
                            var list = new List<float>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextFloatRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                    case ImageTypes.Double:
                        using (var array = new Array<double>())
                        {
                            var list = new List<double>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.NextDoubleRandom());

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            var index = 0;
                            foreach (var item in array)
                                Assert.Equal(list[index++], item);
                        }
                        break;
                }
            }
        }

        [Fact]
        public void PushBackArray2D()
        {
            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            const int length = 10;
            const int rows = 20;
            const int columns = 30;

            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        using (var array = new Array<Array2D<RgbPixel>>())
                        {
                            var list = new List<Array2D<RgbPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DRgbPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        using (var array = new Array<Array2D<RgbAlphaPixel>>())
                        {
                            var list = new List<Array2D<RgbAlphaPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DRgbAlphaPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        using (var array = new Array<Array2D<HsiPixel>>())
                        {
                            var list = new List<Array2D<HsiPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DHsiPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.LabPixel:
                        using (var array = new Array<Array2D<LabPixel>>())
                        {
                            var list = new List<Array2D<LabPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DLabPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt8:
                        using (var array = new Array<Array2D<byte>>())
                        {
                            var list = new List<Array2D<byte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt16:
                        using (var array = new Array<Array2D<ushort>>())
                        {
                            var list = new List<Array2D<ushort>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DUInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt32:
                        using (var array = new Array<Array2D<uint>>())
                        {
                            var list = new List<Array2D<uint>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DUInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int8:
                        using (var array = new Array<Array2D<sbyte>>())
                        {
                            var list = new List<Array2D<sbyte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DSByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int16:
                        using (var array = new Array<Array2D<short>>())
                        {
                            var list = new List<Array2D<short>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int32:
                        using (var array = new Array<Array2D<int>>())
                        {
                            var list = new List<Array2D<int>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Float:
                        using (var array = new Array<Array2D<float>>())
                        {
                            var list = new List<Array2D<float>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DFloat(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Double:
                        using (var array = new Array<Array2D<double>>())
                        {
                            var list = new List<Array2D<double>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomArray2DDouble(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                }
            }
        }

        [Fact]
        public void PushBackMatrix()
        {
            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            const int length = 10;
            const int rows = 20;
            const int columns = 30;

            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        using (var array = new Array<Matrix<RgbPixel>>())
                        {
                            var list = new List<Matrix<RgbPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixRgbPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        using (var array = new Array<Matrix<RgbAlphaPixel>>())
                        {
                            var list = new List<Matrix<RgbAlphaPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixRgbAlphaPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        using (var array = new Array<Matrix<HsiPixel>>())
                        {
                            var list = new List<Matrix<HsiPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixHsiPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.LabPixel:
                        using (var array = new Array<Matrix<LabPixel>>())
                        {
                            var list = new List<Matrix<LabPixel>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixLabPixel(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt8:
                        using (var array = new Array<Matrix<byte>>())
                        {
                            var list = new List<Matrix<byte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt16:
                        using (var array = new Array<Matrix<ushort>>())
                        {
                            var list = new List<Matrix<ushort>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixUInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.UInt32:
                        using (var array = new Array<Matrix<uint>>())
                        {
                            var list = new List<Matrix<uint>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixUInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int8:
                        using (var array = new Array<Matrix<sbyte>>())
                        {
                            var list = new List<Matrix<sbyte>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixSByte(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int16:
                        using (var array = new Array<Matrix<short>>())
                        {
                            var list = new List<Matrix<short>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixInt16(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Int32:
                        using (var array = new Array<Matrix<int>>())
                        {
                            var list = new List<Matrix<int>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixInt32(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Float:
                        using (var array = new Array<Matrix<float>>())
                        {
                            var list = new List<Matrix<float>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixFloat(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                    case ImageTypes.Double:
                        using (var array = new Array<Matrix<double>>())
                        {
                            var list = new List<Matrix<double>>();
                            for (var i = 0; i < length; i++)
                                list.Add(this.CreateRandomMatrixDouble(rows, columns));

                            foreach (var value in list)
                                array.PushBack(value);

                            Assert.Equal(array.Size, list.Count);

                            foreach (var item in array)
                            {
                                Assert.Equal(rows, item.Rows);
                                Assert.Equal(columns, item.Columns);
                                item.Dispose();
                            }

                            foreach (var item in list)
                                item.Dispose();
                        }
                        break;
                }
            }
        }

    }

}
