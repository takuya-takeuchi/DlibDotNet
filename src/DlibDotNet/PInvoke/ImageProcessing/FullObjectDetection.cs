using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr full_object_detection_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint full_object_detection_num_parts(IntPtr predictor);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr full_object_detection_get_rect(IntPtr predictor);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr full_object_detection_part(IntPtr predictor, uint idx);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void full_object_detection_delete(IntPtr point);

    }

}