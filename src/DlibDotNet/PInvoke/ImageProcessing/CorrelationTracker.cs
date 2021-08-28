#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr correlation_tracker_new(uint filterSize,
                                                            uint numScaleLevels,
                                                            uint scaleWindowSize,
                                                            double regularizerSpace,
                                                            double nuSpace,
                                                            double regularizerScale,
                                                            double nuScale,
                                                            double scalePyramidAlpha);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType correlation_tracker_start_track(IntPtr tracker,
                                                                       Array2DType imgType,
                                                                       IntPtr img,
                                                                       IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr correlation_tracker_get_position(IntPtr tracker);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType correlation_tracker_update_noscale(IntPtr tracker,
                                                                          Array2DType imgType,
                                                                          IntPtr img,
                                                                          IntPtr guess,
                                                                          out double confident);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType correlation_tracker_update_noscale2(IntPtr tracker,
                                                                           Array2DType imgType,
                                                                           IntPtr img,
                                                                           out double confident);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType correlation_tracker_update(IntPtr tracker,
                                                                  Array2DType imgType,
                                                                  IntPtr img,
                                                                  IntPtr guess,
                                                                  out double confident);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType correlation_tracker_update2(IntPtr tracker,
                                                                   Array2DType imgType,
                                                                   IntPtr img,
                                                                   out double confident);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void correlation_tracker_delete(IntPtr point);


    }

}
#endif
