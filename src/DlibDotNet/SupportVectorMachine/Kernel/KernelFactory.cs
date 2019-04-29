using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class KernelFactory
    {

        #region Fields

        private static readonly Dictionary<Type, KernelType> SupportKernelTypes = new Dictionary<Type, KernelType>();

        private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixElementTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors

        static KernelFactory()
        {
            var kernelTypes = new[]
            {
                new {Type = typeof(HistogramIntersectionKernel<,>), KernelType = KernelType.HistogramIntersection },
                new {Type = typeof(LinearKernel<,>),                KernelType = KernelType.Linear                },
                new {Type = typeof(RadialBasisKernel<,>),           KernelType = KernelType.RadialBasis           },
                new {Type = typeof(SigmoidKernel<,>),               KernelType = KernelType.Sigmoid               },
                new {Type = typeof(PolynomialKernel<,>),            KernelType = KernelType.Polynomial            }
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
                case KernelType.HistogramIntersection:
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

        public static bool TryParse<TScalar>(out MatrixElementTypes result)
            where TScalar : struct
        {
            return TryParse(typeof(TScalar), out result);
        }

        public static bool TryParse(Type type, out MatrixElementTypes result)
        {
            return SupportMatrixElementTypes.TryGetValue(type, out result);
        }

        public static bool TryParse<TKernel>(out KernelType result)
            where TKernel : KernelBase
        {
            return TryParse(typeof(TKernel), out result);
        }

        public static bool TryParse(Type type, out KernelType result)
        {
            return SupportKernelTypes.TryGetValue(type, out result);
        }

        #endregion

    }

}