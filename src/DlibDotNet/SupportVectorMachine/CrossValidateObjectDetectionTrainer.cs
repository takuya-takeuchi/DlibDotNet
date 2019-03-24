using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        public static Matrix<double> TestObjectDetectionFunction<T, U>(ObjectDetector<T> detector,
                                                                       IEnumerable<Matrix<U>> images,
                                                                       IEnumerable<IEnumerable<Rectangle>> objects)
            where T : ImageScanner
            where U : struct
        {
            if (detector == null)
                throw new ArgumentNullException(nameof(detector));
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            detector.ThrowIfDisposed();
            images.ThrowIfDisposed();

            return detector.TestObjectDetectionFunction(images, objects);
        }

    }

}
