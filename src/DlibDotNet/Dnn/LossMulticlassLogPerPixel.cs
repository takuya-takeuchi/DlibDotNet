using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DlibDotNet.Extensions;
using ErrorType = DlibDotNet.NativeMethods.ErrorType;

namespace DlibDotNet.Dnn
{

    public sealed class LossMulticlassLogPerPixel : Net
    {

        #region Constructors

        public LossMulticlassLogPerPixel(int networkType = 0)
            : base(networkType)
        {
            var ret = NativeMethods.loss_multiclass_log_per_pixel_new(networkType, out var net);
            if (ret == ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(networkType);

            this.NativePtr = net;
        }

        internal LossMulticlassLogPerPixel(IntPtr ptr, int networkType = 0)
            : base(networkType)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public static ushort LabelToIgnore => NativeMethods.loss_multiclass_log_per_pixel_get_label_to_ignore();

        public override int NumLayers
        {
            get
            {
                this.ThrowIfDisposed();

                return NativeMethods.loss_multiclass_log_per_pixel_num_layers(this.NetworkType);
            }
        }

        #endregion

        #region Methods

        public override void Clean()
        {
            this.ThrowIfDisposed();

            NativeMethods.loss_multiclass_log_per_pixel_clean(this.NetworkType);
        }

        public static LossMulticlassLogPerPixel Deserialize(string path, int networkType = 0)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Dlib.Encoding.GetBytes(path);
            var error = NativeMethods.loss_multiclass_log_per_pixel_deserialize(str, networkType, out var net);
            Cuda.ThrowCudaException(error);

            return new LossMulticlassLogPerPixel(net, networkType);
        }

        public static LossMulticlassLogPerPixel Deserialize(ProxyDeserialize deserialize, int networkType = 0)
        {
            if (deserialize == null)
                throw new ArgumentNullException(nameof(deserialize));

            deserialize.ThrowIfDisposed();

            var error = NativeMethods.loss_multiclass_log_per_pixel_deserialize_proxy(deserialize.NativePtr, networkType, out var net);
            Cuda.ThrowCudaException(error);

            return new LossMulticlassLogPerPixel(net, networkType);
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
                NativeMethods.loss_multiclass_log_per_pixel_input_tensor_to_output_tensor(this.NativePtr, this.NetworkType, np.NativePtr, out var ret);
                return new DPoint(ret);
            }
        }

        public OutputLabels<Matrix<ushort>> Operator<T>(Matrix<T> image, ulong batchSize = 128)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            return this.Operator(new[] { image }, batchSize);
        }

        public OutputLabels<Matrix<ushort>> Operator<T>(IEnumerable<Matrix<T>> images, ulong batchSize = 128)
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

                // vecOut is not std::vector<Matrix<float>*>* but std::vector<Matrix<float>>*.
                var ret = NativeMethods.loss_multiclass_log_per_pixel_operator_matrixs(this.NativePtr,
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
                    case ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{imageType} is not supported.");
                }

                return new Output(vecOut);
            }
        }

        public static void Serialize(LossMulticlassLogPerPixel net, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            net.ThrowIfDisposed();

            var str = Dlib.Encoding.GetBytes(path);
            NativeMethods.loss_multiclass_log_per_pixel_serialize(net.NativePtr, net.NetworkType, str);
        }

        public override bool TryGetInputLayer<T>(T layer)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.loss_multiclass_log_per_pixel_delete(this.NativePtr, this.NetworkType);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            var str = "";

            try
            {
                ofstream = NativeMethods.ostringstream_new();
                var ret = NativeMethods.loss_multiclass_log_per_pixel_operator_left_shift(this.NativePtr, this.NetworkType, ofstream);
                switch (ret)
                {
                    case ErrorType.OK:
                        stdstr = NativeMethods.ostringstream_str(ofstream);
                        str = StringHelper.FromStdString(stdstr);
                        break;
                    case ErrorType.DnnNotSupportNetworkType:
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

        public sealed class Subnet : DlibObject
        {

            #region Fields

            private readonly LossMulticlassLogPerPixel _Parent;

            #endregion

            #region Constructors

            internal Subnet(LossMulticlassLogPerPixel parent)
            {
                if (parent == null)
                    throw new ArgumentNullException(nameof(parent));

                parent.ThrowIfDisposed();

                this._Parent = parent;

                var err = NativeMethods.loss_multiclass_log_per_pixel_subnet(parent.NativePtr, parent.NetworkType, out var ret);
                this.NativePtr = ret;
            }

            #endregion

            #region Properties

            public Tensor Output
            {
                get
                {
                    this._Parent.ThrowIfDisposed();
                    var tensor = NativeMethods.loss_multiclass_log_per_pixel_subnet_get_output(this.NativePtr, this._Parent.NetworkType, out var ret);
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

                NativeMethods.loss_multiclass_log_per_pixel_subnet_delete(this._Parent.NetworkType, this.NativePtr);
            }

            #endregion

            #endregion

        }

        private sealed class Output : OutputLabels<Matrix<ushort>>
        {

            #region Fields

            private readonly int _Size;

            #endregion

            #region Constructors

            internal Output(IntPtr output) :
                base(output)
            {
                this._Size = NativeMethods.dnn_output_stdvector_uint16_getSize(output);
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

            public override Matrix<ushort> this[int index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(0 <= index && index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    var ptr = NativeMethods.dnn_output_stdvector_uint16_getItem(this.NativePtr, index);
                    return new Matrix<ushort>(ptr, 0, 0, false);
                }
            }

            public override Matrix<ushort> this[uint index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    var ptr = NativeMethods.dnn_output_stdvector_uint16_getItem(this.NativePtr, (int)index);
                    return new Matrix<ushort>(ptr, 0, 0, false);
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

                NativeMethods.dnn_output_stdvector_uint16_delete(this.NativePtr);
            }

            #endregion

            #endregion

            #region IEnumerable<TItem> Members

            public override IEnumerator<Matrix<ushort>> GetEnumerator()
            {
                this.ThrowIfDisposed();

                for (var index = 0; index < this._Size; index++)
                {
                    var ptr = NativeMethods.dnn_output_stdvector_uint16_getItem(this.NativePtr, index);
                    yield return new Matrix<ushort>(ptr, 0, 0, false);
                }
            }

            #endregion

        }

    }

}
