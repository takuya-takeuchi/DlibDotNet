using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PointTransformProjective : PointTransformBase
    {

        #region Constructors

        public PointTransformProjective()
        {
            this.NativePtr = Native.point_transform_projective_new();
        }

        public PointTransformProjective(Matrix<double> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (matrix.Columns != 3 || matrix.Rows != 3)
                throw new ArgumentException($"{nameof(matrix)} should be 3x3 matrix");

            this.NativePtr = Native.point_transform_projective_new1(matrix.NativePtr);
        }

        #endregion

        #region Properties

        public Matrix<double> M
        {
            get
            {
                var matrix = Native.point_transform_projective_get_m(this.NativePtr);
                return new Matrix<double>(matrix);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            using (var native = point.ToNative())
            {
                var ptr = Native.point_transform_projective_operator(this.NativePtr, native.NativePtr);
                return new DPoint(ptr);
            }
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.point_transform_projective_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_projective_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_projective_new1(IntPtr matrix);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_projective_get_m(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_projective_operator(IntPtr obj, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void point_transform_projective_delete(IntPtr obj);

        }

    }

}