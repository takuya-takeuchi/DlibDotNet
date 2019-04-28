using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class KernelTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        #endregion

        #region Constructors
        
        static KernelTypesRepository()
        {
            var types = new[]
            {
                new { Type = typeof(sbyte),         ElementType = ElementTypes.Int8          },
                new { Type = typeof(short),         ElementType = ElementTypes.Int16         },
                new { Type = typeof(int),           ElementType = ElementTypes.Int32         },
                new { Type = typeof(byte),          ElementType = ElementTypes.UInt8         },
                new { Type = typeof(ushort),        ElementType = ElementTypes.UInt16        },
                new { Type = typeof(uint),          ElementType = ElementTypes.UInt32        },
                new { Type = typeof(float),         ElementType = ElementTypes.Float         },
                new { Type = typeof(double),        ElementType = ElementTypes.Double        },
                new { Type = typeof(RgbPixel),      ElementType = ElementTypes.RgbPixel      },
                new { Type = typeof(HsiPixel),      ElementType = ElementTypes.HsiPixel      },
                new { Type = typeof(RgbAlphaPixel), ElementType = ElementTypes.RgbAlphaPixel }
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
