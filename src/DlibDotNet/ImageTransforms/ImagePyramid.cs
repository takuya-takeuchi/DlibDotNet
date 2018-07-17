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

        public static void CreateTiledPyramid<T, U>(Matrix<T> image, uint padding, uint outerPadding, uint pyramidRate, out Matrix<T> outImage, out IEnumerable<Rectangle> rects)
            where T : struct
            where U : Pyramid
        {
            // 10, 0
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            outImage = default(Matrix<T>);
            rects = default(IEnumerable<Rectangle>);

            image.ThrowIfDisposed();

            if (!Pyramid.TryGetSupportPyramidType<U>(out var pyramidType))
                throw new NotSupportedException();

            var inType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.create_tiled_pyramid(inType,
                                                  image.NativePtr,
                                                  pyramidType,
                                                  pyramidRate,
                                                  padding,
                                                  outerPadding,
                                                  out var outImg,
                                                  out var vecRects);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
                case Native.ErrorType.PyramidNotSupportRate:
                    throw new NotSupportedException();
            }

            outImage = new Matrix<T>(outImg);
            using (var vec = new StdVector<Rectangle>(vecRects))
                rects = vec.ToArray();
        }



        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType create_tiled_pyramid(MatrixElementType img_type,
                                                                IntPtr img,
                                                                PyramidType pyramidType,
                                                                uint pyramidRate,
                                                                uint padding,
                                                                uint outer_padding,
                                                                out IntPtr out_img,
                                                                out IntPtr rects);

        }

    }

}