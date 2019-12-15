using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_new(SvmKernelType kernelType,
                                                         MatrixElementType type,
                                                         out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void svm_c_trainer_delete(SvmKernelType kernelType,
                                                       MatrixElementType type,
                                                       int templateRows,
                                                       int templateColumns,
                                                       IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_kernel(SvmKernelType kernelType,
                                                                MatrixElementType type,
                                                                IntPtr trainer,
                                                                IntPtr kernel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_kernel(SvmKernelType kernelType,
                                                                MatrixElementType type,
                                                                IntPtr trainer,
                                                                out IntPtr kernel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_cache_size(SvmKernelType kernelType,
                                                                    MatrixElementType type,
                                                                    IntPtr trainer,
                                                                    int cache_size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_cache_size(SvmKernelType kernelType,
                                                                    MatrixElementType type,
                                                                    IntPtr trainer,
                                                                    out int cache_size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_train(SvmKernelType kernelType,
                                                           MatrixElementType type,
                                                           IntPtr trainer,
                                                           IntPtr x,
                                                           IntPtr y,
                                                           out IntPtr ret);

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_new2_double(SvmKernelType kernelType,
                                                                 MatrixElementType type,
                                                                 IntPtr kernel,
                                                                 double c,
                                                                 out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_c_double(SvmKernelType kernelType,
                                                                  MatrixElementType type,
                                                                  IntPtr trainer,
                                                                  double c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_c_class1_double(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         IntPtr trainer,
                                                                         double c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_c_class1_double(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         IntPtr trainer,
                                                                         out double c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_c_class2_double(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         IntPtr trainer,
                                                                         double c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_c_class2_double(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         IntPtr trainer,
                                                                         out double c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_epsilon_double(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        double epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_epsilon_double(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        out double epsilon);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_new2_float(SvmKernelType kernelType,
                                                                MatrixElementType type,
                                                                IntPtr kernel,
                                                                float c,
                                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_c_float(SvmKernelType kernelType,
                                                                 MatrixElementType type,
                                                                 IntPtr trainer,
                                                                 float c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_c_class1_float(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        float c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_c_class1_float(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        out float c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_c_class2_float(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        float c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_c_class2_float(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        out float c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_set_epsilon_float(SvmKernelType kernelType,
                                                                       MatrixElementType type,
                                                                       IntPtr trainer,
                                                                       float epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_c_trainer_get_epsilon_float(SvmKernelType kernelType,
                                                                       MatrixElementType type,
                                                                       IntPtr trainer,
                                                                       out float epsilon);
        #endregion

    }

}