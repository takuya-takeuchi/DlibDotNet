using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods


        public static BatchTrainer<TScalar, TKernel, TTrainer> BatchCached<TScalar, TKernel, TTrainer>(TTrainer trainer,
                                                                                                       TScalar minLearningRate,
                                                                                                       int cacheSize = 100)
            where TScalar : struct
            where TKernel : KernelBase
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));

            trainer.ThrowIfDisposed();

            var ret = BatchCachedBridge<TScalar, TTrainer>.Operator(trainer, minLearningRate, cacheSize);
            return new BatchTrainer<TScalar, TKernel, TTrainer>(ret);
        }

        public static Matrix<double> CrossValidateTrainer<TScalar, TTrainer>(TTrainer trainer,
                                                                             IEnumerable<Matrix<TScalar>> x,
                                                                             IEnumerable<TScalar> y,
                                                                             int folds)
            where TScalar : struct
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (y == null)
                throw new ArgumentNullException(nameof(y));

            if (!x.Any())
                throw new ArgumentException();
            if (!y.Any())
                throw new ArgumentException();
            if (!(1 < folds))
                throw new ArgumentException();

            trainer.ThrowIfDisposed();
            x.ThrowIfDisposed();

            TrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                      out var svmTrainerType,
                                                      out var svmKernelType,
                                                      out var sampleType);

            var parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);

            using (var xVector = new StdVector<Matrix<TScalar>>(x))
            using (var yVector = new StdVector<TScalar>(y))
            {
                var err = NativeMethods.cross_validate_trainer_svm_trainer(svmKernelType.ToNativeKernelType(),
                                                                           sampleType.ToNativeMatrixElementType(),
                                                                           svmTrainerType,
                                                                           trainer.NativePtr,
                                                                           parameter.TemplateRows,
                                                                           parameter.TemplateColumns,
                                                                           xVector.NativePtr,
                                                                           yVector.NativePtr,
                                                                           folds,
                                                                           out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.SvmTrainerNotSupport:
                        throw new ArgumentException($"{trainerType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{svmKernelType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{sampleType} is not supported.");
                }

                return new Matrix<double>(ret, 1, 2);
            }
        }

        public static Matrix<double> CrossValidateTrainer<TScalar, TKernel, TTrainer>(BatchTrainer<TScalar, TKernel, TTrainer> trainer,
                                                                                      IEnumerable<Matrix<TScalar>> x,
                                                                                      IEnumerable<TScalar> y,
                                                                                      int folds)
            where TScalar : struct
            where TKernel : KernelBase
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (y == null)
                throw new ArgumentNullException(nameof(y));

            if (!x.Any())
                throw new ArgumentException();
            if (!y.Any())
                throw new ArgumentException();
            if (!(1 < folds))
                throw new ArgumentException();

            trainer.ThrowIfDisposed();
            x.ThrowIfDisposed();

            BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                           out var svmTrainerType,
                                                           out var svmKernelType,
                                                           out var sampleType);

            var parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);

            using (var xVector = new StdVector<Matrix<TScalar>>(x))
            using (var yVector = new StdVector<TScalar>(y))
            {
                var err = NativeMethods.cross_validate_trainer_batch_trainer(svmKernelType.ToNativeKernelType(),
                                                                             sampleType.ToNativeMatrixElementType(),
                                                                             svmTrainerType,
                                                                             trainer.NativePtr,
                                                                             parameter.TemplateRows,
                                                                             parameter.TemplateColumns,
                                                                             xVector.NativePtr,
                                                                             yVector.NativePtr,
                                                                             folds,
                                                                             out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                        throw new ArgumentException($"{trainerType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{svmKernelType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{sampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(parameter.TemplateColumns)} or {nameof(parameter.TemplateRows)} is not supported.");
                }

                return new Matrix<double>(ret, 1, 2);
            }
        }

        public static Matrix<double> CrossValidateTrainer<TScalar, TKernel, TTrainer>(ReducedDecisionFunctionTrainer2<TScalar, TKernel, TTrainer> trainer,
                                                                                      IEnumerable<Matrix<TScalar>> x,
                                                                                      IEnumerable<TScalar> y,
                                                                                      int folds)
            where TScalar : struct
            where TKernel : KernelBase
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (y == null)
                throw new ArgumentNullException(nameof(y));

            if (!x.Any())
                throw new ArgumentException();
            if (!y.Any())
                throw new ArgumentException();
            if (!(1 < folds))
                throw new ArgumentException();

            trainer.ThrowIfDisposed();
            x.ThrowIfDisposed();

            TrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                      out var svmTrainerType,
                                                      out var svmKernelType,
                                                      out var sampleType);

            var parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);

            using (var xVector = new StdVector<Matrix<TScalar>>(x))
            using (var yVector = new StdVector<TScalar>(y))
            {
                var err = NativeMethods.cross_validate_trainer_reduced_decision_function_trainer2(svmKernelType.ToNativeKernelType(),
                                                                                                  sampleType.ToNativeMatrixElementType(),
                                                                                                  svmTrainerType,
                                                                                                  trainer.NativePtr,
                                                                                                  parameter.TemplateRows,
                                                                                                  parameter.TemplateColumns,
                                                                                                  xVector.NativePtr,
                                                                                                  yVector.NativePtr,
                                                                                                  folds,
                                                                                                  out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.SvmTrainerNotSupport:
                        throw new ArgumentException($"{trainerType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{svmKernelType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{sampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(parameter.TemplateColumns)} or {nameof(parameter.TemplateRows)} is not supported.");
                }

                return new Matrix<double>(ret, 1, 2);
            }
        }

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

        public static void RandomizeSamples<T>(IList<T> samples)
            where T : DlibObject
        {
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));

            using (var vector = new StdVector<T>(samples))
            {
                NativeMethods.randomize_samples_pointer(vector.NativePtr);
                var result = vector.ToArray();
                for (var index = 0; index < samples.Count; index++)
                    samples[index] = result[index];
            }
        }

        public static void RandomizeSamples<T1, T2>(IList<T1> samples1, IList<T2> samples2)
        {
            if (samples1 == null)
                throw new ArgumentNullException(nameof(samples1));
            if (samples2 == null)
                throw new ArgumentNullException(nameof(samples2));
            if (samples1.Count != samples2.Count)
                throw new ArgumentException();

            var indexList = Enumerable.Range(0, samples1.Count).ToArray();
            using (var vector = new StdVector<int>(indexList))
            {
                NativeMethods.randomize_samples_value(vector.NativePtr);
                var result = vector.ToArray();
                for (var index = 0; index < indexList.Length; index++)
                {
                    var newIndex = result[index];
                    var old1 = samples1[index];
                    samples1[index] = samples1[newIndex];
                    samples1[newIndex] = old1;

                    var old2 = samples2[index];
                    samples2[index] = samples2[newIndex];
                    samples2[newIndex] = old2;
                }
            }
        }

        public static ReducedDecisionFunctionTrainer2<TScalar, TKernel, TTrainer> Reduced2<TScalar, TKernel, TTrainer>(TTrainer trainer,
                                                                                                                       uint numBaseVector,
                                                                                                                       double eps = 1e-3)
            where TScalar : struct
            where TKernel : KernelBase
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));

            trainer.ThrowIfDisposed();

            TrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                      out var svmTrainerType,
                                                      out var svmKernelType,
                                                      out var sampleType);

            var parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);

            var error = NativeMethods.reduced2(parameter.KernelType.ToNativeKernelType(),
                                               parameter.SampleType.ToNativeMatrixElementType(),
                                               parameter.TemplateRows,
                                               parameter.TemplateColumns,
                                               svmTrainerType,
                                               trainer.NativePtr,
                                               numBaseVector,
                                               eps,
                                               out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.SvmTrainerNotSupport:
                    throw new ArgumentException($"{trainerType} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{svmKernelType} is not supported.");
            }

            return new ReducedDecisionFunctionTrainer2<TScalar, TKernel, TTrainer>(ret);
        }

        public static Matrix<TScalar> RankFeatures<TScalar, TKernel>(KCentroid<TScalar, TKernel> kcentroid,
                                                                     IEnumerable<Matrix<TScalar>> samples,
                                                                     IEnumerable<TScalar> labels)
            where TScalar : struct
            where TKernel : KernelBase
        {
            if (kcentroid == null)
                throw new ArgumentNullException(nameof(kcentroid));
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));
            if (labels == null)
                throw new ArgumentNullException(nameof(labels));

            var sampleArray = samples.ToArray();
            var labelsArray = labels.ToArray();

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
            using (var inLabels = new StdVector<TScalar>(labelsArray))
            {
                var type = first.MatrixElementType.ToNativeMatrixElementType();
                var error = NativeMethods.rank_features(kcentroid.Parameter.KernelType.ToNativeKernelType(),
                                                        type,
                                                        templateRow,
                                                        templateColumn,
                                                        kcentroid.NativePtr,
                                                        inSamples.NativePtr,
                                                        inLabels.NativePtr,
                                                        out var ret);
                switch (error)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{type} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(first.TemplateColumns)} or {nameof(first.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{kcentroid.Parameter.KernelType} is not supported.");
                }

                return new Matrix<TScalar>(ret, 0, 2);
            }
        }

        public static ProbabilisticDecisionFunction<TScalar, TKernel> TrainProbabilisticDecisionFunction<TScalar, TKernel, TTrainer>(TTrainer trainer,
                                                                                                                                     IEnumerable<Matrix<TScalar>> x,
                                                                                                                                     IEnumerable<TScalar> y,
                                                                                                                                     int folds)
            where TScalar : struct
            where TKernel : KernelBase
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (y == null)
                throw new ArgumentNullException(nameof(y));

            if (!x.Any())
                throw new ArgumentException();
            if (!y.Any())
                throw new ArgumentException();
            if (!(1 < folds))
                throw new ArgumentException();

            trainer.ThrowIfDisposed();
            x.ThrowIfDisposed();

            TrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                      out var svmTrainerType,
                                                      out var svmKernelType,
                                                      out var sampleType);

            var parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);

            using (var xVector = new StdVector<Matrix<TScalar>>(x))
            using (var yVector = new StdVector<TScalar>(y))
            {
                var err = NativeMethods.train_probabilistic_decision_function_svm_trainer(svmKernelType.ToNativeKernelType(),
                                                                                          sampleType.ToNativeMatrixElementType(),
                                                                                          svmTrainerType,
                                                                                          trainer.NativePtr,
                                                                                          parameter.TemplateRows,
                                                                                          parameter.TemplateColumns,
                                                                                          xVector.NativePtr,
                                                                                          yVector.NativePtr,
                                                                                          folds,
                                                                                          out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.SvmTrainerNotSupport:
                        throw new ArgumentException($"{trainerType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{svmKernelType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{sampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(parameter.TemplateColumns)} or {nameof(parameter.TemplateRows)} is not supported.");
                }

                return new ProbabilisticDecisionFunction<TScalar, TKernel>(ret, parameter);
            }
        }

        public static ProbabilisticDecisionFunction<TScalar, TKernel> TrainProbabilisticDecisionFunction<TScalar, TKernel, TTrainer>(ReducedDecisionFunctionTrainer2<TScalar, TKernel, TTrainer> trainer,
                                                                                                                                     IEnumerable<Matrix<TScalar>> x,
                                                                                                                                     IEnumerable<TScalar> y,
                                                                                                                                     int folds)
            where TScalar : struct
            where TKernel : KernelBase
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (y == null)
                throw new ArgumentNullException(nameof(y));

            if (!x.Any())
                throw new ArgumentException();
            if (!y.Any())
                throw new ArgumentException();
            if (!(1 < folds))
                throw new ArgumentException();

            trainer.ThrowIfDisposed();
            x.ThrowIfDisposed();

            TrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                      out var svmTrainerType,
                                                      out var svmKernelType,
                                                      out var sampleType);

            var parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);

            using (var xVector = new StdVector<Matrix<TScalar>>(x))
            using (var yVector = new StdVector<TScalar>(y))
            {
                var err = NativeMethods.train_probabilistic_decision_function_svm_trainer(svmKernelType.ToNativeKernelType(),
                                                                                          sampleType.ToNativeMatrixElementType(),
                                                                                          svmTrainerType,
                                                                                          trainer.NativePtr,
                                                                                          parameter.TemplateRows,
                                                                                          parameter.TemplateColumns,
                                                                                          xVector.NativePtr,
                                                                                          yVector.NativePtr,
                                                                                          folds,
                                                                                          out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.SvmTrainerNotSupport:
                        throw new ArgumentException($"{trainerType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{svmKernelType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{sampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(parameter.TemplateColumns)} or {nameof(parameter.TemplateRows)} is not supported.");
                }

                return new ProbabilisticDecisionFunction<TScalar, TKernel>(ret, parameter);
            }
        }

        public static BatchTrainer<TScalar, TKernel, TTrainer> VerboseBatchCached<TScalar, TKernel, TTrainer>(TTrainer trainer,
                                                                                                              TScalar minLearningRate,
                                                                                                              int cacheSize = 100)
            where TScalar : struct
            where TKernel : KernelBase
            where TTrainer : Trainer<TScalar>
        {
            if (trainer == null)
                throw new ArgumentNullException(nameof(trainer));

            trainer.ThrowIfDisposed();

            var ret = VerboseBatchCachedBridge<TScalar, TTrainer>.Operator(trainer, minLearningRate, cacheSize);
            return new BatchTrainer<TScalar, TKernel, TTrainer>(ret);
        }

        #endregion

        private static class BatchCachedBridge<TScalar, TTrainer>
            where TScalar : struct
            where TTrainer : Trainer<TScalar>
        {

            public static IntPtr Operator(TTrainer trainer, TScalar minLearningRate, int cacheSize)
            {
                BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                               out _,
                                                               out _,
                                                               out var sampleType);
                Bridge<TScalar> bridge;
                switch (sampleType)
                {
                    case MatrixElementTypes.Float:
                        bridge = new FloatBridge() as Bridge<TScalar>;
                        break;
                    case MatrixElementTypes.Double:
                        bridge = new DoubleBridge() as Bridge<TScalar>;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                return bridge.Operator(trainer, minLearningRate, cacheSize);
            }

#pragma warning disable 693
            private abstract class Bridge<TScalar>
#pragma warning restore 693
                where TScalar : struct
            {

                #region Methods

                public abstract IntPtr Operator(TTrainer trainer, TScalar minLearningRate, int cacheSize);

                #endregion

            }

            private sealed class FloatBridge : Bridge<float>
            {

                #region Methods

                public override IntPtr Operator(TTrainer trainer, float minLearningRate, int cacheSize)
                {
                    BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                                   out var svmTrainerType,
                                                                   out var svmKernelType,
                                                                   out var sampleType);

                    if (!(minLearningRate > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(minLearningRate)} must be greater than 0.");
                    if (!(cacheSize > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(cacheSize)} must be greater than 0.");

                    var error = NativeMethods.batch_cached_float(svmKernelType.ToNativeKernelType(),
                                                                 sampleType.ToNativeMatrixElementType(),
                                                                 svmTrainerType,
                                                                 trainer.NativePtr,
                                                                 minLearningRate,
                                                                 cacheSize,
                                                                 out var ret);

                    switch (error)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{sampleType} is not supported.");
                        case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                            throw new ArgumentException($"{svmTrainerType} is not supported.");
                        case NativeMethods.ErrorType.SvmKernelNotSupport:
                            throw new ArgumentException($"{svmKernelType} is not supported.");
                    }

                    return ret;
                }

                #endregion

            }

            private sealed class DoubleBridge : Bridge<double>
            {

                #region Methods

                public override IntPtr Operator(TTrainer trainer, double minLearningRate, int cacheSize)
                {
                    BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                                   out var svmTrainerType,
                                                                   out var svmKernelType,
                                                                   out var sampleType);

                    if (!(minLearningRate > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(minLearningRate)} must be greater than 0.");
                    if (!(cacheSize > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(cacheSize)} must be greater than 0.");

                    var error = NativeMethods.batch_cached_double(svmKernelType.ToNativeKernelType(),
                                                                  sampleType.ToNativeMatrixElementType(),
                                                                  svmTrainerType,
                                                                  trainer.NativePtr,
                                                                  minLearningRate,
                                                                  cacheSize,
                                                                  out var ret);

                    switch (error)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{sampleType} is not supported.");
                        case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                            throw new ArgumentException($"{svmTrainerType} is not supported.");
                        case NativeMethods.ErrorType.SvmKernelNotSupport:
                            throw new ArgumentException($"{svmKernelType} is not supported.");
                    }

                    return ret;
                }

                #endregion

            }

        }
        
        private static class VerboseBatchCachedBridge<TScalar, TTrainer>
            where TScalar : struct
            where TTrainer : Trainer<TScalar>
        {

            public static IntPtr Operator(TTrainer trainer, TScalar minLearningRate, int cacheSize)
            {
                BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                               out _,
                                                               out _,
                                                               out var sampleType);
                Bridge<TScalar> bridge;
                switch (sampleType)
                {
                    case MatrixElementTypes.Float:
                        bridge = new FloatBridge() as Bridge<TScalar>;
                        break;
                    case MatrixElementTypes.Double:
                        bridge = new DoubleBridge() as Bridge<TScalar>;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                return bridge.Operator(trainer, minLearningRate, cacheSize);
            }

#pragma warning disable 693
            private abstract class Bridge<TScalar>
#pragma warning restore 693
                where TScalar : struct
            {

                #region Methods

                public abstract IntPtr Operator(TTrainer trainer, TScalar minLearningRate, int cacheSize);

                #endregion

            }

            private sealed class FloatBridge : Bridge<float>
            {

                #region Methods

                public override IntPtr Operator(TTrainer trainer, float minLearningRate, int cacheSize)
                {
                    BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                                   out var svmTrainerType,
                                                                   out var svmKernelType,
                                                                   out var sampleType);

                    if (!(minLearningRate > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(minLearningRate)} must be greater than 0.");
                    if (!(cacheSize > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(cacheSize)} must be greater than 0.");

                    var error = NativeMethods.verbose_batch_cached_float(svmKernelType.ToNativeKernelType(),
                                                                         sampleType.ToNativeMatrixElementType(),
                                                                         svmTrainerType,
                                                                         trainer.NativePtr,
                                                                         minLearningRate,
                                                                         cacheSize,
                                                                         out var ret);

                    switch (error)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{sampleType} is not supported.");
                        case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                            throw new ArgumentException($"{svmTrainerType} is not supported.");
                        case NativeMethods.ErrorType.SvmKernelNotSupport:
                            throw new ArgumentException($"{svmKernelType} is not supported.");
                    }

                    return ret;
                }

                #endregion

            }

            private sealed class DoubleBridge : Bridge<double>
            {

                #region Methods

                public override IntPtr Operator(TTrainer trainer, double minLearningRate, int cacheSize)
                {
                    BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                                   out var svmTrainerType,
                                                                   out var svmKernelType,
                                                                   out var sampleType);

                    if (!(minLearningRate > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(minLearningRate)} must be greater than 0.");
                    if (!(cacheSize > 0))
                        throw new ArgumentOutOfRangeException($"{nameof(cacheSize)} must be greater than 0.");

                    var error = NativeMethods.verbose_batch_cached_double(svmKernelType.ToNativeKernelType(),
                                                                          sampleType.ToNativeMatrixElementType(),
                                                                          svmTrainerType,
                                                                          trainer.NativePtr,
                                                                          minLearningRate,
                                                                          cacheSize,
                                                                          out var ret);

                    switch (error)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{sampleType} is not supported.");
                        case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                            throw new ArgumentException($"{svmTrainerType} is not supported.");
                        case NativeMethods.ErrorType.SvmKernelNotSupport:
                            throw new ArgumentException($"{svmKernelType} is not supported.");
                    }

                    return ret;
                }

                #endregion

            }

        }

    }

}