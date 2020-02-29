using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_new(SvmKernelType kernelType,
                                                       MatrixElementType type,
                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void svm_pegasos_delete(SvmKernelType kernelType,
                                                     MatrixElementType type,
                                                     IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_kernel(SvmKernelType kernelType,
                                                              MatrixElementType type,
                                                              IntPtr trainer,
                                                              IntPtr kernel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_kernel(SvmKernelType kernelType,
                                                              MatrixElementType type,
                                                              IntPtr trainer,
                                                              out IntPtr kernel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_max_num_sv(SvmKernelType kernelType,
                                                                  MatrixElementType type,
                                                                  IntPtr trainer,
                                                                  uint max_num_sv);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_max_num_sv(SvmKernelType kernelType,
                                                                  MatrixElementType type,
                                                                  IntPtr trainer,
                                                                  out uint max_num_sv);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_train_count(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   IntPtr trainer,
                                                                   out uint train_count);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_clear(SvmKernelType kernelType,
                                                         MatrixElementType type,
                                                         IntPtr trainer);

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_new2_double(SvmKernelType kernelType,
                                                               MatrixElementType type,
                                                               IntPtr kernel,
                                                               double lambda,
                                                               double tolerance,
                                                               uint max_num_sv,
                                                               out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_operator_double(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   IntPtr trainer,
                                                                   IntPtr sample,
                                                                   out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_lambda_double(SvmKernelType kernelType,
                                                                     MatrixElementType type,
                                                                     IntPtr trainer,
                                                                     double lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_lambda_class1_double(SvmKernelType kernelType,
                                                                            MatrixElementType type,
                                                                            IntPtr trainer,
                                                                            double lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_lambda_class1_double(SvmKernelType kernelType,
                                                                            MatrixElementType type,
                                                                            IntPtr trainer,
                                                                            out double lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_lambda_class2_double(SvmKernelType kernelType,
                                                                            MatrixElementType type,
                                                                            IntPtr trainer,
                                                                            double lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_lambda_class2_double(SvmKernelType kernelType,
                                                                            MatrixElementType type,
                                                                            IntPtr trainer,
                                                                            out double lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_tolerance_double(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        double epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_tolerance_double(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        IntPtr trainer,
                                                                        out double epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_train_double(SvmKernelType kernelType,
                                                                MatrixElementType type,
                                                                IntPtr trainer,
                                                                IntPtr x,
                                                                double y,
                                                                out double ret);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_new2_float(SvmKernelType kernelType,
                                                              MatrixElementType type,
                                                              IntPtr kernel,
                                                              float lambda,
                                                              float tolerance,
                                                              uint max_num_sv,
                                                              out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_operator_float(SvmKernelType kernelType,
                                                                  MatrixElementType type,
                                                                  IntPtr trainer,
                                                                  IntPtr sample,
                                                                  out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_lambda_float(SvmKernelType kernelType,
                                                                    MatrixElementType type,
                                                                    IntPtr trainer,
                                                                    float lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_lambda_class1_float(SvmKernelType kernelType,
                                                                           MatrixElementType type,
                                                                           IntPtr trainer,
                                                                           float lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_lambda_class1_float(SvmKernelType kernelType,
                                                                           MatrixElementType type,
                                                                           IntPtr trainer,
                                                                           out float lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_lambda_class2_float(SvmKernelType kernelType,
                                                                           MatrixElementType type,
                                                                           IntPtr trainer,
                                                                           float lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_lambda_class2_float(SvmKernelType kernelType,
                                                                           MatrixElementType type,
                                                                           IntPtr trainer,
                                                                           out float lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_set_tolerance_float(SvmKernelType kernelType,
                                                                       MatrixElementType type,
                                                                       IntPtr trainer,
                                                                       float epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_get_tolerance_float(SvmKernelType kernelType,
                                                                       MatrixElementType type,
                                                                       IntPtr trainer,
                                                                       out float epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType svm_pegasos_train_float(SvmKernelType kernelType,
                                                               MatrixElementType type,
                                                               IntPtr trainer,
                                                               IntPtr x,
                                                               float y,
                                                               out float ret);
        #endregion

    }

}