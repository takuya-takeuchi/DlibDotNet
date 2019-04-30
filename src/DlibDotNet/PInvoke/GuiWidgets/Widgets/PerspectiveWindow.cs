using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_new2(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr perspective_window_new3(IntPtr vector, byte[] title);

        #region add_overlay(vector<double>, vector<double>, pixel_color)

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Array2DType type, ref HsiPixel color);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay2(IntPtr window, IntPtr vector);

        #region add_overlay3(vector<double>, vector<double>, pixel_color)

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Array2DType type, ref HsiPixel color);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType perspective_window_add_overlay4(IntPtr window, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void perspective_window_delete(IntPtr ptr);

    }

}