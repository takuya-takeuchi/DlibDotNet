using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static ImageWindow.OverlayLine[] RenderFaceDetections(FullObjectDetection detection)
        {
            return RenderFaceDetections(detection, new RgbPixel { Green = 255 });
        }

        public static ImageWindow.OverlayLine[] RenderFaceDetections(FullObjectDetection detection, RgbPixel color)
        {
            if (detection == null)
                throw new ArgumentNullException(nameof(detection));

            detection.ThrowIfDisposed(nameof(detection));

            using (var vector = new StdVectorOfImageWindowOverlayLine())
            {
                Native.render_face_detections(detection.NativePtr, ref color, vector.NativePtr);
                return vector.ToArray();
            }
        }

        public static ImageWindow.OverlayLine[] RenderFaceDetections(IEnumerable<FullObjectDetection> shapes)
        {
            return RenderFaceDetections(shapes, new RgbPixel { Green = 255 });
        }

        public static ImageWindow.OverlayLine[] RenderFaceDetections(IEnumerable<FullObjectDetection> detection, RgbPixel color)
        {
            if (detection == null)
                throw new ArgumentNullException(nameof(detection));

            using (var vectorIn = new StdVectorOfFullObjectDetection(detection))
            using (var vectorOut = new StdVectorOfImageWindowOverlayLine())
            {
                Native.render_face_detections(vectorIn.NativePtr, ref color, vectorOut.NativePtr);
                return vectorOut.ToArray();
            }
        }

        #endregion

        internal sealed partial class Native
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

}