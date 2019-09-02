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

        public DnnTrainer(T net, Sgd sgd)
        {
            if (sgd == null)
                throw new ArgumentNullException(nameof(sgd));

            sgd.ThrowIfDisposed();

            this._Imp = CreateImp(net, sgd);

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

        public void SetMiniBatchSize(uint size)
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

        public void SetTestIterationsWithoutProgressThreshold(uint threshold)
        {
            this.ThrowIfDisposed();

            this._Imp.SetTestIterationsWithoutProgressThreshold(threshold);
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

        private static TrainerImp<T> CreateImp(T net, Sgd sgd)
        {
            var t = typeof(T);
            if (t == typeof(LossMetric))
                return new LossMetricTrainer(net.NativePtr, net.NetworkType, sgd) as TrainerImp<T>;
            if (t == typeof(LossMmod))
                return new LossMmodTrainer(net.NativePtr, net.NetworkType, sgd) as TrainerImp<T>;
            if (t == typeof(LossMulticlassLog))
                return new LossMulticlassLogTrainer(net.NativePtr, net.NetworkType, sgd) as TrainerImp<T>;
            if (t == typeof(LossMulticlassLogPerPixel))
                return new LossMulticlassLogPerPixelTrainer(net.NativePtr, net.NetworkType, sgd) as TrainerImp<T>;

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

            public abstract void SetTestIterationsWithoutProgressThreshold(uint thresh);

            public abstract string GetString();

            #region Overrids

            protected string ToString(Func<int, IntPtr, IntPtr, NativeMethods.ErrorType> func)
            {
                var ofstream = IntPtr.Zero;
                var stdstr = IntPtr.Zero;
                var str = "";

                try
                {
                    ofstream = NativeMethods.ostringstream_new();
                    var ret = func(this.NetworkType, this.NativePtr, ofstream);
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
                this.NativePtr = NativeMethods.LossMetric_trainer_new(type, net);
            }

            public LossMetricTrainer(IntPtr net, int type, Sgd sgd)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.LossMetric_trainer_new2(type, net, sgd.NativePtr);
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
                NativeMethods.LossMetric_trainer_be_verbose(this.NetworkType, this.NativePtr);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.LossMetric_trainer_delete(this.NetworkType, this.NativePtr);
            }

            public override LossMetric GetNet()
            {
                var err = NativeMethods.LossMetric_trainer_get_net(this.NetworkType, this.NativePtr, out var ret);
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
                NativeMethods.LossMetric_trainer_get_learning_rate(this.NetworkType, this.NativePtr, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.LossMetric_trainer_set_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.LossMetric_trainer_set_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.LossMetric_trainer_set_mini_batch_size(this.NetworkType, this.NativePtr, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.LossMetric_trainer_set_synchronization_file(this.NetworkType, this.NativePtr, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMetric_trainer_set_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetTestIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMetric_trainer_set_test_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override string GetString()
            {
                return base.ToString(NativeMethods.LossMetric_trainer_operator_left_shift);
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
                this.NativePtr = NativeMethods.LossMmod_trainer_new(type, net);
            }

            public LossMmodTrainer(IntPtr net, int type, Sgd sgd)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.LossMmod_trainer_new2(type, net, sgd.NativePtr);
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
                NativeMethods.LossMmod_trainer_be_verbose(this.NetworkType, this.NativePtr);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.LossMmod_trainer_delete(this.NetworkType, this.NativePtr);
            }

            public override LossMmod GetNet()
            {
                var err = NativeMethods.LossMmod_trainer_get_net(this.NetworkType, this.NativePtr, out var ret);
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
                NativeMethods.LossMmod_trainer_get_learning_rate(this.NetworkType, this.NativePtr, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.LossMmod_trainer_set_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.LossMmod_trainer_set_min_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.LossMmod_trainer_set_mini_batch_size(this.NetworkType, this.NativePtr, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.LossMmod_trainer_set_synchronization_file(this.NetworkType, this.NativePtr, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMmod_trainer_set_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetTestIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMmod_trainer_set_test_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override string GetString()
            {
                return base.ToString(NativeMethods.LossMmod_trainer_operator_left_shift);
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
                this.NativePtr = NativeMethods.LossMulticlassLog_trainer_new(type, net);
            }

            public LossMulticlassLogTrainer(IntPtr net, int type, Sgd sgd)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.LossMulticlassLog_trainer_new2(type, net, sgd.NativePtr);
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
                NativeMethods.LossMulticlassLog_trainer_be_verbose(this.NetworkType, this.NativePtr);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.LossMulticlassLog_trainer_delete(this.NetworkType, this.NativePtr);
            }

            public override LossMulticlassLog GetNet()
            {
                var err = NativeMethods.LossMulticlassLog_trainer_get_net(this.NetworkType, this.NativePtr, out var ret);
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
                NativeMethods.LossMulticlassLog_trainer_get_learning_rate(this.NetworkType, this.NativePtr, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.LossMulticlassLog_trainer_set_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.LossMulticlassLog_trainer_set_min_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.LossMulticlassLog_trainer_set_mini_batch_size(this.NetworkType, this.NativePtr, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.LossMulticlassLog_trainer_set_synchronization_file(this.NetworkType, this.NativePtr, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMulticlassLog_trainer_set_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetTestIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMulticlassLog_trainer_set_test_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override string GetString()
            {
                return base.ToString(NativeMethods.LossMulticlassLog_trainer_operator_left_shift);
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
                this.NativePtr = NativeMethods.LossMulticlassLogPerPixel_trainer_new(type, net);
            }

            public LossMulticlassLogPerPixelTrainer(IntPtr net, int type, Sgd sgd)
            {
                this.NetworkType = type;
                this.NativePtr = NativeMethods.LossMulticlassLogPerPixel_trainer_new2(type, net, sgd.NativePtr);
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

                NativeMethods.LossMulticlassLogPerPixel_trainer_delete(this.NetworkType, this.NativePtr);
            }

            public override void BeVerbose()
            {
                NativeMethods.LossMulticlassLogPerPixel_trainer_be_verbose(this.NetworkType, this.NativePtr);
            }

            public override LossMulticlassLogPerPixel GetNet()
            {
                var err = NativeMethods.LossMulticlassLogPerPixel_trainer_get_net(this.NetworkType, this.NativePtr, out var ret);
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
                NativeMethods.LossMulticlassLogPerPixel_trainer_get_learning_rate(this.NetworkType, this.NativePtr, out var learningRate);
                return learningRate;
            }

            public override void SetLearningRate(double learningRate)
            {
                NativeMethods.LossMulticlassLogPerPixel_trainer_set_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinLearningRate(double learningRate)
            {
                NativeMethods.LossMulticlassLogPerPixel_trainer_set_min_learning_rate(this.NetworkType, this.NativePtr, learningRate);
            }

            public override void SetMinBatchSize(uint size)
            {
                NativeMethods.LossMulticlassLogPerPixel_trainer_set_mini_batch_size(this.NetworkType, this.NativePtr, size);
            }

            public override void SetSynchronizationFile(string filename, uint second = 900)
            {
                var str = Dlib.Encoding.GetBytes(filename);
                var ret = NativeMethods.LossMulticlassLogPerPixel_trainer_set_synchronization_file(this.NetworkType, this.NativePtr, str, second);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMulticlassLogPerPixel_trainer_set_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override void SetTestIterationsWithoutProgressThreshold(uint thresh)
            {
                var ret = NativeMethods.LossMulticlassLogPerPixel_trainer_set_test_iterations_without_progress_threshold(this.NetworkType, this.NativePtr, thresh);
                if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                    throw new NotSupportNetworkTypeException(this.NetworkType);
            }

            public override string GetString()
            {
                return base.ToString(NativeMethods.LossMulticlassLogPerPixel_trainer_operator_left_shift);
            }

            #endregion

            #endregion

        }

    }

}
