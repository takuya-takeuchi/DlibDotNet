using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class NumericKernelTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        #endregion

        #region Constructors
        
        static NumericKernelTypesRepository()
        {
            var types = new[]
            {
                new { Type = typeof(Matrix<sbyte>),         ElementType = ElementTypes.Int8          },
                new { Type = typeof(Matrix<short>),         ElementType = ElementTypes.Int16         },
                new { Type = typeof(Matrix<int>),           ElementType = ElementTypes.Int32         },
                new { Type = typeof(Matrix<byte>),          ElementType = ElementTypes.UInt8         },
                new { Type = typeof(Matrix<ushort>),        ElementType = ElementTypes.UInt16        },
                new { Type = typeof(Matrix<uint>),          ElementType = ElementTypes.UInt32        },
                new { Type = typeof(Matrix<float>),         ElementType = ElementTypes.Float         },
                new { Type = typeof(Matrix<double>),        ElementType = ElementTypes.Double        }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion
        
        public enum ElementTypes
        {

            Int8,

            Int16,

            Int32,

            UInt8,

            UInt16,

            UInt32,

            Float,

            Double,

            RgbPixel,

            HsiPixel,

            RgbAlphaPixel

        }

    }

}
