using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_output_stdvector_stdvector_mmod_rect_delete(IntPtr vector);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dnn_output_stdvector_stdvector_mmod_rect_getItem(IntPtr vector, int index);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int dnn_output_stdvector_stdvector_mmod_rect_getSize(IntPtr vector);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_output_stdvector_mmod_rect_delete(IntPtr vector);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dnn_output_stdvector_mmod_rect_getItem(IntPtr vector, int index);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int dnn_output_stdvector_mmod_rect_getSize(IntPtr vector);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType loss_mmod_new(int type, out IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType loss_mmod_new2(int type, IntPtr option, out IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void loss_mmod_delete(IntPtr obj, int type);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType loss_mmod_deserialize(byte[] fileName, int type, out IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType loss_mmod_deserialize_proxy(IntPtr proxy_deserialize, int type, out IntPtr net);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void loss_mmod_serialize(IntPtr obj, int type, byte[] fileName);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void loss_mmod_input_tensor_to_output_tensor(IntPtr net, int networkType, IntPtr p, out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int loss_mmod_num_layers(int type);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void loss_mmod_clean(int type);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void loss_mmod_input_layer(IntPtr net, int networkType, out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType loss_mmod_subnet(IntPtr net, int type, out IntPtr subnet);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType loss_mmod_operator_left_shift(IntPtr obj, int type, IntPtr ofstream);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType loss_mmod_operator_matrixs(IntPtr obj,
                                                                  int type,
                                                                  MatrixElementType element_type,
                                                                  IntPtr matrixs,
                                                                  int templateRows,
                                                                  int templateColumns,
                                                                  ulong batchSize,
                                                                  out IntPtr ret);
        
        #region trainer

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dnn_trainer_loss_mmod_new(IntPtr net, int type);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_delete(IntPtr trainer, int type);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_be_verbose(IntPtr trainer, int type);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_set_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_get_learning_rate(IntPtr trainer, int type, out double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_set_min_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_set_mini_batch_size(IntPtr trainer, int type, uint size);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_set_synchronization_file(IntPtr trainer, int type, byte[] filename, uint second);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_test_one_step(IntPtr trainer,
                                                                           int type,
                                                                           MatrixElementType dataElementType,
                                                                           IntPtr data,
                                                                           MatrixElementType labelElementType,
                                                                           IntPtr label);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_train(IntPtr trainer,
                                                                   int type,
                                                                   MatrixElementType dataElementType,
                                                                   IntPtr data,
                                                                   MatrixElementType labelElementType,
                                                                   IntPtr label);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_train_one_step(IntPtr trainer,
                                                                            int type,
                                                                            MatrixElementType dataElementType,
                                                                            IntPtr data,
                                                                            MatrixElementType labelElementType,
                                                                            IntPtr label);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_set_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                       int type,
                                                                                                       uint thresh);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_set_test_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                            int type,
                                                                                                            uint thresh);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_get_net(IntPtr trainer, int type, out IntPtr ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_operator_left_shift(IntPtr trainer, int type, IntPtr stream);

        #endregion

        #region subnet

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr loss_mmod_subnet_get_layer_details(IntPtr subnet, int type, out ErrorType ret);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void loss_mmod_subnet_delete(int type, IntPtr subnet);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr loss_mmod_subnet_get_output(IntPtr subnet, int type, out ErrorType ret);

        #endregion

        #region layer_details

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void loss_mmod_layer_details_set_num_filters(IntPtr layer, int type, int num);

        #endregion

    }

}