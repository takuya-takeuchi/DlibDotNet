using System;
using System.Collections.Generic;
using System.Linq;

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
                var ptr = NativeMethods.find_similarity_transform_point(f.NativePtr, t.NativePtr);
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
                var ptr = NativeMethods.find_similarity_transform_dpoint(f.NativePtr, t.NativePtr);
                return new PointTransformAffine(ptr);
            }
        }

        public static Matrix<double> RotationMatrix(double angle)
        {
            var ptr = NativeMethods.rotation_matrix(angle);
            return new Matrix<double>(ptr, 2, 2);
        }

        #region Rectangle

        public static Point Center(Rectangle rectangle)
        {
            return rectangle.Center;
        }

        public static DPoint Center(DRectangle rectangle)
        {
            return rectangle.Center;
        }

        public static Rectangle CenteredRect(int x, int y, uint width, uint height)
        {
            return Rectangle.CenteredRect(x, y, width, height);
        }

        public static Rectangle CenteredRect(Point p, uint width, uint height)
        {
            return Rectangle.CenteredRect(p, width, height);
        }

        public static Rectangle CenteredRect(Rectangle rect, uint width, uint height)
        {
            return Rectangle.CenteredRect(rect, width, height);
        }

        public static Rectangle MoveRect(Rectangle rect, Point point)
        {
            using (var r = rect.ToNative())
            using (var p = point.ToNative())
            {
                var ret = NativeMethods.rectangle_move_rect(r.NativePtr, p.NativePtr);
                return new Rectangle(ret);
            }
        }

        public static Rectangle MoveRect(Rectangle rect, int x, int y)
        {
            using (var r = rect.ToNative())
            {
                var ret = NativeMethods.rectangle_move_rect2(r.NativePtr, x, y);
                return new Rectangle(ret);
            }
        }

        #endregion

        #endregion

    }

}
