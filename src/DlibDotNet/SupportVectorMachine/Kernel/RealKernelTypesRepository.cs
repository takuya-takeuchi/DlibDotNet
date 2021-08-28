using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class RealKernelTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, MatrixElementTypes> SupportTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors
        
        static RealKernelTypesRepository()
        {
            var types = new[]
            {
                new { Type = typeof(float),         ElementType = MatrixElementTypes.Float  },
                new { Type = typeof(double),        ElementType = MatrixElementTypes.Double }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion

    }

}
