using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

using OutputLabelType = System.UInt32;

namespace DlibDotNet.Dnn
{

    public sealed class LossMulticlassLog : Net
    {

        #region Constructors

        public LossMulticlassLog(int networkType = 0)
        : base(networkType)
        {
            var ret = Native.loss_multiclass_log_new(networkType, out var net);
            if (ret == Dlib.Native.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(networkType);

            this.NativePtr = net;
        }

        internal LossMulticlassLog(IntPtr ptr, int networkType = 0)
            : base(networkType)
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

                return Native.loss_multiclass_log_num_layers(this.NetworkType);
            }
        }

        #endregion

        #region Methods

        public override void Clean()
        {
            this.ThrowIfDisposed();

            Native.loss_multiclass_log_clean(this.NetworkType);
        }

        public static LossMulticlassLog Deserialize(string path, int networkType = 0)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);
            var error = Native.loss_multiclass_log_deserialize(str, networkType, out var net);
            Cuda.ThrowCudaException(error);

            return new LossMulticlassLog(net, networkType);
        }

        public static LossMulticlassLog Deserialize(ProxyDeserialize deserialize, int networkType = 0)
        {
            if (deserialize == null)
                throw new ArgumentNullException(nameof(deserialize));

            deserialize.ThrowIfDisposed();

            var error = Native.loss_multiclass_log_deserialize_proxy(deserialize.NativePtr, networkType, out var net);
            Cuda.ThrowCudaException(error);

            return new LossMulticlassLog(net, networkType);
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
                Native.loss_multiclass_log_input_tensor_to_output_tensor(this.NativePtr, this.NetworkType, np.NativePtr, out var ret);
                return new DPoint(ret);
            }
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

            using (var vecIn = new StdVector<Matrix<T>>(images))
            {
                Matrix<T>.TryParse<T>(out var imageType);
                var templateRows = images.First().TemplateRows;
                var templateColumns = images.First().TemplateColumns;

                var ret = Native.loss_multiclass_log_operator_matrixs(this.NativePtr,
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
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{imageType} is not supported.");
                }

                return new Output(vecOut);
            }
        }

        public static void Serialize(LossMulticlassLog net, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            net.ThrowIfDisposed();

            var str = Encoding.UTF8.GetBytes(path);
            Native.loss_multiclass_log_serialize(net.NativePtr, net.NetworkType, str);
        }

        public static void Train<T>(DnnTrainer<LossMulticlassLog> trainer, IEnumerable<Matrix<T>> data, IEnumerable<uint> label)
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
                var ret = Native.dnn_trainer_loss_multiclass_log_train(trainer.NativePtr,
                                                                       trainer.Type,
                                                                       dataElementTypes.ToNativeMatrixElementType(),
                                                                       dataVec.NativePtr,
                                                                       Dlib.Native.MatrixElementType.UInt32,
                                                                       labelVec.NativePtr);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new NotSupportedException($"{dataElementTypes} does not support");
                }
            }
        }

        public override bool TryGetInputLayer<T>(T layer)
        {
            throw new NotSupportedException();
        }

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.loss_multiclass_log_delete(this.NativePtr, this.NetworkType);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            var str = "";

            try
            {
                ofstream = Dlib.Native.ostringstream_new();
                var ret = Native.loss_multiclass_log_operator_left_shift(this.NativePtr, this.NetworkType, ofstream);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.OK:
                        stdstr = Dlib.Native.ostringstream_str(ofstream);
                        str = StringHelper.FromStdString(stdstr);
                        break;
                    case Dlib.Native.ErrorType.DnnNotSupportNetworkType:
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
                    Dlib.Native.string_delete(stdstr);
                if (ofstream != IntPtr.Zero)
                    Dlib.Native.ostringstream_delete(ofstream);
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
            {
                if (parent == null)
                    throw new ArgumentNullException(nameof(parent));

                parent.ThrowIfDisposed();

                this._Parent = parent;

                var err = Native.loss_multiclass_log_subnet(parent.NativePtr, parent.NetworkType, out var ret);
                this.NativePtr = ret;
            }

            #endregion

            #region Properties

            public Tensor Output
            {
                get
                {
                    this._Parent.ThrowIfDisposed();
                    var tensor = Native.loss_multiclass_log_subnet_get_output(this.NativePtr, this._Parent.NetworkType, out var ret);
                    return new Tensor(tensor);
                }
            }

            #endregion

            #region Methods

            #region Overrids

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                Native.loss_multiclass_log_subnet_delete(this._Parent.NetworkType, this.NativePtr);
            }

            #endregion

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
                this._Size = Native.dnn_output_uint32_t_getSize(output);
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

                    return Native.dnn_output_uint32_t_getItem(this.NativePtr, (int)index);
                }
            }

            public override OutputLabelType this[uint index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    return Native.dnn_output_uint32_t_getItem(this.NativePtr, (int)index);
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

                Native.dnn_output_uint32_t_delete(this.NativePtr);
            }

            #endregion

            #endregion

            #region IEnumerable<TItem> Members

            public override IEnumerator<OutputLabelType> GetEnumerator()
            {
                this.ThrowIfDisposed();

                for (var index = 0; index < this._Size; index++)
                    yield return Native.dnn_output_uint32_t_getItem(this.NativePtr, index);
            }

            #endregion

            private sealed class Native
            {

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void dnn_output_uint32_t_delete(IntPtr vector);

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern OutputLabelType dnn_output_uint32_t_getItem(IntPtr vector, int index);

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern int dnn_output_uint32_t_getSize(IntPtr vector);

            }

        }

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_multiclass_log_new(int type, out IntPtr net);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_multiclass_log_delete(IntPtr obj, int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_multiclass_log_deserialize(byte[] fileName, int type, out IntPtr net);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_multiclass_log_deserialize_proxy(IntPtr proxy_deserialize, int type, out IntPtr net);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_multiclass_log_serialize(IntPtr obj, int type, byte[] fileName);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_multiclass_log_input_tensor_to_output_tensor(IntPtr net, int networkType, IntPtr p, out IntPtr ret);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int loss_multiclass_log_num_layers(int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_multiclass_log_clean(int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_multiclass_log_subnet(IntPtr net, int type, out IntPtr subnet);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_multiclass_log_subnet_delete(int type, IntPtr subnet);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr loss_multiclass_log_subnet_get_output(IntPtr subnet, int type, out Dlib.Native.ErrorType ret);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_multiclass_log_operator_left_shift(IntPtr obj, int type, IntPtr ofstream);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_multiclass_log_operator_matrixs(IntPtr obj,
                                                                                            int type,
                                                                                            Dlib.Native.MatrixElementType element_type,
                                                                                            IntPtr matrixs,
                                                                                            int templateRows,
                                                                                            int templateColumns,
                                                                                            ulong batchSize,
                                                                                            out IntPtr ret);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType dnn_trainer_loss_multiclass_log_train(IntPtr trainer,
                                                                                             int type,
                                                                                             Dlib.Native.MatrixElementType dataElementType,
                                                                                             IntPtr data,
                                                                                             Dlib.Native.MatrixElementType labelElementType,
                                                                                             IntPtr label);

        }

    }

}