using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_new();

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_new2(IntPtr vector);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr perspective_window_new3(IntPtr vector, byte[] title);

        #region add_overlay(vector<double>, vector<double>, pixel_color)

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref short color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref int color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, NativeMethods.Array2DType type, ref HsiPixel color);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay2(IntPtr window, IntPtr vector);

        #region add_overlay3(vector<double>, vector<double>, pixel_color)

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref short color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref int color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, NativeMethods.Array2DType type, ref HsiPixel color);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType perspective_window_add_overlay4(IntPtr window, IntPtr vector);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void perspective_window_delete(IntPtr ptr);

    }

}