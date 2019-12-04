using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr wrap_string_char(byte[] str, int strLength, uint firstPad = 0, uint restPad = 0, uint maxPerLine = 79);

    }

}