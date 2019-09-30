using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet.Extensions;
using OutputLabelType = System.UInt32;

namespace DlibDotNet.Dnn
{

    /// <summary>
    /// Represents a loss layer for a deep neural network for the multiclass logistic regression loss (e.g. negative log-likelihood loss), which is appropriate for multiclass classification problems. In particular, this class handles scalar outputs. This class cannot be inherited.
    /// </summary>
    public sealed class LossMulticlassLog : Net
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LossMulticlassLog"/> class with a specified network type of deep neural network.
        /// </summary>
        /// <param name="networkType">The network type.</param>
        public LossMulticlassLog(int networkType = 0)
        : base(networkType)
        {
            var ret = NativeMethods.LossMulticlassLog_new(networkType, out var net);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(networkType);

            this.NativePtr = net;
        }

        internal LossMulticlassLog(IntPtr ptr, int networkType = 0, bool isEnabledDispose = true)
            : base(networkType, isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public override int NumLayers
        {
            get
            {
                this.ThrowIfDisposed();

                return NativeMethods.LossMulticlassLog_get_num_layers(this.NetworkType);
            }
        }

        #endregion

        #region Methods

        public override void Clean()
        {
            this.ThrowIfDisposed();

            NativeMethods.LossMulticlassLog_clean(this.NetworkType, this.NativePtr);
        }

        public static LossMulticlassLog Deserialize(string path, int networkType = 0)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Dlib.Encoding.GetBytes(path);
            var error = NativeMethods.LossMulticlassLog_deserialize(networkType,
                                                                    str,
                                                                    out var net,
                                                                    out var errorMessage);
            Cuda.ThrowCudaException(error);
            switch (error)
            {
                case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                    throw new NotSupportNetworkTypeException(networkType);
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
            }

            return new LossMulticlassLog(net, networkType);
        }

        public static LossMulticlassLog Deserialize(ProxyDeserialize deserialize, int networkType = 0)
        {
            if (deserialize == null)
                throw new ArgumentNullException(nameof(deserialize));

            deserialize.ThrowIfDisposed();

            var error = NativeMethods.LossMulticlassLog_deserialize_proxy(networkType,
                                                                          deserialize.NativePtr,
                                                                          out var net,
                                                                          out var errorMessage);
            Cuda.ThrowCudaException(error);
            switch (error)
            {
                case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                    throw new NotSupportNetworkTypeException(networkType);
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
            }

            return new LossMulticlassLog(net, networkType);
        }

        public string[] GetLabels()
        {
            this.ThrowIfDisposed();

            NativeMethods.LossMulticlassLog_get_label(this.NetworkType, this.NativePtr, out var label);
            if (label == IntPtr.Zero)
                return null;

            using (var vector = new StdVector<StdString>(label))
            {
                var tmp = vector.ToArray();
                var ret = tmp.Select(s => s.ToString()).ToArray();
                foreach (var s in tmp) s.Dispose();
                return ret;
            }
        }

        public LossDetails GetLossDetails()
        {
            this.ThrowIfDisposed();

            NativeMethods.LossMulticlassLog_get_loss_details(this.NetworkType, this.NativePtr, out var lossDetails);
            return new LossDetails(this, lossDetails);
        }

        public Subnet GetSubnet()
        {
            this.ThrowIfDisposed();

            return new Subnet(this);
        }

        internal override DPoint InputTensorToOutputTensor(DPoint p)
        {
            // fc layer does not support
            throw new NotSupportedException();
        }

        internal override void NetToXml(string filename)
        {
            this.ThrowIfDisposed();

            var fileNameByte = Dlib.Encoding.GetBytes(filename);
            NativeMethods.LossMulticlassLog_net_to_xml(this.NetworkType, this.NativePtr, fileNameByte);
        }

        public OutputLabels<OutputLabelType> Operator<T>(Matrix<T> image, ulong batchSize = 128)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            return this.Operator(new[] { image }, batchSize);
        }

        public OutputLabels<OutputLabelType> Operator<T>(IEnumerable<Matrix<T>> images, ulong batchSize = 128)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (!images.Any())
                throw new ArgumentException();
            if (images.Any(matrix => matrix == null))
                throw new ArgumentException();

            images.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var imageType);
            var templateRows = images.First().TemplateRows;
            var templateColumns = images.First().TemplateColumns;

            var array = images.Select(matrix => matrix.NativePtr).ToArray();
            var ret = NativeMethods.LossMulticlassLog_operator_matrixs(this.NetworkType,
                                                                       this.NativePtr,
                                                                       imageType.ToNativeMatrixElementType(),
                                                                       array,
                                                                       array.Length,
                                                                       templateRows,
                                                                       templateColumns,
                                                                       (uint)batchSize,
                                                                       out var vecOut);

            Cuda.ThrowCudaException(ret);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{imageType} is not supported.");
            }

            return new Output(vecOut);
        }

        public IEnumerable<float[]> Probability<T>(Matrix<T> image, ulong batchSize = 128)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            return this.Probability(new[] { image }, batchSize);
        }

        public IEnumerable<float[]> Probability<T>(IEnumerable<Matrix<T>> images, ulong batchSize = 128)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (!images.Any())
                throw new ArgumentException();
            if (images.Any(matrix => matrix == null))
                throw new ArgumentException();

            images.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var imageType);
            var templateRows = images.First().TemplateRows;
            var templateColumns = images.First().TemplateColumns;

            var array = images.Select(matrix => matrix.NativePtr).ToArray();
            var ret = NativeMethods.LossMulticlassLog_probability(this.NetworkType,
                                                                  this.NativePtr,
                                                                  imageType.ToNativeMatrixElementType(),
                                                                  array,
                                                                  array.Length,
                                                                  templateRows,
                                                                  templateColumns,
                                                                  (uint)batchSize,
                                                                  out var vecOut);

            using (var vector = new StdVector<float>(vecOut))
            {
                Cuda.ThrowCudaException(ret);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{imageType} is not supported.");
                }

                var probabilityArray = vector.ToArray();
                var batches = probabilityArray.Length;
                var classes = probabilityArray.Length / batches;

                var probability = new List<float[]>(Enumerable.Range(0, probabilityArray.Length).Select<int, float[]>(i => null));
                for (var index = 0; index < batches; index++)
                    probability[index] = probabilityArray.Skip(index * classes).Take(classes).ToArray();

                return probability;
            }
        }

        public static void Serialize(LossMulticlassLog net, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            net.ThrowIfDisposed();

            var str = Dlib.Encoding.GetBytes(path);
            var error = NativeMethods.LossMulticlassLog_serialize(net.NetworkType, net.NativePtr, str, out var errorMessage);
            switch (error)
            {
                case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                    throw new NotSupportNetworkTypeException(net.NetworkType);
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
            }
        }

        public override bool TryGetInputLayer<T>(T layer)
        {
            throw new NotSupportedException();
        }

        public static void TestOneStep<T>(DnnTrainer<LossMulticlassLog> trainer, IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            where T : struct
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            Matrix<T>.TryParse<T>(out var dataElementTypes);

            using (var dataVec = new StdVector<Matrix<T>>(data))
            using (var labelVec = new StdVector<uint>(label))
            {
                var ret = NativeMethods.LossMulticlassLog_trainer_test_one_step(trainer.Type,
                                                                                trainer.NativePtr,
                                                                                dataElementTypes.ToNativeMatrixElementType(),
                                                                                dataVec.NativePtr,
                                                                                NativeMethods.MatrixElementType.UInt32,
                                                                                labelVec.NativePtr);
                Cuda.ThrowCudaException(ret);

                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new NotSupportedException($"{dataElementTypes} does not support");
                }
            }
        }

        /// <summary>
        /// Trains a supervised neural network based on the given training data.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="trainer">The trainer object of <see cref="LossMulticlassLog"/>.</param>
        /// <param name="data">The training data.</param>
        /// <param name="label">The label.</param>
        /// <exception cref="ArgumentNullException"><paramref name="trainer"/>, <paramref name="data"/> or <paramref name="label"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="trainer"/> is disposed.</exception>
        /// <exception cref="NotSupportedException">The specified type of element in the matrix does not supported.</exception>
        public static void Train<T>(DnnTrainer<LossMulticlassLog> trainer, IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            where T : struct
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            trainer.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var dataElementTypes);

            using (var dataVec = new StdVector<Matrix<T>>(data))
            using (var labelVec = new StdVector<uint>(label))
            {
                var ret = NativeMethods.LossMulticlassLog_trainer_train(trainer.Type,
                                                                        trainer.NativePtr,
                                                                        dataElementTypes.ToNativeMatrixElementType(),
                                                                        dataVec.NativePtr,
                                                                        NativeMethods.MatrixElementType.UInt32,
                                                                        labelVec.NativePtr);
                Cuda.ThrowCudaException(ret);

                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new NotSupportedException($"{dataElementTypes} does not support");
                }
            }
        }

        /// <summary>
        /// Performs one stochastic gradient update step based on the mini-batch of data and labels supplied.
        /// </summary>
        /// <typeparam name="T">The type of element in the matrix.</typeparam>
        /// <param name="trainer">The trainer object of <see cref="LossMulticlassLog"/>.</param>
        /// <param name="data">The training data.</param>
        /// <param name="label">The label.</param>
        /// <exception cref="ArgumentNullException"><paramref name="trainer"/>, <paramref name="data"/> or <paramref name="label"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="trainer"/> is disposed.</exception>
        /// <exception cref="NotSupportedException">The specified type of element in the matrix does not supported.</exception>
        public static void TrainOneStep<T>(DnnTrainer<LossMulticlassLog> trainer, IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
            where T : struct
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            trainer.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var dataElementTypes);

            using (var dataVec = new StdVector<Matrix<T>>(data))
            using (var labelVec = new StdVector<uint>(label))
            {
                var ret = NativeMethods.LossMulticlassLog_trainer_train_one_step(trainer.Type,
                                                                                 trainer.NativePtr,
                                                                                 dataElementTypes.ToNativeMatrixElementType(),
                                                                                 dataVec.NativePtr,
                                                                                 NativeMethods.MatrixElementType.UInt32,
                                                                                 labelVec.NativePtr);
                Cuda.ThrowCudaException(ret);

                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new NotSupportedException($"{dataElementTypes} does not support");
                }
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

            NativeMethods.LossMulticlassLog_delete(this.NetworkType, this.NativePtr);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            var str = "";

            try
            {
                ofstream = NativeMethods.ostringstream_new();
                var ret = NativeMethods.LossMulticlassLog_operator_left_shift(this.NetworkType, this.NativePtr, ofstream);
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

        public sealed class Subnet : DlibObject
        {

            #region Fields

            private readonly LossMulticlassLog _Parent;

            #endregion

            #region Constructors

            internal Subnet(LossMulticlassLog parent)
                : base(false)
            {
                if (parent == null)
                    throw new ArgumentNullException(nameof(parent));

                parent.ThrowIfDisposed();

                this._Parent = parent;

                var err = NativeMethods.LossMulticlassLog_subnet(parent.NetworkType, parent.NativePtr, out var ret);
                this.NativePtr = ret;
            }

            #endregion

            #region Properties

            public Tensor Output
            {
                get
                {
                    this._Parent.ThrowIfDisposed();
                    var tensor = NativeMethods.LossMulticlassLog_subnet_get_output(this._Parent.NetworkType, this.NativePtr, out var ret);
                    return new Tensor(tensor);
                }
            }

            #endregion

            #region Methods

            public LayerDetails GetLayerDetails()
            {
                this._Parent.ThrowIfDisposed();
                var ret = NativeMethods.LossMulticlassLog_subnet_get_layer_details( this._Parent.NetworkType, this.NativePtr, out var value);
                return new LayerDetails(this._Parent, value);
            }

            #region Overrids

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.LossMulticlassLog_subnet_delete(this._Parent.NetworkType, this.NativePtr);
            }

            #endregion

            #endregion

        }

        public sealed class LayerDetails : DlibObject
        {

            #region Fields

            private readonly LossMulticlassLog _Parent;

            #endregion

            #region Constructors

            internal LayerDetails(LossMulticlassLog parent, IntPtr ptr)
                : base(false)
            {
                if (parent == null)
                    throw new ArgumentNullException(nameof(parent));

                parent.ThrowIfDisposed();

                this._Parent = parent;
                this.NativePtr = ptr;
            }

            #endregion

            #region Methods

            public void SetNumFilters(int num)
            {
                this._Parent.ThrowIfDisposed();
                var ret = NativeMethods.LossMulticlassLog_layer_details_set_num_filters(this._Parent.NetworkType, this.NativePtr, num);
                switch (ret)
                {
                    case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                        throw new NotSupportNetworkTypeException(this._Parent.NetworkType);
                }
            }

            #region Overrids

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                //NativeMethods.loss_metric_subnet_delete(this._Parent.NetworkType, this.NativePtr);
            }

            #endregion

            #endregion

        }

        public sealed class LossDetails : DlibObject
        {

            #region Fields

            private readonly LossMulticlassLog _Parent;

            #endregion

            #region Constructors

            internal LossDetails(LossMulticlassLog parent, IntPtr ptr)
                : base(false)
            {
                if (parent == null)
                    throw new ArgumentNullException(nameof(parent));

                parent.ThrowIfDisposed();

                this._Parent = parent;
                this.NativePtr = ptr;
            }

            #endregion

        }

        private sealed class Output : OutputLabels<OutputLabelType>
        {

            #region Fields

            private readonly int _Size;

            #endregion

            #region Constructors

            internal Output(IntPtr output) :
                base(output)
            {
                this._Size = NativeMethods.dnn_output_uint32_t_getSize(output);
            }

            #endregion

            #region Properties

            public override int Count
            {
                get
                {
                    this.ThrowIfDisposed();
                    return this._Size;
                }
            }

            public override OutputLabelType this[int index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(0 <= index && index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    return NativeMethods.dnn_output_uint32_t_getItem(this.NativePtr, index);
                }
            }

            public override OutputLabelType this[uint index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    return NativeMethods.dnn_output_uint32_t_getItem(this.NativePtr, (int)index);
                }
            }

            #endregion

            #region Methods

            #region Overrides

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.dnn_output_uint32_t_delete(this.NativePtr);
            }

            #endregion

            #endregion

            #region IEnumerable<TItem> Members

            public override IEnumerator<OutputLabelType> GetEnumerator()
            {
                this.ThrowIfDisposed();

                for (var index = 0; index < this._Size; index++)
                    yield return NativeMethods.dnn_output_uint32_t_getItem(this.NativePtr, index);
            }

            #endregion

        }

    }

}