using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Matrix<T> : MatrixBase
        where T : struct
    {

        #region Fields

        private readonly MatrixElementTypes _MatrixElementTypes;

        private readonly Dlib.Native.MatrixElementType _ElementType;

        private static readonly Dictionary<Type, MatrixElementTypes> SupportTypes = new Dictionary<Type, MatrixElementTypes>();

        private readonly Indexer<T> _Indexer;

        #endregion

        #region Constructors

        static Matrix()
        {
            var types = new[]
            {
                new {Type = typeof(byte),           ElementType = MatrixElementTypes.UInt8 },
                new {Type = typeof(ushort),         ElementType = MatrixElementTypes.UInt16 },
                new {Type = typeof(uint),           ElementType = MatrixElementTypes.UInt32 },
                new {Type = typeof(sbyte),          ElementType = MatrixElementTypes.Int8 },
                new {Type = typeof(short),          ElementType = MatrixElementTypes.Int16 },
                new {Type = typeof(int),            ElementType = MatrixElementTypes.Int32 },
                new {Type = typeof(float),          ElementType = MatrixElementTypes.Float },
                new {Type = typeof(double),         ElementType = MatrixElementTypes.Double },
                new {Type = typeof(RgbPixel),       ElementType = MatrixElementTypes.RgbPixel },
                new {Type = typeof(RgbAlphaPixel),  ElementType = MatrixElementTypes.RgbAlphaPixel },
                new {Type = typeof(HsiPixel),       ElementType = MatrixElementTypes.HsiPixel }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public Matrix()
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            this.NativePtr = Dlib.Native.matrix_new(this._ElementType);

            this._Indexer = this.CreateIndexer(type);
        }

        public Matrix(int row, int column)
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");
            if (row < 0)
                throw new ArgumentOutOfRangeException($"{nameof(row)}", $"{nameof(row)} should be positive value.");
            if (column < 0)
                throw new ArgumentOutOfRangeException($"{nameof(column)}", $"{nameof(column)} should be positive value.");

            this._MatrixElementTypes = type;
            this._ElementType = type.ToNativeMatrixElementType();
            this.NativePtr = Dlib.Native.matrix_new1(this._ElementType, row, column);

            this._Indexer = this.CreateIndexer(type);
        }

        internal Matrix(IntPtr ptr, MatrixElementTypes type)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

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
                Dlib.Native.matrix_nc(this._ElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override MatrixElementTypes MatrixElementType => this._MatrixElementTypes;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.matrix_nr(this._ElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public int Size
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.matrix_size(this._ElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public T this[int index]
        {
            get => this._Indexer[index];
            set => this._Indexer[index] = value;
        }

        public T this[int row, int column]
        {
            get => this._Indexer[row, column];
            set => this._Indexer[row, column] = value;
        }

        #endregion

        #region Methods

        public void Assign(T[] array)
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

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Dlib.Native.matrix_delete(this._ElementType, this.NativePtr);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            string str = null;

            try
            {
                ofstream = Dlib.Native.ostringstream_new();
                var ret = Dlib.Native.matrix_operator_left_shift(this._ElementType, this.NativePtr, ofstream);
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

        #endregion

        #region Helpers

        private Indexer<T> CreateIndexer(MatrixElementTypes types)
        {
            switch (types)
            {
                case MatrixElementTypes.UInt8:
                    return new IndexerUInt8(this) as Indexer<T>;
                case MatrixElementTypes.UInt16:
                    return new IndexerUInt16(this) as Indexer<T>;
                case MatrixElementTypes.UInt32:
                    return new IndexerUInt32(this) as Indexer<T>;
                    ;
                case MatrixElementTypes.Int8:
                    return new IndexerInt8(this) as Indexer<T>;
                case MatrixElementTypes.Int16:
                    return new IndexerInt16(this) as Indexer<T>;
                case MatrixElementTypes.Int32:
                    return new IndexerInt32(this) as Indexer<T>;
                    ;
                case MatrixElementTypes.Float:
                    return new IndexerFloat(this) as Indexer<T>;
                case MatrixElementTypes.Double:
                    return new IndexerDouble(this) as Indexer<T>;
                case MatrixElementTypes.RgbPixel:
                    return new IndexerRgbPixel(this) as Indexer<T>;
                case MatrixElementTypes.RgbAlphaPixel:
                    return new IndexerRgbAlphaPixel(this) as Indexer<T>;
                case MatrixElementTypes.HsiPixel:
                    return new IndexerHsiPixel(this) as Indexer<T>;
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    byte value;
                    Dlib.Native.matrix_operator_get_one_row_column_uint8_t(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_uint8_t(this._Parent.NativePtr, index, value);
                }
            }

            public override byte this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    byte value;
                    Dlib.Native.matrix_operator_get_row_column_uint8_t(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_uint8_t(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    ushort value;
                    Dlib.Native.matrix_operator_get_one_row_column_uint16_t(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_uint16_t(this._Parent.NativePtr, index, value);
                }
            }

            public override ushort this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    ushort value;
                    Dlib.Native.matrix_operator_get_row_column_uint16_t(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_uint16_t(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    uint value;
                    Dlib.Native.matrix_operator_get_one_row_column_uint32_t(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_uint32_t(this._Parent.NativePtr, index, value);
                }
            }

            public override uint this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    uint value;
                    Dlib.Native.matrix_operator_get_row_column_uint32_t(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_uint32_t(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    sbyte value;
                    Dlib.Native.matrix_operator_get_one_row_column_int8_t(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_int8_t(this._Parent.NativePtr, index, value);
                }
            }

            public override sbyte this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    sbyte value;
                    Dlib.Native.matrix_operator_get_row_column_int8_t(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_int8_t(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    short value;
                    Dlib.Native.matrix_operator_get_one_row_column_int16_t(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_int16_t(this._Parent.NativePtr, index, value);
                }
            }

            public override short this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    short value;
                    Dlib.Native.matrix_operator_get_row_column_int16_t(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_int16_t(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    int value;
                    Dlib.Native.matrix_operator_get_one_row_column_int32_t(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_int32_t(this._Parent.NativePtr, index, value);
                }
            }

            public override int this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    int value;
                    Dlib.Native.matrix_operator_get_row_column_int32_t(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_int32_t(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    float value;
                    Dlib.Native.matrix_operator_get_one_row_column_float(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_float(this._Parent.NativePtr, index, value);
                }
            }

            public override float this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    float value;
                    Dlib.Native.matrix_operator_get_row_column_float(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_float(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    double value;
                    Dlib.Native.matrix_operator_get_one_row_column_double(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_double(this._Parent.NativePtr, index, value);
                }
            }

            public override double this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    double value;
                    Dlib.Native.matrix_operator_get_row_column_double(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_double(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    RgbPixel value;
                    Dlib.Native.matrix_operator_get_one_row_column_rgb_pixel(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_rgb_pixel(this._Parent.NativePtr, index, value);
                }
            }

            public override RgbPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    RgbPixel value;
                    Dlib.Native.matrix_operator_get_row_column_rgb_pixel(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_rgb_pixel(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    RgbAlphaPixel value;
                    Dlib.Native.matrix_operator_get_one_row_column_rgb_alpha_pixel(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_rgb_alpha_pixel(this._Parent.NativePtr, index, value);
                }
            }

            public override RgbAlphaPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    RgbAlphaPixel value;
                    Dlib.Native.matrix_operator_get_row_column_rgb_alpha_pixel(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_rgb_alpha_pixel(this._Parent.NativePtr, row, column, value);
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
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    HsiPixel value;
                    Dlib.Native.matrix_operator_get_one_row_column_hsi_pixel(this._Parent.NativePtr, index, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    if (!(r == 1 || c == 1))
                        throw new NotSupportedException();

                    if (!((r == 1 && 0 <= index && index < c) || (c == 1 && 0 <= index && index < r)))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_one_row_column_hsi_pixel(this._Parent.NativePtr, index, value);
                }
            }

            public override HsiPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    HsiPixel value;
                    Dlib.Native.matrix_operator_get_row_column_hsi_pixel(this._Parent.NativePtr, row, column, out value);
                    return value;
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    Dlib.Native.matrix_operator_set_row_column_hsi_pixel(this._Parent.NativePtr, row, column, value);
                }

            }

            #endregion

        }

        #endregion

    }

}