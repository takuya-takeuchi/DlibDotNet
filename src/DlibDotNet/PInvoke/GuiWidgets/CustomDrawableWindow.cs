#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if !DLIB_NO_GUI_SUPPORT
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr custom_drawable_window_new(bool resizable,
                                                               bool undecorated,
                                                               IntPtr constructor_function,
                                                               IntPtr destructor_function,
                                                               IntPtr on_window_resized_function,
                                                               IntPtr on_keydown_function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void custom_drawable_window_delete(IntPtr window);
#endif

    }

}
#endif
