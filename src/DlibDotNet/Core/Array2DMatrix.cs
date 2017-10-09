using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Array2DMatrix<T> : Array2DMatrixBase
        where T : struct
    {

        #region Fields

        private readonly Dlib.Native.MatrixElementType _MatrixElementType;

        private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors

        static Array2DMatrix()
        {
            var matrixTypes = new[]
            {
                new { Type = typeof(byte),          ElementType = MatrixElementTypes.UInt8 },
                new { Type = typeof(ushort),        ElementType = MatrixElementTypes.UInt16 },
                new { Type = typeof(uint),          ElementType = MatrixElementTypes.UInt32 },
                new { Type = typeof(sbyte),         ElementType = MatrixElementTypes.Int8 },
                new { Type = typeof(short),         ElementType = MatrixElementTypes.Int16 },
                new { Type = typeof(int),           ElementType = MatrixElementTypes.Int32 },
                new { Type = typeof(float),         ElementType = MatrixElementTypes.Float },
                new { Type = typeof(double),        ElementType = MatrixElementTypes.Double },
                new { Type = typeof(RgbPixel),      ElementType = MatrixElementTypes.RgbPixel },
                new { Type = typeof(HsiPixel),      ElementType = MatrixElementTypes.HsiPixel },
                new { Type = typeof(RgbAlphaPixel), ElementType = MatrixElementTypes.RgbAlphaPixel }
            };

            foreach (var type in matrixTypes)
                SupportMatrixTypes.Add(type.Type, type.ElementType);
        }

        public Array2DMatrix()
        {
            if (!SupportMatrixTypes.TryGetValue(typeof(T), out var matrixType))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._MatrixElementType = matrixType.ToNativeMatrixElementType();

            this.NativePtr = Dlib.Native.array2d_matrix_new(this._MatrixElementType);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{matrixType} is not supported.");
            
            this.MatrixElementType = matrixType;
        }

        public Array2DMatrix(int rows, int columns)
        {
            if (!SupportMatrixTypes.TryGetValue(typeof(T), out var matrixType))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._MatrixElementType = matrixType.ToNativeMatrixElementType();

            this.NativePtr = Dlib.Native.array2d_matrix_new1(this._MatrixElementType, rows, columns);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{matrixType} is not supported.");
            
            this.MatrixElementType = matrixType;
        }

        #endregion

        #region Properties

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_matrix_nc(this._MatrixElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override MatrixElementTypes MatrixElementType
        {
            get;
        }

        public override Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.rectangle_get_rect2(this._MatrixElementType, this.NativePtr, out var ret);
                return new Rectangle(ret);
            }
        }

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_matrix_nr(this._MatrixElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override int Size
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_matrix_size(this._MatrixElementType, this.NativePtr, out var ret);
                return ret;
            }
        }

        #endregion

        #region Methods 

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Dlib.Native.array2d_matrix_delete(this._MatrixElementType, this.NativePtr);
        }

        #endregion

        #endregion

    }

}
