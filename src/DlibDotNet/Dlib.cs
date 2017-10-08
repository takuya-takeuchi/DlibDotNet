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

                OpJet

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

            internal enum ErrorType
            {

                OK = 0,

                ArrayTypeNotSupport = -1,

                InputArrayTypeNotSupport = -2,

                OutputArrayTypeNotSupport = -3,

                ElementTypeNotSupport = -4,

                InputElementTypeNotSupport = -5,

                OutputElementTypeNotSupport = -6,

                MatrixElementTypeNotSupport = -7

                //InputOutputArrayNotSameSize = -8,

                //InputOutputMatrixNotSameSize = -9

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

            #endregion

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
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, byte[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, ushort[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, uint[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, sbyte[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, short[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, float[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, double[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbPixel[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbAlphaPixel[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, HsiPixel[] array, int arrayNum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_size(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

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

        }

    }

}
