using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new(IntPtr v);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref short color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref int color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, NativeMethods.Array2DType type, ref HsiPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool perspective_window_overlay_dot_p(IntPtr dot, out IntPtr vector);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool perspective_window_overlay_dot_color(IntPtr dot, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void perspective_window_overlay_dot_delete(IntPtr dot);

    }

}