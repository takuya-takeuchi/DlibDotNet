using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;
using DlibDotNet.Interop;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public partial class Matrix<TElement>
        where TElement : struct
    {

        internal abstract class Indexer<T> : IEnumerable<T>
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

            #region Methods

            protected void GetBeginEnd(out IntPtr begin, out IntPtr end)
            {
                var type = this._Type;
                var matrix = this._Parent.NativePtr;
                var tr = this._Parent.TemplateRows;
                var tc = this._Parent.TemplateColumns;

                var ret = Dlib.Native.matrix_begin(type, matrix, tr, tc, out begin);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                        throw new ArgumentException($"Input {type} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                ret = Dlib.Native.matrix_end(type, matrix, tr, tc, out end);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                        throw new ArgumentException($"Input {type} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }
            }

            #endregion

            #region IEnumerable<T> Implementation

            public abstract IEnumerator<T> GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_uint8_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_uint8_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<byte> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(byte);

                var array = new byte[length];
                Marshal.Copy(begin, array, 0, array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_uint16_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_uint16_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<ushort> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(ushort);

                var array = new ushort[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_uint32_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_uint32_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<uint> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(uint);

                var array = new uint[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
            }

            #endregion

        }

        internal sealed class IndexerUInt64 : Indexer<ulong>
        {

            #region Constructors

            public IndexerUInt64(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override ulong this[int index]
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_uint64_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_uint64_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
                }
            }

            public override ulong this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    ulong value;
                    Dlib.Native.matrix_operator_get_row_column_uint64_t(this._Parent.NativePtr, row, column, tr, tc, out value);
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

                    Dlib.Native.matrix_operator_set_row_column_uint64_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

            #region Methods

            public override IEnumerator<ulong> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(ulong);

                var array = new ulong[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_int8_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_int8_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<sbyte> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(sbyte);

                var array = new sbyte[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_int16_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_int16_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<short> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(short);

                var array = new short[length];
                Marshal.Copy(begin, array, 0, array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_int32_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_int32_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<int> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(int);

                var array = new int[length];
                Marshal.Copy(begin, array, 0, array.Length);

                foreach (var b in array)
                    yield return b;
            }

            #endregion

        }

        internal sealed class IndexerInt64 : Indexer<long>
        {

            #region Constructors

            public IndexerInt64(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override long this[int index]
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_int64_t(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_int64_t(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
                }
            }

            public override long this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    long value;
                    Dlib.Native.matrix_operator_get_row_column_int64_t(this._Parent.NativePtr, row, column, tr, tc, out value);
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

                    Dlib.Native.matrix_operator_set_row_column_int64_t(this._Parent.NativePtr, row, column, tr, tc, value);
                }
            }

            #endregion

            #region Methods

            public override IEnumerator<long> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(long);

                var array = new long[length];
                Marshal.Copy(begin, array, 0, array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_float(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_float(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<float> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(float);

                var array = new float[length];
                Marshal.Copy(begin, array, 0, array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_double(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_double(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<double> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / sizeof(double);

                var array = new double[length];
                Marshal.Copy(begin, array, 0, array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_rgb_pixel(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_rgb_pixel(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<RgbPixel> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / (uint)Marshal.SizeOf<RgbPixel>();

                var array = new RgbPixel[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_rgb_alpha_pixel(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_rgb_alpha_pixel(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<RgbAlphaPixel> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / (uint)Marshal.SizeOf<RgbAlphaPixel>();

                var array = new RgbAlphaPixel[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
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

                    var ret = Dlib.Native.matrix_operator_get_one_row_column_hsi_pixel(this._Parent.NativePtr, index, tr, tc, out var value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }

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

                    var ret = Dlib.Native.matrix_operator_set_one_row_column_hsi_pixel(this._Parent.NativePtr, index, tr, tc, value);
                    switch (ret)
                    {
                        case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                    }
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

            #region Methods

            public override IEnumerator<HsiPixel> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / (uint)Marshal.SizeOf<HsiPixel>();

                var array = new HsiPixel[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
            }

            #endregion

        }

    }

}