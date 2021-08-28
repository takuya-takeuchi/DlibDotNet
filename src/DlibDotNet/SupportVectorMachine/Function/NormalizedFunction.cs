#if !LITE
using System;
using System.IO;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class NormalizedFunction<TScalar, TFunction> : DlibObject
        where TScalar : struct
        where TFunction : FunctionBase
    {

        #region Fields

        private readonly KernelBaseParameter _Parameter;

        private readonly NativeMethods.SvmFunctionType _SvmFunctionType;

        private readonly Imp<TScalar> _Imp;

        #endregion

        #region Constructors

        public NormalizedFunction()
        {
            var functionType = typeof(TFunction);
            var svmFunction = functionType.GetGenericTypeDefinition();
            if (!FunctionTypesRepository.Types.TryGetValue(svmFunction, out var svmFunctionType))
                throw new ArgumentException();

            var kernelType = functionType.GenericTypeArguments[1].GetGenericTypeDefinition();
            if (!KernelTypesRepository.KernelTypes.TryGetValue(kernelType, out var svmKernelType))
                throw new ArgumentException();

            var elementType = functionType.GenericTypeArguments[0];
            if (!KernelTypesRepository.ElementTypes.TryGetValue(elementType, out var sampleType))
                throw new ArgumentException();
            
            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._SvmFunctionType = svmFunctionType;
            this._Imp = this.CreateImp(sampleType.ToNativeMatrixElementType());
            this.NativePtr = NativeMethods.normalized_function_new(this._Parameter.KernelType.ToNativeKernelType(),
                                                                   this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                   this._Parameter.TemplateRows,
                                                                   this._Parameter.TemplateColumns,
                                                                   svmFunctionType);
        }

        internal NormalizedFunction(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            var functionType = typeof(TFunction);
            var svmFunction = functionType.GetGenericTypeDefinition();
            if (!FunctionTypesRepository.Types.TryGetValue(svmFunction, out var svmFunctionType))
                throw new ArgumentException();

            var kernelType = functionType.GenericTypeArguments[1].GetGenericTypeDefinition();
            if (!KernelTypesRepository.KernelTypes.TryGetValue(kernelType, out var svmKernelType))
                throw new ArgumentException();

            var elementType = functionType.GenericTypeArguments[0];
            if (!KernelTypesRepository.ElementTypes.TryGetValue(elementType, out var sampleType))
                throw new ArgumentException();

            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._SvmFunctionType = svmFunctionType;
            this._Imp = this.CreateImp(sampleType.ToNativeMatrixElementType());

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public TFunction Function
        {
            // It is hard to create Kernel object from generic
            //get
            //{
            //    this.ThrowIfDisposed();
            //    var err = NativeMethods.normalized_function_get_function(this._Parameter.KernelType.ToNativeKernelType(),
            //                                                             this._Parameter.SampleType.ToNativeMatrixElementType(),
            //                                                             this._Parameter.TemplateRows,
            //                                                             this._Parameter.TemplateColumns,
            //                                                             this._SvmFunctionType,
            //                                                             this.NativePtr,
            //                                                             out var ret);
            //    return null;
            //}
            set
            {
                this.ThrowIfDisposed();

                // value is cloned in native domain.
                // Because, function member is value type.
                var err = NativeMethods.normalized_function_set_function(this._Parameter.KernelType.ToNativeKernelType(),
                                                                         this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         this._Parameter.TemplateRows,
                                                                         this._Parameter.TemplateColumns,
                                                                         this._SvmFunctionType,
                                                                         this.NativePtr,
                                                                         value.NativePtr);
            }
        }

        public Normalizer Normalizer
        {
            // It is hard to create Kernel object from generic
            //get
            //{
            //    this.ThrowIfDisposed();
            //    var err = NativeMethods.normalized_function_get_normalizer(this._Parameter.KernelType.ToNativeKernelType(),
            //                                                               this._Parameter.SampleType.ToNativeMatrixElementType(),
            //                                                               this._Parameter.TemplateRows,
            //                                                               this._Parameter.TemplateColumns,
            //                                                               this._SvmFunctionType,
            //                                                               this.NativePtr,
            //                                                               out var ret);
            //    return null;
            //}
            set
            {
                this.ThrowIfDisposed();
                var err = NativeMethods.normalized_function_set_normalizer(this._Parameter.KernelType.ToNativeKernelType(),
                                                                           this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                           this._Parameter.TemplateRows,
                                                                           this._Parameter.TemplateColumns,
                                                                           this._SvmFunctionType,
                                                                           this.NativePtr,
                                                                           value.NormalizerType.ToNativeNormalizerType(),
                                                                           value.NativePtr);
            }
        }

        #endregion

        #region Methods

        public static NormalizedFunction<TScalar, TFunction> Deserialize(string path, uint templateRows = 0, uint templateColumns = 0)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();
            
            var functionType = typeof(TFunction);
            var svmFunction = functionType.GetGenericTypeDefinition();
            if (!FunctionTypesRepository.Types.TryGetValue(svmFunction, out var svmFunctionType))
                throw new ArgumentException();

            var kernelType = functionType.GenericTypeArguments[1].GetGenericTypeDefinition();
            if (!KernelTypesRepository.KernelTypes.TryGetValue(kernelType, out var svmKernelType))
                throw new ArgumentException();

            var elementType = functionType.GenericTypeArguments[0];
            if (!KernelTypesRepository.ElementTypes.TryGetValue(elementType, out var sampleType))
                throw new ArgumentException();

            var str = Dlib.Encoding.GetBytes(path);
            var err = NativeMethods.normalized_function_deserialize(svmKernelType.ToNativeKernelType(),
                                                                    sampleType.ToNativeMatrixElementType(),
                                                                    (int)templateRows,
                                                                    (int)templateColumns,
                                                                    svmFunctionType,
                                                                    str,
                                                                    str.Length,
                                                                    out IntPtr ret,
                                                                    out var errorMessage);
            switch (err)
            {
                case NativeMethods.ErrorType.SvmFunctionNotSupport:
                    throw new ArgumentException($"{functionType} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{svmKernelType} is not supported.");
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
            }

            return new NormalizedFunction<TScalar, TFunction>();
        }

        public TScalar Operator(Matrix<TScalar> sample)
        {
            if (sample == null) 
                throw new ArgumentNullException(nameof(sample));

            sample.ThrowIfDisposed();

            return this._Imp.Operator(sample);
        }

        public static void Serialize(string path, NormalizedFunction<TScalar, TFunction> function)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            function.ThrowIfDisposed();

            var svmKernelType = function._Parameter.KernelType;
            var matrixElementType = function._Parameter.SampleType.ToNativeMatrixElementType();
            var templateColumns = function._Parameter.TemplateColumns;
            var templateRows = function._Parameter.TemplateRows;
            var functionType = function._SvmFunctionType;

            var str = Dlib.Encoding.GetBytes(path);
            var err = NativeMethods.normalized_function_serialize(svmKernelType.ToNativeKernelType(),
                                                                  matrixElementType,
                                                                  templateRows,
                                                                  templateColumns,
                                                                  functionType,
                                                                  function.NativePtr,
                                                                  str,
                                                                  str.Length,
                                                                  out var errorMessage);
            switch (err)
            {
                case NativeMethods.ErrorType.SvmFunctionNotSupport:
                    throw new ArgumentException($"{functionType} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{svmKernelType} is not supported.");
                case NativeMethods.ErrorType.GeneralSerialization:
                    throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
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

            NativeMethods.normalized_function_delete(this._Parameter.KernelType.ToNativeKernelType(),
                                                     this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                     this._Parameter.TemplateRows,
                                                     this._Parameter.TemplateColumns,
                                                     this._SvmFunctionType,
                                                     this.NativePtr);
        }

        #endregion

        #region Helpers

        private Imp<TScalar> CreateImp(NativeMethods.MatrixElementType type)
        {
            switch (type)
            {
                // case NativeMethods.MatrixElementType.UInt8:
                //     return new UInt8Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                // case NativeMethods.MatrixElementType.Int8:
                //     return new Int8Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                // case NativeMethods.MatrixElementType.UInt16:
                //     return new UInt16Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                // case NativeMethods.MatrixElementType.Int16:
                //     return new Int16Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                // case NativeMethods.MatrixElementType.UInt32:
                //     return new UInt32Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                // case NativeMethods.MatrixElementType.Int32:
                //     return new Int32Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                // case NativeMethods.MatrixElementType.UInt64:
                //     return new UInt64Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                // case NativeMethods.MatrixElementType.Int64:
                //     return new Int64Imp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                case NativeMethods.MatrixElementType.Double:
                    return new DoubleImp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                case NativeMethods.MatrixElementType.Float:
                    return new FloatImp(this, this._Parameter, type, this._SvmFunctionType) as Imp<TScalar>;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        #endregion

        #endregion

        internal abstract class Imp<T>
            where T : struct
        {

            #region Fields 

            protected readonly NativeMethods.MatrixElementType ElementType;

            protected readonly KernelBaseParameter KernelParameter;

            protected readonly DlibObject Parent;

            protected readonly NativeMethods.SvmFunctionType FunctionType;

            #endregion

            #region Constructors 

            internal Imp(DlibObject parent,
                         KernelBaseParameter kernelParameter,
                         NativeMethods.MatrixElementType elementType,
                         NativeMethods.SvmFunctionType functionType)
            {
                this.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.KernelParameter = kernelParameter;
                this.ElementType = elementType;
                this.FunctionType = functionType;
            }

            #endregion

            #region Method

            public abstract T Operator(Matrix<T> x);

            #endregion

        }

        // internal sealed class Int8Imp : Imp<sbyte>
        // {

        //     #region Constructors 

        //     internal Int8Imp(DlibObject parent,
        //                      KernelBaseParameter kernelParameter,
        //                      NativeMethods.MatrixElementType elementType,
        //                      NativeMethods.SvmFunctionType functionType):
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override sbyte Operator(Matrix<sbyte> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_int8_t(svmKernelType.ToNativeKernelType(),
        //                                                                     matrixElementType,
        //                                                                     templateRows,
        //                                                                     templateColumns,
        //                                                                     functionType,
        //                                                                     function,
        //                                                                     x.NativePtr,
        //                                                                     out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        // internal sealed class UInt8Imp : Imp<byte>
        // {

        //     #region Constructors 

        //     internal UInt8Imp(DlibObject parent,
        //                       KernelBaseParameter kernelParameter,
        //                       NativeMethods.MatrixElementType elementType,
        //                       NativeMethods.SvmFunctionType functionType) :
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override byte Operator(Matrix<byte> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_uint8_t(svmKernelType.ToNativeKernelType(),
        //                                                                      matrixElementType,
        //                                                                      templateRows,
        //                                                                      templateColumns,
        //                                                                      functionType,
        //                                                                      function,
        //                                                                      x.NativePtr,
        //                                                                      out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        // internal sealed class Int16Imp : Imp<short>
        // {

        //     #region Constructors 

        //     internal Int16Imp(DlibObject parent,
        //                       KernelBaseParameter kernelParameter,
        //                       NativeMethods.MatrixElementType elementType,
        //                       NativeMethods.SvmFunctionType functionType) :
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override short Operator(Matrix<short> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_int16_t(svmKernelType.ToNativeKernelType(),
        //                                                                      matrixElementType,
        //                                                                      templateRows,
        //                                                                      templateColumns,
        //                                                                      functionType,
        //                                                                      function,
        //                                                                      x.NativePtr,
        //                                                                      out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        // internal sealed class UInt16Imp : Imp<ushort>
        // {

        //     #region Constructors 

        //     internal UInt16Imp(DlibObject parent,
        //                        KernelBaseParameter kernelParameter,
        //                        NativeMethods.MatrixElementType elementType,
        //                        NativeMethods.SvmFunctionType functionType) :
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override ushort Operator(Matrix<ushort> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_uint16_t(svmKernelType.ToNativeKernelType(),
        //                                                                       matrixElementType,
        //                                                                       templateRows,
        //                                                                       templateColumns,
        //                                                                       functionType,
        //                                                                       function,
        //                                                                       x.NativePtr,
        //                                                                       out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        // internal sealed class Int32Imp : Imp<int>
        // {

        //     #region Constructors 

        //     internal Int32Imp(DlibObject parent,
        //                       KernelBaseParameter kernelParameter,
        //                       NativeMethods.MatrixElementType elementType,
        //                       NativeMethods.SvmFunctionType functionType) :
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override int Operator(Matrix<int> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_int32_t(svmKernelType.ToNativeKernelType(),
        //                                                                      matrixElementType,
        //                                                                      templateRows,
        //                                                                      templateColumns,
        //                                                                      functionType,
        //                                                                      function,
        //                                                                      x.NativePtr,
        //                                                                      out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        // internal sealed class UInt32Imp : Imp<uint>
        // {

        //     #region Constructors 

        //     internal UInt32Imp(DlibObject parent,
        //                        KernelBaseParameter kernelParameter,
        //                        NativeMethods.MatrixElementType elementType,
        //                        NativeMethods.SvmFunctionType functionType) :
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override uint Operator(Matrix<uint> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_uint32_t(svmKernelType.ToNativeKernelType(),
        //                                                                       matrixElementType,
        //                                                                       templateRows,
        //                                                                       templateColumns,
        //                                                                       functionType,
        //                                                                       function,
        //                                                                       x.NativePtr,
        //                                                                       out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        // internal sealed class Int64Imp : Imp<long>
        // {

        //     #region Constructors 

        //     internal Int64Imp(DlibObject parent,
        //                       KernelBaseParameter kernelParameter,
        //                       NativeMethods.MatrixElementType elementType,
        //                       NativeMethods.SvmFunctionType functionType) :
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override long Operator(Matrix<long> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_int64_t(svmKernelType.ToNativeKernelType(),
        //                                                                      matrixElementType,
        //                                                                      templateRows,
        //                                                                      templateColumns,
        //                                                                      functionType,
        //                                                                      function,
        //                                                                      x.NativePtr,
        //                                                                      out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        // internal sealed class UInt64Imp : Imp<ulong>
        // {

        //     #region Constructors 

        //     internal UInt64Imp(DlibObject parent,
        //                        KernelBaseParameter kernelParameter,
        //                        NativeMethods.MatrixElementType elementType,
        //                        NativeMethods.SvmFunctionType functionType) :
        //         base(parent, kernelParameter, elementType, functionType)
        //     {
        //     }

        //     #endregion

        //     #region Method

        //     public override ulong Operator(Matrix<ulong> x)
        //     {
        //         var svmKernelType = this.KernelParameter.KernelType;
        //         var matrixElementType = this.ElementType;
        //         var templateColumns = this.KernelParameter.TemplateColumns;
        //         var templateRows = this.KernelParameter.TemplateRows;
        //         var functionType = this.FunctionType;
        //         var function = this.Parent.NativePtr;
        //         var err = NativeMethods.normalized_function_operator_uint64_t(svmKernelType.ToNativeKernelType(),
        //                                                                       matrixElementType,
        //                                                                       templateRows,
        //                                                                       templateColumns,
        //                                                                       functionType,
        //                                                                       function,
        //                                                                       x.NativePtr,
        //                                                                       out var ret);
        //         return ret;
        //     }

        //     #endregion

        // }

        internal sealed class DoubleImp : Imp<double>
        {

            #region Constructors 

            internal DoubleImp(DlibObject parent,
                               KernelBaseParameter kernelParameter,
                               NativeMethods.MatrixElementType elementType,
                               NativeMethods.SvmFunctionType functionType) :
                base(parent, kernelParameter, elementType, functionType)
            {
            }

            #endregion

            #region Method

            public override double Operator(Matrix<double> x)
            {
                var svmKernelType = this.KernelParameter.KernelType;
                var matrixElementType = this.ElementType;
                var templateColumns = this.KernelParameter.TemplateColumns;
                var templateRows = this.KernelParameter.TemplateRows;
                var functionType = this.FunctionType;
                var function = this.Parent.NativePtr;
                var err = NativeMethods.normalized_function_operator_double(svmKernelType.ToNativeKernelType(),
                                                                            matrixElementType,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            functionType,
                                                                            function,
                                                                            x.NativePtr,
                                                                            out var ret);
                return ret;
            }

            #endregion

        }

        internal sealed class FloatImp : Imp<float>
        {

            #region Constructors 

            internal FloatImp(DlibObject parent,
                              KernelBaseParameter kernelParameter,
                              NativeMethods.MatrixElementType elementType,
                              NativeMethods.SvmFunctionType functionType) :
                base(parent, kernelParameter, elementType, functionType)
            {
            }

            #endregion

            #region Method

            public override float Operator(Matrix<float> x)
            {
                var svmKernelType = this.KernelParameter.KernelType;
                var matrixElementType = this.ElementType;
                var templateColumns = this.KernelParameter.TemplateColumns;
                var templateRows = this.KernelParameter.TemplateRows;
                var functionType = this.FunctionType;
                var function = this.Parent.NativePtr;
                var err = NativeMethods.normalized_function_operator_float(svmKernelType.ToNativeKernelType(),
                                                                           matrixElementType,
                                                                           templateRows,
                                                                           templateColumns,
                                                                           functionType,
                                                                           function,
                                                                           x.NativePtr,
                                                                           out var ret);
                return ret;
            }

            #endregion

        }

    }

}

#endif
