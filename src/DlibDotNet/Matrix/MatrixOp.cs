using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MatrixOp : TwoDimentionObjectBase
    {

        #region Fields

        private readonly ImageTypes _ImageType;

        private readonly Dlib.Native.Array2DType _Array2DType;

        private readonly Dlib.Native.ElementType _ElementType;

        #endregion

        #region Constructors

        internal MatrixOp(Dlib.Native.ElementType elementType, ImageTypes type, IntPtr ptr)
        {
            this._ElementType = elementType;
            this._Array2DType = type.ToNativeArray2DType();
            this.NativePtr = ptr;
            this._ImageType = type;
        }

        #endregion

        #region Properties

        internal Dlib.Native.Array2DType Array2DType => this._Array2DType;

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                Native.matrix_op_nc(this._ElementType, this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        internal Dlib.Native.ElementType ElementType => this._ElementType;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Native.matrix_op_nr(this._ElementType, this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.matrix_op_delete(this.NativePtr);
        }

        #endregion

        #region Helpers
        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_operator(Dlib.Native.ElementType etype, Dlib.Native.Array2DType type, IntPtr matrix, int r, int c, IntPtr rgbPixel);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_op_delete(IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_op_nc(Dlib.Native.ElementType etype, Dlib.Native.Array2DType type, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_op_nr(Dlib.Native.ElementType etype, Dlib.Native.Array2DType type, IntPtr matrix, out int ret);

        }

    }

}
