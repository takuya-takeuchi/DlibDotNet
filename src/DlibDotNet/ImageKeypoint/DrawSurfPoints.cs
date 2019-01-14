using System;
using System.Collections.Generic;
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
            if (surfPoints == null)
                throw new ArgumentNullException(nameof(surfPoints));

            window.ThrowIfDisposed();
            surfPoints.ThrowIfDisposed();

            using (var points = new StdVector<SurfPoint>(surfPoints))
                NativeMethods.draw_surf_points(window.NativePtr, points.NativePtr);
        }

        #endregion

    }

}