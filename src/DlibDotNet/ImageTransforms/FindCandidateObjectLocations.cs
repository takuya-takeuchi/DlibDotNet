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

        public static IEnumerable<Rectangle> FindCandidateObjectLocations(Array2DBase image)
        {
            using (var kvals = Linspace(50, 200, 3))
                return FindCandidateObjectLocations(image, kvals);
        }

        public static IEnumerable<Rectangle> FindCandidateObjectLocations<T>(Array2DBase image,
                                                                             Matrix<T> kvals,
                                                                             uint minSize = 20,
                                                                             uint maxMergingIterations = 50)
        where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (kvals == null)
                throw new ArgumentNullException(nameof(kvals));

            var array2DType = image.ImageType.ToNativeArray2DType();
            using (var dets = new StdVector<Rectangle>())
            {
                var matrixType = kvals.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.find_candidate_object_locations(array2DType,
                                                                        image.NativePtr,
                                                                        dets.NativePtr,
                                                                        matrixType,
                                                                        kvals.NativePtr,
                                                                        minSize,
                                                                        maxMergingIterations);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{image.ImageType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{matrixType} is not supported.");
                }

                return dets.ToArray();
            }
        }

        #endregion

    }

}
#endif
