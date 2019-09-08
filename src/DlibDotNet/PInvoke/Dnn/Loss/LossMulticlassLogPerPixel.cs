using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool LossMulticlassLogPerPixelRegistry_add(IntPtr builder);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void LossMulticlassLogPerPixelRegistry_remove(IntPtr builder);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLogPerPixel_net_type_create();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLogPerPixel_anet_type_create();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLogPerPixel_uanet_type_create();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLogPerPixel_ubnet_type_create();

        #region Loss

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_new(int id, out IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void LossMulticlassLogPerPixel_delete(int id, IntPtr obj);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ushort LossMulticlassLogPerPixel_get_label_to_ignore();

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_operator_matrixs(int id,
                                                                                  IntPtr obj,
                                                                                  MatrixElementType element_type,
                                                                                  IntPtr matrix_vector,
                                                                                  int templateRows,
                                                                                  int templateColumns,
                                                                                  ulong batch_size,
                                                                                  out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_deserialize(int id,
                                                                             byte[] file_name,
                                                                             out IntPtr ret,
                                                                             out IntPtr error_message);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_deserialize_proxy(int id,
                                                                                   IntPtr proxy,
                                                                                   out IntPtr ret,
                                                                                   out IntPtr error_message);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_serialize(int id,
                                                                           IntPtr obj,
                                                                           byte[] file_name,
                                                                           out IntPtr error_message);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int LossMulticlassLogPerPixel_get_num_layers(int id);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_clean(int id, IntPtr obj);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_input_tensor_to_output_tensor(int id, IntPtr obj, IntPtr p, out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_net_to_xml(int id, IntPtr obj, byte[] filename);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_operator_left_shift(int id, IntPtr trainer, IntPtr stream);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_set_all_bn_running_stats_window_sizes(int id,
                                                                                                       IntPtr obj,
                                                                                                       uint new_window_size);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_cloneAs(int id,
                                                                         IntPtr obj,
                                                                         int dst_id,
                                                                         out IntPtr ret);
        #region trainer

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLogPerPixel_trainer_new(int id, IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLogPerPixel_trainer_new2(int id, IntPtr net, IntPtr sgd);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void LossMulticlassLogPerPixel_trainer_delete(int id, IntPtr trainer);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_set_learning_rate(int id, IntPtr trainer, double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_get_learning_rate(int id, IntPtr trainer, out double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_get_average_loss(int id, IntPtr trainer, out double loss);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_get_average_test_loss(int id, IntPtr trainer, out double loss);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_set_min_learning_rate(int id, IntPtr trainer, double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_set_mini_batch_size(int id, IntPtr trainer, uint size);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_be_verbose(int id, IntPtr trainer);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_set_synchronization_file(int id,
                                                                                                  IntPtr trainer,
                                                                                                  byte[] filename,
                                                                                                  uint second);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_set_iterations_without_progress_threshold(int id,
                                                                                                                   IntPtr trainer,
                                                                                                                   uint thresh);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_set_test_iterations_without_progress_threshold(int id,
                                                                                                                        IntPtr trainer,
                                                                                                                        uint thresh);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_get_net(int id,
                                                                                 IntPtr trainer,
                                                                                 out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_operator_left_shift(int id,
                                                                                             IntPtr trainer,
                                                                                             IntPtr stream);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_test_one_step(int id,
                                                                                       IntPtr trainer,
                                                                                       MatrixElementType data_element_type,
                                                                                       IntPtr data,
                                                                                       MatrixElementType label_element_type,
                                                                                       IntPtr labels);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_train(int id,
                                                                               IntPtr trainer,
                                                                               MatrixElementType data_element_type,
                                                                               IntPtr data,
                                                                               MatrixElementType label_element_type,
                                                                               IntPtr labels);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_trainer_train_one_step(int id,
                                                                                        IntPtr trainer,
                                                                                        MatrixElementType data_element_type,
                                                                                        IntPtr data,
                                                                                        MatrixElementType label_element_type,
                                                                                        IntPtr labels);

        #endregion

        #region subnet

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_subnet(int id, IntPtr obj, out IntPtr subnet);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_subnet_get_layer_details(int id, IntPtr subnet, out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr LossMulticlassLogPerPixel_subnet_get_output(int id,
                                                                                IntPtr subnet,
                                                                                out int ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_subnet_delete(int id, IntPtr subnet);

        #endregion

        #region layer_details

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType LossMulticlassLogPerPixel_layer_details_set_num_filters(int id, IntPtr layer, long num);

        #endregion

        #endregion

        #region output

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_output_stdvector_uint16_delete(IntPtr vector);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dnn_output_stdvector_uint16_getItem(IntPtr vector, int index);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int dnn_output_stdvector_uint16_getSize(IntPtr vector);

        #endregion

    }

}