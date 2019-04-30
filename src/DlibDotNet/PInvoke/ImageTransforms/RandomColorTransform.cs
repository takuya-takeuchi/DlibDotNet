using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType apply_random_color_offset_matrix(MatrixElementType type,
                                                                        IntPtr matrix,
                                                                        int templateRows,
                                                                        int templateColumns,
                                                                        IntPtr rand);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType disturb_colors_matrix(MatrixElementType type,
                                                             IntPtr matrix,
                                                             int templateRows,
                                                             int templateColumns,
                                                             IntPtr rand,
                                                             double gamma_magnitude,
                                                             double color_magnitude);

    }

}