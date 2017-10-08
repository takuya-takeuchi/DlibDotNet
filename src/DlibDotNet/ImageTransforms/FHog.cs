using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void ExtracFHogFeatures(Array2DBase inImage, Array2DMatrixBase hogImage, int cellSize = 8, int filterRowsPadding = 1, int filterColsPadding = 1)
        {
            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (hogImage == null)
                throw new ArgumentNullException(nameof(hogImage));
            if (!(cellSize > 0))
                throw new ArgumentOutOfRangeException(nameof(cellSize));
            if (!(filterRowsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterRowsPadding));
            if (!(filterColsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterColsPadding));

            inImage.ThrowIfDisposed(nameof(inImage));
            hogImage.ThrowIfDisposed(nameof(hogImage));

            var inType = inImage.ImageType.ToNativeArray2DType();
            var outType = hogImage.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.extract_fhog_features(inType, inImage.NativePtr, outType, hogImage.NativePtr, cellSize, filterRowsPadding, filterColsPadding);
            switch (ret)
            {
                case Native.ErrorType.OutputElementTypeNotSupport:
                    throw new ArgumentException($"Output {outType} is not supported.");
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inImage.ImageType} is not supported.");
            }
        }

        public static Matrix<byte> DrawHog(Array2DMatrixBase hogImage, int cellDrawSize = 15, float minResponseThreshold = 0.0f)
        {
            if (hogImage == null)
                throw new ArgumentNullException(nameof(hogImage));
            if (!(cellDrawSize > 0))
                throw new ArgumentOutOfRangeException(nameof(cellDrawSize));

            hogImage.ThrowIfDisposed(nameof(hogImage));
            var inType = hogImage.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.draw_fhog(inType, hogImage.NativePtr, cellDrawSize, minResponseThreshold, out var outMatrix);
            switch (ret)
            {
                case Native.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }

            return new Matrix<byte>(outMatrix, MatrixElementTypes.UInt8);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_fhog_features(Array2DType img_type,
                                                                IntPtr img,
                                                                MatrixElementType hog_type,
                                                                IntPtr hog,
                                                                int cell_size,
                                                                int filter_rows_padding,
                                                                int filter_cols_padding);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_fhog(MatrixElementType img_type,
                                                     IntPtr hog,
                                                     int cell_draw_size,
                                                     float min_response_threshold,
                                                     out IntPtr out_matrix);

        }
  

    }

}