#if !LITE
using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class VectorNormalizer<TElement> : Normalizer
        where TElement : MatrixBase
    {

        #region Fields

        private readonly NativeMethods.MatrixElementType _ElementType;

        private readonly Imp<TElement> _Imp;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RunningStats{TTKernel}"/> class.
        /// </summary>
        public VectorNormalizer()
        : base(NormalizerType.Vector)
        {
            if (!VectorNormalizerElementTypesRepository.Types.TryGetValue(typeof(TElement), out var type))
                throw new NotSupportedException($"{typeof(TElement).Name} does not support");

            this._ElementType = type.ToNativeMatrixElementType();
            this._Imp = this.CreateImp(type);
            this.NativePtr = NativeMethods.vector_normalizer_new(this._ElementType);
        }

        #endregion

        #region Methods

        public TElement Operator(TElement x)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));

            x.ThrowIfDisposed();

            return this._Imp.Operator(x);
        }

        public void Train(IEnumerable<TElement> samples)
        {
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));

            this.ThrowIfDisposed();
            samples.ThrowIfDisposed();

            using (var vector = new StdVector<TElement>(samples))
            {
                NativeMethods.vector_normalizer_train(this._ElementType,
                                                      this.NativePtr,
                                                      vector.NativePtr);
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

            NativeMethods.vector_normalizer_delete(this._ElementType, this.NativePtr);
        }

        #endregion

        #region Helpers

        private Imp<TElement> CreateImp(MatrixElementTypes type)
        {
            switch (type)
            {
                case MatrixElementTypes.UInt8:
                    return new MatrixUInt8Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.Int8:
                    return new MatrixInt8Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.UInt16:
                    return new MatrixUInt16Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.Int16:
                    return new MatrixInt16Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.UInt32:
                    return new MatrixUInt32Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.Int32:
                    return new MatrixInt32Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.UInt64:
                    return new MatrixUInt64Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.Int64:
                    return new MatrixInt64Imp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.Double:
                    return new MatrixDoubleImp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.Float:
                    return new MatrixFloatImp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.RgbPixel:
                    return new MatrixRgbPixelImp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.BgrPixel:
                    return new MatrixBgrPixelImp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.HsiPixel:
                    return new MatrixHsiPixelImp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.LabPixel:
                    return new MatrixLabPixelImp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                case MatrixElementTypes.RgbAlphaPixel:
                    return new MatrixRgbAlphaPixelImp(this, type.ToNativeMatrixElementType()) as Imp<TElement>;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        #endregion

        #endregion

        internal abstract class Imp<T>
            where T : MatrixBase
        {

            #region Fields 

            protected readonly NativeMethods.MatrixElementType Type;

            protected readonly DlibObject Parent;

            #endregion

            #region Constructors 

            internal Imp(DlibObject parent, NativeMethods.MatrixElementType type)
            {
                this.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.Type = type;
            }

            #endregion

            #region Method

            public abstract T Operator(T x);

            #endregion

        }

        internal sealed class MatrixUInt8Imp : Imp<Matrix<byte>>
        {

            #region Constructors

            internal MatrixUInt8Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<byte> Operator(Matrix<byte> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<byte>(ret);
            }

            #endregion

        }

        internal sealed class MatrixInt8Imp : Imp<Matrix<sbyte>>
        {

            #region Constructors

            internal MatrixInt8Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<sbyte> Operator(Matrix<sbyte> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<sbyte>(ret);
            }

            #endregion

        }

        internal sealed class MatrixUInt16Imp : Imp<Matrix<ushort>>
        {

            #region Constructors

            internal MatrixUInt16Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<ushort> Operator(Matrix<ushort> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<ushort>(ret);
            }

            #endregion

        }

        internal sealed class MatrixInt16Imp : Imp<Matrix<short>>
        {

            #region Constructors

            internal MatrixInt16Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<short> Operator(Matrix<short> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<short>(ret);
            }

            #endregion

        }

        internal sealed class MatrixUInt32Imp : Imp<Matrix<uint>>
        {

            #region Constructors

            internal MatrixUInt32Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<uint> Operator(Matrix<uint> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<uint>(ret);
            }

            #endregion

        }

        internal sealed class MatrixInt32Imp : Imp<Matrix<int>>
        {

            #region Constructors

            internal MatrixInt32Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<int> Operator(Matrix<int> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<int>(ret);
            }

            #endregion

        }

        internal sealed class MatrixUInt64Imp : Imp<Matrix<ulong>>
        {

            #region Constructors

            internal MatrixUInt64Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<ulong> Operator(Matrix<ulong> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<ulong>(ret);
            }

            #endregion

        }

        internal sealed class MatrixInt64Imp : Imp<Matrix<long>>
        {

            #region Constructors

            internal MatrixInt64Imp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<long> Operator(Matrix<long> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<long>(ret);
            }

            #endregion

        }

        internal sealed class MatrixDoubleImp : Imp<Matrix<double>>
        {

            #region Constructors

            internal MatrixDoubleImp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<double> Operator(Matrix<double> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<double>(ret);
            }

            #endregion

        }

        internal sealed class MatrixFloatImp : Imp<Matrix<float>>
        {

            #region Constructors

            internal MatrixFloatImp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<float> Operator(Matrix<float> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<float>(ret);
            }

            #endregion

        }

        internal sealed class MatrixRgbPixelImp : Imp<Matrix<RgbPixel>>
        {

            #region Constructors

            internal MatrixRgbPixelImp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<RgbPixel> Operator(Matrix<RgbPixel> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<RgbPixel>(ret);
            }

            #endregion

        }

        internal sealed class MatrixBgrPixelImp : Imp<Matrix<BgrPixel>>
        {

            #region Constructors

            internal MatrixBgrPixelImp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<BgrPixel> Operator(Matrix<BgrPixel> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<BgrPixel>(ret);
            }

            #endregion

        }

        internal sealed class MatrixHsiPixelImp : Imp<Matrix<HsiPixel>>
        {

            #region Constructors

            internal MatrixHsiPixelImp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<HsiPixel> Operator(Matrix<HsiPixel> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<HsiPixel>(ret);
            }

            #endregion

        }

        internal sealed class MatrixLabPixelImp : Imp<Matrix<LabPixel>>
        {

            #region Constructors

            internal MatrixLabPixelImp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<LabPixel> Operator(Matrix<LabPixel> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                    this.Parent.NativePtr,
                    x.NativePtr,
                    out var ret);
                return new Matrix<LabPixel>(ret);
            }

            #endregion

        }

        internal sealed class MatrixRgbAlphaPixelImp : Imp<Matrix<RgbAlphaPixel>>
        {

            #region Constructors

            internal MatrixRgbAlphaPixelImp(DlibObject parent, NativeMethods.MatrixElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Method

            public override Matrix<RgbAlphaPixel> Operator(Matrix<RgbAlphaPixel> x)
            {
                NativeMethods.vector_normalizer_operator(this.Type,
                                                         this.Parent.NativePtr,
                                                         x.NativePtr,
                                                         out var ret);
                return new Matrix<RgbAlphaPixel>(ret);
            }

            #endregion

        }

    }

}
#endif
