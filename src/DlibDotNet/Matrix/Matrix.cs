using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public partial class Matrix<TElement> : MatrixBase, IEnumerable<TElement>
        where TElement : struct
    {

        #region Fields

        private readonly MatrixElementTypes _MatrixElementTypes;

        private readonly NativeMethods.MatrixElementType _ElementType;

        private readonly Indexer<TElement> _Indexer;

        #endregion

        #region Constructors

        static Matrix()
        {
            ContainerBridgeRepository.Add(new MatrixContainerBridge());
        }

        public Matrix()
        {
            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            this.NativePtr = NativeMethods.matrix_new(this._ElementType);

            this._Indexer = this.CreateIndexer(type);
        }

        public Matrix(Array2DBase array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            array.ThrowIfDisposed();

            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            var ret = NativeMethods.mat_matrix(array.ImageType.ToNativeArray2DType(),
                                               array.NativePtr,
                                               0,
                                               0,
                                               this._ElementType,
                                               out var ptr);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{array.ImageType} can not convert to {type}.");
            }

            this.NativePtr = ptr;
            this._Indexer = this.CreateIndexer(type);
        }

        public Matrix(int row, int column)
        {
            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");
            if (row < 0)
                throw new ArgumentOutOfRangeException($"{nameof(row)}", $"{nameof(row)} should be positive value.");
            if (column < 0)
                throw new ArgumentOutOfRangeException($"{nameof(column)}", $"{nameof(column)} should be positive value.");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            this.NativePtr = NativeMethods.matrix_new1(this._ElementType, row, column);

            this._Indexer = this.CreateIndexer(type);
        }

        public Matrix(TElement[] array, int row, int column)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (row < 0)
                throw new ArgumentOutOfRangeException($"{nameof(row)}", $"{nameof(row)} should be positive value.");
            if (column < 0)
                throw new ArgumentOutOfRangeException($"{nameof(column)}", $"{nameof(column)} should be positive value.");
            if (array.Length != row * column)
                throw new ArgumentOutOfRangeException($"{nameof(array)}.Length should equalt to {nameof(column)}x{nameof(column)}.");

            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();

            unsafe
            {
                switch (this._ElementType)
                {
                    case NativeMethods.MatrixElementType.UInt8:
                        {
                            var tmp = array as byte[];
                            fixed (byte* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.UInt16:
                        {
                            var tmp = array as ushort[];
                            fixed (ushort* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.UInt32:
                        {
                            var tmp = array as uint[];
                            fixed (uint* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.Int8:
                        {
                            var tmp = array as sbyte[];
                            fixed (sbyte* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.Int16:
                        {
                            var tmp = array as short[];
                            fixed (short* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.Int32:
                        {
                            var tmp = array as int[];
                            fixed (int* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.Float:
                        {
                            var tmp = array as float[];
                            fixed (float* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.Double:
                        {
                            var tmp = array as double[];
                            fixed (double* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.RgbPixel:
                        {
                            var tmp = array as RgbPixel[];
                            fixed (RgbPixel* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.RgbAlphaPixel:
                        {
                            var tmp = array as RgbAlphaPixel[];
                            fixed (RgbAlphaPixel* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case NativeMethods.MatrixElementType.HsiPixel:
                        {
                            var tmp = array as HsiPixel[];
                            fixed (HsiPixel* src = &tmp[0])
                                this.NativePtr = NativeMethods.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                }
            }

            this._Indexer = this.CreateIndexer(type);
        }

        public Matrix(byte[] array, int row, int column, int elementSize)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (row < 0)
                throw new ArgumentOutOfRangeException($"{nameof(row)}", $"{nameof(row)} should be positive value.");
            if (column < 0)
                throw new ArgumentOutOfRangeException($"{nameof(column)}", $"{nameof(column)} should be positive value.");
            if (column < 1)
                throw new ArgumentOutOfRangeException($"{nameof(elementSize)} should be more than 1");
            if (array.Length != row * column * elementSize)
                throw new ArgumentOutOfRangeException($"{nameof(array)}.Length should equalt to {nameof(column)}x{nameof(column)}*{nameof(elementSize)}.");

            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();

            var size = ElementSizeDictionary[this._ElementType];
            if (size != elementSize)
                throw new ArgumentOutOfRangeException($"The size of {typeof(TElement).Name} does not equalt to {nameof(elementSize)}.");

            unsafe
            {
                fixed (byte* src = &array[0])
                    this.NativePtr = NativeMethods.matrix_new3(this._ElementType, row, column, src);
            }

            this._Indexer = this.CreateIndexer(type);
        }

        internal Matrix(IntPtr ptr, int templateRows = 0, int templateColumns = 0, bool isEnabledDispose = true)
            : base(templateRows, templateColumns, isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this.NativePtr = ptr;
            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();

            this._Indexer = this.CreateIndexer(type);
        }

        #endregion

        #region Properties

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.matrix_nc(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return ret;
            }
        }

        public override MatrixElementTypes MatrixElementType => this._MatrixElementTypes;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.matrix_nr(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return ret;
            }
        }

        public int Size
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.matrix_size(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return ret;
            }
        }

        public TElement this[int index]
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Indexer[index];
            }
            set
            {
                this.ThrowIfDisposed();
                this._Indexer[index] = value;
            }
        }

        public TElement this[int row, int column]
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Indexer[row, column];
            }
            set
            {
                this.ThrowIfDisposed();
                this._Indexer[row, column] = value;
            }
        }

        #endregion

        #region Methods

        public void Assign(TElement[] array)
        {
            var templateRows = this.TemplateRows;
            var templateColumns = this.TemplateColumns;

            switch (this._MatrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<byte>().ToArray());
                    break;
                case MatrixElementTypes.UInt16:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<ushort>().ToArray());
                    break;
                case MatrixElementTypes.UInt32:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<uint>().ToArray());
                    break;
                case MatrixElementTypes.Int8:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<sbyte>().ToArray());
                    break;
                case MatrixElementTypes.Int16:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<short>().ToArray());
                    break;
                case MatrixElementTypes.Int32:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<int>().ToArray());
                    break;
                case MatrixElementTypes.Float:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<float>().ToArray());
                    break;
                case MatrixElementTypes.Double:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<double>().ToArray());
                    break;
                case MatrixElementTypes.RgbPixel:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<RgbPixel>().ToArray());
                    break;
                case MatrixElementTypes.RgbAlphaPixel:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<RgbAlphaPixel>().ToArray());
                    break;
                case MatrixElementTypes.HsiPixel:
                    NativeMethods.matrix_operator_array(this._ElementType, this.NativePtr, templateRows, templateColumns, array.Cast<HsiPixel>().ToArray());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Matrix<TElement> CreateTemplateParameterizeMatrix(uint templateRows, uint templateColumns)
        {
            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            var ptr = NativeMethods.matrix_new4(type.ToNativeMatrixElementType(), templateRows, templateColumns);
            return new Matrix<TElement>(ptr, (int)templateRows, (int)templateColumns);
        }

        public static Matrix<TElement> Deserialize(ProxyDeserialize deserialize, uint templateRows = 0, uint templateColumns = 0)
        {
            if (!TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            if (deserialize == null)
                throw new ArgumentNullException(nameof(deserialize));

            deserialize.ThrowIfDisposed();

            var ret = NativeMethods.matrix_deserialize_matrix_proxy(deserialize.NativePtr,
                                                                    type.ToNativeMatrixElementType(),
                                                                    (int)templateRows,
                                                                    (int)templateColumns,
                                                                    out var matrix);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
            }

            return new Matrix<TElement>(matrix, (int)templateRows, (int)templateColumns);
        }

        public void SetSize(int length)
        {
            this.ThrowIfDisposed();

            if (!(this.TemplateRows == 1 || this.TemplateColumns == 1))
                throw new InvalidOperationException($"{nameof(this.TemplateRows)} or {nameof(this.TemplateColumns)} must be 1.");

            var templateRows = this.TemplateRows;
            var templateColumns = this.TemplateColumns;
            var ret = NativeMethods.matrix_set_size(this._ElementType, this.NativePtr, templateRows, templateColumns, length);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {this._MatrixElementTypes} is not supported.");
            }
        }

        public void SetSize(int rows, int cols)
        {
            this.ThrowIfDisposed();

            if (!(rows > 0))
                throw new InvalidOperationException($"{nameof(rows)} must be greater than 0.");
            if (!(cols > 0))
                throw new InvalidOperationException($"{nameof(cols)} must be greater than 0.");

            var tr = this.TemplateRows;
            var tc = this.TemplateColumns;
            if (!((tr == 0 || tr == rows) && (tc == 0 || tc == cols)))
                throw new InvalidOperationException($"{nameof(TemplateRows)}: {tr}, {nameof(TemplateColumns)}: {tc}, rows: {rows}, cols: {cols}");

            var ret = NativeMethods.matrix_set_size2(this._ElementType, this.NativePtr, tr, tc, rows, cols);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {this._MatrixElementTypes} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(this.TemplateColumns)} or {nameof(this.TemplateRows)} is not supported.");
            }
        }

        public TElement[] ToArray()
        {
            this.ThrowIfDisposed();

            TElement[] result;
            NativeMethods.ErrorType err;

            var templateRows = this.TemplateRows;
            var templateColumns = this.TemplateColumns;

            switch (this._MatrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new byte[row * column];
                        fixed (byte* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.UInt16:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new ushort[row * column];
                        fixed (ushort* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.UInt32:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new uint[row * column];
                        fixed (uint* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.Int8:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new sbyte[row * column];
                        fixed (sbyte* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.Int16:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new short[row * column];
                        fixed (short* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.Int32:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new int[row * column];
                        fixed (int* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.Float:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new float[row * column];
                        fixed (float* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.Double:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new double[row * column];
                        fixed (double* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.RgbPixel:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new RgbPixel[row * column];
                        fixed (RgbPixel* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.RgbAlphaPixel:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new RgbAlphaPixel[row * column];
                        fixed (RgbAlphaPixel* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                case MatrixElementTypes.HsiPixel:
                    unsafe
                    {
                        var row = this.Rows;
                        var column = this.Columns;

                        var array = new HsiPixel[row * column];
                        fixed (HsiPixel* dst = &array[0])
                        {
                            var src = this.NativePtr;
                            var type = this._ElementType;
                            err = NativeMethods.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (err)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{this._ElementType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(this.TemplateColumns)} or {nameof(this.TemplateRows)} is not supported.");
            }

            return result;
        }

        internal static bool TryParse<T>(out MatrixElementTypes result)
            where T : struct
        {
            return TryParse(typeof(T), out result);
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

            NativeMethods.matrix_delete(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            var str = "";

            try
            {
                ofstream = NativeMethods.ostringstream_new();
                var ret = NativeMethods.matrix_operator_left_shift(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, ofstream);
                switch (ret)
                {
                    case NativeMethods.ErrorType.OK:
                        stdstr = NativeMethods.ostringstream_str(ofstream);
                        str = StringHelper.FromStdString(stdstr) ?? "";
                        break;
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {this._ElementType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (stdstr != IntPtr.Zero)
                    NativeMethods.string_delete(stdstr);
                if (ofstream != IntPtr.Zero)
                    NativeMethods.ostringstream_delete(ofstream);
            }

            return str;
        }

        #endregion

        #region Helpers

        private Indexer<TElement> CreateIndexer(MatrixElementTypes types)
        {
            switch (types)
            {
                case MatrixElementTypes.UInt8:
                    return new IndexerUInt8(this) as Indexer<TElement>;
                case MatrixElementTypes.UInt16:
                    return new IndexerUInt16(this) as Indexer<TElement>;
                case MatrixElementTypes.UInt32:
                    return new IndexerUInt32(this) as Indexer<TElement>;
                case MatrixElementTypes.UInt64:
                    return new IndexerUInt64(this) as Indexer<TElement>;
                case MatrixElementTypes.Int8:
                    return new IndexerInt8(this) as Indexer<TElement>;
                case MatrixElementTypes.Int16:
                    return new IndexerInt16(this) as Indexer<TElement>;
                case MatrixElementTypes.Int32:
                    return new IndexerInt32(this) as Indexer<TElement>;
                case MatrixElementTypes.Int64:
                    return new IndexerInt64(this) as Indexer<TElement>;
                case MatrixElementTypes.Float:
                    return new IndexerFloat(this) as Indexer<TElement>;
                case MatrixElementTypes.Double:
                    return new IndexerDouble(this) as Indexer<TElement>;
                case MatrixElementTypes.RgbPixel:
                    return new IndexerRgbPixel(this) as Indexer<TElement>;
                case MatrixElementTypes.RgbAlphaPixel:
                    return new IndexerRgbAlphaPixel(this) as Indexer<TElement>;
                case MatrixElementTypes.HsiPixel:
                    return new IndexerHsiPixel(this) as Indexer<TElement>;
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }

        #endregion

        #endregion

        #region IEnumerable<TElement> Implementations

        public IEnumerator<TElement> GetEnumerator()
        {
            this.ThrowIfDisposed();

            return this._Indexer.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        private sealed class MatrixContainerBridge : ContainerBridge<Matrix<TElement>>
        {

            #region Methods

            #region Overrids

            public override Matrix<TElement> Create(IntPtr ptr, IParameter parameter = null)
            {
                if (parameter is MatrixTemplateSizeParameter mp)
                    return new Matrix<TElement>(ptr, mp.TemplateRows, mp.TemplateColumns);
                return new Matrix<TElement>(ptr, 0, 0);
            }

            public override IntPtr GetPtr(Matrix<TElement> item)
            {
                return item.NativePtr;
            }

            #endregion

            #endregion

        }

    }

}