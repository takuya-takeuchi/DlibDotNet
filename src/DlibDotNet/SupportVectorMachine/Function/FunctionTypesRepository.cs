#if !LITE
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class FunctionTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, NativeMethods.SvmFunctionType> Types = new Dictionary<Type, NativeMethods.SvmFunctionType>();

        #endregion

        #region Constructors

        static FunctionTypesRepository()
        {
            var elementTypes = new[]
            {
                new { Type = typeof(DecisionFunction<,>),              FunctionType = NativeMethods.SvmFunctionType.Decision                 },
                //new { Type = typeof(ProbabilisticDecisionFunction<,>), FunctionType = NativeMethods.SvmFunctionType.Distance                 },
                //new { Type = typeof(int),                              FunctionType = NativeMethods.SvmFunctionType.MulticlassLinearDecision },
                new { Type = typeof(ProbabilisticDecisionFunction<,>), FunctionType = NativeMethods.SvmFunctionType.ProbabilisticDecision    },
                new { Type = typeof(ProjectionFunction<,>),            FunctionType = NativeMethods.SvmFunctionType.Projection               },
            };

            foreach (var type in elementTypes)
                Types.Add(type.Type, type.FunctionType);
        }

        #endregion

    }

}

#endif
