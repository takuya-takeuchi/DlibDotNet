#if !LITE
using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ShapePredictorTrainer : DlibObject
    {

        #region Constructors

        public ShapePredictorTrainer()
        {
            this.NativePtr = NativeMethods.shape_predictor_trainer_new();
        }

        #endregion

        #region Properties

        public uint CascadeDepth
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_cascade_depth(this.NativePtr);
            }
            set
            {
                if (!(value > 0))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_cascade_depth(this.NativePtr, value);
            }
        }

        public double FeaturePoolRegionPadding
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_feature_pool_region_padding(this.NativePtr);
            }
            set
            {
                if (!(value > -0.5))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_feature_pool_region_padding(this.NativePtr, value);
            }
        }

        public uint FeaturePoolSize
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_feature_pool_size(this.NativePtr);
            }
            set
            {
                if (!(value > 1))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_feature_pool_size(this.NativePtr, value);
            }
        }

        public double Lambda
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_lambda(this.NativePtr);
            }
            set
            {
                if (!(value > 0))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_lambda(this.NativePtr, value);
            }
        }

        public double Nu
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_nu(this.NativePtr);
            }
            set
            {
                if (!(0 < value && value <= 1))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_nu(this.NativePtr, value);
            }
        }

        public uint NumTreesPerCascadeDepth
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_num_trees_per_cascade_level(this.NativePtr);
            }
            set
            {
                if (!(value > 0))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_num_trees_per_cascade_level(this.NativePtr, value);
            }
        }

        public uint NumTestSplits
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_num_test_splits(this.NativePtr);
            }
            set
            {
                if (!(value > 0))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_num_test_splits(this.NativePtr, value);
            }
        }

        public uint NumThreads
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_num_threads(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_num_threads(this.NativePtr, value);
            }
        }

        public uint OverSamplingAmount
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_oversampling_amount(this.NativePtr);
            }
            set
            {
                if (!(value > 0))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_oversampling_amount(this.NativePtr, value);
            }
        }

        public PaddingMode PaddingMode
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_padding_mode(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_padding_mode(this.NativePtr, value);
            }
        }

        public double OverSamplingTranslationJitter
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_oversampling_translation_jitter(this.NativePtr);
            }
            set
            {
                if (!(value >= 0))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_oversampling_translation_jitter(this.NativePtr, value);
            }
        }

        public string RandomSeed
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.shape_predictor_trainer_get_random_seed(this.NativePtr);
                return StringHelper.FromStdString(ret, true);
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                this.ThrowIfDisposed();
                using (var str = new StdString(value))
                    NativeMethods.shape_predictor_trainer_set_random_seed(this.NativePtr, str.NativePtr);
            }
        }

        public uint TreeDepth
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.shape_predictor_trainer_get_tree_depth(this.NativePtr);
            }
            set
            {
                if (!(value > 0))
                    throw new ArgumentOutOfRangeException();
                this.ThrowIfDisposed();
                NativeMethods.shape_predictor_trainer_set_tree_depth(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        public void BeQuiet()
        {
            this.ThrowIfDisposed();
            NativeMethods.shape_predictor_trainer_be_quiet(this.NativePtr);
        }

        public void BeVerbose()
        {
            this.ThrowIfDisposed();
            NativeMethods.shape_predictor_trainer_be_verbose(this.NativePtr);
        }

        public ShapePredictor Train<T>(Array<Array2D<T>> images, IEnumerable<IList<FullObjectDetection>> objects)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            this.ThrowIfDisposed();

            images.ThrowIfDisposed();

            var tmpObjects = objects.ToArray();
            tmpObjects.ThrowIfDisposed();

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            using (var disposer = new EnumerableDisposer<StdVector<FullObjectDetection>>(tmpObjects.Select(r => new StdVector<FullObjectDetection>(r))))
            using (var objectsVector = new StdVector<StdVector<FullObjectDetection>>(disposer.Collection))
            {
                var ret = NativeMethods.shape_predictor_trainer_train(this.NativePtr,
                                                                      type.ToNativeArray2DType(),
                                                                      images.NativePtr,
                                                                      objectsVector.NativePtr,
                                                                      out var predictor);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {type} is not supported.");
                }

                return new ShapePredictor(predictor);
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

            NativeMethods.shape_predictor_trainer_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}

#endif
