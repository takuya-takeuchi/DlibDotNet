#if !LITE
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static ImageWindow.OverlayLine[] RenderFaceDetections(FullObjectDetection detection)
        {
#if !DLIB_NO_GUI_SUPPORT
            return RenderFaceDetections(detection, new RgbPixel { Green = 255 });
#else
            throw new NotSupportedException();
#endif
        }

        public static ImageWindow.OverlayLine[] RenderFaceDetections(FullObjectDetection detection, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (detection == null)
                throw new ArgumentNullException(nameof(detection));

            detection.ThrowIfDisposed(nameof(detection));

            using (var vector = new StdVector<ImageWindow.OverlayLine>())
            {
                NativeMethods.render_face_detections(detection.NativePtr, ref color, vector.NativePtr);
                return vector.ToArray();
            }
#else
            throw new NotSupportedException();
#endif
        }

        public static ImageWindow.OverlayLine[] RenderFaceDetections(IEnumerable<FullObjectDetection> shapes)
        {
#if !DLIB_NO_GUI_SUPPORT
            return RenderFaceDetections(shapes, new RgbPixel { Green = 255 });
#else
            throw new NotSupportedException();
#endif
        }

        public static ImageWindow.OverlayLine[] RenderFaceDetections(IEnumerable<FullObjectDetection> detection, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (detection == null)
                throw new ArgumentNullException(nameof(detection));

            using (var vectorIn = new StdVector<FullObjectDetection>(detection))
            using (var vectorOut = new StdVector<ImageWindow.OverlayLine>())
            {
                NativeMethods.render_face_detections(vectorIn.NativePtr, ref color, vectorOut.NativePtr);
                return vectorOut.ToArray();
            }
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

    }

}
#endif
