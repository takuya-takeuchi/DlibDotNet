using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr custom_multithreaded_object_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void custom_multithreaded_object_delete(IntPtr thread);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void custom_multithreaded_object_register_thread(IntPtr thread, IntPtr mediator);

    }

}