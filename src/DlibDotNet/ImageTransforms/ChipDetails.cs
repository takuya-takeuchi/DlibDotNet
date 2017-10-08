using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ChipDetails : DlibObject
    {

        #region Constructors

        public ChipDetails()
            : this(Native.chip_details_new())
        {
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
                Native.chip_details_angle(this.NativePtr, out var angle);
                return angle;
            }
        }

        public uint Columns
        {
            get
            {
                this.ThrowIfDisposed();
                Native.chip_details_cols(this.NativePtr, out var columns);
                return columns;
            }
        }

        public DRectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                Native.chip_details_rect(this.NativePtr, out var rect);
                return new DRectangle(rect);
            }
        }

        public uint Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Native.chip_details_rows(this.NativePtr, out var rows);
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

            using (var rect = this.Rect)
                return !rect.IsEmpty;
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.chip_details_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr chip_details_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool chip_details_angle(IntPtr chip, out double angle);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool chip_details_cols(IntPtr chip, out uint cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool chip_details_rect(IntPtr chip, out IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool chip_details_rows(IntPtr chip, out uint rows);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void chip_details_delete(IntPtr obj);

        }

    }

}