using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        public static Rectangle ComputeBoxDimensions(double widthToHeightRatio, double area)
        {
            if (!(widthToHeightRatio > 0 && area > 0))
                throw new ArgumentOutOfRangeException();

            var ret = NativeMethods.compute_box_dimensions(widthToHeightRatio, area);
            return new Rectangle(ret);
        }

        public static IEnumerable<Rectangle> CreateGridDetectionTemplate(Rectangle objectBox, uint cellsX, uint cellsY)
        {
            if (!(cellsX > 0 && cellsY > 0))
                throw new ArgumentOutOfRangeException();

            using(var native = objectBox.ToNative())
            {
                var ret = NativeMethods.create_grid_detection_template(native.NativePtr, cellsX, cellsY);
                using (var vector = new StdVector<Rectangle>(ret))
                    return vector.ToArray();
            }
        }

    }

}