#if !LITE
using System;
using System.IO;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class DecisionFunction<TScalar, TKernel> : FunctionBase
        where TScalar : struct
        where TKernel : KernelBase
    {

        #region Fields

        private readonly KernelBaseParameter _Parameter;

        private readonly Bridge<TScalar> _Bridge;

        #endregion

        #region Constructors

        internal DecisionFunction(IntPtr ptr,
                                  KernelBaseParameter parameter,
                                  bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            this._Parameter = parameter;
            this._Bridge = CreateBridge(parameter);
            this.NativePtr = ptr;
        }

        #endregion
        
        #region Methods

        public static DecisionFunction<TScalar, TKernel> Deserialize(string path, int templateRows, int templateColumns)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            KernelFactory.TryParse<TScalar>(out MatrixElementTypes sampleType);
            KernelFactory.TryParse<TKernel>(out SvmKernelType kernelType);

            var param = new KernelBaseParameter(kernelType, sampleType, templateRows, templateColumns);

            var str = Dlib.Encoding.GetBytes(path);
            var error = NativeMethods.deserialize_decision_function(str,
                                                                    str.Length,
                                                                    kernelType.ToNativeKernelType(),
                                                                    sampleType.ToNativeMatrixElementType(),
                                                                    templateRows,
                                                                    templateColumns,
                                                                    out var ret,
                                                                    out var errorMessage);

            switch (error)
            {
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{param.SampleType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(param.TemplateColumns)} or {nameof(param.TemplateRows)} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{param.KernelType} is not supported.");
            }

            return new DecisionFunction<TScalar, TKernel>(ret, param);
        }

        public TScalar Operator(Matrix<TScalar> sample)
        {
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            sample.ThrowIfDisposed();

            return this._Bridge.Operator(this.NativePtr, sample);
        }

        public static void Serialize(DecisionFunction<TScalar, TKernel> function, string path)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            function.ThrowIfDisposed();

            var str = Dlib.Encoding.GetBytes(path);
            var ret = NativeMethods.serialize_decision_function(function._Parameter.KernelType.ToNativeKernelType(),
                                                                function._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                function._Parameter.TemplateRows,
                                                                function._Parameter.TemplateColumns,
                                                                function.NativePtr,
                                                                str,
                                                                str.Length,
                                                                out var errorMessage);

            switch (ret)
            {
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{function._Parameter.SampleType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(function._Parameter.TemplateColumns)} or {nameof(function._Parameter.TemplateRows)} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{function._Parameter.KernelType} is not supported.");
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

            NativeMethods.decision_function_delete(this._Parameter.KernelType.ToNativeKernelType(),
                                                   this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                   this._Parameter.TemplateRows,
                                                   this._Parameter.TemplateColumns,
                                                   this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<TScalar> CreateBridge(KernelBaseParameter parameter)
        {
            switch (parameter.SampleType)
            {
                case MatrixElementTypes.Float:
                    return new FloatBridge(parameter) as Bridge<TScalar>;
                case MatrixElementTypes.Double:
                    return new DoubleBridge(parameter) as Bridge<TScalar>;
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

            protected Bridge(KernelBaseParameter parameter)
            {
                this.Parameter = parameter;
            }

            #endregion

            #region Properties

            protected KernelBaseParameter Parameter
            {
                get;
            }

            #endregion

            #region Methods

            public abstract T Operator(IntPtr function, Matrix<T> x);
            
            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Constructors

            public FloatBridge(KernelBaseParameter parameter) :
                base(parameter)
            {
            }

            #endregion

            #region Methods

            public override float Operator(IntPtr function, Matrix<float> x)
            {
                var err = NativeMethods.decision_function_operator_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         this.Parameter.TemplateRows,
                                                                         this.Parameter.TemplateColumns,
                                                                         function,
                                                                         x.NativePtr,
                                                                         out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Constructors

            public DoubleBridge(KernelBaseParameter parameter) :
                base(parameter)
            {
            }

            #endregion

            #region Methods

            public override double Operator(IntPtr function, Matrix<double> x)
            {
                var err = NativeMethods.decision_function_operator_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                          this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                          this.Parameter.TemplateRows,
                                                                          this.Parameter.TemplateColumns,
                                                                          function,
                                                                          x.NativePtr,
                                                                          out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            #endregion

        }

    }

}

#endif
