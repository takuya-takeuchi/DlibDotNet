using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class FullObjectDetection : DlibObject
    {

        #region Constructors

        internal FullObjectDetection(IntPtr ptr)
        {
            this.NativePtr = ptr;
            this._Parts = Native.full_object_detection_num_parts(this.NativePtr);
        }

        #endregion

        #region Properties

        private readonly uint _Parts;

        public uint Parts
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Parts;
            }
        }

        public Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                var rect = Native.full_object_detection_get_rect(this.NativePtr);
                return new Rectangle(rect);
            }
        }

        #endregion

        #region Methods

        public Point GetPart(uint index)
        {
            this.ThrowIfDisposed();
            if (!(index < this._Parts))
                throw new ArgumentOutOfRangeException(nameof(index));

            var p = Native.full_object_detection_part(this.NativePtr, index);
            return new Point(p);
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

            Native.full_object_detection_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr full_object_detection_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint full_object_detection_num_parts(IntPtr predictor);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr full_object_detection_get_rect(IntPtr predictor);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr full_object_detection_part(IntPtr predictor, uint idx);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void full_object_detection_delete(IntPtr point);

        }

    }
}
