using System;
using System.Runtime.InteropServices;

namespace DlibDotNet.Interop
{

    internal static class InteropHelper
    {

        public static void Copy(IntPtr ptrSource, uint[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, uint[] dest, int startIndex, uint elements)
        {
            fixed (uint* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(uint)));
        }

        public static void Copy(IntPtr ptrSource, ushort[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, ushort[] dest, int startIndex, uint elements)
        {
            fixed (ushort* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(ushort)));
        }

        public static void Copy(IntPtr ptrSource, sbyte[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, sbyte[] dest, int startIndex, uint elements)
        {
            fixed (sbyte* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(sbyte)));
        }

        public static void Copy(IntPtr ptrSource, ulong[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, ulong[] dest, int startIndex, uint elements)
        {
            fixed (ulong* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(ulong)));
        }

        public static void Copy(IntPtr ptrSource, RgbPixel[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, RgbPixel[] dest, int startIndex, uint elements)
        {
            fixed (RgbPixel* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<RgbPixel>()));
        }

        public static void Copy(IntPtr ptrSource, BgrPixel[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, BgrPixel[] dest, int startIndex, uint elements)
        {
            fixed (BgrPixel* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<BgrPixel>()));
        }

        public static void Copy(IntPtr ptrSource, RgbAlphaPixel[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, RgbAlphaPixel[] dest, int startIndex, uint elements)
        {
            fixed (RgbAlphaPixel* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<RgbAlphaPixel>()));
        }

        public static void Copy(IntPtr ptrSource, HsiPixel[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static void Copy(IntPtr ptrSource, LabPixel[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, HsiPixel[] dest, int startIndex, uint elements)
        {
            fixed (HsiPixel* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<HsiPixel>()));
        }

        public static unsafe void Copy(IntPtr ptrSource, LabPixel[] dest, int startIndex, uint elements)
        {
            fixed (LabPixel* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * Marshal.SizeOf<LabPixel>()));
        }

        public static unsafe void Copy(uint[] source, IntPtr ptrDest, uint elements)
        {
            fixed (uint* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(uint)));
        }

        public static unsafe void Copy(ushort[] source, IntPtr ptrDest, uint elements)
        {
            fixed (ushort* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(ushort)));
        }

        public static unsafe void Copy(sbyte[] source, IntPtr ptrDest, uint elements)
        {
            fixed (sbyte* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(sbyte)));
        }

        public static unsafe void Copy(ulong[] source, IntPtr ptrDest, uint elements)
        {
            fixed (ulong* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(ulong)));
        }

        public static unsafe void Copy(RgbPixel[] source, IntPtr ptrDest, uint elements)
        {
            fixed (RgbPixel* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * Marshal.SizeOf<RgbPixel>()));
        }

        public static unsafe void Copy(RgbAlphaPixel[] source, IntPtr ptrDest, uint elements)
        {
            fixed (RgbAlphaPixel* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * Marshal.SizeOf<RgbAlphaPixel>()));
        }

        public static unsafe void Copy(HsiPixel[] source, IntPtr ptrDest, uint elements)
        {
            fixed (HsiPixel* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * Marshal.SizeOf<HsiPixel>()));
        }

        public static unsafe void Copy(LabPixel[] source, IntPtr ptrDest, uint elements)
        {
            fixed (LabPixel* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * Marshal.SizeOf<LabPixel>()));
        }

    }

}
