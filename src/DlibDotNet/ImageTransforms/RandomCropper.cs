using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

namespace DlibDotNet.ImageTransforms
{

    public sealed class RandomCropper : DlibObject
    {

        #region Constructors

        public RandomCropper()
        {
            this.NativePtr = NativeMethods.random_cropper_new();
        }

        #endregion

        #region Properties

        public double BackgroundCropsFraction
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_background_crops_fraction(this.NativePtr, out var value);
                return value;
            }
            set
            {
                if (!(0 <= value && value < 1))
                    throw new ArgumentOutOfRangeException();

                this.ThrowIfDisposed();
                NativeMethods.random_cropper_set_background_crops_fraction(this.NativePtr, value);
            }
        }

        public ChipDims ChipDims
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_chip_dims(this.NativePtr, out var value);
                return new ChipDims(value);
            }
            set
            {
                this.ThrowIfDisposed();
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                NativeMethods.random_cropper_set_chip_dims(this.NativePtr, value.Rows, value.Columns);
            }
        }

        public double MaxObjectSize
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_max_object_size(this.NativePtr, out var value);
                return value;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                this.ThrowIfDisposed();
                NativeMethods.random_cropper_set_max_object_size(this.NativePtr, value);
            }
        }

        public double MaxRotationDegrees
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_max_rotation_degrees(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_set_max_rotation_degrees(this.NativePtr, value);
            }
        }
        
        public long MinObjectLengthLongDim
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_min_object_length_long_dim(this.NativePtr, out var value);
                return value;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                this.ThrowIfDisposed();

                var s = this.MinObjectLengthShortDim;
                if (!(0 < s && s <= value))
                    throw new ArgumentException($"{nameof(this.MinObjectLengthShortDim)} should be more than 0 and {nameof(this.MinObjectLengthShortDim)} should be less than {nameof(MinObjectLengthLongDim)}.");
                
                NativeMethods.random_cropper_set_min_object_size(this.NativePtr, value, s);
            }
        }

        public long MinObjectLengthShortDim
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_min_object_length_short_dim(this.NativePtr, out var value);
                return value;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                this.ThrowIfDisposed();

                // -0 < short_dim <= long_dim
                var l = this.MinObjectLengthLongDim;
                if (!(0 < value && value <= l))
                    throw new ArgumentException($"{nameof(this.MinObjectLengthShortDim)} should be more than 0 and {nameof(this.MinObjectLengthShortDim)} should be less than {nameof(MinObjectLengthLongDim)}.");

                NativeMethods.random_cropper_set_min_object_size(this.NativePtr, l, value);
            }
        }

        public bool RandomlyFlip
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_randomly_flip(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_set_randomly_flip(this.NativePtr, value);
            }
        }

        public double TranslateAmount
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.random_cropper_get_translate_amount(this.NativePtr, out var value);
                return value;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                this.ThrowIfDisposed();
                NativeMethods.random_cropper_set_translate_amount(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        public void Operator<T>(
            uint numCrops,
            IEnumerable<Matrix<T>> images,
            IEnumerable<IEnumerable<MModRect>> rects,
            out IEnumerable<Matrix<T>> crops,
            out IEnumerable<IEnumerable<MModRect>> cropRects)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            var count = images.Count();
            var numRect = rects.Count();
            if (count != numRect)
                throw new ArgumentException();

            List<StdVector<MModRect>> listOfVectorOfMModRect = null;

            try
            {
                listOfVectorOfMModRect = rects.Select(r => new StdVector<MModRect>(r)).ToList();

                using (var matrix = new Matrix<T>())
                using (var inImages = new StdVector<Matrix<T>>(images))
                using (var inRects = new StdVector<StdVector<MModRect>>(listOfVectorOfMModRect))
                using (var outCrops = new StdVector<Matrix<T>>())
                using (var outCropRects = new StdVector<StdVector<MModRect>>())
                {
                    var type = matrix.MatrixElementType.ToNativeMatrixElementType();
                    var ret = NativeMethods.random_cropper_operator(this.NativePtr,
                                                                    numCrops,
                                                                    type,
                                                                    inImages.NativePtr,
                                                                    inRects.NativePtr,
                                                                    outCrops.NativePtr,
                                                                    outCropRects.NativePtr);
                    if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                        throw new ArgumentException($"{type} is not supported.");

                    crops = outCrops.ToArray();
                    cropRects = outCropRects.ToArray().Select(box => box.ToArray()).ToList();
                }
            }
            finally
            {
                if(listOfVectorOfMModRect!=null)
                    foreach (var stdVector in listOfVectorOfMModRect)
                        stdVector?.Dispose();
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

            NativeMethods.random_cropper_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
