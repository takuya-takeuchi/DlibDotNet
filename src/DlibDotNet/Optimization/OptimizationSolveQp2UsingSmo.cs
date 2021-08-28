#if !LITE
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static double MaximumNu(IEnumerable<double> y)
        {
            if (y == null)
                throw new ArgumentNullException(nameof(y));
            if (!y.Any())
                throw new ArgumentException();

            using (var vector = new StdVector<double>(y))
            {
                var ret = NativeMethods.maximum_nu_double_vector(vector.NativePtr,
                                                                 out var result);

                return result;
            }
        }

        public static float MaximumNu(IEnumerable<float> y)
        {
            if (y == null)
                throw new ArgumentNullException(nameof(y));
            if (!y.Any())
                throw new ArgumentException();

            using (var vector = new StdVector<float>(y))
            {
                var ret = NativeMethods.maximum_nu_float_vector(vector.NativePtr,
                                                                out var result);

                return result;
            }
        }

        #endregion

    }

}

#endif
