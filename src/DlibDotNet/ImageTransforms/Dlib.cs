#if !LITE
using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        public static void EqualizeHistogram(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            var elementType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.equalize_histogram_array2d(elementType, image.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{elementType} is not supported.");
            }
        }

        public static void EqualizeHistogram<T>(Array2DBase image, out Array2D<T> outImage)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (!Array2D<T>.TryParse<T>(out _))
                throw new NotSupportedException();

            outImage = new Array2D<T>();

            var inType = image.ImageType.ToNativeArray2DType();
            var outType = outImage.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.equalize_histogram_array2d_2(inType,
                                                                 image.NativePtr,
                                                                 outType,
                                                                 outImage.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"Input {inType} or Output {outType} is not supported.");
            }
        }

    }

}
#endif
