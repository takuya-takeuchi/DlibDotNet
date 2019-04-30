using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr shape_predictor_trainer_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_delete(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_trainer_get_cascade_depth(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_cascade_depth(IntPtr trainer, uint depth);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_trainer_get_tree_depth(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_tree_depth(IntPtr trainer, uint depth);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_trainer_get_num_trees_per_cascade_level(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_num_trees_per_cascade_level(IntPtr trainer, uint num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double shape_predictor_trainer_get_nu(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_nu(IntPtr trainer, double nu);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_trainer_get_oversampling_amount(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_oversampling_amount(IntPtr trainer, uint amount);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double shape_predictor_trainer_get_oversampling_translation_jitter(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_oversampling_translation_jitter(IntPtr trainer, double amount);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_trainer_get_feature_pool_size(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_feature_pool_size(IntPtr trainer, uint size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double shape_predictor_trainer_get_lambda(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_lambda(IntPtr trainer, double lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_trainer_get_num_test_splits(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_num_test_splits(IntPtr trainer, uint num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_trainer_get_num_threads(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_num_threads(IntPtr trainer, uint num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double shape_predictor_trainer_get_feature_pool_region_padding(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_feature_pool_region_padding(IntPtr trainer, double lambda);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern PaddingMode shape_predictor_trainer_get_padding_mode(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_padding_mode(IntPtr trainer, PaddingMode mode);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr shape_predictor_trainer_get_random_seed(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_set_random_seed(IntPtr trainer, IntPtr seed);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_be_verbose(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_trainer_be_quiet(IntPtr trainer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType shape_predictor_trainer_train(IntPtr trainer,
                                                                     Array2DType imgType,
                                                                     IntPtr array_array2d,
                                                                     IntPtr objects,
                                                                     out IntPtr predictor);

    }

}