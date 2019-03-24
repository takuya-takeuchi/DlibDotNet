using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_output_stdvector_float_1_1_delete(IntPtr vector);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr dnn_output_stdvector_float_1_1_getItem(IntPtr vector, int index);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern int dnn_output_stdvector_float_1_1_getSize(IntPtr vector);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType loss_metric_new(int type, out IntPtr net);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void loss_metric_delete(IntPtr obj, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType loss_metric_deserialize(byte[] fileName, int type, out IntPtr net);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType loss_metric_deserialize_proxy(IntPtr proxy_deserialize, int type, out IntPtr net);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void loss_metric_serialize(IntPtr obj, int type, byte[] fileName);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void loss_metric_input_tensor_to_output_tensor(IntPtr net, int networkType, IntPtr p, out IntPtr ret);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern int loss_metric_num_layers(int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void loss_metric_clean(int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType loss_metric_subnet(IntPtr net, int type, out IntPtr subnet);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType loss_metric_operator_left_shift(IntPtr obj, int type, IntPtr ofstream);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType loss_metric_operator_matrixs(IntPtr obj,
                                                                    int type,
                                                                    MatrixElementType element_type,
                                                                    IntPtr matrixs,
                                                                    int templateRows,
                                                                    int templateColumns,
                                                                    ulong batchSize,
                                                                    out IntPtr ret);

        #region subnet

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void loss_metric_subnet_delete(int type, IntPtr subnet);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr loss_metric_subnet_get_output(IntPtr subnet, int type, out ErrorType ret);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr loss_metric_subnet_get_layer_details(IntPtr subnet, int type, out ErrorType ret);

        #endregion

        #region layer_details

        #endregion

    }

}