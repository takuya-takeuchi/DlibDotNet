using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ChipDims : DlibObject
    {

        #region Constructors

        public ChipDims(uint rows, uint cols)
            : this(Native.chip_dims_new(rows, cols))
        {
            this.Rows = rows;
            this.Columns = cols;
        }

        internal ChipDims(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public uint Columns
        {
            get
            {
                this.ThrowIfDisposed();
                Native.chip_dims_get_cols(this.NativePtr, out var columns);
                return columns;
            }
            set
            {
                this.ThrowIfDisposed();
                Native.chip_dims_set_cols(this.NativePtr, value);
            }
        }

        public uint Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Native.chip_dims_get_rows(this.NativePtr, out var rows);
                return rows;
            }
            set
            {
                this.ThrowIfDisposed();
                Native.chip_dims_set_rows(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.chip_dims_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr chip_dims_new(uint rows, uint cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool chip_dims_get_cols(IntPtr chip, out uint cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void chip_dims_set_cols(IntPtr chip, uint cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool chip_dims_get_rows(IntPtr chip, out uint rows);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void chip_dims_set_rows(IntPtr chip, uint rows);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void chip_dims_delete(IntPtr obj);

        }

    }

}