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

        public static IEnumerable<Rectangle> FindCandidateObjectLocations(
            Array2DBase image)
        {
            using (var kvals = Linspace(50, 200, 3))
                return FindCandidateObjectLocations(image, kvals);
        }

        public static IEnumerable<Rectangle> FindCandidateObjectLocations(
            Array2DBase image,
            MatrixRangeExp<double> kvals,
            uint minSize = 20,
            uint maxMergingIterations = 50)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (kvals == null)
                throw new ArgumentNullException(nameof(kvals));

            var array2DType = image.ImageType.ToNativeArray2DType();
            using (var dets = new StdVectorOfRectangle())
            {
                var ret = Native.find_candidate_object_locations(array2DType, image.NativePtr, dets.NativePtr, IntPtr.Zero, minSize, maxMergingIterations);
                if (ret == Native.ErrorType.ArrayTypeNotSupport)
                    throw new ArgumentException($"{image.ImageType} is not supported.");
                return dets.ToArray();
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType find_candidate_object_locations(Array2DType type, IntPtr img, IntPtr rect, IntPtr kvals, uint minSize, uint maxMergingIterations);

        }

    }

}