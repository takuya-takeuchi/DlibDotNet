using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType heatmap(Array2DType type, IntPtr img, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType heatmap2(Array2DType type, IntPtr img, double maxVal, double minVal, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType heatmap_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType heatmap2_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, double maxVal, double minVal, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType jet(Array2DType type, IntPtr img, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType jet2(Array2DType type, IntPtr img, double maxVal, double minVal, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType jet_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType jet2_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, double maxVal, double minVal, out IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void colormap_heat(double value, double min_val, double max_val, ref RgbPixel pixel);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void colormap_jet(double value, double min_val, double max_val, ref RgbPixel pixel);

    }

}