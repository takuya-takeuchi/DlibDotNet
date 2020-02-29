using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType reduced2(SvmKernelType kernel_type,
                                                MatrixElementType type,
                                                int templateRows,
                                                int templateColumns,
                                                SvmTrainerType trainer_type,
                                                IntPtr trainer,
                                                uint num_bv,
                                                double eps,
                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType reduced_decision_function_trainer2_new(SvmKernelType kernel_type,
                                                                              MatrixElementType type,
                                                                              int templateRows,
                                                                              int templateColumns,
                                                                              SvmTrainerType trainer_type,
                                                                              IntPtr trainer,
                                                                              uint num_bv,
                                                                              double eps,
                                                                              out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void reduced_decision_function_trainer2_delete(SvmKernelType kernel_type,
                                                                            MatrixElementType type,
                                                                            int templateRows,
                                                                            int templateColumns,
                                                                            SvmTrainerType trainer_type,
                                                                            IntPtr trainer);

        #region train
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType reduced_decision_function_trainer2_train_double(SvmKernelType kernel_type,
                                                                                       MatrixElementType type,
                                                                                       int templateRows,
                                                                                       int templateColumns,
                                                                                       SvmTrainerType trainer_type,
                                                                                       IntPtr trainer,
                                                                                       IntPtr x,
                                                                                       IntPtr y,
                                                                                       out IntPtr ret);

        #endregion

    }

}