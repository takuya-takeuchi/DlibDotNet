using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr menu_item_separator_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void menu_item_separator_delete(IntPtr ptr);

#endif

    }

}