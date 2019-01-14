using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void render_face_detections(IntPtr dets,
                                                         ref RgbPixel color,
                                                         IntPtr vectorOfLine);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void render_face_detections2(IntPtr dets,
                                                          ref RgbPixel color,
                                                          IntPtr vectorOfLine);

    }

}