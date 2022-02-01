using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr label_new(IntPtr drawable_window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void label_delete(IntPtr ptr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void label_set_text(IntPtr ptr, byte[] text, int textLength);
        
#endif

    }

}