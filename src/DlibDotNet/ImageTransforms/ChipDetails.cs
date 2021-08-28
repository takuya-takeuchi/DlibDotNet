using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ChipDetails : DlibObject
    {

        #region Constructors

        public ChipDetails()
            : this(NativeMethods.chip_details_new())
        {
        }

        public ChipDetails(DRectangle rect, ChipDims dims)
        {
            if (dims == null)
                throw new ArgumentNullException(nameof(dims));

            dims.ThrowIfDisposed();

            using (var tmp = rect.ToNative())
                this.NativePtr = NativeMethods.chip_details_new2(tmp.NativePtr, dims.NativePtr);
        }

        public ChipDetails(Rectangle rect, ChipDims dims)
        {
            if (dims == null)
                throw new ArgumentNullException(nameof(dims));

            dims.ThrowIfDisposed();

            using (var tmp = rect.ToNative())
                this.NativePtr = NativeMethods.chip_details_new3(tmp.NativePtr, dims.NativePtr);
        }
		
		public ChipDetails(Rectangle rect)
        {
            using (var tmp = rect.ToNative())
                this.NativePtr = NativeMethods.chip_details_new6(tmp.NativePtr);
        }

        public ChipDetails(DRectangle rect, uint size)
        {
            using (var tmp = rect.ToNative())
                this.NativePtr = NativeMethods.chip_details_new4(tmp.NativePtr, size);
        }

        public ChipDetails(DRectangle rect, uint size, double angle)
        {
            using (var tmp = rect.ToNative())
                this.NativePtr = NativeMethods.chip_details_new5(tmp.NativePtr, size, angle);
        }

        internal ChipDetails(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public double Angle
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.chip_details_angle(this.NativePtr, out var angle);
                return angle;
            }
        }

        public uint Columns
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.chip_details_cols(this.NativePtr, out var columns);
                return columns;
            }
        }

        public DRectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.chip_details_rect(this.NativePtr, out var rect);
                return new DRectangle(rect);
            }
        }

        public uint Rows
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.chip_details_rows(this.NativePtr, out var rows);
                return rows;
            }
        }

        #endregion

        #region Methods

        internal bool IsValid()
        {
            this.ThrowIfDisposed();

            if (this.Rows == 0)
                return false;
            if (this.Columns == 0)
                return false;

            return !this.Rect.IsEmpty;
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

            NativeMethods.chip_details_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
