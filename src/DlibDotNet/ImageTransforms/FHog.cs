using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static Matrix<byte> DrawFHog(Array2DMatrixBase hogImage, int cellDrawSize = 15, float minResponseThreshold = 0.0f)
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

            return new Matrix<byte>(outMatrix);
        }

        public static Matrix<byte> DrawFHog<T>(ObjectDetector<T> hogImage, uint weightIndex = 0,int cellDrawSize = 15)
            where T : ImageScanner
        {
            if (hogImage == null)
                throw new ArgumentNullException(nameof(hogImage));
            //// make sure requires clause is not broken
            //DLIB_ASSERT(weight_index < detector.num_detectors(),
            //    "\t matrix draw_fhog()"
            //    << "\n\t Invalid arguments were given to this function. "
            //    << "\n\t weight_index:             " << weight_index
            //    << "\n\t detector.num_detectors(): " << detector.num_detectors()
            //);
            //DLIB_ASSERT(cell_draw_size > 0 && detector.get_w(weight_index).size() >= detector.get_scanner().get_num_dimensions(),
            //    "\t matrix draw_fhog()"
            //    << "\n\t Invalid arguments were given to this function. "
            //    << "\n\t cell_draw_size:                              " << cell_draw_size
            //    << "\n\t weight_index:                                " << weight_index
            //    << "\n\t detector.get_w(weight_index).size():         " << detector.get_w(weight_index).size()
            //    << "\n\t detector.get_scanner().get_num_dimensions(): " << detector.get_scanner().get_num_dimensions()
            //);

            hogImage.ThrowIfDisposed();

            return hogImage.DrawFHog(weightIndex, cellDrawSize);
        }

        public static Array2DMatrix<T> ExtracFHogFeatures<T>(Array2DBase inImage, int cellSize = 8, int filterRowsPadding = 1, int filterColsPadding = 1)
            where T : struct
        {
            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (!(cellSize > 0))
                throw new ArgumentOutOfRangeException(nameof(cellSize));
            if (!(filterRowsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterRowsPadding));
            if (!(filterColsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterColsPadding));

            inImage.ThrowIfDisposed(nameof(inImage));

            //var hogImage = new FHogArray2DMatrix<T>();
            var hogImage = new Array2DMatrix<T>(0, 0, 31, 1);

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

            return hogImage;
        }

        public static Array<Array2D<T>> ExtracFHogFeaturesArray<T>(Array2DBase inImage, int cellSize = 8, int filterRowsPadding = 1, int filterColsPadding = 1)
            where T : struct
        {
            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (!(cellSize > 0))
                throw new ArgumentOutOfRangeException(nameof(cellSize));
            if (!(filterRowsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterRowsPadding));
            if (!(filterColsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterColsPadding));

            inImage.ThrowIfDisposed(nameof(inImage));

            var hogImage = new Array<Array2D<T>>();
            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var inType = inImage.ImageType.ToNativeArray2DType();
            var outType = type.ToNativeArray2DType();
            var ret = Native.extract_fhog_features_array(inType, inImage.NativePtr, outType, hogImage.NativePtr, cellSize, filterRowsPadding, filterColsPadding);
            switch (ret)
            {
                case Native.ErrorType.OutputElementTypeNotSupport:
                    throw new ArgumentException($"Output {outType} is not supported.");
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inImage.ImageType} is not supported.");
            }

            return hogImage;
        }

        public static Point ImgaeToFHog(Point point, int cellSize = 8, int filterRowsPadding = 1, int filterColsPadding = 1)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));
            if (!(cellSize > 0))
                throw new ArgumentOutOfRangeException(nameof(cellSize));
            if (!(filterRowsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterRowsPadding));
            if (!(filterColsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterColsPadding));

            using (var native = point.ToNative())
            {
                var ret = Native.image_to_fhog(native.NativePtr, cellSize, filterRowsPadding, filterColsPadding);
                return new Point(ret);
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_fhog(MatrixElementType img_type,
                                                     IntPtr hog,
                                                     int cell_draw_size,
                                                     float min_response_threshold,
                                                     out IntPtr out_matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_fhog_object_detector_scan_fhog_pyramid(PyramidType pyramid_type,
                                                                                       uint pyramid_rate,
                                                                                       FHogFeatureExtractorType extractor_type,
                                                                                       IntPtr obj,
                                                                                       uint weightIndex,
                                                                                       int cellDrawSize,
                                                                                       out IntPtr out_matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_fhog_features(Array2DType img_type,
                                                                 IntPtr img,
                                                                 MatrixElementType hog_type,
                                                                 IntPtr hog,
                                                                 int cell_size,
                                                                 int filter_rows_padding,
                                                                 int filter_cols_padding);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_fhog_features_array(Array2DType img_type,
                                                                       IntPtr img,
                                                                       Array2DType hog_type,
                                                                       IntPtr hog,
                                                                       int cell_size,
                                                                       int filter_rows_padding,
                                                                       int filter_cols_padding);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_to_fhog(IntPtr p,
                                                      int cell_size,
                                                      int filter_rows_padding,
                                                      int filter_cols_padding);

        }
        
    }

}