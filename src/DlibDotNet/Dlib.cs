using System;
using System.IO;
using System.Linq;
using System.Text;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;

namespace DlibDotNet
{

    /// <summary>
    /// Provides the methods of dlib.
    /// </summary>
    public static partial class Dlib
    {

        #region Methods

        public static string GetNativeVersion()
        {
            return StringHelper.FromStdString(NativeMethods.get_version(), true);
        }

        public static string GetNativeDnnVersion()
        {
            return StringHelper.FromStdString(NativeMethods.dnn_get_version(), true);
        }

        #region AssignPixel

        public static void AssignPixel(ref RgbPixel dest, RgbAlphaPixel src)
        {
            NativeMethods.assign_pixel_rgb_rgbalpha(ref dest, ref src);
        }

        public static void AssignPixel(ref RgbAlphaPixel dest, RgbPixel src)
        {
            NativeMethods.assign_pixel_rgbalpha_rgb(ref dest, ref src);
        }

        public static void AssignPixel(ref RgbPixel dest, HsiPixel src)
        {
            NativeMethods.assign_pixel_rgb_hsi(ref dest, ref src);
        }

        public static void AssignPixel(ref RgbPixel dest, LabPixel src)
        {
            NativeMethods.assign_pixel_rgb_lab(ref dest, ref src);
        }

        public static void AssignPixel(ref RgbAlphaPixel dest, HsiPixel src)
        {
            NativeMethods.assign_pixel_rgbalpha_hsi(ref dest, ref src);
        }

        public static void AssignPixel(ref RgbAlphaPixel dest, LabPixel src)
        {
            NativeMethods.assign_pixel_rgbalpha_lab(ref dest, ref src);
        }

        #endregion

        #region ExtractImage4Points

        /// <summary>
        /// This function extracts an arbitrary quadrilateral patch from an image.
        /// </summary>
        /// <typeparam name="T">The type of element in the image.</typeparam>
        /// <param name="image">The image.</param>
        /// <param name="points">The 4 points on the image.</param>
        /// <param name="width">The width of return image.</param>
        /// <param name="height">The height of return image.</param>
        /// <returns><see cref="Array2D{T}"/>.</returns>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> or <paramref name="points"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><see cref="Array.Length"/> of <paramref name="points"/> must be 4.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> are less than or equal to zero.</exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"><paramref name="image"/> is disposed.</exception>
        public static Array2D<T> ExtractImage4Points<T>(Array2D<T> image,
                                                        DPoint[] points,
                                                        int width,
                                                        int height)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (points == null)
                throw new ArgumentNullException(nameof(points));
            if (points.Length != 4)
                throw new ArgumentOutOfRangeException($"{nameof(points.Length)} of {nameof(points)} must be 4.");
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(width)} or {nameof(height)} are less than or equal to zero.");

            image.ThrowIfDisposed();

            if (!Array2D<T>.TryParse<T>(out _))
                throw new NotSupportedException();

            var natives = points.Select(point => point.ToNative()).ToArray();

            try
            {
                var ps = natives.Select(point => point.NativePtr).ToArray();
                var elementType = image.ImageType.ToNativeArray2DType();
                var ret = NativeMethods.extract_image_4points(elementType,
                                                              image.NativePtr,
                                                              ps,
                                                              width,
                                                              height,
                                                              out var output);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{elementType} is not supported.");
                }

                return new Array2D<T>(output, image.ImageType);
            }
            finally
            {
                foreach (var n in natives)
                    n.Dispose();
            }
        }

        /// <summary>
        /// This function extracts an arbitrary quadrilateral patch from an matrix.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="matrix">The matrix.</param>
        /// <param name="points">The 4 points on the matrix.</param>
        /// <param name="width">The width of return matrix.</param>
        /// <param name="height">The height of return matrix.</param>
        /// <returns><see cref="Matrix{T}"/>.</returns>
        /// <exception cref="ArgumentException">The specified type of matrix is not supported.</exception>
        /// <exception cref="ArgumentException">The <see cref="MatrixBase.TemplateRows"/> or <see cref="MatrixBase.TemplateColumns"/> is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> or <paramref name="points"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><see cref="Array.Length"/> of <paramref name="points"/> must be 4.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> are less than or equal to zero.</exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"><paramref name="matrix"/> is disposed.</exception>
        public static Matrix<T> ExtractImage4Points<T>(Matrix<T> matrix,
                                                       DPoint[] points,
                                                       int width,
                                                       int height)
            where T : struct
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (points == null)
                throw new ArgumentNullException(nameof(points));
            if (points.Length != 4)
                throw new ArgumentOutOfRangeException($"{nameof(points.Length)} of {nameof(points)} must be 4.");
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(width)} or {nameof(height)} are less than or equal to zero.");

            matrix.ThrowIfDisposed();

            var natives = points.Select(point => point.ToNative()).ToArray();

            try
            {
                var ps = natives.Select(point => point.NativePtr).ToArray();
                var matrixElementType = matrix.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.extract_image_4points_matrix(matrixElementType,
                                                                     matrix.NativePtr,
                                                                     matrix.TemplateRows,
                                                                     matrix.TemplateColumns,
                                                                     ps,
                                                                     width,
                                                                     height,
                                                                     out var output);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(matrix.TemplateRows)} or {nameof(matrix.TemplateColumns)} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{matrixElementType} is not supported.");
                }

                return new Matrix<T>(output, matrix.TemplateRows, matrix.TemplateColumns);
            }
            finally
            {
                foreach (var n in natives)
                    n.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// This function returns an object detector that is configured to find human faces that are looking towards the camera.
        /// </summary>
        /// <returns><see cref="FrontalFaceDetector"/>.</returns>
        public static FrontalFaceDetector GetFrontalFaceDetector()
        {
            var ret = NativeMethods.get_frontal_face_detector();
            return new FrontalFaceDetector(ret);
        }

#if !LITE
        public static Rectangle GetRect(HoughTransform houghTransform)
        {
            if (houghTransform == null)
                throw new ArgumentNullException(nameof(houghTransform));

            houghTransform.ThrowIfDisposed();

            NativeMethods.hough_transform_get_rect(houghTransform.NativePtr, out var rect);
            return new Rectangle(rect);
        }
#endif

        public static Rectangle GetRect(MatrixBase matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();
            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.rectangle_get_rect_matrix(type,
                                                              matrix.NativePtr,
                                                              matrix.TemplateRows,
                                                              matrix.TemplateColumns,
                                                              out var rect);
            if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");

            return new Rectangle(rect);
        }

        public static Matrix<float> ImagePlane(Tensor tensor, int sample = 0, int k = 0)
        {
            if (tensor == null)
                throw new ArgumentNullException(nameof(tensor));

            tensor.ThrowIfDisposed();

            var ret = NativeMethods.image_plane(tensor.NativePtr, sample, k);
            return new Matrix<float>(ret);
        }

        /// <summary>
        /// This function loads Microsoft Windows Bitmap file into an <see cref="Array2D{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the image.</typeparam>
        /// <param name="path">A string that contains the name of the file from which to create the <see cref="Array2D{T}"/>.</param>
        /// <returns>The <see cref="Array2D{T}"/> this method creates.</returns>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="ImageLoadException">Failed to load image on dlib.</exception>
        public static Array2D<T> LoadBmp<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"The specified {nameof(path)} does not exist.", path);

            var str = Encoding.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.load_bmp(array2DType, image.NativePtr, str, str.Length, out var errorMessage);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case NativeMethods.ErrorType.GeneralFileImageLoad:
                    throw new ImageLoadException(path, StringHelper.FromStdString(errorMessage));
            }

            return image;
        }

        /// <summary>
        /// This function loads DNG (Digital Negative) file into an <see cref="Array2D{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the image.</typeparam>
        /// <param name="path">A string that contains the name of the file from which to create the <see cref="Array2D{T}"/>.</param>
        /// <returns>The <see cref="Array2D{T}"/> this method creates.</returns>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="ImageLoadException">Failed to load image on dlib.</exception>
        public static Array2D<T> LoadDng<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"The specified {nameof(path)} does not exist.", path);

            var str = Encoding.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.load_dng(array2DType, image.NativePtr, str, str.Length, out var errorMessage);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case NativeMethods.ErrorType.GeneralFileImageLoad:
                    throw new ImageLoadException(path, StringHelper.FromStdString(errorMessage));
            }

            return image;
        }

        /// <summary>
        /// This function loads image file into an <see cref="Array2D{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the image.</typeparam>
        /// <param name="path">A string that contains the name of the file from which to create the <see cref="Array2D{T}"/>.</param>
        /// <returns>The <see cref="Array2D{T}"/> this method creates.</returns>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="ImageLoadException">Failed to load image on dlib.</exception>
        public static Array2D<T> LoadImage<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"The specified {nameof(path)} does not exist.", path);

            var str = Encoding.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.load_image(array2DType, image.NativePtr, str, str.Length, out var errorMessage);
            switch(ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case NativeMethods.ErrorType.GeneralFileImageLoad:
                    throw new ImageLoadException(path, StringHelper.FromStdString(errorMessage));
            }

            return image;
        }

        /// <summary>
        /// This function loads image file into an <see cref="Matrix{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="path">A string that contains the name of the file from which to create the <see cref="Matrix{T}"/>.</param>
        /// <returns>The <see cref="Matrix{T}"/> this method creates.</returns>
        /// <exception cref="ArgumentException">The specified type of matrix is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="ImageLoadException">Failed to load image on dlib.</exception>
        public static Matrix<T> LoadImageAsMatrix<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"The specified {nameof(path)} does not exist.", path);

            if (!MatrixBase.TryParse(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            var str = Encoding.GetBytes(path);

            var matrixElementType = type.ToNativeMatrixElementType();
            var ret = NativeMethods.load_image_matrix(matrixElementType, str, str.Length, out var matrix, out var errorMessage);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case NativeMethods.ErrorType.GeneralFileImageLoad:
                    throw new ImageLoadException(path, StringHelper.FromStdString(errorMessage));
            }

            return new Matrix<T>(matrix);
        }

        /// <summary>
        /// This function loads JPEG (Joint Photographic Experts Group) file into an <see cref="Array2D{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the image.</typeparam>
        /// <param name="path">A string that contains the name of the file from which to create the <see cref="Array2D{T}"/>.</param>
        /// <returns>The <see cref="Array2D{T}"/> this method creates.</returns>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="ImageLoadException">Failed to load image on dlib.</exception>
        public static Array2D<T> LoadJpeg<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"The specified {nameof(path)} does not exist.", path);

            var str = Encoding.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.load_jpeg(array2DType, image.NativePtr, str, str.Length, out var errorMessage);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case NativeMethods.ErrorType.GeneralFileImageLoad:
                    throw new ImageLoadException(path, StringHelper.FromStdString(errorMessage));
            }

            return image;
        }

        /// <summary>
        /// This function loads PNG (Portable Network Graphics) file into an <see cref="Array2D{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the image.</typeparam>
        /// <param name="path">A string that contains the name of the file from which to create the <see cref="Array2D{T}"/>.</param>
        /// <returns>The <see cref="Array2D{T}"/> this method creates.</returns>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="ImageLoadException">Failed to load image on dlib.</exception>
        public static Array2D<T> LoadPng<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"The specified {nameof(path)} does not exist.", path);

            var str = Encoding.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.load_png(array2DType, image.NativePtr, str, str.Length, out var errorMessage);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case NativeMethods.ErrorType.GeneralFileImageLoad:
                    throw new ImageLoadException(path, StringHelper.FromStdString(errorMessage));
            }

            return image;
        }

        /// <summary>
        /// This function loads PNG (Portable Network Graphics) file into an <see cref="Array2D{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in the image.</typeparam>
        /// <param name="png">A byte array that contains png image data from which to create the <see cref="Array2D{T}"/>.</param>
        /// <returns>The <see cref="Array2D{T}"/> this method creates.</returns>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="png"/> is null.</exception>
        /// <exception cref="ImageLoadException">Failed to load image on dlib.</exception>
        public static Array2D<T> LoadPng<T>(byte[] png)
            where T : struct
        {
            if (png == null)
                throw new ArgumentNullException(nameof(png));

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.load_png_from_buffer(array2DType, image.NativePtr, png, png.Length, out var errorMessage);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                case NativeMethods.ErrorType.GeneralFileImageLoad:
                    throw new ImageLoadException(StringHelper.FromStdString(errorMessage));
            }

            return image;
        }

        #region LoadImageData

        public static Array2D<T> LoadImageData<T>(IntPtr data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == IntPtr.Zero)
                throw new ArgumentException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = type.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from pointer to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<T> LoadImageData<T>(byte[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.UInt8.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.UInt8} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<byte> LoadImageData(byte[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.UInt8.ToNativeArray2DType();
            var dstType = ImageTypes.UInt8.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.UInt8} to {dstType}.");

            return new Array2D<byte>(ret, ImageTypes.UInt8);
        }

        //public static Array2D<sbyte> LoadImageData(sbyte[] data, uint rows, uint columns, uint steps)
        //{
        //    if (data == null)
        //        throw new ArgumentNullException(nameof(data));

        //    var srcType = ImageTypes.Int8.ToNativeArray2DType();
        //    var dstType = ImageTypes.Int8.ToNativeArray2DType();
        //    var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
        //    if (ret == IntPtr.Zero)
        //        throw new ArgumentException($"Can not import from {ImageTypes.Int8} to {dstType}.");

        //    return new Array2D<sbyte>(ret, ImageTypes.Int8);
        //}

        public static Array2D<T> LoadImageData<T>(ImagePixelFormat format, byte[] data, uint rows, uint columns, uint steps)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            var srcType = ImageTypes.UInt8.ToNativeArray2DType();
            var dstType = type.ToNativeArray2DType();
            var pixelType = format.ToImagePixelType();
            var ret = NativeMethods.extensions_load_image_data2(dstType, srcType, pixelType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.UInt8} to {dstType}.");

            return new Array2D<T>(ret, type);
        }

        public static Array2D<ushort> LoadImageData(ushort[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.UInt16.ToNativeArray2DType();
            var dstType = ImageTypes.UInt16.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.UInt16} to {dstType}.");

            return new Array2D<ushort>(ret, ImageTypes.UInt16);
        }

        public static Array2D<short> LoadImageData(short[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.Int16.ToNativeArray2DType();
            var dstType = ImageTypes.Int16.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Int16} to {dstType}.");

            return new Array2D<short>(ret, ImageTypes.Int16);
        }

        public static Array2D<int> LoadImageData(int[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.Int32.ToNativeArray2DType();
            var dstType = ImageTypes.Int32.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Int32} to {dstType}.");

            return new Array2D<int>(ret, ImageTypes.Int32);
        }

        public static Array2D<float> LoadImageData(float[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.Float.ToNativeArray2DType();
            var dstType = ImageTypes.Float.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Float} to {dstType}.");

            return new Array2D<float>(ret, ImageTypes.Float);
        }

        public static Array2D<double> LoadImageData(double[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.Double.ToNativeArray2DType();
            var dstType = ImageTypes.Double.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.Double} to {dstType}.");

            return new Array2D<double>(ret, ImageTypes.Double);
        }

        public static Array2D<RgbPixel> LoadImageData(RgbPixel[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.RgbPixel.ToNativeArray2DType();
            var dstType = ImageTypes.RgbPixel.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.RgbPixel} to {dstType}.");

            return new Array2D<RgbPixel>(ret, ImageTypes.RgbPixel);
        }

        public static Array2D<BgrPixel> LoadImageData(BgrPixel[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.BgrPixel.ToNativeArray2DType();
            var dstType = ImageTypes.BgrPixel.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.BgrPixel} to {dstType}.");

            return new Array2D<BgrPixel>(ret, ImageTypes.BgrPixel);
        }

        public static Array2D<RgbAlphaPixel> LoadImageData(RgbAlphaPixel[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.RgbAlphaPixel.ToNativeArray2DType();
            var dstType = ImageTypes.RgbAlphaPixel.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.RgbAlphaPixel} to {dstType}.");

            return new Array2D<RgbAlphaPixel>(ret, ImageTypes.RgbAlphaPixel);
        }

        public static Array2D<HsiPixel> LoadImageData(HsiPixel[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.HsiPixel.ToNativeArray2DType();
            var dstType = ImageTypes.HsiPixel.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.HsiPixel} to {dstType}.");

            return new Array2D<HsiPixel>(ret, ImageTypes.HsiPixel);
        }

        public static Array2D<LabPixel> LoadImageData(LabPixel[] data, uint rows, uint columns, uint steps)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var srcType = ImageTypes.LabPixel.ToNativeArray2DType();
            var dstType = ImageTypes.LabPixel.ToNativeArray2DType();
            var ret = NativeMethods.extensions_load_image_data(dstType, srcType, data, rows, columns, steps);
            if (ret == IntPtr.Zero)
                throw new ArgumentException($"Can not import from {ImageTypes.LabPixel} to {dstType}.");

            return new Array2D<LabPixel>(ret, ImageTypes.LabPixel);
        }

        #endregion

        /// <summary>
        /// This function saves image to disk as Microsoft Windows Bitmap file.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="path">A string that contains the name of the file to which to save image.</param>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="image"/> is disposed.</exception>
        public static void SaveBmp(Array2DBase image, string path)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_bmp does not throw exception but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.save_bmp(array2DType, image.NativePtr, str, str.Length);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        /// <summary>
        /// This function saves matrix to disk as Microsoft Windows Bitmap file.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="matrix">The matrix.</param>
        /// <param name="path">A string that contains the name of the file to which to save matrix.</param>
        /// <exception cref="ArgumentException">The specified type of matrix is not supported.</exception>
        /// <exception cref="ArgumentException">The <see cref="MatrixBase.TemplateRows"/> or <see cref="MatrixBase.TemplateColumns"/> is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="matrix"/> is disposed.</exception>
        public static void SaveBmp(MatrixBase matrix, string path)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_bmp does not throw exception but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (matrix.Rows <= 0 || matrix.Columns <= 0)
                throw new ArgumentException($"{nameof(matrix.Columns)} and {nameof(matrix.Rows)} is less than or equal to zero.", nameof(matrix));

            var str = Encoding.GetBytes(path);

            var matrixElementType = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.save_bmp_matrix(matrixElementType, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, str, str.Length);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }
        }

        /// <summary>
        /// This function saves image to disk as DNG (Digital Negative) file.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="path">A string that contains the name of the file to which to save image.</param>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="image"/> is disposed.</exception>
        public static void SaveDng(Array2DBase image, string path)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_dng does not throw exception but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.save_dng(array2DType, image.NativePtr, str, str.Length);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        /// <summary>
        /// This function saves matrix to disk as DNG (Digital Negative) file.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="matrix">The matrix.</param>
        /// <param name="path">A string that contains the name of the file to which to save matrix.</param>
        /// <exception cref="ArgumentException">The specified type of matrix is not supported.</exception>
        /// <exception cref="ArgumentException">The <see cref="MatrixBase.TemplateRows"/> or <see cref="MatrixBase.TemplateColumns"/> is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="matrix"/> is disposed.</exception>
        public static void SaveDng(MatrixBase matrix, string path)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_dng does not throw exception but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (matrix.Rows <= 0 || matrix.Columns <= 0)
                throw new ArgumentException($"{nameof(matrix.Columns)} and {nameof(matrix.Rows)} is less than or equal to zero.", nameof(matrix));

            var str = Encoding.GetBytes(path);

            var matrixElementType = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.save_dng_matrix(matrixElementType, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, str, str.Length);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }
        }

        /// <summary>
        /// This function saves image to disk as JPEG (Joint Photographic Experts Group) file.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="path">A string that contains the name of the file to which to save image.</param>
        /// <param name="quality">The quality of file. It must be 0 - 100. The default value is 75.</param>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="quality"/> is less than zero or greater than 100.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="image"/> is disposed.</exception>
        public static void SaveJpeg(Array2DBase image, string path, int quality = 75)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));
            if (quality < 0)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is less than zero.");
            if (quality > 100)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is greater than 100.");

            var str = Encoding.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.save_jpeg(array2DType, image.NativePtr, str, str.Length, quality);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        /// <summary>
        /// This function saves matrix to disk as JPEG (Joint Photographic Experts Group) file.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="matrix">The matrix.</param>
        /// <param name="path">A string that contains the name of the file to which to save matrix.</param>
        /// <param name="quality">The quality of file. It must be 0 - 100. The default value is 75.</param>
        /// <exception cref="ArgumentException">The specified type of matrix is not supported.</exception>
        /// <exception cref="ArgumentException">The <see cref="MatrixBase.TemplateRows"/> or <see cref="MatrixBase.TemplateColumns"/> is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="quality"/> is less than zero or greater than 100.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="matrix"/> is disposed.</exception>
        public static void SaveJpeg(MatrixBase matrix, string path, int quality = 75)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (matrix.Rows <= 0 || matrix.Columns <= 0)
                throw new ArgumentException($"{nameof(matrix.Columns)} and {nameof(matrix.Rows)} is less than or equal to zero.", nameof(matrix));
            if (quality < 0)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is less than zero.");
            if (quality > 100)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is greater than 100.");

            var str = Encoding.GetBytes(path);

            var matrixElementType = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.save_jpeg_matrix(matrixElementType, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, str, str.Length, quality);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }
        }

        /// <summary>
        /// This function saves image to disk as PNG (Portable Network Graphics) file.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="path">A string that contains the name of the file to which to save image.</param>
        /// <exception cref="ArgumentException">The specified type of image is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="image"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="image"/> is disposed.</exception>
        public static void SavePng(Array2DBase image, string path)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.save_png(array2DType, image.NativePtr, str, str.Length);
            if (ret == NativeMethods.ErrorType.Array2DTypeTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        /// <summary>
        /// This function saves matrix to disk as PNG (Portable Network Graphics) file.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="matrix">The matrix.</param>
        /// <param name="path">A string that contains the name of the file to which to save matrix.</param>
        /// <exception cref="ArgumentException">The specified type of matrix is not supported.</exception>
        /// <exception cref="ArgumentException">The <see cref="MatrixBase.TemplateRows"/> or <see cref="MatrixBase.TemplateColumns"/> is not supported.</exception>
        /// <exception cref="ArgumentException"><see cref="TwoDimensionObjectBase.Rows"/> or <see cref="TwoDimensionObjectBase.Columns"/> are less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> or <paramref name="path"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="matrix"/> is disposed.</exception>
        public static void SavePng(MatrixBase matrix, string path)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (matrix.Rows <= 0 || matrix.Columns <= 0)
                throw new ArgumentException($"{nameof(matrix.Columns)} and {nameof(matrix.Rows)} is less than or equal to zero.", nameof(matrix));

            var str = Encoding.GetBytes(path);

            var matrixElementType = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.save_png_matrix(matrixElementType, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, str, str.Length);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }
        }

        #endregion

        #region Properties

        private static Encoding _Encoding = Encoding.UTF8;

        public static Encoding Encoding
        {
            get => _Encoding;
            set => _Encoding = value ?? Encoding.UTF8;
        }

        public static bool IsSupportGui => NativeMethods.is_support_gui();

        public static bool IsDnnSupportGui => NativeMethods.dnn_is_support_gui();

        public static bool IsSupportCuda => NativeMethods.is_support_cuda();

        public static bool IsDnnSupportCuda => NativeMethods.dnn_is_support_cuda();

        #endregion

    }

}
