#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType create_tiled_pyramid(MatrixElementType img_type,
                                                            IntPtr img,
                                                            PyramidType pyramidType,
                                                            uint pyramidRate,
                                                            uint padding,
                                                            uint outer_padding,
                                                            out IntPtr out_img,
                                                            out IntPtr rects);

    }

}
#endif
