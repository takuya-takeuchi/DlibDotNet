using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr custom_logger_new(IntPtr logFunc);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void custom_logger_delete(IntPtr logger);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void custom_set_all_logging_output_hooks(IntPtr logger);

    }

}