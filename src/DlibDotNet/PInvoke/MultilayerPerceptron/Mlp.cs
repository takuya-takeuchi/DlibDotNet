using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr mlp_kernel_new(MlpKernelType kernel_type, int nodes_in_input_layer, int nodes_in_first_hidden_layer, int nodes_in_second_hidden_layer, int nodes_in_output_layer, double alpha, double momentum);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType mlp_kernel_train(MlpKernelType kernel_type, IntPtr kernel, IntPtr example_in, double example_out);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType mlp_kernel_train_matrix(MlpKernelType kernel_type, IntPtr kernel, IntPtr example_in, IntPtr example_out);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType mlp_kernel_operator(MlpKernelType kernel_type, IntPtr kernel, MatrixElementType type, IntPtr data, out IntPtr ret_mat);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mlp_kernel_delete(MlpKernelType kernel_type, IntPtr kernel);

    }

}