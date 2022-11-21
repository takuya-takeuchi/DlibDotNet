using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr get_image_gradients(long scale);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_gradients_gradient_x(IntPtr gradients,
                                                                  Array2DType imgType,
                                                                  IntPtr in_img,
                                                                  IntPtr out_img,
                                                                  IntPtr valid_rectangle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_gradients_gradient_y(IntPtr gradients,
                                                                  Array2DType imgType,
                                                                  IntPtr in_img,
                                                                  IntPtr out_img,
                                                                  IntPtr valid_rectangle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_gradients_gradient_xx(IntPtr gradients,
                                                                   Array2DType imgType,
                                                                   IntPtr in_img,
                                                                   IntPtr out_img,
                                                                   IntPtr valid_rectangle);


        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_gradients_gradient_xy(IntPtr gradients,
                                                                   Array2DType imgType,
                                                                   IntPtr in_img,
                                                                   IntPtr out_img,
                                                                   IntPtr valid_rectangle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_gradients_gradient_yy(IntPtr gradients,
                                                                   Array2DType imgType,
                                                                   IntPtr in_img,
                                                                   IntPtr out_img,
                                                                   IntPtr valid_rectangle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_gradients_delete(IntPtr gradients);

    }

}