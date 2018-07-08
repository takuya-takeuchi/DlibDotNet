using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public class Array2DMatrix<TElement> : Array2DMatrixBase
        where TElement : struct
    {

        #region Fields

        private readonly Dlib.Native.MatrixElementType _MatrixElementType;

        private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors

        static Array2DMatrix()
        {
            var matrixTypes = new[]
            {
                new { Type = typeof(byte),          ElementType = MatrixElementTypes.UInt8 },
                new { Type = typeof(ushort),        ElementType = MatrixElementTypes.UInt16 },
                new { Type = typeof(uint),          ElementType = MatrixElementTypes.UInt32 },
                new { Type = typeof(sbyte),         ElementType = MatrixElementTypes.Int8 },
                new { Type = typeof(short),         ElementType = MatrixElementTypes.Int16 },
                new { Type = typeof(int),           ElementType = MatrixElementTypes.Int32 },
                new { Type = typeof(float),         ElementType = MatrixElementTypes.Float },
                new { Type = typeof(double),        ElementType = MatrixElementTypes.Double },
                new { Type = typeof(RgbPixel),      ElementType = MatrixElementTypes.RgbPixel },
                new { Type = typeof(HsiPixel),      ElementType = MatrixElementTypes.HsiPixel },
                new { Type = typeof(RgbAlphaPixel), ElementType = MatrixElementTypes.RgbAlphaPixel }
            };

            foreach (var type in matrixTypes)
                SupportMatrixTypes.Add(type.Type, type.ElementType);
        }

        public Array2DMatrix(int templateRows = 0, int temlateColumns = 0) :
            base(templateRows, temlateColumns)
        {
            if (!SupportMatrixTypes.TryGetValue(typeof(TElement), out var matrixType))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementType = matrixType.ToNativeMatrixElementType();

            this.NativePtr = Dlib.Native.array2d_matrix_new(this._MatrixElementType, templateRows, temlateColumns);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{matrixType} is not supported.");

            this.MatrixElementType = matrixType;
        }

        public Array2DMatrix(int rows, int columns, int templateRows = 0, int temlateColumns = 0) :
            base(templateRows, temlateColumns)
        {
            if (!SupportMatrixTypes.TryGetValue(typeof(TElement), out var matrixType))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementType = matrixType.ToNativeMatrixElementType();

            this.NativePtr = Dlib.Native.array2d_matrix_new1(this._MatrixElementType, rows, columns, templateRows, temlateColumns);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{matrixType} is not supported.");

            this.MatrixElementType = matrixType;
        }

        #endregion

        #region Properties

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_matrix_nc(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return ret;
            }
        }

        public override MatrixElementTypes MatrixElementType
        {
            get;
        }

        public override Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_matrix_get_rect(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return new Rectangle(ret);
            }
        }

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_matrix_nr(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return ret;
            }
        }

        public override int Size
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_matrix_size(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
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

                var type = this._MatrixElementType;
                Dlib.Native.array2d_matrix_row(type, this.NativePtr, this.TemplateRows, this.TemplateColumns, row, out var ret);

                switch (type)
                {
                    case Dlib.Native.MatrixElementType.UInt8:
                        return new RowUInt8(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.UInt16:
                        return new RowUInt16(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.UInt32:
                        return new RowUInt32(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.Int8:
                        return new RowInt8(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.Int16:
                        return new RowInt16(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.Int32:
                        return new RowInt32(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.Float:
                        return new RowFloat(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.Double:
                        return new RowDouble(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.RgbPixel:
                        return new RowRgbPixel(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.RgbAlphaPixel:
                        return new RowRgbAlphaPixel(ret, type, this) as Row<TElement>;
                    case Dlib.Native.MatrixElementType.HsiPixel:
                        return new RowHsiPixel(ret, type, this) as Row<TElement>;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region Methods 

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            if (!this.IsDisposed)
                Dlib.Native.array2d_matrix_delete(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
        }

        #endregion

        #endregion

        public abstract class Row<T> : DlibObject
            where T : struct
        {

            #region Fields 

            internal readonly Dlib.Native.MatrixElementType _Type;

            protected readonly Array2DMatrixBase _Parent;

            #endregion

            #region Constructors 

            internal Row(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
            {
                if (ptr == IntPtr.Zero)
                    throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

                this._Parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.NativePtr = ptr;
                this._Type = type;
            }

            #endregion

            #region Properties

            public abstract Matrix<T> this[int column]
            {
                get;
                set;
            }

            #endregion

            #region Overrides 

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();
                Dlib.Native.array2d_matrix_row_delete(this._Type, this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns);
            }

            #endregion

        }

        public sealed class RowUInt8 : Row<byte>
        {

            #region Constructors 

            internal RowUInt8(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<byte> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_uint8_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<byte>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_uint8_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowUInt16 : Row<ushort>
        {

            #region Constructors 

            internal RowUInt16(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<ushort> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_uint16_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<ushort>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_uint16_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowUInt32 : Row<uint>
        {

            #region Constructors 

            internal RowUInt32(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<uint> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_uint32_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<uint>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_uint32_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowInt8 : Row<sbyte>
        {

            #region Constructors 

            internal RowInt8(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<sbyte> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_int8_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<sbyte>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_int8_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowInt16 : Row<short>
        {

            #region Constructors 

            internal RowInt16(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<short> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_int16_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<short>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_int16_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowInt32 : Row<int>
        {

            #region Constructors 

            internal RowInt32(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<int> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_int32_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<int>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_int32_t(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowFloat : Row<float>
        {

            #region Constructors 

            internal RowFloat(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<float> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_float(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<float>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_float(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowDouble : Row<double>
        {

            #region Constructors 

            internal RowDouble(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<double> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_double(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<double>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_double(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowRgbPixel : Row<RgbPixel>
        {

            #region Constructors 

            internal RowRgbPixel(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<RgbPixel> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_rgb_pixel(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<RgbPixel>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_rgb_pixel(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowRgbAlphaPixel : Row<RgbAlphaPixel>
        {

            #region Constructors 

            internal RowRgbAlphaPixel(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<RgbAlphaPixel> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_rgb_alpha_pixel(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<RgbAlphaPixel>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_rgb_alpha_pixel(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

        public sealed class RowHsiPixel : Row<HsiPixel>
        {

            #region Constructors 

            internal RowHsiPixel(IntPtr ptr, Dlib.Native.MatrixElementType type, Array2DMatrixBase parent)
                : base(ptr, type, parent)
            {
            }

            #endregion

            #region Properties

            public override Matrix<HsiPixel> this[int column]
            {
                get
                {
                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    IntPtr value;
                    Dlib.Native.array2d_matrix_get_row_column_hsi_pixel(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, out value);
                    return new Matrix<HsiPixel>(value, this._Parent.TemplateRows, this._Parent.TemplateColumns);
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    value.ThrowIfDisposed();

                    if (!(0 <= column && column < this._Parent.Columns))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.array2d_matrix_set_row_column_hsi_pixel(this.NativePtr, this._Parent.TemplateRows, this._Parent.TemplateColumns, column, value.NativePtr);
                }
            }

            #endregion

        }

    }

}
