using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region menu_bar

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr menu_bar_new(IntPtr drawable_window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void menu_bar_delete(IntPtr ptr);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void menu_bar_set_number_of_menus(IntPtr menubar, uint num);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void menu_bar_set_menu_name(IntPtr menubar, uint idx, byte[] name, char underline_ch);

        #endregion

        #region list_box

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr list_box_new(IntPtr drawable_window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void list_box_delete(IntPtr ptr);

        #endregion

        #region label

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr label_new(IntPtr drawable_window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void label_delete(IntPtr ptr);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void label_set_text(IntPtr ptr, byte[] text);

        #endregion

        #region text_field

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr text_field_new(IntPtr drawable_window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_delete(IntPtr ptr);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_set_text(IntPtr ptr, byte[] text);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_set_width(IntPtr ptr, uint width);

        #endregion
    }

}