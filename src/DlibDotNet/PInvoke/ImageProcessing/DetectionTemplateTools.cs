#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr compute_box_dimensions(double filterSize,
                                                           double numScaleLevels);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr create_grid_detection_template(IntPtr object_box,
                                                                   uint cells_x,
                                                                   uint cells_y);

    }

}
#endif
