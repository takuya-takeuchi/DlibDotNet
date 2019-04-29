using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class KernelFactory
    {

        #region Methods

        public static TKernel Create<TKernel, TScalar, TSample>(IntPtr ptr, 
                                                                KernelType kernelType,
                                                                int templateRow, 
                                                                int templateColumn,
                                                                bool isEnabledDispose = true)
            where TKernel : KernelBase
            where TScalar : struct
            where TSample : Matrix<TScalar>, new()
        {
            switch (kernelType)
            {
                case KernelType.Histogramintersection:
                    return new HistogramIntersectionKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case KernelType.Linear:
                    return new LinearKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case KernelType.Polynomial:
                    return new PolynomialKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case KernelType.RadialBasis:
                    return new RadialBasisKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case KernelType.Sigmoid:
                    return new SigmoidKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kernelType), kernelType, null);
            }
        }

        #endregion

    }

}