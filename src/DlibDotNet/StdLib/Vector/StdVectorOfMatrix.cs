using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdVectorOfMatrix<T> : StdVector<Matrix<T>>
        where T : struct
    {

        #region Fields

        private readonly MatrixElementTypes _Type;

        #endregion

        #region Constructors

        public StdVectorOfMatrix()
        {
            using (var matrix = new Matrix<T>())
            {
                this._Type = matrix.MatrixElementType;
                var type = this._Type.ToNativeMatrixElementType();
                this.NativePtr = Dlib.Native.stdvector_matrix_new1(type);
            }
        }

        public StdVectorOfMatrix(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            using (var matrix = new Matrix<T>())
            {
                this._Type = matrix.MatrixElementType;
                var type = this._Type.ToNativeMatrixElementType();
                this.NativePtr = Dlib.Native.stdvector_matrix_new2(type, new IntPtr(size));
            }
        }

        public StdVectorOfMatrix(IEnumerable<MatrixBase> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (var matrix = new Matrix<T>())
            {
                this._Type = matrix.MatrixElementType;
                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                var type = this._Type.ToNativeMatrixElementType();
                this.NativePtr = Dlib.Native.stdvector_matrix_new3(type, array, new IntPtr(array.Length));
            }
        }

        internal StdVectorOfMatrix(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Dlib.Native.stdvector_matrix_getPointer(this.NativePtr);

        public override int Size => Dlib.Native.stdvector_matrix_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override Matrix<T>[] ToArray()
        {
            var size = this.Size;
            if (size == 0)
                return new Matrix<T>[0];

            var dst = new IntPtr[size];
            Dlib.Native.stdvector_matrix_copy(this.NativePtr, dst);
            return dst.Select(p => p != IntPtr.Zero ? new Matrix<T>(p, this._Type) : null).ToArray();
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            // Do NOT dispose each item element except for std::vector
            //foreach (var item in this.ToArray())
            //    item?.Dispose();

            var type = this._Type.ToNativeMatrixElementType();
            Dlib.Native.stdvector_matrix_delete(type, this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

        }

    }

}