using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace DlibDotNet.Tests
{

    public abstract class TestBase
    {

        #region Fields

        private readonly Random _Random;

        #endregion

        #region Constructors

        protected TestBase()
        {
            this._Random = new Random();
        }

        #endregion

        #region Properties

        public bool CanGuiDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        #endregion

        #region Methods

        public void DisposeAndCheckDisposedState(DlibObject obj)
        {
            obj.Dispose();
            Assert.True(obj.IsDisposed);
            Assert.True(obj.NativePtr == IntPtr.Zero);
        }

        public void DisposeAndCheckDisposedStates(IEnumerable<DlibObject> objs)
        {
            foreach (var obj in objs)
                this.DisposeAndCheckDisposedState(obj);
        }

        public static void DoTest<T>(Func<bool, T> outputImageFunc, bool expect, Action<T> successAction, Action finallyAction, Action failAction, Action exceptionAction)
            where T : class
        {
            try
            {
                try
                {
                    var outputImage = outputImageFunc(expect);

                    if (!expect)
                    {
                        failAction();
                    }
                    else
                    {
                        successAction(outputImage);
                    }
                }
                catch (ArgumentException)
                {
                    if (!expect)
                        Console.WriteLine("OK");
                    else
                        throw;
                }
                catch (NotSupportedException)
                {
                    if (!expect)
                        Console.WriteLine("OK");
                    else
                        throw;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                exceptionAction();
                throw;
            }
            finally
            {
                finallyAction();
            }
        }

        public FileInfo GetDataFile(string filename)
        {
            return new FileInfo(Path.Combine("data", filename));
        }

        public IEnumerable<FileInfo> GetDataFiles(string directoryName)
        {
            var dir = new DirectoryInfo(Path.Combine("data", directoryName));
            return dir.GetFiles();
        }

        public string GetOutDir(string function)
        {
            var path = Path.Combine("out", function);
            Directory.CreateDirectory(path);
            return path;
        }

        public string GetOutDir(params string[] function)
        {
            var path = Path.Combine("out", Path.Combine(function));
            Directory.CreateDirectory(path);
            return path;
        }

        public static long GetCurrentMemory()
        {
            return Environment.WorkingSet;
            //return GC.GetTotalMemory(true);
        }

        #region Random

        public byte NextByteRandom()
        {
            return (byte)this._Random.Next(0, 255);
        }

        public sbyte NextSByteRandom()
        {
            return (sbyte)this._Random.Next(sbyte.MinValue, sbyte.MaxValue);
        }

        public ushort NextUInt16Random()
        {
            return (ushort)this._Random.Next(ushort.MinValue, ushort.MaxValue);
        }

        public short NextInt16Random()
        {
            return (short)this._Random.Next(short.MinValue, short.MaxValue);
        }

        public int NextInt32Random()
        {
            return this._Random.Next(int.MinValue, int.MaxValue);
        }

        public uint NextUInt32Random()
        {
            return (uint)this._Random.Next(0, int.MaxValue);
        }

        public int NextRandom()
        {
            return this._Random.Next();
        }

        public int NextRandom(int maxValue)
        {
            return this._Random.Next(maxValue);
        }

        public int NextRandom(int minValue, int maxValue)
        {
            return this._Random.Next(minValue, maxValue);
        }

        public double NextDoubleRandom()
        {
            return this._Random.NextDouble();
        }

        public float NextFloatRandom()
        {
            return (float)this._Random.NextDouble();
        }

        public RgbAlphaPixel NextRgbAlphaPixelRandom()
        {
            return this.NextRgbAlphaPixelRandom(0, 255);
        }

        public RgbAlphaPixel NextRgbAlphaPixelRandom(int minValue, int maxValue)
        {
            return new RgbAlphaPixel
            {
                Blue = (byte)this._Random.Next(minValue, maxValue),
                Green = (byte)this._Random.Next(minValue, maxValue),
                Red = (byte)this._Random.Next(minValue, maxValue),
                Alpha = (byte)this._Random.Next(minValue, maxValue)
            };
        }

        public RgbPixel NextRgbPixelRandom()
        {
            return this.NextRgbPixelRandom(0, 255);
        }

        public RgbPixel NextRgbPixelRandom(int minValue, int maxValue)
        {
            return new RgbPixel
            {
                Blue = (byte)this._Random.Next(minValue, maxValue),
                Green = (byte)this._Random.Next(minValue, maxValue),
                Red = (byte)this._Random.Next(minValue, maxValue)
            };
        }

        public HsiPixel NextHsiPixelRandom()
        {
            return this.NextHsiPixelRandom(0, 255);
        }

        public HsiPixel NextHsiPixelRandom(int minValue, int maxValue)
        {
            return new HsiPixel
            {
                H = (byte)this._Random.Next(minValue, maxValue),
                S = (byte)this._Random.Next(minValue, maxValue),
                I = (byte)this._Random.Next(minValue, maxValue)
            };
        }

        #region Array2D

        public Array2D<Byte> CreateRandomArray2DByte(int rows, int columns)
        {
            var array = new Array2D<byte>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextByteRandom();

            return array;
        }

        public Array2D<UInt16> CreateRandomArray2DUInt16(int rows, int columns)
        {
            var array = new Array2D<ushort>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextUInt16Random();

            return array;
        }

        public Array2D<UInt32> CreateRandomArray2DUInt32(int rows, int columns)
        {
            var array = new Array2D<uint>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextUInt32Random();

            return array;
        }

        public Array2D<SByte> CreateRandomArray2DSByte(int rows, int columns)
        {
            var array = new Array2D<sbyte>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextSByteRandom();

            return array;
        }

        public Array2D<Int16> CreateRandomArray2DInt16(int rows, int columns)
        {
            var array = new Array2D<short>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextInt16Random();

            return array;
        }

        public Array2D<Int32> CreateRandomArray2DInt32(int rows, int columns)
        {
            var array = new Array2D<int>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextInt32Random();

            return array;
        }

        public Array2D<float> CreateRandomArray2DFloat(int rows, int columns)
        {
            var array = new Array2D<float>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextFloatRandom();

            return array;
        }

        public Array2D<Double> CreateRandomArray2DDouble(int rows, int columns)
        {
            var array = new Array2D<double>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r][c] = this.NextDoubleRandom();

            return array;
        }

        public Array2D<RgbPixel> CreateRandomArray2DRgbPixel(int rows, int columns)
        {
            var array = new Array2D<RgbPixel>(rows, columns);
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
                array[r][c] = this.NextRgbPixelRandom();

            return array;
        }

        public Array2D<RgbAlphaPixel> CreateRandomArray2DRgbAlphaPixel(int rows, int columns)
        {
            var array = new Array2D<RgbAlphaPixel>(rows, columns);
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
                array[r][c] = this.NextRgbAlphaPixelRandom();

            return array;
        }

        public Array2D<HsiPixel> CreateRandomArray2DHsiPixel(int rows, int columns)
        {
            var array = new Array2D<HsiPixel>(rows, columns);
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
                array[r][c] = this.NextHsiPixelRandom();

            return array;
        }

        #endregion

        #region Matrix

        public Matrix<Byte> CreateRandomMatrixByte(int rows, int columns)
        {
            var array = new Matrix<byte>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextByteRandom();

            return array;
        }

        public Matrix<UInt16> CreateRandomMatrixUInt16(int rows, int columns)
        {
            var array = new Matrix<ushort>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextUInt16Random();

            return array;
        }

        public Matrix<UInt32> CreateRandomMatrixUInt32(int rows, int columns)
        {
            var array = new Matrix<uint>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextUInt32Random();

            return array;
        }

        public Matrix<SByte> CreateRandomMatrixSByte(int rows, int columns)
        {
            var array = new Matrix<sbyte>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextSByteRandom();

            return array;
        }

        public Matrix<Int16> CreateRandomMatrixInt16(int rows, int columns)
        {
            var array = new Matrix<short>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextInt16Random();

            return array;
        }

        public Matrix<Int32> CreateRandomMatrixInt32(int rows, int columns)
        {
            var array = new Matrix<int>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextInt32Random();

            return array;
        }

        public Matrix<float> CreateRandomMatrixFloat(int rows, int columns)
        {
            var array = new Matrix<float>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextFloatRandom();

            return array;
        }

        public Matrix<Double> CreateRandomMatrixDouble(int rows, int columns)
        {
            var array = new Matrix<double>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextDoubleRandom();

            return array;
        }

        public Matrix<RgbPixel> CreateRandomMatrixRgbPixel(int rows, int columns)
        {
            var array = new Matrix<RgbPixel>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextRgbPixelRandom();

            return array;
        }

        public Matrix<RgbAlphaPixel> CreateRandomMatrixRgbAlphaPixel(int rows, int columns)
        {
            var array = new Matrix<RgbAlphaPixel>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextRgbAlphaPixelRandom();

            return array;
        }

        public Matrix<HsiPixel> CreateRandomMatrixHsiPixel(int rows, int columns)
        {
            var array = new Matrix<HsiPixel>(rows, columns);
            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    array[r, c] = this.NextHsiPixelRandom();

            return array;
        }

        #endregion

        #endregion

        #endregion

    }

}
