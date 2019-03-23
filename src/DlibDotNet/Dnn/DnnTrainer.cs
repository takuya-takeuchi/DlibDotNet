using System;

namespace DlibDotNet.Dnn
{

    public sealed class DnnTrainer<T> : DlibObject
        where T : Net
    {

        #region Fields

        private readonly TrainerImp<T> _Imp;

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

        public T GetNet()
        {
            this.ThrowIfDisposed();

            return this._Imp.GetNet();
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
        
        public override string ToString()
        {
            this.ThrowIfDisposed();

            return this._Imp.GetString();
        }

        #endregion

        #region Helpers

        private static TrainerImp<T> CreateImp(T net)
        {
            var t = typeof(T);
            if (t == typeof(LossMetric))
                return new LossMetricTrainer(net.NativePtr, net.NetworkType) as TrainerImp<T>;
            if (t == typeof(LossMmod))
                return new LossMmodTrainer(net.NativePtr, net.NetworkType) as TrainerImp<T>;
            if (t == typeof(LossMulticlassLog))
                return new LossMulticlassLogTrainer(net.NativePtr, net.NetworkType) as TrainerImp<T>;
            if (t == typeof(LossMulticlassLogPerPixel))
                return new LossMulticlassLogPerPixelTrainer(net.NativePtr, net.NetworkType) as TrainerImp<T>;

            throw new NotSupportedException();
        }

        #endregion

        #endregion

#pragma warning disable 693
        private abstract class TrainerImp<T> : DlibObject
#pragma warning restore 693
            where T : Net
        {

            #region Properties

            public abstract int NetworkType
            {
                get;
            }

            #endregion

            #region Methods

            public abstract void BeVerbose();

            public abstract T GetNet();

            public abstract double GetLearningRate();

            public abstract void SetLearningRate(double learningRate);

            public abstract void SetMinLearningRate(double learningRate);

            public abstract void SetMinBatchSize(uint size);

            public abstract void SetSynchronizationFile(string filename, uint second = 15 * 60);

            public abstract void SetIterationsWithoutProgressThreshold(uint thresh);

            public abstract string GetString();

            #region Overrids

            protected string ToString(Func<IntPtr, int, IntPtr, NativeMethods.ErrorType> func)
            {
                var ofstream = IntPtr.Zero;
                var stdstr = IntPtr.Zero;
                var str = "";

                try
                {
                    ofstream = NativeMethods.ostringstream_new();
                    var ret = func(this.NativePtr, this.NetworkType, ofstream);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.OK:
                            stdstr = NativeMethods.ostringstream_str(ofstream);
                            str = StringHelper.FromStdString(stdstr);
                            break;
                        case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                            throw new NotSupportNetworkTypeException(this.NetworkType);
                    }
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

        private sealed class LossMetricTrainer : TrainerImp<LossMetric>
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

            public override LossMetric GetNet()
            {
                var err = NativeMethods.dnn_trainer_loss_metric_get_net(this.NativePtr, this.NetworkType, out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                        throw new NotSupportNetworkTypeException(this.NetworkType);
                    case NativeMethods.ErrorType.DnnPropagateException:
                        throw new PropagateException();
                }

                return new LossMetric(ret, this.NetworkType, false);
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

            public override string GetString()
            {
                return base.ToString(NativeMethods.dnn_trainer_loss_metric_operator_left_shift);
            }

            #endregion

            #endregion

        }

        private sealed class LossMmodTrainer : TrainerImp<LossMmod>
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

            public override LossMmod GetNet()
            {
                var err = NativeMethods.dnn_trainer_loss_mmod_get_net(this.NativePtr, this.NetworkType, out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                        throw new NotSupportNetworkTypeException(this.NetworkType);
                    case NativeMethods.ErrorType.DnnPropagateException:
                        throw new PropagateException();
                }

                return new LossMmod(ret, this.NetworkType, false);
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

            public override string GetString()
            {
                return base.ToString(NativeMethods.dnn_trainer_loss_mmod_operator_left_shift);
            }

            #endregion

            #endregion

        }

        private sealed class LossMulticlassLogTrainer : TrainerImp<LossMulticlassLog>
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

            public override LossMulticlassLog GetNet()
            {
                var err = NativeMethods.dnn_trainer_loss_multiclass_log_get_net(this.NativePtr, this.NetworkType, out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                        throw new NotSupportNetworkTypeException(this.NetworkType);
                    case NativeMethods.ErrorType.DnnPropagateException:
                        throw new PropagateException();
                }

                return new LossMulticlassLog(ret, this.NetworkType, false);
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

            public override string GetString()
            {
                return base.ToString(NativeMethods.dnn_trainer_loss_multiclass_log_operator_left_shift);
            }

            #endregion

            #endregion

        }

        private sealed class LossMulticlassLogPerPixelTrainer : TrainerImp<LossMulticlassLogPerPixel>
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

            public override LossMulticlassLogPerPixel GetNet()
            {
                var err = NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_get_net(this.NativePtr, this.NetworkType, out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                        throw new NotSupportNetworkTypeException(this.NetworkType);
                    case NativeMethods.ErrorType.DnnPropagateException:
                        throw new PropagateException();
                }

                return new LossMulticlassLogPerPixel(ret, this.NetworkType, false);
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

            public override string GetString()
            {
                return base.ToString(NativeMethods.dnn_trainer_loss_multiclass_log_per_pixel_operator_left_shift);
            }

            #endregion

            #endregion

        }

    }

}
