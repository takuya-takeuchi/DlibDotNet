using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class VectorOfImageWindowOverlayLine : StdVector<ImageWindow.OverlayLine>
    {

        #region Constructors

        public VectorOfImageWindowOverlayLine()
        {
            this.NativePtr = Native.vector_image_window_overlay_line_new1();
        }

        public VectorOfImageWindowOverlayLine(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.vector_image_window_overlay_line_new2(new IntPtr(size));
        }

        public VectorOfImageWindowOverlayLine(IEnumerable<ImageWindow.OverlayLine> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
            this.NativePtr = Native.vector_image_window_overlay_line_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.vector_image_window_overlay_line_getPointer(this.NativePtr);

        public override int Size => Native.vector_image_window_overlay_line_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override ImageWindow.OverlayLine[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new ImageWindow.OverlayLine[0];

            var dst = new IntPtr[size];
            Native.vector_image_window_overlay_line_copy(this.NativePtr, dst);
            return dst.Select(p=> new ImageWindow.OverlayLine(p)).ToArray();
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            // Do NOT dispose each item element except for std::vector
            //foreach (var item in this.ToArray())
            //    item?.Dispose();

            Native.vector_image_window_overlay_line_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_image_window_overlay_line_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_image_window_overlay_line_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_image_window_overlay_line_new3([In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_image_window_overlay_line_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_image_window_overlay_line_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_image_window_overlay_line_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_image_window_overlay_line_delete(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_image_window_overlay_line_copy(IntPtr vector, IntPtr[] dst);

        }

    }

}