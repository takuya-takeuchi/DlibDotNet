#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_new(SvmKernelType kernelType,
                                                          MatrixElementType type,
                                                          out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void svm_nu_trainer_delete(SvmKernelType kernelType,
                                                        MatrixElementType type,
                                                        int templateRows,
                                                        int templateColumns,
                                                        IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_set_kernel(SvmKernelType kernelType,
                                                                 MatrixElementType type,
                                                                 IntPtr trainer,
                                                                 IntPtr kernel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_get_kernel(SvmKernelType kernelType,
                                                                MatrixElementType type,
                                                                IntPtr trainer,
                                                                out IntPtr kernel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_set_cache_size(SvmKernelType kernelType,
                                                                     MatrixElementType type,
                                                                     IntPtr trainer,
                                                                     int cache_size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_get_cache_size(SvmKernelType kernelType,
                                                                     MatrixElementType type,
                                                                     IntPtr trainer,
                                                                     out int cache_size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_train(SvmKernelType kernelType,
                                                            MatrixElementType type,
                                                            IntPtr trainer,
                                                            IntPtr x,
                                                            IntPtr y,
                                                            out IntPtr ret);

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_new2_double(SvmKernelType kernelType,
                                                                  MatrixElementType type,
                                                                  IntPtr kernel,
                                                                  double nu,
                                                                  out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_set_nu_double(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   IntPtr trainer,
                                                                   double nu);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_set_nu_double(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   IntPtr trainer,
                                                                   out double nu);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_set_epsilon_double(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         IntPtr trainer,
                                                                         double epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_get_epsilon_double(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         IntPtr trainer,
                                                                         out double epsilon);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_new2_float(SvmKernelType kernelType,
                                                                 MatrixElementType type,
                                                                 IntPtr kernel,
                                                                 float nu,
                                                                 out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_set_nu_float(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   IntPtr trainer,
                                                                   float nu);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_get_nu_float(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   IntPtr trainer,
                                                                   out float nu);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_set_epsilon_float(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        float epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_nu_trainer_get_epsilon_float(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        out float epsilon);
        #endregion

    }

}
#endif
