using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType disturb_colors_matrix(MatrixElementType type,
                                                             IntPtr matrix,
                                                             int templateRows,
                                                             int templateColumns,
                                                             IntPtr rand,
                                                             double gamma_magnitude,
                                                             double color_magnitude);

    }

}