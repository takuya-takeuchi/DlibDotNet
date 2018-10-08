using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static PointTransformAffine FindSimilarityTransform(IEnumerable<Point> fromPoints, IEnumerable<Point> toPoints)
        {
            if (fromPoints == null)
                throw new ArgumentNullException(nameof(fromPoints));
            if (toPoints == null)
                throw new ArgumentNullException(nameof(toPoints));

            if (fromPoints.Count() != toPoints.Count() || fromPoints.Count() < 2)
                throw new ArgumentException();

            using (var f = new StdVector<Point>(fromPoints))
            using (var t = new StdVector<Point>(toPoints))
            {
                var ptr = Native.find_similarity_transform_point(f.NativePtr, t.NativePtr);
                return new PointTransformAffine(ptr);
            }
        }

        public static PointTransformAffine FindSimilarityTransform(IEnumerable<DPoint> fromPoints, IEnumerable<DPoint> toPoints)
        {
            if (fromPoints == null)
                throw new ArgumentNullException(nameof(fromPoints));
            if (toPoints == null)
                throw new ArgumentNullException(nameof(toPoints));

            if (fromPoints.Count() != toPoints.Count() || fromPoints.Count() < 2)
                throw new ArgumentException();

            using (var f = new StdVector<DPoint>(fromPoints))
            using (var t = new StdVector<DPoint>(toPoints))
            {
                var ptr = Native.find_similarity_transform_dpoint(f.NativePtr, t.NativePtr);
                return new PointTransformAffine(ptr);
            }
        }

        public static Matrix<double> RotationMatrix(double angle)
        {
            var ptr = Native.rotation_matrix(angle);
            return new Matrix<double>(ptr, 2, 2);
        }

        #endregion

    }

}
