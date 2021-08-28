#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr proxy_serialize_new(byte[] fileName, int fileNameLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void proxy_serialize_delete(IntPtr serialize);

    }

}
#endif
