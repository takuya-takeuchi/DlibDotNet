#if !LITE
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class BatchTrainerTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, NativeMethods.SvmBatchTrainerType> Types = new Dictionary<Type, NativeMethods.SvmBatchTrainerType>();

        #endregion

        #region Constructors

        static BatchTrainerTypesRepository()
        {
            var elementTypes = new[]
            {
                new { Type = typeof(SvmPegasos<,>), TrainerType = NativeMethods.SvmBatchTrainerType.Pegasos  },
            };

            foreach (var type in elementTypes)
                Types.Add(type.Type, type.TrainerType);
        }

        #endregion

    }

}

#endif
