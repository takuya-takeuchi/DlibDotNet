using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void LoadImageDataset<T>(string path, out IList<Matrix<T>> images, out IList<IList<MModRect>> boxes)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);

            var str = Dlib.Encoding.GetBytes(path);

            using (var matrix = new Matrix<T>())
            using (var retImages = new StdVector<Matrix<T>>())
            using (var retBoxes = new StdVector<StdVector<MModRect>>())
            {
                var type = matrix.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.load_image_dataset_mmod_rect(type, retImages.NativePtr, retBoxes.NativePtr, str);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{type} is not supported.");

                images = retImages.ToArray();
                boxes = retBoxes.ToArray().Select(box => box.ToArray()).ToArray();
            }
        }

        public static void LoadImageDataset<T>(string path, out IList<Matrix<T>> images, out IList<IList<Rectangle>> boxes)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);

            var str = Dlib.Encoding.GetBytes(path);

            using (var matrix = new Matrix<T>())
            using (var retImages = new StdVector<Matrix<T>>())
            using (var retBoxes = new StdVector<StdVector<Rectangle>>())
            {
                var type = matrix.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.load_image_dataset_rectangle(type, retImages.NativePtr, retBoxes.NativePtr, str);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{type} is not supported.");

                images = retImages.ToArray();
                boxes = retBoxes.ToArray().Select(box => box.ToArray()).ToArray();
            }
        }

        #endregion

    }

}