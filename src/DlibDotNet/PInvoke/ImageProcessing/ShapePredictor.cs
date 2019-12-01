using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr shape_predictor_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_num_parts(IntPtr predictor);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint shape_predictor_num_features(IntPtr predictor);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType serialize_shape_predictor(IntPtr predictor, byte[] filName, int filNameLength, out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_shape_predictor(byte[] filName, int fileNameLength, out IntPtr predictor, out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_shape_predictor_proxy(IntPtr proxy_deserialize, 
                                                                         out IntPtr predictor,
                                                                         out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType shape_predictor_operator(IntPtr detector,
                                                                Array2DType imgType,
                                                                IntPtr img,
                                                                IntPtr rect,
                                                                out IntPtr fullObjDetect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType shape_predictor_matrix_operator(IntPtr detector,
                                                                       MatrixElementType imgType,
                                                                       IntPtr img,
                                                                       IntPtr rect,
                                                                       out IntPtr fullObjDetect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType shape_predictor_operator_mmod_rect(IntPtr detector,
                                                                          Array2DType imgType,
                                                                          IntPtr img,
                                                                          IntPtr rect,
                                                                          out IntPtr fullObjDetect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType shape_predictor_matrix_operator_mmod_rect(IntPtr detector,
                                                                                 MatrixElementType imgType,
                                                                                 IntPtr img,
                                                                                 IntPtr rect,
                                                                                 out IntPtr fullObjDetect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void shape_predictor_delete(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType shape_predictor_test_shape_predictor(IntPtr predictor,
                                                                            Array2DType imgType,
                                                                            IntPtr array_array2d,
                                                                            IntPtr objects,
                                                                            IntPtr scales,
                                                                            out double ret);

    }

}