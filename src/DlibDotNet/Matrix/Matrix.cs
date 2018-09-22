using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public class Matrix<TElement> : MatrixBase
        where TElement : struct
    {

        #region Fields

        private readonly MatrixElementTypes _MatrixElementTypes;

        private readonly Dlib.Native.MatrixElementType _ElementType;

        private static readonly IDictionary<Dlib.Native.MatrixElementType, int> ElementSizeDictionary;

        private readonly Indexer<TElement> _Indexer;

        #endregion

        #region Constructors

        static Matrix()
        {
            ElementSizeDictionary = new Dictionary<Dlib.Native.MatrixElementType, int>();
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.UInt8, sizeof(byte));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.UInt16, sizeof(ushort));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.UInt32, sizeof(uint));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Int8, sizeof(sbyte));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Int16, sizeof(short));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Int32, sizeof(int));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Float, sizeof(float));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Double, sizeof(double));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.RgbPixel, Marshal.SizeOf<RgbPixel>());
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.RgbAlphaPixel, Marshal.SizeOf<RgbAlphaPixel>());
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.HsiPixel, Marshal.SizeOf<HsiPixel>());
        }

        public Matrix()
        {
            if (!MatrixBase.TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            this.NativePtr = Dlib.Native.matrix_new(this._ElementType);

            this._Indexer = this.CreateIndexer(type);
        }

        public Matrix(Array2DBase array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            array.ThrowIfDisposed();

            if (!MatrixBase.TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            var ret = Dlib.Native.mat_matrix(array.ImageType.ToNativeArray2DType(),
                                             array.NativePtr,
                                             0,
                                             0,
                                             this._ElementType,
                                             out var ptr);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{array.ImageType} can not convert to {type}.");
            }

            this.NativePtr = ptr;
            this._Indexer = this.CreateIndexer(type);
        }

        public Matrix(int row, int column)
        {
            if (!MatrixBase.TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");
            if (row < 0)
                throw new ArgumentOutOfRangeException($"{nameof(row)}", $"{nameof(row)} should be positive value.");
            if (column < 0)
                throw new ArgumentOutOfRangeException($"{nameof(column)}", $"{nameof(column)} should be positive value.");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            this.NativePtr = Dlib.Native.matrix_new1(this._ElementType, row, column);

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

            if (!MatrixBase.TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();

            unsafe
            {
                switch (this._ElementType)
                {
                    case Dlib.Native.MatrixElementType.UInt8:
                        {
                            var tmp = array as byte[];
                            fixed (byte* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.UInt16:
                        {
                            var tmp = array as ushort[];
                            fixed (ushort* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.UInt32:
                        {
                            var tmp = array as uint[];
                            fixed (uint* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.Int8:
                        {
                            var tmp = array as sbyte[];
                            fixed (sbyte* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.Int16:
                        {
                            var tmp = array as short[];
                            fixed (short* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.Int32:
                        {
                            var tmp = array as int[];
                            fixed (int* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.Float:
                        {
                            var tmp = array as float[];
                            fixed (float* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.Double:
                        {
                            var tmp = array as double[];
                            fixed (double* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.RgbPixel:
                        {
                            var tmp = array as RgbPixel[];
                            fixed (RgbPixel* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.RgbAlphaPixel:
                        {
                            var tmp = array as RgbAlphaPixel[];
                            fixed (RgbAlphaPixel* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
                        }
                        break;
                    case Dlib.Native.MatrixElementType.HsiPixel:
                        {
                            var tmp = array as HsiPixel[];
                            fixed (HsiPixel* src = &tmp[0])
                                this.NativePtr = Dlib.Native.matrix_new2(this._ElementType, row, column, (IntPtr)src);
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

            if (!MatrixBase.TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();

            var size = ElementSizeDictionary[this._ElementType];
            if (size != elementSize)
                throw new ArgumentOutOfRangeException($"The size of {typeof(TElement).Name} does not equalt to {nameof(elementSize)}.");

            unsafe
            {
                fixed (byte* src = &array[0])
                    this.NativePtr = Dlib.Native.matrix_new3(this._ElementType, row, column, src);
            }

            this._Indexer = this.CreateIndexer(type);
        }

        internal Matrix(IntPtr ptr, int templateRows = 0, int templateColumns = 0, bool isEnabledDispose = true)
            : base(templateRows, templateColumns, isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            if (!MatrixBase.TryParse(typeof(TElement), out var type))
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
                Dlib.Native.matrix_nc(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return ret;
            }
        }

        public override MatrixElementTypes MatrixElementType => this._MatrixElementTypes;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.matrix_nr(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
                return ret;
            }
        }

        public int Size
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.matrix_size(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out var ret);
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
            switch (this._MatrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<byte>().ToArray());
                    break;
                case MatrixElementTypes.UInt16:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<ushort>().ToArray());
                    break;
                case MatrixElementTypes.UInt32:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<uint>().ToArray());
                    break;
                case MatrixElementTypes.Int8:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<sbyte>().ToArray());
                    break;
                case MatrixElementTypes.Int16:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<short>().ToArray());
                    break;
                case MatrixElementTypes.Int32:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<int>().ToArray());
                    break;
                case MatrixElementTypes.Float:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<float>().ToArray());
                    break;
                case MatrixElementTypes.Double:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<double>().ToArray());
                    break;
                case MatrixElementTypes.RgbPixel:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<RgbPixel>().ToArray());
                    break;
                case MatrixElementTypes.RgbAlphaPixel:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<RgbAlphaPixel>().ToArray());
                    break;
                case MatrixElementTypes.HsiPixel:
                    Dlib.Native.matrix_operator_array(this._ElementType, this.NativePtr, array.Cast<HsiPixel>().ToArray());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Matrix<TElement> CreateTemplateParameterizeMatrix<TElement>(uint templateRows, uint templateColumns)
            where TElement : struct
        {
            if (!MatrixBase.TryParse(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            var ptr = Dlib.Native.matrix_new4(type.ToNativeMatrixElementType(), templateRows, templateColumns);
            return new Matrix<TElement>(ptr, (int)templateRows, (int)templateColumns);
        }

        public TElement[] ToArray()
        {
            this.ThrowIfDisposed();

            TElement[] result;
            Dlib.Native.ErrorType err;

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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
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
                            err = Dlib.Native.extensions_matrix_to_array(src, type, templateRows, templateColumns, (IntPtr)dst);
                        }

                        result = array as TElement[];
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (err)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{this._ElementType} is not supported.");
                case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(this.TemplateColumns)} or {nameof(this.TemplateRows)} is not supported.");
            }

            return result;
        }

        internal static bool TryParse<T>(out MatrixElementTypes result)
            where T : struct
        {
            return MatrixBase.TryParse(typeof(T), out result);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Dlib.Native.matrix_delete(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            string str = null;

            try
            {
                ofstream = Dlib.Native.ostringstream_new();
                var ret = Dlib.Native.matrix_operator_left_shift(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, ofstream);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.OK:
                        stdstr = Dlib.Native.ostringstream_str(ofstream);
                        str = StringHelper.FromStdString(stdstr);
                        break;
                    case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                        throw new ArgumentException($"Input {this._ElementType} is not supported.");
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
                    Dlib.Native.string_delete(stdstr);
                if (ofstream != IntPtr.Zero)
                    Dlib.Native.ostringstream_delete(ofstream);
            }

            return str;
        }

        public static Matrix<TElement> operator +(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            if (!(lhs.Rows == rhs.Rows && lhs.Columns == rhs.Columns))
                throw new ArgumentException();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rihgtTemplateRows = rhs.TemplateRows;
            var rihgtTemplateColumns = rhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            var ret = Dlib.Native.matrix_operator_add(type,
                                                      lhs.NativePtr,
                                                      rhs.NativePtr,
                                                      leftTemplateRows,
                                                      leftTemplateColumns,
                                                      rihgtTemplateRows,
                                                      rihgtTemplateColumns,
                                                      out var matrix);
            switch (ret)
            {
                case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
            }

            return new Matrix<TElement>(matrix);
        }

        public static Matrix<TElement> operator -(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            if (!(lhs.Rows == rhs.Rows && lhs.Columns == rhs.Columns))
                throw new ArgumentException();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rihgtTemplateRows = rhs.TemplateRows;
            var rihgtTemplateColumns = rhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            var ret = Dlib.Native.matrix_operator_subtract(type,
                                                           lhs.NativePtr,
                                                           rhs.NativePtr,
                                                           leftTemplateRows,
                                                           leftTemplateColumns,
                                                           rihgtTemplateRows,
                                                           rihgtTemplateColumns,
                                                           out var matrix);
            switch (ret)
            {
                case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
            }

            return new Matrix<TElement>(matrix);
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            if (!(lhs.Columns == rhs.Rows && lhs.Size > 0 && rhs.Size > 0))
                throw new ArgumentException();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rihgtTemplateRows = rhs.TemplateRows;
            var rihgtTemplateColumns = rhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            var ret = Dlib.Native.matrix_operator_multiply(type,
                                                           lhs.NativePtr,
                                                           rhs.NativePtr,
                                                           leftTemplateRows,
                                                           leftTemplateColumns,
                                                           rihgtTemplateRows,
                                                           rihgtTemplateColumns,
                                                           out var matrix);
            switch (ret)
            {
                case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
            }

            return new Matrix<TElement>(matrix);
        }

        public static Matrix<TElement> operator /(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            if (!(rhs.Columns == 1 && rhs.Rows == 1))
                throw new ArgumentException();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rihgtTemplateRows = rhs.TemplateRows;
            var rihgtTemplateColumns = rhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            try
            {
                var ret = Dlib.Native.matrix_operator_divide(type,
                                                             lhs.NativePtr,
                                                             rhs.NativePtr,
                                                             leftTemplateRows,
                                                             leftTemplateColumns,
                                                             rihgtTemplateRows,
                                                             rihgtTemplateColumns,
                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                }

                return new Matrix<TElement>(matrix);
            }
            catch (DivideByZeroException)
            {
                // NOTE
                // Hide the source which throw exception
                throw new DivideByZeroException("Right operand may be zero matrix.");
            }
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
                case MatrixElementTypes.Int8:
                    return new IndexerInt8(this) as Indexer<TElement>;
                case MatrixElementTypes.Int16:
                    return new IndexerInt16(this) as Indexer<TElement>;
                case MatrixElementTypes.Int32:
                    return new IndexerInt32(this) as Indexer<TElement>;
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

        #region Indexer

        internal abstract class Indexer<T>
            where T : struct
        {

            #region Fields 

            protected readonly Dlib.Native.MatrixElementType _Type;

            protected readonly MatrixBase _Parent;

            #endregion

            #region Constructors 

            internal Indexer(MatrixBase parent)
            {
                this._Parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this._Type = this._Parent.MatrixElementType.ToNativeMatrixElementType();
            }

            #endregion

            #region Properties

            public abstract T this[int index]
            {
                get;
                set;
            }

            public abstract T this[int row, int column]
            {
                get;
                set;
            }

            #endregion

        }

        internal sealed class IndexerUInt8 : Indexer<byte>
        {

            #region Constructors

            public IndexerUInt8(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override byte this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    byte value;
                    Dlib.Native.matrix_operator_get_one_row_column_uint8_t(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_uint8_t(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override byte this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    byte value;
                    Dlib.Native.matrix_operator_get_row_column_uint8_t(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_uint8_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerUInt16 : Indexer<ushort>
        {

            #region Constructors

            public IndexerUInt16(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override ushort this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    ushort value;
                    Dlib.Native.matrix_operator_get_one_row_column_uint16_t(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_uint16_t(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override ushort this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    ushort value;
                    Dlib.Native.matrix_operator_get_row_column_uint16_t(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_uint16_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerUInt32 : Indexer<uint>
        {

            #region Constructors

            public IndexerUInt32(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override uint this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    uint value;
                    Dlib.Native.matrix_operator_get_one_row_column_uint32_t(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_uint32_t(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override uint this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    uint value;
                    Dlib.Native.matrix_operator_get_row_column_uint32_t(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_uint32_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerInt8 : Indexer<sbyte>
        {

            #region Constructors

            public IndexerInt8(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override sbyte this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    sbyte value;
                    Dlib.Native.matrix_operator_get_one_row_column_int8_t(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_int8_t(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override sbyte this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    sbyte value;
                    Dlib.Native.matrix_operator_get_row_column_int8_t(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_int8_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerInt16 : Indexer<short>
        {

            #region Constructors

            public IndexerInt16(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override short this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    short value;
                    Dlib.Native.matrix_operator_get_one_row_column_int16_t(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_int16_t(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override short this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    short value;
                    Dlib.Native.matrix_operator_get_row_column_int16_t(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_int16_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerInt32 : Indexer<int>
        {

            #region Constructors

            public IndexerInt32(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override int this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    int value;
                    Dlib.Native.matrix_operator_get_one_row_column_int32_t(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_int32_t(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override int this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    int value;
                    Dlib.Native.matrix_operator_get_row_column_int32_t(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_int32_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerFloat : Indexer<float>
        {

            #region Constructors

            public IndexerFloat(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override float this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    float value;
                    Dlib.Native.matrix_operator_get_one_row_column_float(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_float(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override float this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    float value;
                    Dlib.Native.matrix_operator_get_row_column_float(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_float(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerDouble : Indexer<double>
        {

            #region Constructors

            public IndexerDouble(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override double this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    double value;
                    Dlib.Native.matrix_operator_get_one_row_column_double(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_double(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override double this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    double value;
                    Dlib.Native.matrix_operator_get_row_column_double(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_double(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerRgbPixel : Indexer<RgbPixel>
        {

            #region Constructors

            public IndexerRgbPixel(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override RgbPixel this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    RgbPixel value;
                    Dlib.Native.matrix_operator_get_one_row_column_rgb_pixel(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_rgb_pixel(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override RgbPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    RgbPixel value;
                    Dlib.Native.matrix_operator_get_row_column_rgb_pixel(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_rgb_pixel(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerRgbAlphaPixel : Indexer<RgbAlphaPixel>
        {

            #region Constructors

            public IndexerRgbAlphaPixel(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override RgbAlphaPixel this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    RgbAlphaPixel value;
                    Dlib.Native.matrix_operator_get_one_row_column_rgb_alpha_pixel(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_rgb_alpha_pixel(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override RgbAlphaPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    RgbAlphaPixel value;
                    Dlib.Native.matrix_operator_get_row_column_rgb_alpha_pixel(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_rgb_alpha_pixel(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

        }

        internal sealed class IndexerHsiPixel : Indexer<HsiPixel>
        {

            #region Constructors

            public IndexerHsiPixel(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override HsiPixel this[int index]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    HsiPixel value;
                    Dlib.Native.matrix_operator_get_one_row_column_hsi_pixel(this._Parent.NativePtr, index, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_hsi_pixel(this._Parent.NativePtr, index, tr, tc, value);
                }
            }

            public override HsiPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    HsiPixel value;
                    Dlib.Native.matrix_operator_get_row_column_hsi_pixel(this._Parent.NativePtr, row, column, tr, tc, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_hsi_pixel(this._Parent.NativePtr, row, column, tr, tc, value);
                }

            }

            #endregion

        }

        #endregion

    }

}