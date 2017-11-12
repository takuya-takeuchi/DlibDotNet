using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void DrawSurfPoints(ImageWindow window, IEnumerable<SurfPoint> surfPoints)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            window.ThrowIfDisposed();
            surfPoints.ThrowIfDisposed();

            using (var points = new StdVector<SurfPoint>(surfPoints))
                Native.draw_surf_points(window.NativePtr, points.NativePtr);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void draw_surf_points(IntPtr win, IntPtr points);

        }

    }

}