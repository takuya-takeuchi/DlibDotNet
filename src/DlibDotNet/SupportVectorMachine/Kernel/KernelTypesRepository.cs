using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class KernelTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, MatrixElementTypes> ElementTypes = new Dictionary<Type, MatrixElementTypes>();

        public static readonly Dictionary<Type, SvmKernelType> KernelTypes = new Dictionary<Type, SvmKernelType>();

        #endregion

        #region Constructors

        static KernelTypesRepository()
        {
            var elementTypes = new[]
            {
                new { Type = typeof(sbyte),         ElementType = MatrixElementTypes.Int8          },
                new { Type = typeof(short),         ElementType = MatrixElementTypes.Int16         },
                new { Type = typeof(int),           ElementType = MatrixElementTypes.Int32         },
                new { Type = typeof(byte),          ElementType = MatrixElementTypes.UInt8         },
                new { Type = typeof(ushort),        ElementType = MatrixElementTypes.UInt16        },
                new { Type = typeof(uint),          ElementType = MatrixElementTypes.UInt32        },
                new { Type = typeof(float),         ElementType = MatrixElementTypes.Float         },
                new { Type = typeof(double),        ElementType = MatrixElementTypes.Double        },
                new { Type = typeof(RgbPixel),      ElementType = MatrixElementTypes.RgbPixel      },
                new { Type = typeof(HsiPixel),      ElementType = MatrixElementTypes.HsiPixel      },
                new { Type = typeof(RgbAlphaPixel), ElementType = MatrixElementTypes.RgbAlphaPixel }
            };

            foreach (var type in elementTypes)
                ElementTypes.Add(type.Type, type.ElementType);

            var kernelTypes = new[]
            {
                new { Type = typeof(HistogramIntersectionKernel<,>), KernelType = SvmKernelType.HistogramIntersection },
                new { Type = typeof(LinearKernel<,>),                KernelType = SvmKernelType.Linear                },
                //new { Type = typeof(OffsetKernel<,>),              KernelType = SvmKernelType.Offset                },
                new { Type = typeof(PolynomialKernel<,>),            KernelType = SvmKernelType.Polynomial            },
                new { Type = typeof(RadialBasisKernel<,>),           KernelType = SvmKernelType.RadialBasis           },
                new { Type = typeof(SigmoidKernel<,>),               KernelType = SvmKernelType.Sigmoid               },
            };

            foreach (var type in kernelTypes)
                KernelTypes.Add(type.Type, type.KernelType);
        }

        #endregion

    }

}
