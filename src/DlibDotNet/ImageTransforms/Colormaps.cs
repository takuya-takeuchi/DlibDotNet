using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static RgbPixel ColormapHeat(double value, double minVal, double maxVal)
        {
            var pixel = new RgbPixel();
            NativeMethods.colormap_heat(value, minVal, maxVal, ref pixel);
            return pixel;
        }

        public static RgbPixel ColormapJet(double value, double minVal, double maxVal)
        {
            var pixel = new RgbPixel();
            NativeMethods.colormap_jet(value, minVal, maxVal, ref pixel);
            return pixel;
        }

        public static MatrixOp Heatmap(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = NativeMethods.heatmap(array2DType, image.NativePtr, out var matrix);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpHeatmap, image.ImageType, matrix);
        }

        public static MatrixOp Heatmap(MatrixBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.heatmap_matrix(matrixElementType,
                                            image.NativePtr,
                                            image.TemplateRows,
                                            image.TemplateColumns,
                                            out var matrix);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        public static MatrixOp Heatmap(Array2DBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = NativeMethods.heatmap2(array2DType, image.NativePtr, maxValue, minValue, out var matrix);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpHeatmap, image.ImageType, matrix);
        }

        public static MatrixOp Heatmap(MatrixBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.heatmap2_matrix(matrixElementType,
                                             image.NativePtr,
                                             image.TemplateRows,
                                             image.TemplateColumns,
                                             maxValue,
                                             minValue,
                                             out var matrix);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        public static MatrixOp Jet(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = NativeMethods.jet(array2DType, image.NativePtr, out var matrix);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpJet, image.ImageType, matrix);
        }
        
        public static MatrixOp Jet(MatrixBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.jet_matrix(matrixElementType,
                                        image.NativePtr,
                                        image.TemplateRows,
                                        image.TemplateColumns,
                                        out var matrix);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        public static MatrixOp Jet(Array2DBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = NativeMethods.jet2(array2DType, image.NativePtr, maxValue, minValue, out var matrix);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpJet, image.ImageType, matrix);
        }

        public static MatrixOp Jet(MatrixBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.jet2_matrix(matrixElementType,
                                         image.NativePtr,
                                         image.TemplateRows,
                                         image.TemplateColumns,
                                         maxValue,
                                         minValue,
                                         out var matrix);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(NativeMethods.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        #endregion

    }

}