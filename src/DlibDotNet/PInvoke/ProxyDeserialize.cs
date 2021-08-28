#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr proxy_deserialize_new(byte[] fileName, int fileNameLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void proxy_deserialize_delete(IntPtr deserialize);

    }

}
#endif
