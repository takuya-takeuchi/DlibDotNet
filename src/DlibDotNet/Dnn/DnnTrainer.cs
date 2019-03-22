using System;
using System.Collections.Generic;
using System.Text;
using DlibDotNet.Extensions;

namespace DlibDotNet.Dnn
{

    public sealed class DnnTrainer<T> : DlibObject
        where T : Net
    {

        #region Fields

        private readonly TrainerImp _Imp;

        #endregion

        #region Constructors

        public DnnTrainer(T net)
        {
            this._Imp = CreateImp(net);

            this.NativePtr = this._Imp.NativePtr;
        }

        #endregion

        #region Properties

        internal int Type
        {
            get
            {
                this.ThrowIfDisposed();

                return this._Imp.NetworkType;
            }
        }

        #endregion

        #region Methods

        public void BeVerbose()
        {
            this.ThrowIfDisposed();

            this._Imp.BeVerbose();
        }

        public double GetLearningRate()
        {
            this.ThrowIfDisposed();

            return this._Imp.GetLearningRate();
        }

        public void SetLearningRate(double learningRate)
        {
            this.ThrowIfDisposed();

            this._Imp.SetLearningRate(learningRate);
        }

        public void SetMinLearningRate(double learningRate)
        {
            this.ThrowIfDisposed();

            this._Imp.SetMinLearningRate(learningRate);
        }

        public void SetMinBatchSize(uint size)
        {
            this.ThrowIfDisposed();

            this._Imp.SetMinBatchSize(size);
        }

        public void SetSynchronizationFile(string filename, uint second = 15 * 60)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException();

            this.ThrowIfDisposed();

            this._Imp.SetSynchronizationFile(filename, second);
        }

        public void SetIterationsWithoutProgressThreshold(uint threshold)
        {
            this.ThrowIfDisposed();

            this._Imp.SetIterationsWithoutProgressThreshold(threshold);
        }

        public void Train<U>(IEnumerable<Matrix<U>> data, IEnumerable<uint> label)
            where U : struct
        {
            this.ThrowIfDisposed();

            this._Imp.Train(data, label);
        }

        public void TrainOneStep<U>(IEnumerable<Matrix<U>> data, IEnumerable<uint> label)
            where U : struct
        {
            this.ThrowIfDisposed();

            this._Imp.TrainOneStep(data, label);
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

            this._Imp.Dispose();
        }

        #endregion

        #region Helpers

        private static TrainerImp CreateImp(T net)
        {
            var t = typeof(T);
            if (t == typeof(LossMetric))
                return new LossMetricTrainer(net.NativePtr, net.NetworkType);
            if (t == typeof(LossMmod))
                return new LossMmodTrainer(net.NativePtr, net.NetworkType);
            if (t == typeof(LossMulticlassLog))
                return new LossMulticlassLogTrainer(net.NativePtr, net.NetworkType);
            if (t == typeof(LossMulticlassLogPerPixel))
                return new LossMulticlassLogPerPixelTrainer(net.NativePtr, net.NetworkType);

            throw new NotSupportedException();
        }

        #endregion

        #endregion

        private abstract class TrainerImp : DlibObject
        {

            public abstract int NetworkType
            {
                get;
            }

            public abstract void BeVerbose();

            public abstract double GetLearningRate();

            public abstract void SetLearningRate(double learningRate);

            public abstract void SetMinLearningRate(double learningRate);

            public abstract void SetMinBatchSize(uint size);

            public abstract void SetSynchronizationFile(string filename, uint second = 15 * 60);

            public abstract void SetIterationsWithoutProgressThreshold(uint thresh);

            public abstract void Train<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
                where T : struct;

            public abstract void TrainOneStep<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
                where T : struct;

        }
        // set_iterations_without_progress_threshold
        private sealed class LossMetricTrainer : TrainerImp
        {

            #region Constructors

            public LossMetricTrainer(IntPtr net, int type)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.dnn_trainer_loss_metric_new(net, type);
            }

            #endregion

            #region Methods

            #region Overrids

            public override int NetworkType
            {
                get;
            }

            public override void BeVerbose()
            {
                NativeMethods.dnn_trainer_loss_metric_be_verbose(this.NativePtr, this.NetworkType);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.dnn_trainer_loss_metric_delete(this.NativePtr, this.NetworkType);
            }

            public override double GetLearningRate()
            {
                NativeMethods.dnn_trainer_loss_metric_get_learning_rate(this.NativePtr, this.NetworkType, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_metric_set_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_metric_set_min_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.dnn_trainer_loss_metric_set_mini_batch_size(this.NativePtr, this.NetworkType, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.dnn_trainer_loss_metric_set_synchronization_file(this.NativePtr, this.NetworkType, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.dnn_trainer_loss_metric_set_iterations_without_progress_threshold(this.NativePtr, this.NetworkType, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void Train<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_metric_train(this.NativePtr,
                                                                          this.NetworkType,
                                                                          dataElementTypes.ToNativeMatrixElementType(),
                                                                          dataVec.NativePtr,
                                                                          NativeMethods.MatrixElementType.UInt32,
                                                                          labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            public override void TrainOneStep<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_metric_train_one_step(this.NativePtr,
                                                                                   this.NetworkType,
                                                                                   dataElementTypes.ToNativeMatrixElementType(),
                                                                                   dataVec.NativePtr,
                                                                                   NativeMethods.MatrixElementType.UInt32,
                                                                                   labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            #endregion

            #endregion

        }

        private sealed class LossMmodTrainer : TrainerImp
        {

            #region Constructors

            public LossMmodTrainer(IntPtr net, int type)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.dnn_trainer_loss_mmod_new(net, type);
            }

            #endregion

            #region Methods

            #region Overrids

            public override int NetworkType
            {
                get;
            }

            public override void BeVerbose()
            {
                NativeMethods.dnn_trainer_loss_mmod_be_verbose(this.NativePtr, this.NetworkType);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.dnn_trainer_loss_mmod_delete(this.NativePtr, this.NetworkType);
            }

            public override double GetLearningRate()
            {
                NativeMethods.dnn_trainer_loss_mmod_get_learning_rate(this.NativePtr, this.NetworkType, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_mmod_set_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_mmod_set_min_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.dnn_trainer_loss_mmod_set_mini_batch_size(this.NativePtr, this.NetworkType, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.dnn_trainer_loss_mmod_set_synchronization_file(this.NativePtr, this.NetworkType, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.dnn_trainer_loss_mmod_set_iterations_without_progress_threshold(this.NativePtr, this.NetworkType, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void Train<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_mmod_train(this.NativePtr,
                                                                        this.NetworkType,
                                                                        dataElementTypes.ToNativeMatrixElementType(),
                                                                        dataVec.NativePtr,
                                                                        NativeMethods.MatrixElementType.UInt32,
                                                                        labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            public override void TrainOneStep<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_mmod_train_one_step(this.NativePtr,
                                                                                 this.NetworkType,
                                                                                 dataElementTypes.ToNativeMatrixElementType(),
                                                                                 dataVec.NativePtr,
                                                                                 NativeMethods.MatrixElementType.UInt32,
                                                                                 labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            #endregion

            #endregion

        }

        private sealed class LossMulticlassLogTrainer : TrainerImp
        {

            #region Constructors

            public LossMulticlassLogTrainer(IntPtr net, int type)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.dnn_trainer_loss_multiclass_log_new(net, type);
            }

            #endregion

            #region Methods

            #region Overrids

            public override int NetworkType
            {
                get;
            }

            public override void BeVerbose()
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_be_verbose(this.NativePtr, this.NetworkType);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.dnn_trainer_loss_multiclass_log_delete(this.NativePtr, this.NetworkType);
            }

            public override double GetLearningRate()
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_get_learning_rate(this.NativePtr, this.NetworkType, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_set_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_set_min_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_set_mini_batch_size(this.NativePtr, this.NetworkType, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.dnn_trainer_loss_multiclass_log_set_synchronization_file(this.NativePtr, this.NetworkType, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.dnn_trainer_loss_multiclass_log_set_iterations_without_progress_threshold(this.NativePtr, this.NetworkType, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }
            
            public override void Train<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_multiclass_log_train(this.NativePtr,
                                                                                  this.NetworkType,
                                                                                  dataElementTypes.ToNativeMatrixElementType(),
                                                                                  dataVec.NativePtr,
                                                                                  NativeMethods.MatrixElementType.UInt32,
                                                                                  labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            public override void TrainOneStep<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_multiclass_log_train_one_step(this.NativePtr,
                                                                                           this.NetworkType,
                                                                                           dataElementTypes.ToNativeMatrixElementType(),
                                                                                           dataVec.NativePtr,
                                                                                           NativeMethods.MatrixElementType.UInt32,
                                                                                           labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            #endregion

            #endregion

        }

        private sealed class LossMulticlassLogPerPixelTrainer : TrainerImp
        {

            #region Constructors

            public LossMulticlassLogPerPixelTrainer(IntPtr net, int type)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_new(net, type);
            }

            #endregion

            #region Methods

            #region Overrids

            public override int NetworkType
            {
                get;
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_delete(this.NativePtr, this.NetworkType);
            }

            public override void BeVerbose()
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_be_verbose(this.NativePtr, this.NetworkType);
            }

            public override double GetLearningRate()
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_get_learning_rate(this.NativePtr, this.NetworkType, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_set_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_set_min_learning_rate(this.NativePtr, this.NetworkType, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_set_mini_batch_size(this.NativePtr, this.NetworkType, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_set_synchronization_file(this.NativePtr, this.NetworkType, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_set_iterations_without_progress_threshold(this.NativePtr, this.NetworkType, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void Train<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_train(this.NativePtr,
                                                                                            this.NetworkType,
                                                                                            dataElementTypes.ToNativeMatrixElementType(),
                                                                                            dataVec.NativePtr,
                                                                                            NativeMethods.MatrixElementType.UInt32,
                                                                                            labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            public override void TrainOneStep<T>(IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (label == null)
                    throw new ArgumentNullException(nameof(label));

                Matrix<T>.TryParse<T>(out var dataElementTypes);

                using (var dataVec = new StdVector<Matrix<T>>(data))
                using (var labelVec = new StdVector<uint>(label))
                {
                    var ret = NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_train_one_step(this.NativePtr,
                                                                                                     this.NetworkType,
                                                                                                     dataElementTypes.ToNativeMatrixElementType(),
                                                                                                     dataVec.NativePtr,
                                                                                                     NativeMethods.MatrixElementType.UInt32,
                                                                                                     labelVec.NativePtr);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new NotSupportedException($"{dataElementTypes} does not support");
                    }
                }
            }

            #endregion

            #endregion

        }

    }

}
