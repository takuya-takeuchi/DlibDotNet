using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_new(uint pyramidRate, out IntPtr pyramid);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void pyramid_down_delete(uint pyramidRate, IntPtr pyramid);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_up(IntPtr pyramid,
                                                            uint pyramidRate,
                                                            IntPtr rect,
                                                            out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_up_rectangle(IntPtr pyramid,
                                                                      uint pyramidRate,
                                                                      IntPtr rect,
                                                                      out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_up2(IntPtr pyramid,
                                                             uint pyramidRate,
                                                             IntPtr rect,
                                                             uint levels,
                                                             out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_up2_rectangle(IntPtr pyramid,
                                                                       uint pyramidRate,
                                                                       IntPtr rect,
                                                                       uint levels,
                                                                       out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_down(IntPtr pyramid,
                                                              uint pyramidRate,
                                                              IntPtr rect,
                                                              out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_down_rectangle(IntPtr pyramid,
                                                                        uint pyramidRate,
                                                                        IntPtr rect,
                                                                        out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_down2(IntPtr pyramid,
                                                               uint pyramidRate,
                                                               IntPtr rect,
                                                               uint levels,
                                                               out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType pyramid_down_rect_down2_rectangle(IntPtr pyramid,
                                                                         uint pyramidRate,
                                                                         IntPtr rect,
                                                                         uint levels,
                                                                         out IntPtr ret);

    }

}