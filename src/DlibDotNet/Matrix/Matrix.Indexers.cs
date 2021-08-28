#if !LITE
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

            protected readonly NativeMethods.MatrixElementType _Type;

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

                var ret = NativeMethods.matrix_begin(type, matrix, tr, tc, out begin);
                this.ThrowIfHasError(ret);

                ret = NativeMethods.matrix_end(type, matrix, tr, tc, out end);
                this.ThrowIfHasError(ret);
            }

            protected void ThrowIfHasError(NativeMethods.ErrorType error)
            {
                switch (error)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {this._Type} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
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

                    unsafe
                    {
                        byte value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        byte value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        ushort value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        ushort value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        uint value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        uint value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        ulong value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        ulong value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        sbyte value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        sbyte value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        short value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        short value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        int value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        int value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        long value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        long value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        float value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        float value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        double value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        double value;
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        var value = new RgbPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        var value = new RgbPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

        internal sealed class IndexerBgrPixel : Indexer<BgrPixel>
        {

            #region Constructors

            public IndexerBgrPixel(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override BgrPixel this[int index]
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

                    unsafe
                    {
                        var value = new BgrPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
                }
            }

            public override BgrPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var value = new BgrPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
                }
            }

            #endregion

            #region Methods

            public override IEnumerator<BgrPixel> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / (uint)Marshal.SizeOf<BgrPixel>();

                var array = new BgrPixel[length];
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

                    unsafe
                    {
                        var value = new RgbAlphaPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        var value = new RgbAlphaPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

                    unsafe
                    {
                        var value = new HsiPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
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

                    unsafe
                    {
                        var value = new HsiPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
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

        internal sealed class IndexerLabPixel : Indexer<LabPixel>
        {

            #region Constructors

            public IndexerLabPixel(MatrixBase parent)
                : base(parent)
            {
            }

            #endregion

            #region Properties

            public override LabPixel this[int index]
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

                    unsafe
                    {
                        var value = new LabPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
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

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_one_row_column(this._Type, this._Parent.NativePtr, index, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
                }
            }

            public override LabPixel this[int row, int column]
            {
                get
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var value = new LabPixel();
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_get_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);

                        return value;
                    }
                }
                set
                {
                    var r = this._Parent.Rows;
                    var c = this._Parent.Columns;
                    var tr = this._Parent.TemplateRows;
                    var tc = this._Parent.TemplateColumns;

                    if (!(0 <= column && column < c) && (0 <= row && row < r))
                        throw new IndexOutOfRangeException();

                    unsafe
                    {
                        var p = (IntPtr)(&value);
                        var ret = NativeMethods.matrix_operator_set_row_column(this._Type, this._Parent.NativePtr, row, column, tr, tc, p);
                        this.ThrowIfHasError(ret);
                    }
                }

            }

            #endregion

            #region Methods

            public override IEnumerator<LabPixel> GetEnumerator()
            {
                this.GetBeginEnd(out var begin, out var end);

                var length = ((ulong)end - (ulong)begin) / (uint)Marshal.SizeOf<LabPixel>();

                var array = new LabPixel[length];
                InteropHelper.Copy(begin, array, (uint)array.Length);

                foreach (var b in array)
                    yield return b;
            }

            #endregion

        }

    }

}
#endif
