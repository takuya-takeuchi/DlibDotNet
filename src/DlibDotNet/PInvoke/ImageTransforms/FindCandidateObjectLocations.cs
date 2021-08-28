#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType find_candidate_object_locations(Array2DType type,
                                                                       IntPtr img,
                                                                       IntPtr rect,
                                                                       MatrixElementType matrixElementType,
                                                                       IntPtr kvals,
                                                                       uint minSize,
                                                                       uint maxMergingIterations);

    }

}
#endif
