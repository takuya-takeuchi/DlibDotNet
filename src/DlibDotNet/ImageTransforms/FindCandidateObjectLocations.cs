﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                var ret = Native.find_candidate_object_locations(array2DType,
                                                                 image.NativePtr,
                                                                 dets.NativePtr,
                                                                 matrixType,
                                                                 kvals.NativePtr,
                                                                 minSize,
                                                                 maxMergingIterations);
                switch (ret)
                {
                    case Native.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{image.ImageType} is not supported.");
                    case Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{matrixType} is not supported.");
                }

                return dets.ToArray();
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType find_candidate_object_locations(Array2DType type,
                                                                           IntPtr img,
                                                                           IntPtr rect,
                                                                           MatrixElementType matrixElementType,
                                                                           IntPtr kvals,
                                                                           uint minSize,
                                                                           uint maxMergingIterations);

        }

    }

}