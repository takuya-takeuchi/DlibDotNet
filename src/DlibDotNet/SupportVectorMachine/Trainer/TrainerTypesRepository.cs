using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class TrainerTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, NativeMethods.SvmTrainerType> Types = new Dictionary<Type, NativeMethods.SvmTrainerType>();

        #endregion

        #region Constructors

        static TrainerTypesRepository()
        {
            var elementTypes = new[]
            {
                new { Type = typeof(SvmCTrainer<,>),              TrainerType = NativeMethods.SvmTrainerType.C                 },
            };

            foreach (var type in elementTypes)
                Types.Add(type.Type, type.TrainerType);
        }

        #endregion

    }

}
