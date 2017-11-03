using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MultilayerPerceptron<T> : DlibObject
        where T : MultilayerPerceptronKernelBase
    {

        #region Fields

        private readonly MultilayerPerceptronKernelTypes _MultilayerPerceptronKernelType;

        private static readonly Dictionary<Type, MultilayerPerceptronKernelTypes> SupportTypes = new Dictionary<Type, MultilayerPerceptronKernelTypes>();

        #endregion

        #region Constructors

        static MultilayerPerceptron()
        {
            var types = new[]
            {
                new { Type = typeof(Kernel1), ElementType = MultilayerPerceptronKernelTypes.Kernel1 }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public MultilayerPerceptron(
            int nodesInInputLayer,
            int nodesInFirstHiddenLayer,
            int nodesInSecondHiddenLayer = 0,
            int nodesInOutputLayer = 1,
            double alpha = 0.1,
            double momentum = 0.8)
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._MultilayerPerceptronKernelType = type;
            var native = type.ToNativeMlpKernelType();
            this.NativePtr = Dlib.Native.mlp_kernel_new(
                native,
                nodesInInputLayer,
                nodesInFirstHiddenLayer,
                nodesInSecondHiddenLayer,
                nodesInOutputLayer,
                alpha,
                momentum);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void Train(MatrixBase exampleIn, double exampleOut)
        {
            if (exampleIn == null)
                throw new ArgumentNullException(nameof(exampleIn));

            exampleIn.ThrowIfDisposed();

            var type = exampleIn.MatrixElementType.ToNativeMatrixElementType();
            Dlib.Native.mlp_kernel_train(this.NativePtr, type, exampleIn.NativePtr, exampleOut);
        }

        #region Overrids
        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

    }

}
