using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ImageGradients : DlibObject
    {

        #region Constructors

        public ImageGradients(long scale = 1)
        {
            this.NativePtr = NativeMethods.get_image_gradients(scale);
        }

        #endregion
        
        #region Methods

        public Rectangle GetGradientX(Array2DBase image, Array2D<float> gradient)
        {
            return InvokeGetGradient(image, gradient, NativeMethods.image_gradients_gradient_x);
        }
        public Rectangle GetGradientY(Array2DBase image, Array2D<float> gradient)
        {
            return InvokeGetGradient(image, gradient, NativeMethods.image_gradients_gradient_y);
        }

        public Rectangle GetGradientXY(Array2DBase image, Array2D<float> gradient)
        {
            return InvokeGetGradient(image, gradient, NativeMethods.image_gradients_gradient_xy);
        }

        public Rectangle GetGradientXX(Array2DBase image, Array2D<float> gradient)
        {
            return InvokeGetGradient(image, gradient, NativeMethods.image_gradients_gradient_xx);
        }

        public Rectangle GetGradientYY(Array2DBase image, Array2D<float> gradient)
        {
            return InvokeGetGradient(image, gradient, NativeMethods.image_gradients_gradient_yy);
        }


        private Rectangle InvokeGetGradient(Array2DBase image, Array2D<float> gradient, Func<IntPtr, NativeMethods.Array2DType, IntPtr, IntPtr, IntPtr, NativeMethods.ErrorType> gradientMethod)
        {
            this.ThrowIfDisposed();
            
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            if (gradient == null)
                throw new ArgumentNullException(nameof(gradient));

            image.ThrowIfDisposed();
            gradient.ThrowIfDisposed();

            using(var rect = new Rectangle.NativeRectangle())
            {
                var inType = image.ImageType.ToNativeArray2DType();

                var ret = gradientMethod(this.NativePtr, inType, image.NativePtr, gradient.NativePtr, rect.NativePtr);

                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return rect.ToManaged();
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

            NativeMethods.image_gradients_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}