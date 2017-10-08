using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static Matrix<T> TileImages<T>(Array<Array2D<T>> array)
            where T: struct 
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            array.ThrowIfDisposed();

            var imageType = array.ImageType;
            var ret = Native.tile_images(imageType.ToNativeArray2DType(), array.NativePtr, out var ret_image);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{imageType} is not supported.");
            }

            return new Matrix<T>(ret_image, MatrixElementTypes.RgbPixel);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType tile_images(Array2DType inType, IntPtr array, out IntPtr ret_image);

        }

    }

}