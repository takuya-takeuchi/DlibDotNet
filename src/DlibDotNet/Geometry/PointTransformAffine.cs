using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PointTransformAffine : PointTransformBase
    {

        #region Constructors

        public PointTransformAffine()
        {
            this.NativePtr = Native.point_transform_affine_new();
        }

        public PointTransformAffine(Matrix<double> matrix, DPoint vector)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (vector == null)
                throw new ArgumentNullException(nameof(vector));

            matrix.ThrowIfDisposed();
            vector.ThrowIfDisposed();

            if (matrix.Columns != 2 || matrix.Rows != 2)
                throw new ArgumentException($"{nameof(matrix)} should be 2x2 matrix");

            this.NativePtr = Native.point_transform_affine_new1(matrix.NativePtr, vector.NativePtr);
        }

        public PointTransformAffine(Matrix<double> matrix, double x, double y)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (matrix.Columns != 2 || matrix.Rows != 2)
                throw new ArgumentException($"{nameof(matrix)} should be 2x2 matrix");

            using (var vector = new DPoint(x, y))
                this.NativePtr = Native.point_transform_affine_new1(matrix.NativePtr, vector.NativePtr);
        }

        internal PointTransformAffine(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public DPoint B
        {
            get
            {
                var vector = Native.point_transform_affine_get_b(this.NativePtr);
                return new DPoint(vector);
            }
        }

        public Matrix<double> M
        {
            get
            {
                var matrix = Native.point_transform_affine_get_m(this.NativePtr);
                return new Matrix<double>(matrix, MatrixElementTypes.Double);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            point.ThrowIfDisposed();

            var ptr = Native.point_transform_affine_operator(this.NativePtr, point.NativePtr);
            return new DPoint(ptr);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.point_transform_affine_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_affine_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_affine_new1(IntPtr matrix, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_affine_get_b(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_affine_get_m(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_affine_operator(IntPtr obj, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void point_transform_affine_delete(IntPtr obj);

        }

    }

}