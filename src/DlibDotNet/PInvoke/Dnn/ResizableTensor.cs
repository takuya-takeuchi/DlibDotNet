#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr resizable_tensor_new();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void resizable_tensor_delete(IntPtr tensor);

    }

}
#endif
