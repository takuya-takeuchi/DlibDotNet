#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void multithreaded_object_pause(IntPtr thread);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void multithreaded_object_start(IntPtr thread);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void multithreaded_object_stop(IntPtr thread);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void multithreaded_object_wait(IntPtr thread);

    }

}
#endif
