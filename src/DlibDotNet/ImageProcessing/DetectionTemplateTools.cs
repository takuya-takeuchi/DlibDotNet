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

    }

}