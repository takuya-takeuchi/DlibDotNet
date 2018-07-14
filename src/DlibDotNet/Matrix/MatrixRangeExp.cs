using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MatrixRangeExp<T> : MatrixBase
        where T : struct
    {

        #region Fields

        private readonly MatrixElementTypes _MatrixElementType;

        private readonly Dlib.Native.MatrixElementType _NativeMatrixElementType;

        private static readonly Dictionary<Type, MatrixElementTypes> SupportTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors

        static MatrixRangeExp()
        {
            var types = new[]
            {
                new { Type = typeof(byte),          ElementType = MatrixElementTypes.UInt8  },
                new { Type = typeof(ushort),        ElementType = MatrixElementTypes.UInt16 },
                new { Type = typeof(sbyte),         ElementType = MatrixElementTypes.Int8  },
                new { Type = typeof(short),         ElementType = MatrixElementTypes.Int16 },
                new { Type = typeof(int),           ElementType = MatrixElementTypes.Int32 },
                new { Type = typeof(float),         ElementType = MatrixElementTypes.Float  },
                new { Type = typeof(double),        ElementType = MatrixElementTypes.Double }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public MatrixRangeExp(T start, T end)
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._NativeMatrixElementType = type.ToNativeMatrixElementType();
            this._MatrixElementType = type;

            var param = this.CreataParam(start, end, false, default(T), false, 0);
            this.NativePtr = Dlib.Native.matrix_range_exp_create(this._NativeMatrixElementType, ref param);
        }

        public MatrixRangeExp(T start, T inc, T end)
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._NativeMatrixElementType = type.ToNativeMatrixElementType();
            this._MatrixElementType = type;

            var param = this.CreataParam(start, end, true, inc, false, 0);
            this.NativePtr = Dlib.Native.matrix_range_exp_create(this._NativeMatrixElementType, ref param);
        }

        public MatrixRangeExp(T start, T end, uint num)
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._NativeMatrixElementType = type.ToNativeMatrixElementType();
            this._MatrixElementType = type;

            var param = this.CreataParam(start, end, false, default(T), true, num);
            this.NativePtr = Dlib.Native.matrix_range_exp_create(this._NativeMatrixElementType, ref param);
        }

        internal MatrixRangeExp(IntPtr ptr)
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

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
                Dlib.Native.matrix_range_exp_nc(this._NativeMatrixElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override MatrixElementTypes MatrixElementType => this._MatrixElementType;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.matrix_range_exp_nr(this._NativeMatrixElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Dlib.Native.matrix_range_exp_delete(this.NativePtr);
        }

        #endregion

        #region Helpers

        private Dlib.Native.matrix_range_exp_create_param CreataParam(T start, T end, bool useInc, T inc, bool useNum, uint num)
        {
            var param = new Dlib.Native.matrix_range_exp_create_param();

            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                return param;

            switch (type)
            {
                case MatrixElementTypes.UInt8:
                    param.uint8_t_start = (uint8_t) (object) start;
                    param.uint8_t_end = (uint8_t)(object)end;
                    if(useInc)
                        param.uint8_t_inc = (uint8_t)(object)inc;
                    break;
                case MatrixElementTypes.UInt16:
                    param.uint16_t_start = (uint16_t)(object)start;
                    param.uint16_t_end = (uint16_t)(object)end;
                    if (useInc)
                        param.uint16_t_inc = (uint16_t)(object)inc;
                    break;
                case MatrixElementTypes.Int8:
                    param.int8_t_start = (int8_t)(object)start;
                    param.int8_t_end = (int8_t)(object)end;
                    if (useInc)
                        param.int8_t_inc = (int8_t)(object)inc;
                    break;
                case MatrixElementTypes.Int16:
                    param.int16_t_start = (int16_t)(object)start;
                    param.int16_t_end = (int16_t)(object)end;
                    if (useInc)
                        param.int16_t_inc = (int16_t)(object)inc;
                    break;
                case MatrixElementTypes.Int32:
                    param.int32_t_start = (int32_t)(object)start;
                    param.int32_t_end = (int32_t)(object)end;
                    if (useInc)
                        param.int32_t_inc = (int32_t)(object)inc;
                    break;
                case MatrixElementTypes.Float:
                    param.float_start = (float)(object)start;
                    param.float_end = (float)(object)end;
                    if (useInc)
                        param.float_inc = (float)(object)inc;
                    break;
                case MatrixElementTypes.Double:
                    param.double_start = (double)(object)start;
                    param.double_end = (double)(object)end;
                    if (useInc)
                        param.double_inc = (double)(object)inc;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (useNum)
                param.num = (int)num;

            return param;
        }

        #endregion

        #endregion

    }

}
