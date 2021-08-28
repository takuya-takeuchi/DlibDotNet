#if !LITE
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

        public int MinObjectLengthLongDim
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

        public int MinObjectLengthShortDim
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

        public void Operator<T>(uint numCrops,
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

            images.ThrowIfDisposed();
            rects.ThrowIfDisposed();

            using (var matrix = new Matrix<T>())
            using (var inImages = new StdVector<Matrix<T>>(images))
            using (var disposer = new EnumerableDisposer<StdVector<MModRect>>(rects.Select(r => new StdVector<MModRect>(r))))
            using (var inRects = new StdVector<StdVector<MModRect>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<MModRect>>(inRects))
            using (var outCrops = new StdVector<Matrix<T>>())
            using (var outCropRects = new StdVector<StdVector<MModRect>>())
            using (new EnumerableDisposer<StdVector<MModRect>>(outCropRects))
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

        public void Operator<T>(Matrix<T> image,
                                IEnumerable<MModRect> rects,
                                out Matrix<T> crop,
                                out IEnumerable<MModRect> cropRects)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            image.ThrowIfDisposed();
            rects.ThrowIfDisposed();

            using (var matrix = new Matrix<T>())
            using (var inRects = new StdVector<MModRect>(rects))
            using (var outCropRects = new StdVector<MModRect>())
            {
                var type = matrix.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.random_cropper_operator2(this.NativePtr,
                                                                 type,
                                                                 image.NativePtr,
                                                                 inRects.NativePtr,
                                                                 out var outCrop,
                                                                 outCropRects.NativePtr);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{type} is not supported.");

                crop = new Matrix<T>(outCrop, image.TemplateRows, image.TemplateColumns);
                cropRects = outCropRects.ToArray();
            }
        }

        public void SetChipDims(uint rows, uint cols)
        {
            this.ThrowIfDisposed();
            NativeMethods.random_cropper_set_chip_dims(this.NativePtr, rows, cols);
        }

        public void SetMinObjectSize(int longDim, int shortDim)
        {
            this.ThrowIfDisposed();

            if (!(0 < shortDim && shortDim <= longDim))
                throw new ArgumentOutOfRangeException();

            NativeMethods.random_cropper_set_min_object_size(this.NativePtr, longDim, shortDim);
        }

        public void SetSeed(long seed)
        {
            this.ThrowIfDisposed();

            // random_cropper_set_seed accept time_t as IntPtr
            // time_t is long on 32bit, other wise __int64 on 64bit
            NativeMethods.random_cropper_set_seed(this.NativePtr, new IntPtr(seed));
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

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            var str = "";

            try
            {
                ofstream = NativeMethods.ostringstream_new();
                NativeMethods.random_cropper_operator_left_shift(this.NativePtr, ofstream);
                stdstr = NativeMethods.ostringstream_str(ofstream);
                str = StringHelper.FromStdString(stdstr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (stdstr != IntPtr.Zero)
                    NativeMethods.string_delete(stdstr);
                if (ofstream != IntPtr.Zero)
                    NativeMethods.ostringstream_delete(ofstream);
            }

            return str;
        }

        #endregion

        #endregion

    }

}

#endif
