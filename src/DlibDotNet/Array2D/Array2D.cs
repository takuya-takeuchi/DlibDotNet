using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Array2D<TElement> : Array2DBase
        where TElement : struct
    {

        #region Fields

        private readonly Dlib.Native.Array2DType _Array2DType;

        private static readonly Dictionary<Type, ImageTypes> SupportTypes = new Dictionary<Type, ImageTypes>();

        #endregion

        #region Constructors

        static Array2D()
        {
            var types = new[]
            {
                new { Type = typeof(byte),          ElementType = ImageTypes.UInt8  },
                new { Type = typeof(ushort),        ElementType = ImageTypes.UInt16 },
                new { Type = typeof(short),         ElementType = ImageTypes.Int16 },
                new { Type = typeof(int),           ElementType = ImageTypes.Int32 },
                new { Type = typeof(float),         ElementType = ImageTypes.Float  },
                new { Type = typeof(double),        ElementType = ImageTypes.Double },
                new { Type = typeof(RgbPixel),      ElementType = ImageTypes.RgbPixel },
                new { Type = typeof(RgbAlphaPixel), ElementType = ImageTypes.RgbAlphaPixel },
                new { Type = typeof(HsiPixel),      ElementType = ImageTypes.HsiPixel }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public Array2D()
        {
            if (!SupportTypes.TryGetValue(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._Array2DType = type.ToNativeArray2DType();

            this.NativePtr = Dlib.Native.array2d_new(this._Array2DType);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{type} is not supported.");

            this.ImageType = type;
        }

        public Array2D(int rows, int columns)
        {
            if (!SupportTypes.TryGetValue(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._Array2DType = type.ToNativeArray2DType();

            this.NativePtr = Dlib.Native.array2d_new1(this._Array2DType, rows, columns);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{type} is not supported.");

            this.ImageType = type;
        }

        internal Array2D(IntPtr ptr, ImageTypes type)
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
                Dlib.Native.array2d_nc(this._Array2DType, this.NativePtr, out var ret);
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
                Dlib.Native.rectangle_get_rect(this._Array2DType, this.NativePtr, out var ret);
                return new Rectangle(ret);
            }
        }

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_nr(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override int Size
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_size(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public Row<TElement> this[int row]
        {
            get
            {
                this.ThrowIfDisposed();

                if (!(0 <= row && row < this.Rows))
                    throw new IndexOutOfRangeException();

                Dlib.Native.array2d_row(this._Array2DType, this.NativePtr, row, out var ret);

                switch (this.ImageType)
                {
                    case ImageTypes.RgbPixel:
                        return new RowRgbPixel(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.RgbAlphaPixel:
                        return new RowRgbAlphaPixel(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.UInt8:
                        return new RowUInt8(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.UInt16:
                        return new RowUInt16(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.Int16:
                        return new RowInt16(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.Int32:
                        return new RowInt32(ret, this.ImageType, this) as Row<TElement>;
                    case ImageTypes.HsiPixel:
                        return new RowHsiPixel(ret, this.ImageType, this) as Row<TElement>;
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

        internal static bool TryParse<T>(out ImageTypes type)
            where T : struct
        {
            return SupportTypes.TryGetValue(typeof(T), out type);
        }

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Dlib.Native.array2d_delete(this._Array2DType, this.NativePtr);
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

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                Dlib.Native.array2d_row_delete(this._Type.ToNativeArray2DType(), this.NativePtr);
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
                    Dlib.Native.array2d_get_row_column_uint8_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_uint8_t(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_uint16_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_uint16_t(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_int16_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_int16_t(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_int32_t(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_int32_t(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_float(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_float(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_double(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_double(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_rgb_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_rgb_pixel(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_rgb_alpha_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_rgb_alpha_pixel(this.NativePtr, column, value);
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
                    Dlib.Native.array2d_get_row_column_hsi_pixel(this.NativePtr, column, out value);
                    return value;
                }
                set
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_set_row_column_hsi_pixel(this.NativePtr, column, value);
                }
            }

            #endregion

        }

        #endregion

    }

}
