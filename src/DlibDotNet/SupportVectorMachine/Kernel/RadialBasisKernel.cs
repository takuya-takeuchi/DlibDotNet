#if !LITE
using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class RadialBasisKernel<TScalar, TSample> : KernelBase
        where TScalar : struct
        where TSample : Matrix<TScalar>, new()
    {

        #region Fields

        private readonly NativeMethods.MatrixElementType _ElementType;

        private readonly Bridge<TScalar> _Bridge;

        #endregion

        #region Constructors

        public RadialBasisKernel(TScalar gamma, int templateRow, int templateColumn) :
            base(SvmKernelType.RadialBasis, templateRow, templateColumn)
        {
            if (!NumericKernelTypesRepository.SupportTypes.TryGetValue(typeof(TScalar), out _))
                throw new NotSupportedException();

            if (!Matrix<TScalar>.TryParse<TScalar>(out var type))
                throw new NotSupportedException();

            this.SampleType = type;
            this._ElementType = type.ToNativeMatrixElementType();

            this._Bridge = CreateBridge(type, templateRow, templateColumn);

            var error = this._Bridge.Create(gamma, templateRow, templateColumn, out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(templateColumn)} or {nameof(templateRow)} is not supported.");
            }

            this.NativePtr = ret;
        }
        
        internal RadialBasisKernel(IntPtr ptr, int templateRow, int templateColumn, bool isEnabledDispose = true) :
            base(SvmKernelType.RadialBasis, templateRow, templateColumn, isEnabledDispose)
        {
            Matrix<TScalar>.TryParse<TScalar>(out var type);
            this.SampleType = type;
            this._ElementType = type.ToNativeMatrixElementType();

            this._Bridge = CreateBridge(type, templateRow, templateColumn);

            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.radial_basis_kernel_delete(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
        }

        #endregion

        #region Helpers

        private static Bridge<TScalar> CreateBridge(MatrixElementTypes sampleType, int templateRow, int templateColumn)
        {
            switch (sampleType)
            {
                case MatrixElementTypes.Int8:
                    return new Int8Bridge(sampleType) as Bridge<TScalar>;
                case MatrixElementTypes.Int16:
                    return new Int16Bridge(sampleType) as Bridge<TScalar>;
                case MatrixElementTypes.Int32:
                    return new Int32Bridge(sampleType) as Bridge<TScalar>;
                case MatrixElementTypes.UInt8:
                    return new UInt8Bridge(sampleType) as Bridge<TScalar>;
                case MatrixElementTypes.UInt16:
                    return new UInt16Bridge(sampleType) as Bridge<TScalar>;
                case MatrixElementTypes.UInt32:
                    return new UInt32Bridge(sampleType) as Bridge<TScalar>;
                case MatrixElementTypes.Float:
                    return new FloatBridge(sampleType) as Bridge<TScalar>;
                case MatrixElementTypes.Double:
                    return new DoubleBridge(sampleType) as Bridge<TScalar>;
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion

        #endregion

        private abstract class Bridge<T>
            where T : struct
        {

            #region Constructors

            protected Bridge(MatrixElementTypes sampleType)
            {
                this.SampleType = sampleType;
            }

            #endregion

            #region Properties

            protected MatrixElementTypes SampleType
            {
                get;
            }

            #endregion

            #region Methods

            public abstract NativeMethods.ErrorType Create(T gamma, int templateRow, int templateColumn, out IntPtr ret);

            #endregion

        }

        private sealed class Int8Bridge : Bridge<sbyte>
        {

            #region Constructors

            public Int8Bridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(sbyte gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_int8_t(this.SampleType.ToNativeMatrixElementType(),
                                                                    templateRow,
                                                                    templateColumn,
                                                                    gamma,
                                                                    out ret);
            }

            #endregion

        }

        private sealed class Int16Bridge : Bridge<short>
        {

            #region Constructors

            public Int16Bridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(short gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_int16_t(this.SampleType.ToNativeMatrixElementType(),
                                                                     templateRow,
                                                                     templateColumn,
                                                                     gamma,
                                                                     out ret);
            }

            #endregion

        }

        private sealed class Int32Bridge : Bridge<int>
        {

            #region Constructors

            public Int32Bridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(int gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_int32_t(this.SampleType.ToNativeMatrixElementType(),
                                                                     templateRow,
                                                                     templateColumn,
                                                                     gamma,
                                                                     out ret);
            }

            #endregion

        }
        
        private sealed class UInt8Bridge : Bridge<byte>
        {

            #region Constructors

            public UInt8Bridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(byte gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_uint8_t(this.SampleType.ToNativeMatrixElementType(),
                                                                     templateRow,
                                                                     templateColumn,
                                                                     gamma,
                                                                     out ret);
            }

            #endregion

        }

        private sealed class UInt16Bridge : Bridge<ushort>
        {

            #region Constructors

            public UInt16Bridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(ushort gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_uint16_t(this.SampleType.ToNativeMatrixElementType(),
                                                                      templateRow,
                                                                      templateColumn,
                                                                      gamma,
                                                                      out ret);
            }

            #endregion

        }

        private sealed class UInt32Bridge : Bridge<uint>
        {

            #region Constructors

            public UInt32Bridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(uint gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_uint32_t(this.SampleType.ToNativeMatrixElementType(),
                                                                      templateRow,
                                                                      templateColumn,
                                                                      gamma,
                                                                      out ret);
            }

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Constructors

            public FloatBridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(float gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_float(this.SampleType.ToNativeMatrixElementType(),
                                                                   templateRow,
                                                                   templateColumn,
                                                                   gamma,
                                                                   out ret);
            }

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Constructors

            public DoubleBridge(MatrixElementTypes sampleType) :
                base(sampleType)
            {
            }

            #endregion

            #region Methods

            public override NativeMethods.ErrorType Create(double gamma, int templateRow, int templateColumn, out IntPtr ret)
            {
                return NativeMethods.radial_basis_kernel_new_double(this.SampleType.ToNativeMatrixElementType(),
                                                                    templateRow,
                                                                    templateColumn,
                                                                    gamma,
                                                                    out ret);
            }

            #endregion

        }

    }

}
#endif
