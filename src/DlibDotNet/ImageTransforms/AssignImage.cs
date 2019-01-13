using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

using Array2DType = DlibDotNet.NativeMethods.Array2DType;
using ErrorType = DlibDotNet.NativeMethods.ErrorType;
using MatrixElementType = DlibDotNet.NativeMethods.MatrixElementType;

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
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.UInt8, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<ushort> dest, ushort pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.UInt16, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<uint> dest, uint pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.Int32, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<sbyte> dest, sbyte pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.UInt8, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<short> dest, short pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.Int16, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<int> dest, int pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.Int32, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<float> dest, float pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.Float, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<double> dest, double pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.Double, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<RgbPixel> dest, RgbPixel pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.RgbPixel, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<RgbAlphaPixel> dest, RgbAlphaPixel pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.RgbAlphaPixel, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void AssignAllPpixels(Array2D<HsiPixel> dest, HsiPixel pixel)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            dest.ThrowIfDisposed(nameof(dest));

            var outType = dest.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.assign_all_pixels(outType, dest.NativePtr, Array2DType.HsiPixel, ref pixel);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
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
            var ret = NativeMethods.assign_image(outType, dest.NativePtr, inType, src.NativePtr);
            switch (ret)
            {
                case ErrorType.Array2DTypeTypeNotSupport:
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
            var ret = NativeMethods.assign_image_matrix(outType, dest.NativePtr, inType, src.NativePtr);
            switch (ret)
            {
                case ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{outType} is not supported.");
            }
        }

        #endregion
        

    }

}