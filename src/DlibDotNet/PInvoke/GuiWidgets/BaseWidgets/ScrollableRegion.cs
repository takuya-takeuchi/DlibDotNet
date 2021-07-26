using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void scrollable_region_set_pos(IntPtr region, int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void scrollable_region_set_size(IntPtr region, uint width, uint height);

#endif

    }

}