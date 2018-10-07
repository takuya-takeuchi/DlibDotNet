using System;
using System.Runtime.InteropServices;
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
            Native.colormap_heat(value, minVal, maxVal, ref pixel);
            return pixel;
        }

        public static RgbPixel ColormapJet(double value, double minVal, double maxVal)
        {
            var pixel = new RgbPixel();
            Native.colormap_jet(value, minVal, maxVal, ref pixel);
            return pixel;
        }

        public static MatrixOp Heatmap(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = Native.heatmap(array2DType, image.NativePtr, out var matrix);
            if (ret == Native.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(Native.ElementType.OpHeatmap, image.ImageType, matrix);
        }

        public static MatrixOp Heatmap(MatrixBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.heatmap_matrix(matrixElementType,
                                            image.NativePtr,
                                            image.TemplateRows,
                                            image.TemplateColumns,
                                            out var matrix);
            if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(Native.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        public static MatrixOp Heatmap(Array2DBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = Native.heatmap2(array2DType, image.NativePtr, maxValue, minValue, out var matrix);
            if (ret == Native.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(Native.ElementType.OpHeatmap, image.ImageType, matrix);
        }

        public static MatrixOp Heatmap(MatrixBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.heatmap2_matrix(matrixElementType,
                                             image.NativePtr,
                                             image.TemplateRows,
                                             image.TemplateColumns,
                                             maxValue,
                                             minValue,
                                             out var matrix);
            if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(Native.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        public static MatrixOp Jet(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = Native.jet(array2DType, image.NativePtr, out var matrix);
            if (ret == Native.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(Native.ElementType.OpJet, image.ImageType, matrix);
        }
        
        public static MatrixOp Jet(MatrixBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.jet_matrix(matrixElementType,
                                        image.NativePtr,
                                        image.TemplateRows,
                                        image.TemplateColumns,
                                        out var matrix);
            if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(Native.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        public static MatrixOp Jet(Array2DBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var array2DType = image.ImageType.ToNativeArray2DType();

            var ret = Native.jet2(array2DType, image.NativePtr, maxValue, minValue, out var matrix);
            if (ret == Native.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return new MatrixOp(Native.ElementType.OpJet, image.ImageType, matrix);
        }

        public static MatrixOp Jet(MatrixBase image, double maxValue, double minValue = 0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var matrixElementType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.jet2_matrix(matrixElementType,
                                         image.NativePtr,
                                         image.TemplateRows,
                                         image.TemplateColumns,
                                         maxValue,
                                         minValue,
                                         out var matrix);
            if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{image.MatrixElementType} is not supported.");

            return new MatrixOp(Native.ElementType.OpJet, image.MatrixElementType, matrix);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType heatmap(Array2DType type, IntPtr img, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType heatmap2(Array2DType type, IntPtr img, double maxVal, double minVal, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType heatmap_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType heatmap2_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, double maxVal, double minVal, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType jet(Array2DType type, IntPtr img, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType jet2(Array2DType type, IntPtr img, double maxVal, double minVal, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType jet_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType jet2_matrix(MatrixElementType type, IntPtr img, int templateRow, int templateColumn, double maxVal, double minVal, out IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void colormap_heat(double value, double min_val, double max_val, ref RgbPixel pixel);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void colormap_jet(double value, double min_val, double max_val, ref RgbPixel pixel);


        }

    }

}