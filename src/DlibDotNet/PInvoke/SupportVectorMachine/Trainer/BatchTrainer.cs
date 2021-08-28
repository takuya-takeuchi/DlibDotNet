#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_trainer_new(SvmKernelType kernelType,
                                                         MatrixElementType type,
                                                         SvmBatchTrainerType trainerType,
                                                         out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void batch_trainer_delete(SvmKernelType kernelType,
                                                       MatrixElementType type,
                                                       SvmBatchTrainerType trainerType,
                                                       IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_trainer_train(SvmKernelType kernel_type,
                                                           MatrixElementType type,
                                                           SvmBatchTrainerType trainer_type,
                                                           IntPtr trainer,
                                                           IntPtr x,
                                                           IntPtr y,
                                                           out IntPtr ret);

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_cached_double(SvmKernelType kernelType,
                                                           MatrixElementType type,
                                                           SvmBatchTrainerType trainerType,
                                                           IntPtr trainer,
                                                           double minLearningRate,
                                                           int cacheSize,
                                                           out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType verbose_batch_cached_double(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   SvmBatchTrainerType trainerType,
                                                                   IntPtr trainer,
                                                                   double minLearningRate,
                                                                   int cacheSize,
                                                                   out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_trainer_new2_double(SvmKernelType kernelType,
                                                                 MatrixElementType type,
                                                                 SvmBatchTrainerType trainerType,
                                                                 IntPtr trainer,
                                                                 double minLearningRate,
                                                                 bool verbose,
                                                                 bool useCache,
                                                                 int cacheSize,
                                                                 out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_trainer_get_min_learning_rate_double(SvmKernelType kernelType,
                                                                                  MatrixElementType type,
                                                                                  SvmBatchTrainerType trainerType,
                                                                                  IntPtr trainer,
                                                                                  out double ret);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_cached_float(SvmKernelType kernelType,
                                                          MatrixElementType type,
                                                          SvmBatchTrainerType trainerType,
                                                          IntPtr trainer,
                                                          float minLearningRate,
                                                          int cacheSize,
                                                          out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType verbose_batch_cached_float(SvmKernelType kernelType,
                                                                  MatrixElementType type,
                                                                  SvmBatchTrainerType trainerType,
                                                                  IntPtr trainer,
                                                                  float minLearningRate,
                                                                  int cacheSize,
                                                                  out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_trainer_new2_float(SvmKernelType kernelType,
                                                                MatrixElementType type,
                                                                SvmBatchTrainerType trainerType,
                                                                IntPtr trainer,
                                                                float minLearningRate,
                                                                bool verbose,
                                                                bool useCache,
                                                                int cacheSize,
                                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType batch_trainer_get_min_learning_rate_float(SvmKernelType kernelType,
                                                                                 MatrixElementType type,
                                                                                 SvmBatchTrainerType trainerType,
                                                                                 IntPtr trainer,
                                                                                 out float ret);

        #endregion

    }

}
#endif
