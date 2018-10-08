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

        public static IEnumerable<SurfPoint> GetSurfPoints(Array2DBase image, long maxPoints = 10000, double detectionThreshold = 30.0)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (maxPoints <= 0)
                throw  new ArgumentOutOfRangeException();
            if(detectionThreshold < 0)
                throw new ArgumentOutOfRangeException();

            using (var points = new StdVector<SurfPoint>())
            {
                var array2DType = image.ImageType.ToNativeArray2DType();
                var ret = Native.get_surf_points(array2DType, image.NativePtr, maxPoints, detectionThreshold, points.NativePtr);
                if (ret == Native.ErrorType.Array2DTypeTypeNotSupport)
                    throw new ArgumentException($"{image.ImageType} is not supported.");

                return points.ToArray();
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType get_surf_points(Array2DType type, IntPtr img, long max_points, double detection_threshold, IntPtr points);
            
        }

    }

}