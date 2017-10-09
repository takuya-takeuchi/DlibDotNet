using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static Array2DMatrixBase ExtracFHogFeatures<T>(Array2DBase inImage, int cellSize = 8, int filterRowsPadding = 1, int filterColsPadding = 1)
            where T : struct
        {
            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (!(cellSize > 0))
                throw new ArgumentOutOfRangeException(nameof(cellSize));
            if (!(filterRowsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterRowsPadding));
            if (!(filterColsPadding > 0))
                throw new ArgumentOutOfRangeException(nameof(filterColsPadding));

            inImage.ThrowIfDisposed(nameof(inImage));

            var hogImage = new FHogArray2DMatrix<T>();

            var inType = inImage.ImageType.ToNativeArray2DType();
            var outType = hogImage.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.extract_fhog_features(inType, inImage.NativePtr, outType, hogImage.NativePtr, cellSize, filterRowsPadding, filterColsPadding);
            switch (ret)
            {
                case Native.ErrorType.OutputElementTypeNotSupport:
                    throw new ArgumentException($"Output {outType} is not supported.");
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inImage.ImageType} is not supported.");
            }

            return hogImage;
        }

        public static Matrix<byte> DrawHog(Array2DMatrixBase hogImage, int cellDrawSize = 15, float minResponseThreshold = 0.0f)
        {
            if (hogImage == null)
                throw new ArgumentNullException(nameof(hogImage));
            if (!(cellDrawSize > 0))
                throw new ArgumentOutOfRangeException(nameof(cellDrawSize));

            hogImage.ThrowIfDisposed(nameof(hogImage));
            var inType = hogImage.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.draw_fhog(inType, hogImage.NativePtr, cellDrawSize, minResponseThreshold, out var outMatrix);
            switch (ret)
            {
                case Native.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }

            return new Matrix<byte>(outMatrix, MatrixElementTypes.UInt8);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType extract_fhog_features(Array2DType img_type,
                                                                IntPtr img,
                                                                MatrixElementType hog_type,
                                                                IntPtr hog,
                                                                int cell_size,
                                                                int filter_rows_padding,
                                                                int filter_cols_padding);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_fhog(MatrixElementType img_type,
                                                     IntPtr hog,
                                                     int cell_draw_size,
                                                     float min_response_threshold,
                                                     out IntPtr out_matrix);

        }

        private sealed class FHogArray2DMatrix<T> : Array2DMatrixBase
            where T : struct
        {

            #region Fields

            private readonly Native.MatrixElementType _MatrixElementType;

            private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixTypes = new Dictionary<Type, MatrixElementTypes>();

            #endregion

            #region Constructors

            static FHogArray2DMatrix()
            {
                var matrixTypes = new[]
                {
                    new { Type = typeof(float),         ElementType = MatrixElementTypes.Float },
                    new { Type = typeof(double),        ElementType = MatrixElementTypes.Double },
                };

                foreach (var type in matrixTypes)
                    SupportMatrixTypes.Add(type.Type, type.ElementType);
            }

            public FHogArray2DMatrix()
            {
                if (!SupportMatrixTypes.TryGetValue(typeof(T), out var matrixType))
                    throw new NotSupportedException($"{typeof(T).Name} does not support");

                this._MatrixElementType = matrixType.ToNativeMatrixElementType();

                this.NativePtr = Dlib.Native.array2d_fhog_matrix_new(this._MatrixElementType);
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
                    Dlib.Native.array2d_fhog_matrix_nc(this._MatrixElementType, this.NativePtr, out var ret);
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
                    Dlib.Native.array2d_fhog_matrix_get_rect2(this._MatrixElementType, this.NativePtr, out var ret);
                    return new Rectangle(ret);
                }
            }

            public override int Rows
            {
                get
                {
                    this.ThrowIfDisposed();
                    Dlib.Native.array2d_fhog_matrix_nr(this._MatrixElementType, this.NativePtr, out var ret);
                    return ret;
                }
            }

            public override int Size
            {
                get
                {
                    this.ThrowIfDisposed();
                    Dlib.Native.array2d_fhog_matrix_size(this._MatrixElementType, this.NativePtr, out var ret);
                    return ret;
                }
            }

            #endregion

            #region Methods 

            #region Overrides 

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();
                Dlib.Native.array2d_fhog_matrix_delete(this._MatrixElementType, this.NativePtr);
            }

            #endregion

            #endregion

        }

    }

}