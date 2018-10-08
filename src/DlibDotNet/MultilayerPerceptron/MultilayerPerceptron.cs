using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// This class represents a multi layer perceptron network that is trained using the back propagation algorithm.
    /// </summary>
    /// <typeparam name="T">The type of kernel.</typeparam>
    public sealed class MultilayerPerceptron<T> : DlibObject
        where T : MultilayerPerceptronKernelBase
    {

        #region Fields

        private readonly MultilayerPerceptronKernelType _MultilayerPerceptronKernelType;

        private static readonly Dictionary<Type, MultilayerPerceptronKernelType> SupportTypes = new Dictionary<Type, MultilayerPerceptronKernelType>();

        #endregion

        #region Constructors

        static MultilayerPerceptron()
        {
            var types = new[]
            {
                new { Type = typeof(Kernel1), ElementType = MultilayerPerceptronKernelType.Kernel1 }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilayerPerceptron{T}"/> class with the specified nodes of input layer, nodes of first hidden layer, nodes of second hidden layer, nodes of output layer, alpha and momentum.
        /// </summary>
        /// <param name="nodesInInputLayer">The number of nodes for input layer.</param>
        /// <param name="nodesInFirstHiddenLayer">The number of nodes for first hidden layer.</param>
        /// <param name="nodesInSecondHiddenLayer">The number of nodes for second hidden layer.</param>
        /// <param name="nodesInOutputLayer">The number of nodes for output layer.</param>
        /// <param name="alpha">The learning rate. The default value is 0.1.</param>
        /// <param name="momentum">The momentum. The default value is 0.8.</param>
        /// <exception cref="NotSupportedException">The specified type of kernel does not supported.</exception>
        public MultilayerPerceptron(int nodesInInputLayer,
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
            this.NativePtr = Dlib.Native.mlp_kernel_new(native,
                                                        nodesInInputLayer,
                                                        nodesInFirstHiddenLayer,
                                                        nodesInSecondHiddenLayer,
                                                        nodesInOutputLayer,
                                                        alpha,
                                                        momentum);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the output of the network when it is given the input <paramref name="data"/>. The output's elements are always in the range of 0.0 to 1.0.
        /// </summary>
        /// <param name="data">The input data.</param>
        /// <returns>The output of the network.</returns>
        /// <exception cref="ArgumentException">The specified type of matrix is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is null.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="data"/> is disposed.</exception>
        public Matrix<double> Operator(MatrixBase data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.ThrowIfDisposed();

            var kernelType = this._MultilayerPerceptronKernelType.ToNativeMlpKernelType();
            var type = data.MatrixElementType.ToNativeMatrixElementType();

            var ret = Dlib.Native.mlp_kernel_operator(kernelType, this.NativePtr, type, data.NativePtr, out var retMat);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Dlib.Native.ErrorType.MlpKernelNotSupport:
                    throw new ArgumentException($"{kernelType} is not supported.");
            }

            return new Matrix<double>(retMat);
        }

        /// <summary>
        /// This function trains the network that the correct output when given <paramref name="exampleIn"/> should be <paramref name="exampleOut"/>.
        /// </summary>
        /// <param name="exampleIn">The input of example.</param>
        /// <param name="exampleOut">The output of example.</param>
        /// <exception cref="ArgumentException">The specified type of kernel is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="exampleIn"/> or <paramref name="exampleOut"/>is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="exampleOut"/> must be 0.0 - 1.0.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="exampleIn"/> or <paramref name="exampleOut"/> is disposed.</exception>
        public void Train(Matrix<double> exampleIn, Matrix<double> exampleOut)
        {
            if (exampleIn == null)
                throw new ArgumentNullException(nameof(exampleIn));
            if (exampleOut == null)
                throw new ArgumentNullException(nameof(exampleOut));

            exampleIn.ThrowIfDisposed();
            exampleOut.ThrowIfDisposed();

            var max = Dlib.Max(exampleOut);
            var min = Dlib.Min(exampleOut);
            if (!(0 <= min && max <= 1.0))
                throw new ArgumentOutOfRangeException(nameof(exampleOut), $"{nameof(exampleOut)} must be 0.0 - 1.0.");

            var kernelType = this._MultilayerPerceptronKernelType.ToNativeMlpKernelType();
            var ret = Dlib.Native.mlp_kernel_train_matrix(kernelType, this.NativePtr, exampleIn.NativePtr, exampleOut.NativePtr);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MlpKernelNotSupport:
                    throw new ArgumentException($"{kernelType} is not supported.");
            }
        }

        /// <summary>
        /// This function trains the network that the correct output when given <paramref name="exampleIn"/> should be <paramref name="exampleOut"/>.
        /// </summary>
        /// <param name="exampleIn">The input of example.</param>
        /// <param name="exampleOut">The output of example.</param>
        /// <exception cref="ArgumentException">The specified type of kernel is not supported.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="exampleIn"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="exampleOut"/> must be 0.0 - 1.0.</exception>
        /// <exception cref="ObjectDisposedException"><paramref name="exampleIn"/> is disposed.</exception>
        public void Train(Matrix<double> exampleIn, double exampleOut)
        {
            if (exampleIn == null)
                throw new ArgumentNullException(nameof(exampleIn));

            exampleIn.ThrowIfDisposed();
            
            if(!(0<= exampleOut && exampleOut <= 1.0))
                throw new ArgumentOutOfRangeException(nameof(exampleOut), $"{nameof(exampleOut)} must be 0.0 - 1.0.");

            var kernelType = this._MultilayerPerceptronKernelType.ToNativeMlpKernelType();
            var ret = Dlib.Native.mlp_kernel_train(kernelType, this.NativePtr, exampleIn.NativePtr, exampleOut);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MlpKernelNotSupport:
                    throw new ArgumentException($"{kernelType} is not supported.");
            }
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            var kernelType = this._MultilayerPerceptronKernelType.ToNativeMlpKernelType();
            Dlib.Native.mlp_kernel_delete(kernelType, this.NativePtr);
        }

        #endregion

        #endregion

    }

}