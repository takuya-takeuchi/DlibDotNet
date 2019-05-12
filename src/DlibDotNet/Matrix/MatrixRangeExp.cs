using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [Obsolete]
    public sealed class MatrixRangeExp<T> : MatrixBase
        where T : struct
    {

        #region Fields

        private readonly MatrixElementTypes _MatrixElementType;

        private readonly NativeMethods.MatrixElementType _NativeMatrixElementType;

        private readonly Bridge<T> _Bridge;

        #endregion

        #region Constructors

        public MatrixRangeExp(T start, T end)
        {
            Matrix<T>.TryParse<T>(out var type);

            this._Bridge = CreateBridge(type);

            this._NativeMatrixElementType = type.ToNativeMatrixElementType();
            this._MatrixElementType = type;

            this.NativePtr = this._Bridge.Create1(start, end);
        }

        public MatrixRangeExp(T start, T inc, T end)
        {
            Matrix<T>.TryParse<T>(out var type);

            this._Bridge = CreateBridge(type);

            this._NativeMatrixElementType = type.ToNativeMatrixElementType();
            this._MatrixElementType = type;

            this.NativePtr = this._Bridge.Create2(start, inc, end);
        }

        public MatrixRangeExp(T start, T end, int num)
        {
            Matrix<T>.TryParse<T>(out var type);

            this._Bridge = CreateBridge(type);

            this._NativeMatrixElementType = type.ToNativeMatrixElementType();
            this._MatrixElementType = type;

            this.NativePtr = this._Bridge.Create3(start, end, num);
        }

        internal MatrixRangeExp(IntPtr ptr)
        {
            Matrix<T>.TryParse<T>(out var type);

            this._NativeMatrixElementType = type.ToNativeMatrixElementType();
            this._MatrixElementType = type;

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.matrix_range_exp_nc(this._NativeMatrixElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override MatrixElementTypes MatrixElementType => this._MatrixElementType;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.matrix_range_exp_nr(this._NativeMatrixElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        #endregion

        #region Methods

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.matrix_range_exp_delete(this._NativeMatrixElementType, this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<T> CreateBridge(MatrixElementTypes types)
        {
            switch (types)
            {
                case MatrixElementTypes.UInt8:
                    return new UInt8Bridge() as Bridge<T>;
                case MatrixElementTypes.UInt16:
                    return new UInt16Bridge() as Bridge<T>;
                case MatrixElementTypes.Int8:
                    return new Int8Bridge() as Bridge<T>;
                case MatrixElementTypes.Int16:
                    return new Int16Bridge() as Bridge<T>;
                case MatrixElementTypes.Int32:
                    return new Int32Bridge() as Bridge<T>;
                case MatrixElementTypes.Int64:
                    return new Int64Bridge() as Bridge<T>;
                case MatrixElementTypes.Float:
                    return new FloatBridge() as Bridge<T>;
                case MatrixElementTypes.Double:
                    return new DoubleBridge() as Bridge<T>;
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }

        #endregion

        #endregion

        #region Bridge
        
        private abstract class Bridge<T>
        {

            #region Methods

            public abstract IntPtr Create1(T start, T end);

            public abstract IntPtr Create2(T start, T inc, T end);

            public abstract IntPtr Create3(T start, T end, int num);

            #endregion

        }

        private sealed class Int8Bridge : Bridge<sbyte>
        {

            #region Methods

            public override IntPtr Create1(sbyte start, sbyte end)
            {
                return NativeMethods.matrix_range_exp_create_int8_t_new3(start, end);
            }

            public override IntPtr Create2(sbyte start, sbyte inc, sbyte end)
            {
                if(inc == 0)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_int8_t_new1(start, inc, end);
            }

            public override IntPtr Create3(sbyte start, sbyte end, int num)
            {
                return NativeMethods.matrix_range_exp_create_int8_t_new2(start, end, num);
            }

            #endregion

        }

        private sealed class Int16Bridge : Bridge<short>
        {

            #region Methods

            public override IntPtr Create1(short start, short end)
            {
                return NativeMethods.matrix_range_exp_create_int16_t_new3(start, end);
            }

            public override IntPtr Create2(short start, short inc, short end)
            {
                if (inc == 0)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_int16_t_new1(start, inc, end);
            }

            public override IntPtr Create3(short start, short end, int num)
            {
                return NativeMethods.matrix_range_exp_create_int16_t_new2(start, end, num);
            }

            #endregion

        }

        private sealed class Int32Bridge : Bridge<int>
        {

            #region Methods

            public override IntPtr Create1(int start, int end)
            {
                return NativeMethods.matrix_range_exp_create_int32_t_new3(start, end);
            }

            public override IntPtr Create2(int start, int inc, int end)
            {
                if (inc == 0)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_int32_t_new1(start, inc, end);
            }

            public override IntPtr Create3(int start, int end, int num)
            {
                return NativeMethods.matrix_range_exp_create_int32_t_new2(start, end, num);
            }

            #endregion

        }

        private sealed class Int64Bridge : Bridge<long>
        {

            #region Methods

            public override IntPtr Create1(long start, long end)
            {
                return NativeMethods.matrix_range_exp_create_int64_t_new3(start, end);
            }

            public override IntPtr Create2(long start, long inc, long end)
            {
                if (inc == 0)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_int64_t_new1(start, inc, end);
            }

            public override IntPtr Create3(long start, long end, int num)
            {
                return NativeMethods.matrix_range_exp_create_int64_t_new2(start, end, num);
            }

            #endregion

        }

        private sealed class UInt8Bridge : Bridge<byte>
        {

            #region Methods

            public override IntPtr Create1(byte start, byte end)
            {
                return NativeMethods.matrix_range_exp_create_uint8_t_new3(start, end);
            }

            public override IntPtr Create2(byte start, byte inc, byte end)
            {
                if (inc == 0)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_uint8_t_new1(start, inc, end);
            }

            public override IntPtr Create3(byte start, byte end, int num)
            {
                return NativeMethods.matrix_range_exp_create_uint8_t_new2(start, end, num);
            }

            #endregion

        }

        private sealed class UInt16Bridge : Bridge<ushort>
        {

            #region Methods

            public override IntPtr Create1(ushort start, ushort end)
            {
                return NativeMethods.matrix_range_exp_create_uint16_t_new3(start, end);
            }

            public override IntPtr Create2(ushort start, ushort inc, ushort end)
            {
                if (inc == 0)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_uint16_t_new1(start, inc, end);
            }

            public override IntPtr Create3(ushort start, ushort end, int num)
            {
                return NativeMethods.matrix_range_exp_create_uint16_t_new2(start, end, num);
            }

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Methods

            public override IntPtr Create1(float start, float end)
            {
                return NativeMethods.matrix_range_exp_create_float_new3(start, end);
            }

            public override IntPtr Create2(float start, float inc, float end)
            {
                if (Math.Abs(inc) < float.Epsilon)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_float_new1(start, inc, end);
            }

            public override IntPtr Create3(float start, float end, int num)
            {
                return NativeMethods.matrix_range_exp_create_float_new2(start, end, num);
            }

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Methods

            public override IntPtr Create1(double start, double end)
            {
                return NativeMethods.matrix_range_exp_create_double_new3(start, end);
            }

            public override IntPtr Create2(double start, double inc, double end)
            {
                if (Math.Abs(inc) < double.Epsilon)
                    throw new ArgumentOutOfRangeException($"{nameof(inc)} must not be 0.");

                return NativeMethods.matrix_range_exp_create_double_new1(start, inc, end);
            }

            public override IntPtr Create3(double start, double end, int num)
            {
                return NativeMethods.matrix_range_exp_create_double_new2(start, end, num);
            }

            #endregion

        }

        #endregion

    }

}
