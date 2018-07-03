using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

namespace DlibDotNet.Dnn
{

    public sealed class LossMulticlassLogPerPixel : Net
    {

        #region Constructors

        public LossMulticlassLogPerPixel(int type = 0)
            : base(type)
        {
            this.NativePtr = Native.loss_multiclass_log_per_pixel_new(type);
        }

        internal LossMulticlassLogPerPixel(IntPtr ptr, int type = 0)
            : base(type)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public static ushort LabelToIgnore => Native.loss_multiclass_log_per_pixel_get_label_to_ignore();

        #endregion

        #region Methods

        public static LossMulticlassLogPerPixel Deserialize(string path, int type = 0)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);
            var ret = Native.loss_multiclass_log_per_pixel_deserialize(str, type);
            return new LossMulticlassLogPerPixel(ret, type);
        }

        public OutputLabels<Matrix<ushort>> Operator<T>(Matrix<T> image)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            return this.Operator(new[] { image });
        }

        public OutputLabels<Matrix<ushort>> Operator<T>(IEnumerable<Matrix<T>> images)
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
                var ret = Native.loss_multiclass_log_per_pixel_operator_matrixs(this.NativePtr,
                                                                                this.Type,
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

        #endregion

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.loss_multiclass_log_per_pixel_delete(this.NativePtr, this.Type);
        }

        #endregion

        private sealed class Output : OutputLabels<Matrix<ushort>>
        {

            #region Fields

            private readonly int _Size;

            #endregion

            #region Constructors

            internal Output(IntPtr output) :
                base(output)
            {
                this._Size = Native.dnn_output_stdvector_uint16_getSize(output);
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

                    var ptr = Native.dnn_output_stdvector_uint16_getItem(this.NativePtr, index);
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

                    var ptr = Native.dnn_output_stdvector_uint16_getItem(this.NativePtr, (int)index);
                    return new Matrix<ushort>(ptr, 0, 0, false);
                }
            }

            #endregion

            #region Methods

            #region Overrides

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();
                Native.dnn_output_stdvector_uint16_delete(this.NativePtr);
            }

            #endregion

            #endregion

            #region IEnumerable<TItem> Members

            public override IEnumerator<Matrix<ushort>> GetEnumerator()
            {
                this.ThrowIfDisposed();

                for (var index = 0; index < this._Size; index++)
                {
                    var ptr = Native.dnn_output_stdvector_uint16_getItem(this.NativePtr, index);
                    yield return new Matrix<ushort>(ptr, 0, 0, false);
                }
            }

            #endregion

            private sealed class Native
            {

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void dnn_output_stdvector_uint16_delete(IntPtr vector);

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr dnn_output_stdvector_uint16_getItem(IntPtr vector, int index);

                [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern int dnn_output_stdvector_uint16_getSize(IntPtr vector);

            }

        }

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ushort loss_multiclass_log_per_pixel_get_label_to_ignore();

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr loss_multiclass_log_per_pixel_new(int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void loss_multiclass_log_per_pixel_delete(IntPtr obj, int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr loss_multiclass_log_per_pixel_deserialize(byte[] fileName, int type);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType loss_multiclass_log_per_pixel_operator_matrixs(IntPtr obj, int type, Dlib.Native.MatrixElementType element_type, IntPtr matrixs, int templateRows, int templateColumns, out IntPtr ret);

        }

    }

}
