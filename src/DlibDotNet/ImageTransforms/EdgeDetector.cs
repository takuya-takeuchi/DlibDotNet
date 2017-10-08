using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void SobelEdgeDetector(Array2DBase image, Array2DBase horizontalGradient, Array2DBase verticalGradient)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (verticalGradient == null)
                throw new ArgumentNullException(nameof(horizontalGradient));
            if (horizontalGradient == null)
                throw new ArgumentNullException(nameof(verticalGradient));

            if (horizontalGradient == verticalGradient)
                throw new ArgumentException();

            var inType = image.ImageType.ToNativeArray2DType();
            var horzType = horizontalGradient.ImageType.ToNativeArray2DType();
            var vertType = verticalGradient.ImageType.ToNativeArray2DType();

            if (horzType != vertType)
                throw new ArgumentException($"{nameof(horizontalGradient)}.ImageType == {nameof(verticalGradient)}.ImageType");

            var ret = Native.sobel_edge_detector(inType, image.NativePtr, horzType, horizontalGradient.NativePtr, verticalGradient.NativePtr);
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"{nameof(horizontalGradient)} or {nameof(verticalGradient)} should be signed grayscale image.");
            }
        }

        public static void SuppressNonMaximumEdges(Array2DBase horizontalGradient, Array2DBase verticalGradient, Array2DBase outImage)
        {
            if (verticalGradient == null)
                throw new ArgumentNullException(nameof(horizontalGradient));
            if (horizontalGradient == null)
                throw new ArgumentNullException(nameof(verticalGradient));
            if (outImage == null)
                throw new ArgumentNullException(nameof(outImage));

            if (horizontalGradient.Columns != verticalGradient.Columns || horizontalGradient.Rows != verticalGradient.Rows)
                throw new ArgumentException();

            var horzType = horizontalGradient.ImageType.ToNativeArray2DType();
            var vertType = verticalGradient.ImageType.ToNativeArray2DType();
            var outType = outImage.ImageType.ToNativeArray2DType();

            if (horzType != vertType)
                throw new ArgumentException($"{nameof(horizontalGradient)}.ImageType == {nameof(verticalGradient)}.ImageType");

            var ret = Native.suppress_non_maximum_edges(horzType, horizontalGradient.NativePtr, verticalGradient.NativePtr, outType, outImage.NativePtr);
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"{nameof(horizontalGradient)} or {nameof(verticalGradient)} should be signed grayscale image.");
                case Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"{outImage.ImageType} is not supported.");
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType sobel_edge_detector(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr horz, IntPtr vert);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType suppress_non_maximum_edges(Array2DType inType, IntPtr horz, IntPtr vert, Array2DType outType, IntPtr outImg);

        }

    }

}