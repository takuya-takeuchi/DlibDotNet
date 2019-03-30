using System;
using System.IO;
using System.Linq;
using System.Text;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Provides the methods of dlib.
    /// </summary>
    public static partial class Dlib
    {

        public static void EqualizeHistogram<T>(Array2D<T> image)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (!Array2D<T>.TryParse<T>(out _))
                throw new NotSupportedException();

            var elementType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.equalize_histogram_array2d(elementType, image.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{elementType} is not supported.");
            }
        }

        public static void EqualizeHistogram<T, U>(Array2D<T> image, out Array2D<U> outImage)
            where T : struct
            where U : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (!Array2D<T>.TryParse<T>(out _))
                throw new NotSupportedException();
            if (!Array2D<U>.TryParse<U>(out _))
                throw new NotSupportedException();

            outImage = new Array2D<U>();

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