using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class KernelFactory
    {

        #region Fields

        private static readonly Dictionary<Type, SvmKernelType> SupportKernelTypes = new Dictionary<Type, SvmKernelType>();

        private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixElementTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors

        static KernelFactory()
        {
            var kernelTypes = new[]
            {
                new {Type = typeof(HistogramIntersectionKernel<,>), KernelType = SvmKernelType.HistogramIntersection },
                new {Type = typeof(LinearKernel<,>),                KernelType = SvmKernelType.Linear                },
                new {Type = typeof(RadialBasisKernel<,>),           KernelType = SvmKernelType.RadialBasis           },
                new {Type = typeof(SigmoidKernel<,>),               KernelType = SvmKernelType.Sigmoid               },
                new {Type = typeof(PolynomialKernel<,>),            KernelType = SvmKernelType.Polynomial            }
            };

            var matrixElementTypes = new[]
            {
                new {Type = typeof(float),   MatrixElementType = MatrixElementTypes.Float  },
                new {Type = typeof(double),  MatrixElementType = MatrixElementTypes.Double }
            };

            foreach (var type in kernelTypes)
                SupportKernelTypes.Add(type.Type, type.KernelType);
            foreach (var type in matrixElementTypes)
                SupportMatrixElementTypes.Add(type.Type, type.MatrixElementType);
        }

        #endregion

        #region Methods

        public static TKernel Create<TKernel, TScalar, TSample>(IntPtr ptr, 
                                                                SvmKernelType kernelType,
                                                                int templateRow, 
                                                                int templateColumn,
                                                                bool isEnabledDispose = true)
            where TKernel : KernelBase
            where TScalar : struct
            where TSample : Matrix<TScalar>, new()
        {
            switch (kernelType)
            {
                case SvmKernelType.HistogramIntersection:
                    return new HistogramIntersectionKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case SvmKernelType.Linear:
                    return new LinearKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case SvmKernelType.Polynomial:
                    return new PolynomialKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case SvmKernelType.RadialBasis:
                    return new RadialBasisKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                case SvmKernelType.Sigmoid:
                    return new SigmoidKernel<TScalar, TSample>(ptr, templateRow, templateColumn, isEnabledDispose) as TKernel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kernelType), kernelType, null);
            }
        }

        public static bool TryParse<TScalar>(out MatrixElementTypes result)
            where TScalar : struct
        {
            return TryParse(typeof(TScalar), out result);
        }

        public static bool TryParse(Type type, out MatrixElementTypes result)
        {
            return SupportMatrixElementTypes.TryGetValue(type, out result);
        }

        public static bool TryParse<TKernel>(out SvmKernelType result)
            where TKernel : KernelBase
        {
            return TryParse(typeof(TKernel), out result);
        }

        public static bool TryParse(Type type, out SvmKernelType result)
        {
            return SupportKernelTypes.TryGetValue(type, out result);
        }

        #endregion

    }

}
