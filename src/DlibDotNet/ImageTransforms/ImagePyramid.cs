#if !LITE
using System;
using System.Collections.Generic;
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
            var ret = NativeMethods.create_tiled_pyramid(inType,
                                                         image.NativePtr,
                                                         pyramidType,
                                                         pyramidRate,
                                                         padding,
                                                         outerPadding,
                                                         out var outImg,
                                                         out var vecRects);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
                case NativeMethods.ErrorType.PyramidNotSupportRate:
                    throw new NotSupportedException();
            }

            outImage = new Matrix<T>(outImg);
            using (var vec = new StdVector<Rectangle>(vecRects))
                rects = vec.ToArray();
        }



        #endregion

    }

}
#endif
