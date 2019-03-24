using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region input

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType input_rgb_image_pyramid_new(PyramidType pyramidType,
                                                                   uint pyramidRate,
                                                                   out IntPtr ret);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void input_rgb_image_pyramid_delete(IntPtr input,
                                                                 PyramidType pyramidType,
                                                                 uint pyramidRate);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType input_rgb_image_pyramid_to_tensor(IntPtr input,
                                                                         PyramidType pyramidType,
                                                                         uint pyramidRate,
                                                                         MatrixElementType elementType,
                                                                         IntPtr matrix,
                                                                         int templateRows,
                                                                         int templateColumns,
                                                                         uint iteratorCount,
                                                                         IntPtr tensor);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType input_rgb_image_pyramid_get_pyramid_padding(IntPtr input,
                                                                                   PyramidType pyramidType,
                                                                                   uint pyramidRate,
                                                                                   out uint pyramidPadding);


        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType input_rgb_image_pyramid_get_pyramid_outer_padding(IntPtr input,
                                                                                         PyramidType pyramidType,
                                                                                         uint pyramidRate,
                                                                                         out uint pyramidOuterPadding);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType input_rgb_image_pyramid_image_space_to_tensor_space(IntPtr input,
                                                                                           PyramidType pyramidType,
                                                                                           uint pyramidRate,
                                                                                           IntPtr data,
                                                                                           double scale,
                                                                                           IntPtr r,
                                                                                           out IntPtr rect);

        #endregion

        #region validation

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType test_object_detection_function_net(int type,
                                                                          IntPtr detector,
                                                                          MatrixElementType elementType,
                                                                          IntPtr matrixVector,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          IntPtr truthDets,
                                                                          IntPtr overlapTester,
                                                                          double adjustThreshold,
                                                                          IntPtr overlapsIgnoreTester,
                                                                          out IntPtr ret);

        #endregion

    }

}