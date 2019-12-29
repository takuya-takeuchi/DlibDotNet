using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType cross_validate_trainer_svm_trainer(SvmKernelType kernelType,
                                                                          MatrixElementType type,
                                                                          SvmTrainerType trainerType,
                                                                          IntPtr trainer,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          IntPtr x,
                                                                          IntPtr y,
                                                                          int folds,
                                                                          out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType cross_validate_trainer_batch_trainer(SvmKernelType kernelType,
                                                                            MatrixElementType type,
                                                                            SvmBatchTrainerType trainerType,
                                                                            IntPtr trainer,
                                                                            int templateRows,
                                                                            int templateColumns,
                                                                            IntPtr x,
                                                                            IntPtr y,
                                                                            int folds,
                                                                            out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType cross_validate_trainer_reduced_decision_function_trainer2(SvmKernelType kernelType,
                                                                                                 MatrixElementType type,
                                                                                                 SvmTrainerType trainerType,
                                                                                                 IntPtr trainer,
                                                                                                 int templateRows,
                                                                                                 int templateColumns,
                                                                                                 IntPtr x,
                                                                                                 IntPtr y,
                                                                                                 int folds,
                                                                                                 out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType nearest_center(MatrixElementType type,
                                                      int templateRows,
                                                      int templateColumns,
                                                      IntPtr centers,
                                                      IntPtr sample,
                                                      out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType find_clusters_using_angular_kmeans(MatrixElementType type,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          IntPtr centers,
                                                                          IntPtr samples,
                                                                          uint max_iter,
                                                                          IntPtr result);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType pick_initial_centers(SvmKernelType kernelType, 
                                                            MatrixElementType elementType,
                                                            int templateRows,
                                                            int templateColumns,
                                                            long num_centers,
                                                            IntPtr centers,
                                                            IntPtr samples,
                                                            IntPtr k,
                                                            double percentile);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType rank_features(SvmKernelType kernelType, 
                                                     MatrixElementType type,
                                                     int templateRows,
                                                     int templateColumns,
                                                     IntPtr kcentroid,
                                                     IntPtr samples,
                                                     IntPtr labels,
                                                     out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType randomize_samples_pointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType randomize_samples_value(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType train_probabilistic_decision_function_svm_trainer(SvmKernelType kernelType,
                                                                                         MatrixElementType type,
                                                                                         SvmTrainerType trainerType,
                                                                                         IntPtr trainer,
                                                                                         int templateRows,
                                                                                         int templateColumns,
                                                                                         IntPtr x,
                                                                                         IntPtr y,
                                                                                         int folds,
                                                                                         out IntPtr ret);

    }

}