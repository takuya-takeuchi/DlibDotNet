using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class RectangleTransform : DlibObject
    {

        #region Constructors

        public RectangleTransform()
        {
            this.NativePtr = Native.rectangle_transform_new();
        }

        public RectangleTransform(PointTransformAffine transform)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            transform.ThrowIfDisposed();

            this.NativePtr = Native.rectangle_transform_new1(transform.NativePtr);
        }

        #endregion

        #region Properties

        public PointTransformAffine Transform
        {
            get
            {
                var matrix = Native.rectangle_transform_get_tform(this.NativePtr);
                return new PointTransformAffine(matrix);
            }
        }

        #endregion

        #region Methods

        public Rectangle Operator(Rectangle rectangle)
        {
            if (rectangle == null)
                throw new ArgumentNullException(nameof(rectangle));

            rectangle.ThrowIfDisposed();

            var ptr = Native.rectangle_transform_operator(this.NativePtr, rectangle.NativePtr);
            return new Rectangle(ptr);
        }

        public DRectangle Operator(DRectangle drectangle)
        {
            if (drectangle == null)
                throw new ArgumentNullException(nameof(drectangle));

            drectangle.ThrowIfDisposed();

            var ptr = Native.rectangle_transform_operator_d(this.NativePtr, drectangle.NativePtr);
            return new DRectangle(ptr);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.rectangle_transform_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_transform_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_transform_new1(IntPtr tform);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_transform_get_tform(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_transform_operator(IntPtr obj, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_transform_operator_d(IntPtr obj, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rectangle_transform_delete(IntPtr obj);

        }

    }

}