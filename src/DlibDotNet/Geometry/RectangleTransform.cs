﻿using System;
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
            using (var native = rectangle.ToNative())
            {
                var ptr = Native.rectangle_transform_operator(this.NativePtr, native.NativePtr);
                return new Rectangle(ptr);
            }
        }

        public DRectangle Operator(DRectangle drectangle)
        {
            using (var native = drectangle.ToNative())
            {
                var ptr = Native.rectangle_transform_operator_d(this.NativePtr, native.NativePtr);
                return new DRectangle(ptr);
            }
        }

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

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