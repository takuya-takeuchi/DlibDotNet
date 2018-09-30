using System;
using System.Runtime.InteropServices;

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

        public static unsafe void Copy(IntPtr ptrSource, sbyte[] dest, uint elements)
        {
            fixed (sbyte* ptrDest = &dest[0])
                NativeMethods.memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(sbyte)));
        }

        public static unsafe void Copy(IntPtr ptrSource, ulong[] dest, uint elements)
        {
            fixed (ulong* ptrDest = &dest[0])
                NativeMethods.memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(ulong)));
        }

        public static unsafe void Copy(IntPtr ptrSource, RgbPixel[] dest, uint elements)
        {
            fixed (RgbPixel* ptrDest = &dest[0])
                NativeMethods.memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<RgbPixel>()));
        }

        public static unsafe void Copy(IntPtr ptrSource, RgbAlphaPixel[] dest, uint elements)
        {
            fixed (RgbAlphaPixel* ptrDest = &dest[0])
                NativeMethods.memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<RgbAlphaPixel>()));
        }

        public static unsafe void Copy(IntPtr ptrSource, HsiPixel[] dest, uint elements)
        {
            fixed (HsiPixel* ptrDest = &dest[0])
                NativeMethods.memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<HsiPixel>()));
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

        public static unsafe void Copy(sbyte[] source, IntPtr ptrDest, uint elements)
        {
            fixed (sbyte* ptrSource = &source[0])
                NativeMethods.memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(sbyte)));
        }

        public static unsafe void Copy(ulong[] source, IntPtr ptrDest, uint elements)
        {
            fixed (ulong* ptrSource = &source[0])
                NativeMethods.memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(ulong)));
        }

        public static unsafe void Copy(RgbPixel[] source, IntPtr ptrDest, uint elements)
        {
            fixed (RgbPixel* ptrSource = &source[0])
                NativeMethods.memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * Marshal.SizeOf<RgbPixel>()));
        }

        public static unsafe void Copy(RgbAlphaPixel[] source, IntPtr ptrDest, uint elements)
        {
            fixed (RgbAlphaPixel* ptrSource = &source[0])
                NativeMethods.memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * Marshal.SizeOf<RgbAlphaPixel>()));
        }

        public static unsafe void Copy(HsiPixel[] source, IntPtr ptrDest, uint elements)
        {
            fixed (HsiPixel* ptrSource = &source[0])
                NativeMethods.memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * Marshal.SizeOf<HsiPixel>()));
        }

    }

}
