using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ChipDims : DlibObject
    {

        #region Constructors

        public ChipDims(uint rows, uint cols)
            : this(NativeMethods.chip_dims_new(rows, cols))
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
                NativeMethods.chip_dims_get_cols(this.NativePtr, out var columns);
                return columns;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.chip_dims_set_cols(this.NativePtr, value);
            }
        }

        public uint Rows
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.chip_dims_get_rows(this.NativePtr, out var rows);
                return rows;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.chip_dims_set_rows(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.chip_dims_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
