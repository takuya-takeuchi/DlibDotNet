using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static MatrixOp Mat(Array2DBase array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            array.ThrowIfDisposed();

            var imageType = array.ImageType.ToNativeArray2DType();
            var ret = Native.mat_array2d(imageType, array.NativePtr, out var matrix);
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"{imageType} is not supported.");
            }

            return new MatrixOp(Native.ElementType.OpArray2DToMat, array.ImageType, matrix);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mat_array2d(Array2DType type, IntPtr array, out IntPtr ret);

        }

    }

}