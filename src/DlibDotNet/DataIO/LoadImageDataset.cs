using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void LoadImageDataset<T>(string path, out IEnumerable<Matrix<T>> images, out IEnumerable<IEnumerable<MModRect>> boxes)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);

            var str = Encoding.UTF8.GetBytes(path);

            using (var matrix = new Matrix<T>())
            using (var retImages = new StdVector<Matrix<T>>())
            using (var retBoxes = new StdVector<StdVector<MModRect>>())
            {
                var type = matrix.MatrixElementType.ToNativeMatrixElementType();
                var ret = Native.load_image_dataset_mmod_rect(type, retImages.NativePtr, retBoxes.NativePtr, str);
                if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{type} is not supported.");

                images = retImages.ToArray();
                boxes = retBoxes.ToArray().Select(box => box.ToArray()).ToList();
            }
        }

        public static void LoadImageDataset<T>(string path, out IEnumerable<Matrix<T>> images, out IEnumerable<IEnumerable<Rectangle>> boxes)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);

            var str = Encoding.UTF8.GetBytes(path);

            using (var matrix = new Matrix<T>())
            using (var retImages = new StdVector<Matrix<T>>())
            using (var retBoxes = new StdVector<StdVector<Rectangle>>())
            {
                var type = matrix.MatrixElementType.ToNativeMatrixElementType();
                var ret = Native.load_image_dataset_rectangle(type, retImages.NativePtr, retBoxes.NativePtr, str);
                if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{type} is not supported.");

                images = retImages.ToArray();
                boxes = retBoxes.ToArray().Select(box => box.ToArray()).ToList();
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_image_dataset_mmod_rect(MatrixElementType type, IntPtr images, IntPtr boxes, byte[] path);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_image_dataset_rectangle(MatrixElementType type, IntPtr images, IntPtr boxes, byte[] path);

        }

    }

}