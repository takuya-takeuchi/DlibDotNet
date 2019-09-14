using System;
using System.Runtime.InteropServices;
using OutputLabelType = System.UInt32;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool LossMulticlassLogRegistry_add(IntPtr builder);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void LossMulticlassLogRegistry_remove(IntPtr builder);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool LossMulticlassLogRegistry_contains(int id);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLog_anet_1000_type_create();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLog_net_type2_create();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLog_net_type_create();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLog_net_1000_type_create();

        #region Loss

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_new(int id, out IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void LossMulticlassLog_delete(int id, IntPtr obj);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_operator_matrixs(int id,
                                                                          IntPtr obj,
                                                                          MatrixElementType element_type,
                                                                          IntPtr matrix_vector,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          ulong batch_size,
                                                                          out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_probability(int id,
                                                                     IntPtr obj,
                                                                     MatrixElementType element_type,
                                                                     IntPtr matrix_vector,
                                                                     int templateRows,
                                                                     int templateColumns,
                                                                     ulong batch_size,
                                                                     out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_deserialize(int id,
                                                                     byte[] file_name,
                                                                     out IntPtr ret,
                                                                     out IntPtr error_message);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_deserialize_proxy(int id,
                                                                           IntPtr proxy,
                                                                           out IntPtr ret,
                                                                           out IntPtr error_message);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_serialize(int id,
                                                                   IntPtr obj,
                                                                   byte[] file_name,
                                                                   out IntPtr error_message);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int LossMulticlassLog_get_num_layers(int id);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_clean(int id, IntPtr obj);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_input_tensor_to_output_tensor(int id, IntPtr obj, IntPtr p, out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_net_to_xml(int id, IntPtr obj, byte[] filename);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_operator_left_shift(int id, IntPtr trainer, IntPtr stream);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_set_all_bn_running_stats_window_sizes(int id,
                                                                                               IntPtr obj,
                                                                                               uint new_window_size);

        #region trainer

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLog_trainer_new(int id, IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLog_trainer_new2(int id, IntPtr net, IntPtr sgd);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void LossMulticlassLog_trainer_delete(int id, IntPtr trainer);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_set_learning_rate(int id, IntPtr trainer, double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_get_learning_rate(int id, IntPtr trainer, out double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_get_average_loss(int id, IntPtr trainer, out double loss);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_get_average_test_loss(int id, IntPtr trainer, out double loss);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_set_min_learning_rate(int id, IntPtr trainer, double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_set_mini_batch_size(int id, IntPtr trainer, uint size);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_be_verbose(int id, IntPtr trainer);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_set_synchronization_file(int id,
                                                                                          IntPtr trainer,
                                                                                          byte[] filename,
                                                                                          uint second);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_set_iterations_without_progress_threshold(int id,
                                                                                                           IntPtr trainer,
                                                                                                           uint thresh);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_set_test_iterations_without_progress_threshold(int id,
                                                                                                                IntPtr trainer,
                                                                                                                uint thresh);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_get_net(int id,
                                                                         IntPtr trainer,
                                                                         out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_operator_left_shift(int id,
                                                                                     IntPtr trainer,
                                                                                     IntPtr stream);
        
        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_test_one_step(int id,
                                                                               IntPtr trainer,
                                                                               MatrixElementType data_element_type,
                                                                               IntPtr data,
                                                                               MatrixElementType label_element_type,
                                                                               IntPtr labels);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_train(int id,
                                                                       IntPtr trainer,
                                                                       MatrixElementType data_element_type,
                                                                       IntPtr data,
                                                                       MatrixElementType label_element_type,
                                                                       IntPtr labels);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_trainer_train_one_step(int id,
                                                                                IntPtr trainer,
                                                                                MatrixElementType data_element_type,
                                                                                IntPtr data,
                                                                                MatrixElementType label_element_type,
                                                                                IntPtr labels);

        #endregion

        #region subnet

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_subnet(int id, IntPtr obj, out IntPtr subnet);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_subnet_get_layer_details(int id, IntPtr subnet, out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLog_subnet_get_output(int id,
                                                                        IntPtr subnet,
                                                                        out int ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_subnet_delete(int id, IntPtr subnet);

        #endregion

        #region layer_details

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLog_layer_details_set_num_filters(int id, IntPtr layer, long num);

        #endregion

        #endregion

        #region output

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_output_uint32_t_delete(IntPtr vector);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern OutputLabelType dnn_output_uint32_t_getItem(IntPtr vector, int index);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int dnn_output_uint32_t_getSize(IntPtr vector);

        #endregion

    }

}