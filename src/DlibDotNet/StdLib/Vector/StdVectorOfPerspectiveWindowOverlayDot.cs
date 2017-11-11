using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdVectorOfPerspectiveWindowOverlayDot : StdVector<PerspectiveWindow.OverlayDot>
    {
        
        #region Constructors

        public StdVectorOfPerspectiveWindowOverlayDot()
        {
            this.NativePtr = Native.stdvector_perspective_window_overlay_dot_new1();
        }

        public StdVectorOfPerspectiveWindowOverlayDot(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.stdvector_perspective_window_overlay_dot_new2(new IntPtr(size));
        }

        public StdVectorOfPerspectiveWindowOverlayDot(IEnumerable<PerspectiveWindow.OverlayDot> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
            this.NativePtr = Native.stdvector_perspective_window_overlay_dot_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.stdvector_perspective_window_overlay_dot_getPointer(this.NativePtr);

        public override int Size => Native.stdvector_perspective_window_overlay_dot_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override PerspectiveWindow.OverlayDot[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new PerspectiveWindow.OverlayDot[0];

            var dst = new IntPtr[size];
            Native.stdvector_perspective_window_overlay_dot_copy(this.NativePtr, dst);
            return dst.Select(p=> new PerspectiveWindow.OverlayDot(p)).ToArray();
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            // Do NOT dispose each item element except for std::vector
            //foreach (var item in this.ToArray())
            //    item?.Dispose();

            Native.stdvector_perspective_window_overlay_dot_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_perspective_window_overlay_dot_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_perspective_window_overlay_dot_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_perspective_window_overlay_dot_new3([In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_perspective_window_overlay_dot_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_perspective_window_overlay_dot_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_perspective_window_overlay_dot_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_perspective_window_overlay_dot_delete(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_perspective_window_overlay_dot_copy(IntPtr vector, IntPtr[] dst);

        }

    }

}