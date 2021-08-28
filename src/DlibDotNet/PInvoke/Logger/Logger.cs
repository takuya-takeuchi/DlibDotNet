#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr logger_new(byte[] name, int nameLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void logger_delete(IntPtr logger);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void logger_set_level(IntPtr logger, LogLevel log_level);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void logger_operator_left_shift(IntPtr logger, LogLevel log_level, byte[] message, int messageLength);

    }

}
#endif
