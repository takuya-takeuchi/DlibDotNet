using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new(IntPtr v);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Array2DType type, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool perspective_window_overlay_dot_p(IntPtr dot, out IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool perspective_window_overlay_dot_color(IntPtr dot, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void perspective_window_overlay_dot_delete(IntPtr dot);

    }

}