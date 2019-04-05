using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double rect_detection_get_detection_confidence(IntPtr detection);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rect_detection_set_detection_confidence(IntPtr detection, double detection_confidence);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rect_detection_get_rect(IntPtr detection);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rect_detection_set_rect(IntPtr detection, IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ulong rect_detection_get_weight_index(IntPtr detection);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rect_detection_set_weight_index(IntPtr detection, ulong weight_index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rect_detection_delete(IntPtr detection);

    }

}