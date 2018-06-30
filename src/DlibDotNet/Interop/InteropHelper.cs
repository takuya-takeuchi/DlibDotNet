using System;

namespace DlibDotNet.Interop
{

    internal static class InteropHelper
    {

        public static unsafe void Copy(IntPtr ptrSource, uint[] dest, uint elements)
        {
            fixed (uint* ptrDest = &dest[0])
                NativeMethods.memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(uint)));
        }

        public static unsafe void Copy(IntPtr ptrSource, ushort[] dest, uint elements)
        {
            fixed (ushort* ptrDest = &dest[0])
                NativeMethods.memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(ushort)));
        }

        public static unsafe void Copy(uint[] source, IntPtr ptrDest, uint elements)
        {
            fixed (uint* ptrSource = &source[0])
                NativeMethods.memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(uint)));
        }

        public static unsafe void Copy(ushort[] source, IntPtr ptrDest, uint elements)
        {
            fixed (ushort* ptrSource = &source[0])
                NativeMethods.memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(ushort)));
        }

    }

}
