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
        public static extern IntPtr menu_item_text_new(byte[] str, int strLength, IntPtr drawable_window, IntPtr event_handler, char hk);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr menu_item_text_new(byte[] str, int strLength, IntPtr mediator, char hk);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void menu_item_text_delete(IntPtr ptr);

#endif

    }

}
#endif
