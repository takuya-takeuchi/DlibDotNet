using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr menu_bar_new(IntPtr drawable_window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void menu_bar_delete(IntPtr ptr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void menu_bar_set_number_of_menus(IntPtr menubar, uint num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void menu_bar_set_menu_name(IntPtr menubar, uint idx, byte[] name, int nameLength, char underline_ch);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr menu_bar_menu(IntPtr menubar, uint idx);
        
#endif

    }

}