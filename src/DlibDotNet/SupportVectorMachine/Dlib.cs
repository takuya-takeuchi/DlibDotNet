using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static IEnumerable<Matrix<T>> FindClustersUsingAngularKMeans<T>(IEnumerable<Matrix<T>> samples, IEnumerable<Matrix<T>> centers, uint maxIteration = 1000)
            where T : struct
        {
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));
            if (centers == null)
                throw new ArgumentNullException(nameof(centers));

            var sampleArray = samples.ToArray();
            if (!sampleArray.Any())
                yield break;

            var centerArray = centers.ToArray();
            if (!centerArray.Any())
                yield break;

            var sample = sampleArray.FirstOrDefault();
            if (sample == null)
                throw new ArgumentException($"{nameof(samples)} contains null object", nameof(samples));

            var center = centerArray.FirstOrDefault();
            if (center == null)
                throw new ArgumentException($"{nameof(centers)} contains null object", nameof(centers));

            var templateRow = sample.TemplateRows;
            var templateColumn = sample.TemplateColumns;

            var param = new MatrixTemplateSizeParameter(templateRow, templateColumn);
            using (var inSamples = new StdVector<Matrix<T>>(sampleArray, param))
            using (var inCenters = new StdVector<Matrix<T>>(centerArray, param))
            using (var outResult = new StdVector<Matrix<T>>(param))
            {
                var type = sample.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.find_clusters_using_angular_kmeans(type,
                                                                           templateRow,
                                                                           templateColumn,
                                                                           inCenters.NativePtr,
                                                                           inSamples.NativePtr,
                                                                           maxIteration,
                                                                           outResult.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{type} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(sample.TemplateColumns)} or {nameof(sample.TemplateRows)} is not supported.");
                }

                foreach (var result in outResult.ToArray())
                    yield return result;
            }
        }

        public static uint NearestCenter<T>(IEnumerable<Matrix<T>> centers, Matrix<T> sample)
            where T : struct
        {
            if (centers == null)
                throw new ArgumentNullException(nameof(centers));
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            var centerArray = centers.ToArray();
            if (!centerArray.Any())
                throw new ArgumentException($"{nameof(centers)} does not contain any element");

            var center = centerArray.FirstOrDefault();
            if (center == null)
                throw new ArgumentException($"{nameof(centers)} contains null object", nameof(centers));

            var templateRow = sample.TemplateRows;
            var templateColumn = sample.TemplateColumns;

            var param = new MatrixTemplateSizeParameter(templateRow, templateColumn);
            using (var inCenters = new StdVector<Matrix<T>>(centerArray, param))
            {
                var type = sample.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.nearest_center(type,
                                                       templateRow,
                                                       templateColumn,
                                                       inCenters.NativePtr,
                                                       sample.NativePtr,
                                                       out var result);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{type} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(sample.TemplateColumns)} or {nameof(sample.TemplateRows)} is not supported.");
                }

                return result;
            }
        }

        public static IEnumerable<Matrix<TScalar>> PickInitialCenters<TScalar, TKernel>(int numberCenters, 
                                                                                        IEnumerable<Matrix<TScalar>> samples, 
                                                                                        TKernel kernel, 
                                                                                        double percentile = 0.01)
            where TScalar : struct
            where TKernel : KernelBase
        {
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));

            var sampleArray = samples.ToArray();
            if (!sampleArray.Any())
                return new Matrix<TScalar>[0];

            var first = sampleArray.FirstOrDefault();
            if (first == null)
                throw new ArgumentException($"{nameof(samples)} contains null object", nameof(samples));

            var templateRow = first.TemplateRows;
            var templateColumn = first.TemplateColumns;
            foreach (var sample in sampleArray)
            {
                if (sample == null)
                    throw new ArgumentException($"{nameof(samples)} contains null object", nameof(samples));
                if (sample.TemplateRows != templateRow)
                    throw new ArgumentException($"{nameof(samples)} contains different {nameof(sample.TemplateRows)} of {typeof(Matrix<TScalar>).Name}", nameof(samples));
                if (sample.TemplateColumns != templateColumn)
                    throw new ArgumentException($"{nameof(samples)} contains different {nameof(sample.TemplateColumns)} of {typeof(Matrix<TScalar>).Name}", nameof(samples));
            }

            var param = new MatrixTemplateSizeParameter(templateRow, templateColumn);
            using (var inSamples = new StdVector<Matrix<TScalar>>(sampleArray, param))
            using (var outCenters = new StdVector<Matrix<TScalar>>(0, param))
            {
                var type = first.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.pick_initial_centers(kernel.KernelType.ToNativeKernelType(),
                                                             type,
                                                             templateRow,
                                                             templateColumn,
                                                             numberCenters,
                                                             outCenters.NativePtr,
                                                             inSamples.NativePtr,
                                                             kernel.NativePtr,
                                                             percentile);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{type} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(first.TemplateColumns)} or {nameof(first.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{kernel.KernelType} is not supported.");
                }

                return outCenters.ToArray();
            }
        }

        #endregion

    }

}