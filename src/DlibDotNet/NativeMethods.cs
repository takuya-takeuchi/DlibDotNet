using System.Runtime.InteropServices;

namespace DlibDotNet
{

    internal static partial class NativeMethods
    {

#if LINUX
        public const string NativeLibrary = "libDlibDotNet.Native.so";

        public const string NativeDnnLibrary = "libDlibDotNet.Native.Dnn.so";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#elif MAC
        public const string NativeLibrary = "libDlibDotNet.Native.dylib";
    
        public const string NativeDnnLibrary = "libDlibDotNet.Native.Dnn.dylib";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#else
        public const string NativeLibrary = "DlibDotNet.Native.dll";

        public const string NativeDnnLibrary = "DlibDotNet.Native.Dnn.dll";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#endif

    }
}
