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
        public static extern IntPtr list_box_new(IntPtr drawable_window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void list_box_delete(IntPtr list_box);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ulong list_box_size(IntPtr list_box);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void list_box_select(IntPtr list_box, uint index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void list_box_unselect(IntPtr list_box, uint index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void list_box_enable_multiple_select(IntPtr list_box);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void list_box_disable_multiple_select(IntPtr list_box);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool list_box_multiple_select_enabled(IntPtr list_box);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void list_box_set_click_handler(IntPtr list_box, IntPtr mediator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void list_box_load_stdstring(IntPtr list_box, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr list_box_get_selected(IntPtr list_box);
        
#endif

    }

}
#endif
