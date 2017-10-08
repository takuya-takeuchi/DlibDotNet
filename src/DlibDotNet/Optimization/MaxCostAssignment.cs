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

        public static IEnumerable<long> MaxCostAssignment<T>(Matrix<T> cost)
            where T : struct
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new VectorOfLong())
            {
                var ret = Native.max_cost_assignment(
                    cost.MatrixElementType.ToNativeMatrixElementType(),
                    cost.NativePtr,
                    vector.NativePtr);
                if (ret == Native.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return vector.ToArray();
            }
        }

        #region Helpers
        #endregion

        #endregion

        internal sealed partial class Native
        {

            #region max_cost_assignment

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType max_cost_assignment(MatrixElementType elementType, IntPtr cost, IntPtr assignments);

            #endregion

        }

    }

}
