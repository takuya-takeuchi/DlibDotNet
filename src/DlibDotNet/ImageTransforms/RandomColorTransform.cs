#if !LITE
using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void ApplyRandomColorOffset(MatrixBase image, Rand rand)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (rand == null)
                throw new ArgumentNullException(nameof(rand));

            image.ThrowIfDisposed();
            rand.ThrowIfDisposed();

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.apply_random_color_offset_matrix(matrixElementType,
                                                                     image.NativePtr,
                                                                     image.TemplateRows,
                                                                     image.TemplateColumns,
                                                                     rand.NativePtr);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");
        }

        public static void DisturbColors(MatrixBase image, Rand rand, double gammaMagnitude = 0.5, double colorMagnitude = 0.2)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (rand == null)
                throw new ArgumentNullException(nameof(rand));

            image.ThrowIfDisposed();
            rand.ThrowIfDisposed();

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.disturb_colors_matrix(matrixElementType,
                                                          image.NativePtr,
                                                          image.TemplateRows,
                                                          image.TemplateColumns,
                                                          rand.NativePtr,
                                                          gammaMagnitude,
                                                          colorMagnitude);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");
        }

        #endregion

    }

}
#endif
