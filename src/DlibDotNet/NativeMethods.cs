using System.Runtime.InteropServices;

namespace DlibDotNet
{

    internal static class NativeMethods
    {

#if LINUX
        public const string NativeLibrary = "libDlibDotNet.Native.so";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#elif MAC
        public const string NativeLibrary = "libDlibDotNet.Native.dylib";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#else
        public const string NativeLibrary = "DlibDotNet.Native.dll";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#endif

    }
}
