using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static Rectangle GetRect(HoughTransform houghTransform)
        {
            if (houghTransform == null)
                throw new ArgumentNullException(nameof(houghTransform));

            houghTransform.ThrowIfDisposed();

            HoughTransform.Native.hough_transform_get_rect(houghTransform.NativePtr, out var rect);
            return new Rectangle(rect);
        }

        public static Array2D<T> LoadBmp<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_bmp(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadDng<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_dng(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadImage<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_image(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadJpeg<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_jpeg(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadPng<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_png(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        #region LoadImageData

        public static Array2D<T> LoadImageData<T>(byte[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.UInt8.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.UInt8} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(ushort[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.UInt16.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.UInt16} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(short[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.Int16.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Int16} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(int[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.Int32.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Int32} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(float[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.Float.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Float} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(double[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.Double.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Double} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(RgbPixel[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.RgbPixel.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.RgbPixel} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(RgbAlphaPixel[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.RgbAlphaPixel.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.RgbAlphaPixel} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(HsiPixel[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.HsiPixel.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = Native.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.HsiPixel} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        #endregion

        public static void SaveBmp(Array2DBase image, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_bmp does not throw excpetion but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_bmp(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void SaveDng(Array2DBase image, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_dng does not throw excpetion but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_dng(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void SaveJpeg(Array2DBase image, string path, int quality = 75)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));
            if (quality < 0)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is less than zero.");
            if (quality > 100)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is greater than 100.");

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_jpeg(array2DType, image.NativePtr, str, quality);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void SavePng(Array2DBase image, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_png(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        #endregion

        internal sealed partial class Native
        {

            internal enum Array2DType
            {

                UInt8 = 0,

                UInt16,

                Int16,

                Int32,

                Float,

                Double,

                RgbPixel,

                RgbAlphaPixel,

                HsiPixel,

                Matrix

            }

            internal enum ElementType
            {

                OpHeatmap,

                OpJet,

                OpArray2DToMat,

                OpTrans

            }

            internal enum MatrixElementType
            {

                UInt8 = 0,

                UInt16,

                UInt32,

                Int8,

                Int16,

                Int32,

                Float,

                Double,

                RgbPixel,

                RgbAlphaPixel,

                HsiPixel

            }

            internal enum VectorElementType
            {

                UInt8 = 0,

                UInt16,

                UInt32,

                Int8,

                Int16,

                Int32,

                Float,

                Double

            }

            internal enum InterpolationTypes
            {

                NearestNeighbor = 0,

                Bilinear,

                Quadratic

            }

            internal enum PointMappingTypes
            {

                Rotator = 0,

                Transform,

                TransformAffine,

                TransformProjective

            }

            internal enum MlpKernelType
            {

                Kernel1 = 0

            }

            internal enum RunningStatsType
            {

                Float = 0,

                Double

            }

            internal enum ErrorType
            {

                OK = 0,

                ArrayTypeNotSupport = -1,

                InputArrayTypeNotSupport = -2,

                OutputArrayTypeNotSupport = -3,

                ElementTypeNotSupport = -4,

                InputElementTypeNotSupport = -5,

                OutputElementTypeNotSupport = -6,

                MatrixElementTypeNotSupport = -7,

                //InputOutputArrayNotSameSize = -8,

                //InputOutputMatrixNotSameSize = -9

                MlpKernelNotSupport = -8,

                RunningStatsTypeNotSupport = -9

            }

            #region array

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_new(Array2DType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_new1(Array2DType type, uint newSize);

            #region array2d

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_array2d_new(Array2DType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_array2d_new1(Array2DType type, uint newSize);

            #endregion

            #region matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_matrix_new(MatrixElementType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_matrix_new1(MatrixElementType type, uint newSize);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array_delete(IntPtr point);

            #endregion

            #region array2d

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_new(Array2DType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_new1(Array2DType type, int rows, int cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_delete(Array2DType type, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_nc(Array2DType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_nr(Array2DType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_size(Array2DType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType rectangle_get_rect(Array2DType type, IntPtr array, out IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType array2d_row(Array2DType type, IntPtr array, int row, out IntPtr ret);

            #region array2d_get_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_uint8_t(IntPtr row, int column, out byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_uint16_t(IntPtr row, int column, out ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_int16_t(IntPtr row, int column, out short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_int32_t(IntPtr row, int column, out int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_double(IntPtr row, int column, out double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_float(IntPtr row, int column, out float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_rgb_pixel(IntPtr row, int column, out RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_rgb_alpha_pixel(IntPtr row, int column, out RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_hsi_pixel(IntPtr row, int column, out HsiPixel value);

            #endregion

            #region array2d_set_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_uint8_t(IntPtr row, int column, byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_uint16_t(IntPtr row, int column, ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_int16_t(IntPtr row, int column, short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_int32_t(IntPtr row, int column, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_double(IntPtr row, int column, double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_float(IntPtr row, int column, float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_rgb_pixel(IntPtr row, int column, RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_rgb_alpha_pixel(IntPtr row, int column, RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_hsi_pixel(IntPtr row, int column, HsiPixel value);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_row_delete(Array2DType type, IntPtr row);

            #region array2d_matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_matrix_new(MatrixElementType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_matrix_new1(MatrixElementType type, int rows, int cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_delete(MatrixElementType type, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_matrix_nc(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_matrix_nr(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_matrix_size(MatrixElementType type, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType rectangle_get_rect2(MatrixElementType type, IntPtr matrix, out IntPtr rect);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType array2d_matrix_row(MatrixElementType type, IntPtr array, int row, out IntPtr ret);

            #region array2d_matrix_get_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_uint8_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_uint16_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_uint32_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_int8_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_int16_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_int32_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_double(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_float(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_rgb_pixel(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_rgb_alpha_pixel(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_hsi_pixel(IntPtr row, int column, out IntPtr value);

            #endregion

            #region array2d_matrix_set_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_uint8_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_uint16_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_uint32_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_int8_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_int16_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_int32_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_double(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_float(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_rgb_pixel(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_rgb_alpha_pixel(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_hsi_pixel(IntPtr row, int column, IntPtr value);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_row_delete(MatrixElementType type, IntPtr row);

            #region array2d_fhog_matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_fhog_matrix_new(MatrixElementType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_fhog_matrix_new1(MatrixElementType type, int rows, int cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_fhog_matrix_delete(MatrixElementType type, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_fhog_matrix_nc(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_fhog_matrix_nr(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_fhog_matrix_size(MatrixElementType type, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType array2d_fhog_matrix_get_rect2(MatrixElementType type, IntPtr matrix, out IntPtr rect);

            #endregion

            #endregion

            #region load_bmp

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_bmp(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_dng

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_dng(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_image

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_image(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_jpeg

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_jpeg(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_png

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_png(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region save_bmp

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_bmp(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region save_dng

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_dng(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region save_jpeg

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_jpeg(Array2DType type, IntPtr array, byte[] path, int quality);

            #endregion

            #region save_png

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_png(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new(MatrixElementType matrixElementType);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new1(MatrixElementType matrixElementType, int row, int column);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_delete(MatrixElementType matrixElementType, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_nc(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_nr(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, byte[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, ushort[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, uint[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, sbyte[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, short[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, float[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, double[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbAlphaPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, HsiPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_left_shift(MatrixElementType type, IntPtr matrix, IntPtr ofstream);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_size(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            #region matrix_operator_get_one_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_uint8_t(IntPtr matrix, int index, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_uint16_t(IntPtr matrix, int index, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_uint32_t(IntPtr matrix, int index, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_int8_t(IntPtr matrix, int index, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_int16_t(IntPtr matrix, int index, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_int32_t(IntPtr matrix, int index, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_double(IntPtr matrix, int index, out double ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_float(IntPtr matrix, int index, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_rgb_pixel(IntPtr matrix, int index, out RgbPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_rgb_alpha_pixel(IntPtr matrix, int index, out RgbAlphaPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_hsi_pixel(IntPtr matrix, int index, out HsiPixel ret);

            #endregion

            #region matrix_operator_set_one_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_uint8_t(IntPtr matrix, int index, byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_uint16_t(IntPtr matrix, int index, ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_uint32_t(IntPtr matrix, int index, uint value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_int8_t(IntPtr matrix, int index, sbyte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_int16_t(IntPtr matrix, int index, short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_int32_t(IntPtr matrix, int index, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_double(IntPtr matrix, int index, double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_float(IntPtr matrix, int index, float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_rgb_pixel(IntPtr matrix, int index, RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_rgb_alpha_pixel(IntPtr matrix, int index, RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_hsi_pixel(IntPtr matrix, int index, HsiPixel value);

            #endregion

            #region matrix_operator_get_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_uint8_t(IntPtr matrix, int row, int column, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_uint16_t(IntPtr matrix, int row, int column, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_uint32_t(IntPtr matrix, int row, int column, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_int8_t(IntPtr matrix, int row, int column, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_int16_t(IntPtr matrix, int row, int column, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_int32_t(IntPtr matrix, int row, int column, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_double(IntPtr matrix, int row, int column, out double ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_float(IntPtr matrix, int row, int column, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_rgb_pixel(IntPtr matrix, int row, int column, out RgbPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_rgb_alpha_pixel(IntPtr matrix, int row, int column, out RgbAlphaPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_hsi_pixel(IntPtr matrix, int row, int column, out HsiPixel ret);

            #endregion

            #region matrix_operator_set_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_uint8_t(IntPtr matrix, int row, int column, byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_uint16_t(IntPtr matrix, int row, int column, ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_uint32_t(IntPtr matrix, int row, int column, uint value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_int8_t(IntPtr matrix, int row, int column, sbyte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_int16_t(IntPtr matrix, int row, int column, short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_int32_t(IntPtr matrix, int row, int column, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_double(IntPtr matrix, int row, int column, double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_float(IntPtr matrix, int row, int column, float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_rgb_pixel(IntPtr matrix, int row, int column, RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_rgb_alpha_pixel(IntPtr matrix, int row, int column, RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_hsi_pixel(IntPtr matrix, int row, int column, HsiPixel value);

            #endregion

            #endregion

            #region matrix_range_exp

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct matrix_range_exp_create_param
            {
                // uint8_t
                public uint8_t uint8_t_start;
                public uint8_t uint8_t_inc;
                public uint8_t uint8_t_end;
                public bool use_uint8_t_inc;

                // uint16_t
                public uint16_t uint16_t_start;
                public uint16_t uint16_t_inc;
                public uint16_t uint16_t_end;
                bool use_uint16_t_inc;

                // int8_t
                public int8_t int8_t_start;
                public int8_t int8_t_inc;
                public int8_t int8_t_end;
                bool use_int8_t_inc;

                // int16_t
                public int16_t int16_t_start;
                public int16_t int16_t_inc;
                public int16_t int16_t_end;
                bool use_int16_t_inc;

                // int32_t
                public int32_t int32_t_start;
                public int32_t int32_t_inc;
                public int32_t int32_t_end;
                public bool use_int32_t_inc;

                // float
                public float float_start;
                public float float_inc;
                public float float_end;
                public bool use_float_inc;

                // double
                public double double_start;
                public double double_inc;
                public double double_end;
                public bool use_double_inc;

                public bool use_num;
                public int num;
            }

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_range_exp_create(MatrixElementType matrixElementType, ref matrix_range_exp_create_param param);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_range_exp_delete(IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_range_exp_nc(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_range_exp_nr(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            #endregion

            #region mlp

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr mlp_kernel_new(MlpKernelType kernel_type, int nodes_in_input_layer, int nodes_in_first_hidden_layer, int nodes_in_second_hidden_layer, int nodes_in_output_layer, double alpha, double momentum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mlp_kernel_train(MlpKernelType kernel_type, IntPtr kernel, MatrixElementType matrixElementType, IntPtr example_in, double example_out);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mlp_kernel_operator(MlpKernelType kernel_type, IntPtr kernel, MatrixElementType type, IntPtr data, out IntPtr ret_mat);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void mlp_kernel_delete(MlpKernelType kernel_type, IntPtr kernel);

            #endregion

            #region running_stats

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr running_stats_new(RunningStatsType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref float val);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref double val);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_clear(RunningStatsType type, IntPtr stats);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out float n);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out double n);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out float ex_kurtosis);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out double ex_kurtosis);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out float max);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out double max);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out float mean);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out double mean);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out float min);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out double min);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref float scale, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref double scale, out double ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out float skewness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out double skewness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out float stddev);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out double stddev);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out float variance);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out double variance);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void running_stats_delete(RunningStatsType type, IntPtr stats);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr running_stats_operator_add(RunningStatsType type, IntPtr left, IntPtr right);

            #endregion

            #region vector

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new(VectorElementType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_uint8_t(byte x, byte y, byte z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_uint16_t(ushort x, ushort y, ushort z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_uint32_t(uint x, uint y, uint z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_int8_t(sbyte x, sbyte y, sbyte z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_int16_t(short x, short y, short z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_int32_t(int x, int y, int z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_float(float x, float y, float z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_new1_double(double x, double y, double z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_delete(VectorElementType type, IntPtr vector);

            #region vector_get_xyz

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_uint8_t(IntPtr vector, out byte x, out byte y, out byte z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_uint16_t(IntPtr vector, out ushort x, out ushort y, out ushort z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_uint32_t(IntPtr vector, out uint x, out uint y, out uint z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_int8_t(IntPtr vector, out sbyte x, out sbyte y, out sbyte z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_int16_t(IntPtr vector, out short x, out short y, out short z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_int32_t(IntPtr vector, out int x, out int y, out int z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_float(IntPtr vector, out float x, out float y, out float z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_get_xyz_double(IntPtr vector, out double x, out double y, out double z);

            #endregion

            #region vector_set_xyz

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_uint8_t(IntPtr vector, byte x, byte y, byte z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_uint16_t(IntPtr vector, ushort x, ushort y, ushort z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_uint32_t(IntPtr vector, uint x, uint y, uint z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_int8_t(IntPtr vector, sbyte x, sbyte y, sbyte z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_int16_t(IntPtr vector, short x, short y, short z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_int32_t(IntPtr vector, int x, int y, int z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_float(IntPtr vector, float x, float y, float z);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_set_xyz_double(IntPtr vector, double x, double y, double z);

            #endregion

            #region vector_operator_add

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_uint8_t(IntPtr left, IntPtr right, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_uint16_t(IntPtr left, IntPtr right, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_uint32_t(IntPtr left, IntPtr right, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_int8_t(IntPtr left, IntPtr right, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_int16_t(IntPtr left, IntPtr right, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_int32_t(IntPtr left, IntPtr right, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_float(IntPtr left, IntPtr right, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_add_double(IntPtr left, IntPtr right, out IntPtr ret);

            #endregion

            #region vector_operator_div

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_uint8_t(IntPtr vector, byte value, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_uint16_t(IntPtr vector, ushort value, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_uint32_t(IntPtr vector, uint value, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_int8_t(IntPtr vector, sbyte value, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_int16_t(IntPtr vector, short value, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_int32_t(IntPtr vector, int value, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_float(IntPtr vector, float value, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_operator_div_double(IntPtr vector, double value, out IntPtr ret);

            #endregion

            #endregion

            #region vector_matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_matrix_new1(MatrixElementType matrixElementType);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_matrix_new2(MatrixElementType matrixElementType, IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_matrix_new3([In] MatrixElementType matrixElementType, [In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_matrix_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_matrix_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_matrix_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_matrix_delete(MatrixElementType matrixElementType, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_matrix_copy(IntPtr vector, IntPtr[] dst);

            #endregion

            #region  extensions

            #region extensions_load_image_data

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, byte[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, ushort[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, uint[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, sbyte[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, short[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, int[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, float[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, double[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, RgbPixel[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, RgbAlphaPixel[] data, uint rows, uint columns, uint steps);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, HsiPixel[] data, uint rows, uint columns, uint steps);

            #endregion

            #endregion

        }

    }

}