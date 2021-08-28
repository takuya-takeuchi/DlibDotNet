#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr get_frontal_face_detector();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType frontal_face_detector_operator(IntPtr detector,
                                                                      Array2DType imgType,
                                                                      IntPtr img,
                                                                      double adjustThreshold,
                                                                      IntPtr dets);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType frontal_face_detector_matrix_operator(IntPtr detector,
                                                                             MatrixElementType imgType,
                                                                             IntPtr img,
                                                                             double adjustThreshold,
                                                                             IntPtr dets);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType frontal_face_detector_matrix_operator2(IntPtr detector,
                                                                              MatrixElementType imgType,
                                                                              IntPtr img,
                                                                              double adjustThreshold,
                                                                              IntPtr dets);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void frontal_face_detector_delete(IntPtr detector);

    }

}
#endif
