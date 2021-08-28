#if !LITE
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class TrainerHelper
    {

        #region Methods

        public static void GetTypes<TScalar, TTrainer>(out Type trainerType,
                                                       out NativeMethods.SvmTrainerType svmTrainerType,
                                                       out SvmKernelType svmKernelType,
                                                       out MatrixElementTypes sampleType)
            where TScalar : struct
            where TTrainer : Trainer<TScalar>
        {
            trainerType = typeof(TTrainer);
            var svmTrainer = trainerType.GetGenericTypeDefinition();
            if (!TrainerTypesRepository.Types.TryGetValue(svmTrainer, out svmTrainerType))
                throw new ArgumentException();

            var kernelType = trainerType.GenericTypeArguments[1].GetGenericTypeDefinition();
            if (!KernelTypesRepository.KernelTypes.TryGetValue(kernelType, out svmKernelType))
                throw new ArgumentException();

            var elementType = trainerType.GenericTypeArguments[0];
            if (!KernelTypesRepository.ElementTypes.TryGetValue(elementType, out sampleType))
                throw new ArgumentException();
        }

        #endregion

    }

}

#endif
