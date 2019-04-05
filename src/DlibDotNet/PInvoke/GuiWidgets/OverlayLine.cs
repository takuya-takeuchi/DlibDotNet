using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_window_overlay_line_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_window_overlay_line_new_rgb(IntPtr p1, IntPtr p2, RgbPixel pixel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_window_overlay_line_p1(IntPtr line, out IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_window_overlay_line_p2(IntPtr line, out IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_window_overlay_line_color(IntPtr line, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_window_overlay_line_delete(IntPtr line);

    }

}