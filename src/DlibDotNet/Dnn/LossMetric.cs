using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

namespace DlibDotNet.Dnn
{

    public sealed class LossMetric : Net
    {

        #region Constructors

        public LossMetric(int networkType = 0)
        : base(networkType)
        {
            var ret = Native.loss_metric_new(networkType, out var net);
            if (ret == Dlib.Native.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(networkType);

            this.NativePtr = net;
        }

        internal LossMetric(IntPtr ptr, int networkType = 0)
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

                return Native.loss_metric_num_layers(this.NetworkType);
            }
        }

        #endregion

        #region Methods

        public override void Clean()
        {
            this.ThrowIfDisposed();

            Native.loss_metric_clean(this.NetworkType);
        }

        public static LossMetric Deserialize(string path, int networkType = 0)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);
            var ret = Native.loss_metric_deserialize(str, networkType);
            return new LossMetric(ret, networkType);
        }

        public OutputLabels<Matrix<float>> Operator<T>(Matrix<T> image)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            return this.Operator(new[] { image });
        }

        public OutputLabels<Matrix<float>> Operator<T>(IEnumerable<Matrix<T>> images)
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
                var ret = Native.loss_metric_operator_matrixs(this.NativePtr,
                                                              this.NetworkType,
                                                              imageType.ToNativeMatrixElementType(),
                                                              vecIn.NativePtr,
                                                              templateRows,
                                                              templateColumns,
                                                              out var vecOut);

                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{imageType} is not supported.");
                }

                return new Output(vecOut);
            }
        }

        public static void Serialize(LossMetric net, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            net.ThrowIfDisposed();

            var str = Encoding.UTF8.GetBytes(path);
            Native.loss_metric_serialize(net.NativePtr, net.NetworkType, str);
        }

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.loss_metric_delete(this.NativePtr, this.NetworkType);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            var str = "";

            try
            {
                ofstream = Dlib.Native.ostringstream_new();
                var ret = Native.loss_metric_operator_left_shift(this.NativePtr, this.NetworkType, ofstream);
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

        private sealed class Output : OutputLabels<Matrix<float>>
        {

            #region Fields

            private readonly int _Size;

            #endregion

            #region Constructors

            internal Output(IntPtr output) :
                base(output)
            {
                this._Size = Native.dnn_output_stdvector_float_1_1_getSize(output);
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

            public override Matrix<float> this[int index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(0 <= index && index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    var ptr = Native.dnn_output_stdvector_float_1_1_getItem(this.NativePtr, index);
                    return new Matrix<float>(ptr, 0, 1, false);
                }
            }

            public override Matrix<float> this[uint index]
            {
                get
                {
                    this.ThrowIfDisposed();

                    if (!(index < this._Size))
                        throw new ArgumentOutOfRangeException();

                    var ptr = Native.dnn_output_stdvector_float_1_1_getItem(this.NativePtr, (int)index);
                    return new Matrix<float>(ptr, 0, 1, false);
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

                Native.dnn_output_stdvector_float_1_1_delete(this.NativePtr);
            }

            #endregion

            #endregion

            #region IEnumerable<TItem> Members

            public override IEnumerator<Matrix<float>> GetEnumerator()
            {
                this.ThrowIfDisposed();

                for (var index = 0; index < this._Size; index++)
                {
                    var ptr = Native.dnn_output_stdvector_float_1_1_getItem(this.NativePtr, index);
                    yield return new Matrix<float>(ptr, 0, 1, false);
                }
            }

            #endregion

            private sealed class Native
            {

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void dnn_output_stdvector_float_1_1_delete(IntPtr vector);

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr dnn_output_stdvector_float_1_1_getItem(IntPtr vector, int index);

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern int dnn_output_stdvector_float_1_1_getSize(IntPtr vector);

            }

        }

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_metric_new(int type, out IntPtr net);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_metric_delete(IntPtr obj, int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr loss_metric_deserialize(byte[] fileName, int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_metric_serialize(IntPtr obj, int type, byte[] fileName);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int loss_metric_num_layers(int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_metric_clean(int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_metric_operator_left_shift(IntPtr obj, int type, IntPtr ofstream);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_metric_operator_matrixs(IntPtr obj, int type, Dlib.Native.MatrixElementType element_type, IntPtr matrixs, int templateRows, int templateColumns, out IntPtr ret);

        }

    }

}