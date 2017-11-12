using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PointTransform : PointTransformBase
    {

        #region Constructors

        public PointTransform()
        {
            this.NativePtr = Native.point_transform_new();
        }

        public PointTransform(double angle, DPoint vector)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector));

            using (var native = vector.ToNative())
                this.NativePtr = Native.point_transform_new1(angle, native.NativePtr);
        }

        public PointTransform(double angle, double x, double y)
        {
            using (var native = new DPoint(x, y).ToNative())
                this.NativePtr = Native.point_transform_new1(angle, native.NativePtr);
        }

        #endregion

        #region Properties

        public DPoint B
        {
            get
            {
                var vector = Native.point_transform_get_b(this.NativePtr);
                return new DPoint(vector);
            }
        }

        public Matrix<double> M
        {
            get
            {
                var matrix = Native.point_transform_get_m(this.NativePtr);
                return new Matrix<double>(matrix, MatrixElementTypes.Double);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            using (var native = point.ToNative())
            {
                var ptr = Native.point_transform_operator(this.NativePtr, native.NativePtr);
                return new DPoint(ptr);
            }
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.point_transform_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_new1(double angle, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_get_b(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_get_m(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_transform_operator(IntPtr obj, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void point_transform_delete(IntPtr obj);

        }

    }

}