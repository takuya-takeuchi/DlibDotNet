using System;
using DlibDotNet.Extensions;

namespace DlibDotNet.Dnn
{

    public sealed class InputRgbImagePyramid<P> : Input
        where P : Pyramid
    {

        #region Fields

        private readonly uint _PyramidRate;

        private readonly Dlib.Native.PyramidType _PyramidType;

        #endregion

        #region Constructors

        public InputRgbImagePyramid(uint pyramidRate)
        {
            this._PyramidRate = pyramidRate;
            Pyramid.TryGetSupportPyramidType<P>(out this._PyramidType);

            //var err = Native.input_rgb_image_pyramid_new(this._PyramidType, this._PyramidRate, out var ret);
            //this.NativePtr = ret;
        }

        #endregion

        #region Methods

        public uint GetPyramidPadding()
        {
            this.ThrowIfDisposed();

            Dlib.Native.input_rgb_image_pyramid_get_pyramid_padding(this.NativePtr,
                                                                    this._PyramidType,
                                                                    this._PyramidRate,
                                                                    out var padding);
            return padding;
        }

        public uint GetPyramidOuterPadding()
        {
            this.ThrowIfDisposed();

            Dlib.Native.input_rgb_image_pyramid_get_pyramid_outer_padding(this.NativePtr,
                                                                          this._PyramidType,
                                                                          this._PyramidRate,
                                                                          out var padding);
            return padding;
        }

        public DRectangle ImageSpaceToTensorSpace(Tensor data, double scale, DRectangle rectangle)
        {
            this.ThrowIfDisposed();

            using (var r = rectangle.ToNative())
            {
                Dlib.Native.input_rgb_image_pyramid_image_space_to_tensor_space(this.NativePtr,
                                                                                this._PyramidType,
                                                                                this._PyramidRate,
                                                                                data.NativePtr,
                                                                                scale,
                                                                                r.NativePtr,
                                                                                out var rect);

                return new DRectangle(rect);
            }
        }

        public void ToTensor<T>(MatrixBase matrix, uint count, T tensor)
            where T : Tensor
        {
            this.ThrowIfDisposed();

            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Dlib.Native.input_rgb_image_pyramid_to_tensor(this.NativePtr,
                                                                    this._PyramidType,
                                                                    this._PyramidRate,
                                                                    type,
                                                                    matrix.NativePtr,
                                                                    matrix.TemplateRows,
                                                                    matrix.TemplateRows,
                                                                    count,
                                                                    tensor.NativePtr);

            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Dlib.Native.ErrorType.PyramidNotSupportRate:
                case Dlib.Native.ErrorType.PyramidNotSupportType:
                    throw new NotSupportedException();
            }
        }
        
        #region Overrids

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Dlib.Native.input_rgb_image_pyramid_delete(this.NativePtr, this._PyramidType, this._PyramidRate);
        }

        #endregion

        #endregion

    }

}