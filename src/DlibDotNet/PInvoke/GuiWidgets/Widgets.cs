#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ImageDisplayOverlayRectSelectedActionDelegate(IntPtr rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SelectIndexedActionDelegate(uint index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ClickActionDelegate(IntPtr point, bool isDoubleClick, uint button);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void StringActionDelegate(IntPtr file);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void VoidActionDelegate();

        #endregion

#if !DLIB_NO_GUI_SUPPORT
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void message_box(byte[] title, int titleLength, byte[] message, int messageLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void save_file_box(IntPtr stringActionMediator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr void_action_mediator_new(IntPtr callback);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void void_action_mediator_delete(IntPtr mediator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr string_action_mediator_new(IntPtr callback);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void string_action_mediator_delete(IntPtr mediator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr click_action_mediator_new(IntPtr callback);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void click_action_mediator_delete(IntPtr mediator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr uint32t_action_mediator_new(IntPtr callback);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void uint32t_action_mediator_delete(IntPtr mediator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_display_overlay_rect_action_mediator_new(IntPtr callback);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_display_overlay_rect_action_mediator_delete(IntPtr mediator);
#endif

    }

}
#endif
