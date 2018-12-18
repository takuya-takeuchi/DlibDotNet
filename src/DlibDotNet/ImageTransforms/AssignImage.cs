﻿using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        #region AssignAllPpixels

        public static void AssignAllPpixels(Array2D<byte> dest, byte pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.UInt8, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<ushort> dest, ushort pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.UInt16, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<uint> dest, uint pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.Int32, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<sbyte> dest, sbyte pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.UInt8, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<short> dest, short pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.Int16, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<int> dest, int pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.Int32, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<float> dest, float pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.Float, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<double> dest, double pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.Double, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<RgbPixel> dest, RgbPixel pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.RgbPixel, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<RgbAlphaPixel> dest, RgbAlphaPixel pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.RgbAlphaPixel, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<HsiPixel> dest, HsiPixel pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_all_pixels(outType, dest.NativePtr, Native.Array2DType.HsiPixel, ref pixel);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        #endregion

        public static void AssignImage(Array2DBase src, Array2DBase dest)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            src.ThrowIfDisposed(nameof(src));
            dest.ThrowIfDisposed(nameof(dest));

            var inType = src.ImageType.ToNativeArray2DType();
            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = Native.assign_image(outType, dest.NativePtr, inType, src.NativePtr);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignImage(MatrixBase src, MatrixBase dest)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            src.ThrowIfDisposed(nameof(src));
            dest.ThrowIfDisposed(nameof(dest));

            var inType = src.MatrixElementType.ToNativeMatrixElementType();
            var outType = dest.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.assign_image_matrix(outType, dest.NativePtr, inType, src.NativePtr);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{outType} is not supported.");
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref byte color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref ushort color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref uint color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref sbyte color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref short color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref int color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref float color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref double color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref RgbPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref RgbAlphaPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref HsiPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_image(Array2DType out_type,
                                                        IntPtr out_img,
                                                        Array2DType in_type,
                                                        IntPtr in_img);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType assign_image_matrix(MatrixElementType out_type,
                                                               IntPtr out_img,
                                                               MatrixElementType in_type,
                                                               IntPtr in_img);

        }

    }

}