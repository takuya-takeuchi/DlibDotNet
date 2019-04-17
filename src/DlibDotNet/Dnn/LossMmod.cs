using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet.Extensions;

namespace DlibDotNet.Dnn
{

    /// <summary>
    /// Represents a loss layer for a deep neural network for the Max Margin Object Detection loss. This class cannot be inherited.
    /// </summary>
    public sealed class LossMmod : Net
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LossMmod"/> class with a specified network type of deep neural network.
        /// </summary>
        /// <param name="networkType">The network type.</param>
        public LossMmod(int networkType = 0)
            : base(networkType)
        {
            var ret = NativeMethods.loss_mmod_new(networkType, out var net);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(networkType);

            this.NativePtr = net;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LossMetric"/> class with a specified parameter and network type of deep neural network.
        /// </summary>
        /// <param name="options">The parameter.</param>
        /// <param name="networkType">The network type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="options"/> is disposed.</exception>
        public LossMmod(MModOptions options, int networkType = 0)
            : base(networkType)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.ThrowIfDisposed();

            var ret = NativeMethods.loss_mmod_new2(networkType, options.NativePtr, out var net);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(networkType);

            this.NativePtr = net;
        }

        internal LossMmod(IntPtr ptr, int networkType = 0, bool isEnabledDispose = true)
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

                return NativeMethods.loss_mmod_num_layers(this.NetworkType);
            }
        }

        #endregion

        #region Methods

        public override void Clean()
        {
            this.ThrowIfDisposed();

            NativeMethods.loss_mmod_clean(this.NetworkType);
        }

        public LossMmod CloneAs(int networkType)
        {
            this.ThrowIfDisposed();

            var ret = NativeMethods.loss_mmod_clone(this.NativePtr, this.NetworkType, networkType, out var net);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(networkType);

            return new LossMmod(net, networkType);
        }

        public static LossMmod Deserialize(string path, int networkType = 0)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Dlib.Encoding.GetBytes(path);
            var error = NativeMethods.loss_mmod_deserialize(str,
                                                            networkType,
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

            return new LossMmod(net, networkType);
        }

        public static LossMmod Deserialize(ProxyDeserialize deserialize, int networkType = 0)
        {
            if (deserialize == null)
                throw new ArgumentNullException(nameof(deserialize));

            deserialize.ThrowIfDisposed();

            var error = NativeMethods.loss_mmod_deserialize_proxy(deserialize.NativePtr,
                                                                  networkType, 
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

            return new LossMmod(net, networkType);
        }

        public Subnet GetSubnet()
        {
            this.ThrowIfDisposed();

            return new Subnet(this);
        }

        internal override DPoint InputTensorToOutputTensor(DPoint p)
        {
            using (var np = p.ToNative())
            {
                NativeMethods.loss_mmod_input_tensor_to_output_tensor(this.NativePtr, this.NetworkType, np.NativePtr, out var ret);
                return new DPoint(ret);
            }
        }

        public OutputLabels<IEnumerable<MModRect>> Operator<T>(Matrix<T> image, ulong batchSize = 128)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            return this.Operator(new[] { image }, batchSize);
        }

        public OutputLabels<IEnumerable<MModRect>> Operator<T>(IEnumerable<Matrix<T>> images, ulong batchSize = 128)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (!images.Any())
                throw new ArgumentException();
            if (images.Any(matrix => matrix == null))
                throw new ArgumentException();

            images.ThrowIfDisposed();

            using (var vecIn = new StdVector<Matrix<T>>(images))
            {
                Matrix<T>.TryParse<T>(out var imageType);
                var templateRows = images.First().TemplateRows;
                var templateColumns = images.First().TemplateColumns;

                // vecOut is not std::vector<std::vector<mmod_rect>*>* but std::vector<std::vector<mmod_rect>>*.
                var ret = NativeMethods.loss_mmod_operator_matrixs(this.NativePtr,
                                                                   this.NetworkType,
                                                                   imageType.ToNativeMatrixElementType(),
                                                                   vecIn.NativePtr,
                                                                   templateRows,
                                                                   templateColumns,
                                                                   batchSize,
                                                                   out var vecOut);

                Cuda.ThrowCudaException(ret);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{imageType} is not supported.");
                }

                return new Output(vecOut);
            }
        }

        public static void Serialize(LossMmod net, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            net.ThrowIfDisposed();

            var str = Dlib.Encoding.GetBytes(path);
            var error = NativeMethods.loss_mmod_serialize(net.NativePtr, net.NetworkType, str, out var errorMessage);
            switch (error)
            {
                case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                    throw new NotSupportNetworkTypeException(net.NetworkType);
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
            }
        }

        public static void TestOneStep<T>(DnnTrainer<LossMmod> trainer, IEnumerable<Matrix<T>> data, IEnumerable<IEnumerable<MModRect>> label)
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
            using (var disposer = new EnumerableDisposer<StdVector<MModRect>>(label.Select(r => new StdVector<MModRect>(r))))
            using (var labelVec = new StdVector<StdVector<MModRect>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<MModRect>>(labelVec))
            {
                var ret = NativeMethods.dnn_trainer_loss_mmod_test_one_step(trainer.NativePtr,
                                                                            trainer.Type,
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
        /// <param name="trainer">The trainer object of <see cref="LossMmod"/>.</param>
        /// <param name="data">The training data.</param>
        /// <param name="label">The label.</param>
        /// <exception cref="ArgumentNullException"><paramref name="trainer"/>, <paramref name="data"/> or <paramref name="label"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="trainer"/> is disposed.</exception>
        /// <exception cref="NotSupportedException">The specified type of element in the matrix does not supported.</exception>
        public static void Train<T>(DnnTrainer<LossMmod> trainer, IEnumerable<Matrix<T>> data, IEnumerable<IEnumerable<MModRect>> label)
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
            using (var disposer = new EnumerableDisposer<StdVector<MModRect>>(label.Select(r => new StdVector<MModRect>(r))))
            using (var labelVec = new StdVector<StdVector<MModRect>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<MModRect>>(labelVec))
            {
                var ret = NativeMethods.dnn_trainer_loss_mmod_train(trainer.NativePtr,
                                                                    trainer.Type,
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
        /// <param name="trainer">The trainer object of <see cref="LossMmod"/>.</param>
        /// <param name="data">The training data.</param>
        /// <param name="label">The label.</param>
        /// <exception cref="ArgumentNullException"><paramref name="trainer"/>, <paramref name="data"/> or <paramref name="label"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="trainer"/> is disposed.</exception>
        /// <exception cref="NotSupportedException">The specified type of element in the matrix does not supported.</exception>
        public static void TrainOneStep<T>(DnnTrainer<LossMmod> trainer, IEnumerable<Matrix<T>> data, IEnumerable<IEnumerable<MModRect>> label)
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
            using (var disposer = new EnumerableDisposer<StdVector<MModRect>>(label.Select(r => new StdVector<MModRect>(r))))
            using (var labelVec = new StdVector<StdVector<MModRect>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<MModRect>>(labelVec))
            {
                var ret = NativeMethods.dnn_trainer_loss_mmod_train_one_step(trainer.NativePtr,
                                                                             trainer.Type,
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

        public override bool TryGetInputLayer<T>(T layer)
        {
            this.ThrowIfDisposed();

            NativeMethods.loss_mmod_input_layer(this.NativePtr, this.NetworkType, out var ret);
            layer.NativePtr = ret;

            return true;
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

            NativeMethods.loss_mmod_delete(this.NativePtr, this.NetworkType);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            var str = "";

            try
            {
                ofstream = NativeMethods.ostringstream_new();
                var ret = NativeMethods.loss_mmod_operator_left_shift(this.NativePtr, this.NetworkType, ofstream);
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

            private readonly LossMmod _Parent;

            #endregion

            #region Constructors

            internal Subnet(LossMmod parent)
                : base(false)
            {
                if (parent == null)
                    throw new ArgumentNullException(nameof(parent));

                parent.ThrowIfDisposed();

                this._Parent = parent;

                var err = NativeMethods.loss_mmod_subnet(parent.NativePtr, parent.NetworkType, out var ret);
                this.NativePtr = ret;
            }

            #endregion

            #region Properties

            public Tensor Output
            {
                get
                {
                    this._Parent.ThrowIfDisposed();
                    var tensor = NativeMethods.loss_mmod_subnet_get_output(this.NativePtr, this._Parent.NetworkType, out var ret);
                    return new Tensor(tensor);
                }
            }

            #endregion

            #region Methods

            public LayerDetails GetLayerDetails()
            {
                this._Parent.ThrowIfDisposed();
                var ret = NativeMethods.loss_mmod_subnet_get_layer_details(this.NativePtr, this._Parent.NetworkType, out _);
                return new LayerDetails(this._Parent, ret);
            }

            #region Overrids

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.loss_mmod_subnet_delete(this._Parent.NetworkType, this.NativePtr);
            }

            #endregion

            #endregion

        }

        public sealed class LayerDetails : DlibObject
        {

            #region Fields

            private readonly LossMmod _Parent;

            #endregion

            #region Constructors

            internal LayerDetails(LossMmod parent, IntPtr ptr)
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
                NativeMethods.loss_mmod_layer_details_set_num_filters(this.NativePtr, this._Parent.NetworkType, num);
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

        private sealed class Output : OutputLabels<IEnumerable<MModRect>>
        {

            #region Fields

            private readonly int _Size;

            #endregion

            #region Constructors

            internal Output(IntPtr output) :
                base(output)
            {
                this._Size = NativeMethods.dnn_output_stdvector_stdvector_mmod_rect_getSize(output);
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

            public override IEnumerable<MModRect> this[int index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(0 <= index && index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    var ptr = NativeMethods.dnn_output_stdvector_stdvector_mmod_rect_getItem(this.NativePtr, index);
                    var size = NativeMethods.dnn_output_stdvector_mmod_rect_getSize(ptr);
                    for (var i = 0; i < size; i++)
                    {
                        var pItem = NativeMethods.dnn_output_stdvector_mmod_rect_getItem(ptr, i);
                        yield return new MModRect(pItem, false);
                    }
                }
            }

            public override IEnumerable<MModRect> this[uint index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    var ptr = NativeMethods.dnn_output_stdvector_stdvector_mmod_rect_getItem(this.NativePtr, (int)index);
                    var size = NativeMethods.dnn_output_stdvector_mmod_rect_getSize(ptr);
                    for (var i = 0; i < size; i++)
                    {
                        var pItem = NativeMethods.dnn_output_stdvector_mmod_rect_getItem(ptr, i);
                        yield return new MModRect(pItem, false);
                    }
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

                NativeMethods.dnn_output_stdvector_stdvector_mmod_rect_delete(this.NativePtr);
            }

            #endregion

            #endregion

            #region IEnumerable<TItem> Members

            public override IEnumerator<IEnumerable<MModRect>> GetEnumerator()
            {
                this.ThrowIfDisposed();

                for (var index = 0; index < this._Size; index++)
                {
                    var ptr = NativeMethods.dnn_output_stdvector_stdvector_mmod_rect_getItem(this.NativePtr, index);
                    var size = NativeMethods.dnn_output_stdvector_mmod_rect_getSize(ptr);

                    var list = new List<MModRect>(size);
                    for (var i = 0; i < size; i++)
                    {
                        var pItem = NativeMethods.dnn_output_stdvector_mmod_rect_getItem(ptr, i);
                        list.Add(new MModRect(pItem, false));
                    }

                    yield return list.ToArray();
                }
            }

            #endregion

        }

    }

}