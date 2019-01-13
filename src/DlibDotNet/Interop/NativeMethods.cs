using System;
using System.Runtime.InteropServices;
using System.Security;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if LINUX
        public const string CLibrary = "glibc.so";
#elif MAC
        public const string CLibrary = "glibc.so";
#else
        public const string CLibrary = "msvcrt.dll";
#endif

        [SuppressUnmanagedCodeSecurity]
        [DllImport(CLibrary, EntryPoint = "memcpy", CallingConvention = CallingConvention)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);

    }
}
