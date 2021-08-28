#if !LITE
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Array2D<TElement> : Array2DBase
        where TElement : struct
    {

        #region Fields

        private readonly NativeMethods.Array2DType _Array2DType;

        private static readonly IDictionary<ImageTypes, int> ElementSizeDictionary = new Dictionary<ImageTypes, int>();

        private static readonly Dictionary<Type, ImageTypes> SupportTypes = new Dictionary<Type, ImageTypes>();

        #endregion

        #region Constructors

        static Array2D()
        {
            var types = new[]
            {
                new { Type = typeof(byte),          ElementType = ImageTypes.UInt8,         Size = sizeof(byte) },
                new { Type = typeof(ushort),        ElementType = ImageTypes.UInt16,        Size = sizeof(ushort) },
                new { Type = typeof(uint),          ElementType = ImageTypes.UInt32,        Size = sizeof(uint) },
                new { Type = typeof(sbyte),         ElementType = ImageTypes.Int8,          Size = sizeof(sbyte) },
                new { Type = typeof(short),         ElementType = ImageTypes.Int16,         Size = sizeof(short) },
                new { Type = typeof(int),           ElementType = ImageTypes.Int32,         Size = sizeof(int) },
                new { Type = typeof(float),         ElementType = ImageTypes.Float,         Size = sizeof(float) },
                new { Type = typeof(double),        ElementType = ImageTypes.Double,        Size = sizeof(double) },
                new { Type = typeof(RgbPixel),      ElementType = ImageTypes.RgbPixel,      Size = Marshal.SizeOf<RgbPixel>() },
                new { Type = typeof(BgrPixel),      ElementType = ImageTypes.BgrPixel,      Size = Marshal.SizeOf<BgrPixel>() },
                new { Type = typeof(RgbAlphaPixel), ElementType = ImageTypes.RgbAlphaPixel, Size = Marshal.SizeOf<RgbAlphaPixel>() },
                new { Type = typeof(HsiPixel),      ElementType = ImageTypes.HsiPixel,      Size = Marshal.SizeOf<HsiPixel>() },
                new { Type = typeof(LabPixel),      ElementType = ImageTypes.LabPixel,      Size = Marshal.SizeOf<LabPixel>() }
            };

            foreach (var type in types)
            {
                SupportTypes.Add(type.Type, type.ElementType);
                ElementSizeDictionary.Add(type.ElementType, type.Size);
            }
        }

        public Array2D()
        {
            if (!SupportTypes.TryGetValue(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._Array2DType = type.ToNativeArray2DType();

            this.NativePtr = NativeMethods.array2d_new(this._Array2DType);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{type} is not supported.");

            this.ImageType = type;
        }

        public Array2D(int rows, int columns)
        {
            if (!SupportTypes.TryGetValue(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._Array2DType = type.ToNativeArray2DType();

            this.NativePtr = NativeMethods.array2d_new1(this._Array2DType, rows, columns);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{type} is not supported.");

            this.ImageType = type;
        }

        internal Array2D(IntPtr ptr, ImageTypes type, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
            this._Array2DType = type.ToNativeArray2DType();

            this.ImageType = type;
        }

        #endregion

        #region Properties

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.array2d_nc(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override ImageTypes ImageType
        {
            get;
        }

        public override Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.rectangle_get_rect(this._Array2DType, this.NativePtr, out var ret);
                return new Rectangle(ret);
            }
        }

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.array2d_nr(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override int Size
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.array2d_size(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override IntPtr Data
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.array2d_data_raw(this._Array2DType, this.NativePtr);
            }
        }

        public Row<TElement> this[int row]
        {
            get
            {
                this.ThrowIfDisposed();

                if (!(0 <= row && row < this.Rows))
                    throw new IndexOutOfRangeException();

                NativeMethods.array2d_row(this._Array2DType, this.NativePtr, row, out var ret);

                switch (this.ImageType)
                {
                    case ImageTypes.RgbPixel:
                        return new RowRgbPixel(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.BgrPixel:
                        return new RowBgrPixel(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.RgbAlphaPixel:
                        return new RowRgbAlphaPixel(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.UInt8:
                        return new RowUInt8(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.UInt16:
                        return new RowUInt16(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.UInt32:
                        return new RowUInt32(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.Int8:
                        return new RowInt8(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.Int16:
                        return new RowInt16(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.Int32:
                        return new RowInt32(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.HsiPixel:
                        return new RowHsiPixel(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.LabPixel:
                        return new RowLabPixel(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.Float:
                        return new RowFloat(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.Double:
                        return new RowDouble(ret, this.ImageType, this) as Row<TElement>;
                    //case ImageTypes.Matrix:
                    //    break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region Methods  

        public void SetSize(int rows, int columns)
        {
            if (!(rows >= 0 && columns >= 0))
                throw new ArrayTypeMismatchException();

            this.ThrowIfDisposed();
            NativeMethods.array2d_set_size(this._Array2DType,
                                           this.NativePtr,
                                           rows,
                                           columns);
        }

        public byte[] ToBytes()
        {
            switch (this._Array2DType)
            {
                case NativeMethods.Array2DType.UInt8:
                case NativeMethods.Array2DType.UInt16:
                case NativeMethods.Array2DType.UInt32:
                case NativeMethods.Array2DType.Int8:
                case NativeMethods.Array2DType.Int16:
                case NativeMethods.Array2DType.Int32:
                case NativeMethods.Array2DType.Float:
                case NativeMethods.Array2DType.Double:
                case NativeMethods.Array2DType.BgrPixel:
                case NativeMethods.Array2DType.RgbPixel:
                case NativeMethods.Array2DType.RgbAlphaPixel:
                case NativeMethods.Array2DType.HsiPixel:
                case NativeMethods.Array2DType.LabPixel:
                    var rows = (uint)this.Rows;
                    var columns = (uint)this.Columns;
                    var size = ElementSizeDictionary[this.ImageType];
                    var src = this.NativePtr;
                    var dst = new byte[(int)(rows * columns * size)];
                    var ret = NativeMethods.extensions_convert_array_to_bytes(this._Array2DType, src, dst, rows, columns);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                            throw new ArgumentException($"Cannot convert Array2D<{this.ImageType}> to byte array.");
                    }
                    return dst;
            }

            throw new ArgumentException($"Cannot convert {this.GetType().Name} to byte array.");
        }

        internal static bool TryParse<T>(out ImageTypes type)
            where T : struct
        {
            return SupportTypes.TryGetValue(typeof(T), out type);
        }

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.array2d_delete(this._Array2DType, this.NativePtr);
        }

        #endregion

        #endregion

        #region Row

        public abstract class Row<T> : DlibObject
            where T : struct
        {

            #region Fields 

            protected readonly ImageTypes _Type;

            protected readonly Array2DBase _Parent;

            #endregion

            #region Constructors 

            internal Row(IntPtr ptr, ImageTypes type, Array2DBase parent)
            {
                if (ptr == IntPtr.Zero)
                    throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

                this._Parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.NativePtr = ptr;
                this._Type = type;
            }

            #endregion

            #region Properties

            public abstract T this[int column]
            {
                get;
                set;
            }

            #endregion

            #region Overrides 

            /// <summary>
            /// Releases all unmanaged resources.
            /// </summary>
            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.array2d_row_delete(this._Type.ToNativeArray2DType(), this.NativePtr);
            }

            #endregion

        }

        public sealed class RowUInt8 : Row<byte>
        {

            #region Constructors

            public RowUInt8(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override byte this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    byte value;
                    NativeMethods.array2d_get_row_column_uint8_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_uint8_t(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowUInt16 : Row<ushort>
        {

            #region Constructors

            public RowUInt16(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override ushort this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    ushort value;
                    NativeMethods.array2d_get_row_column_uint16_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_uint16_t(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowUInt32 : Row<uint>
        {

            #region Constructors

            public RowUInt32(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override uint this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    uint value;
                    NativeMethods.array2d_get_row_column_uint32_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_uint32_t(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowInt8 : Row<sbyte>
        {

            #region Constructors

            public RowInt8(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override sbyte this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    sbyte value;
                    NativeMethods.array2d_get_row_column_int8_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_int8_t(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowInt16 : Row<short>
        {

            #region Constructors

            public RowInt16(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override short this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    short value;
                    NativeMethods.array2d_get_row_column_int16_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_int16_t(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowInt32 : Row<int>
        {

            #region Constructors

            public RowInt32(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override int this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    int value;
                    NativeMethods.array2d_get_row_column_int32_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_int32_t(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowFloat : Row<float>
        {

            #region Constructors

            public RowFloat(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override float this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    float value;
                    NativeMethods.array2d_get_row_column_float(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_float(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowDouble : Row<double>
        {

            #region Constructors

            public RowDouble(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override double this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException(); ;

                    double value;
                    NativeMethods.array2d_get_row_column_double(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_double(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowRgbPixel : Row<RgbPixel>
        {

            #region Constructors

            public RowRgbPixel(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override RgbPixel this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    RgbPixel value;
                    NativeMethods.array2d_get_row_column_rgb_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_rgb_pixel(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowBgrPixel : Row<BgrPixel>
        {

            #region Constructors

            public RowBgrPixel(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override BgrPixel this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    BgrPixel value;
                    NativeMethods.array2d_get_row_column_bgr_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_bgr_pixel(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowRgbAlphaPixel : Row<RgbAlphaPixel>
        {

            #region Constructors

            public RowRgbAlphaPixel(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override RgbAlphaPixel this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    RgbAlphaPixel value;
                    NativeMethods.array2d_get_row_column_rgb_alpha_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_rgb_alpha_pixel(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowHsiPixel : Row<HsiPixel>
        {

            #region Constructors

            public RowHsiPixel(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override HsiPixel this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    HsiPixel value;
                    NativeMethods.array2d_get_row_column_hsi_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_hsi_pixel(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        public sealed class RowLabPixel : Row<LabPixel>
        {

            #region Constructors

            public RowLabPixel(IntPtr ptr, ImageTypes type, Array2DBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override LabPixel this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    LabPixel value;
                    NativeMethods.array2d_get_row_column_lab_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    NativeMethods.array2d_set_row_column_lab_pixel(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        #endregion

    }

}

#endif
