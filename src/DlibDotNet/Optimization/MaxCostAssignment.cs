#if !LITE
using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        #region AssignmentCost

        public static byte AssignmentCost(Matrix<byte> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out byte result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        public static ushort AssignmentCost(Matrix<ushort> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out ushort result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        public static uint AssignmentCost(Matrix<uint> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out uint result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        public static sbyte AssignmentCost(Matrix<sbyte> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out sbyte result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        public static short AssignmentCost(Matrix<short> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out short result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        public static int AssignmentCost(Matrix<int> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out int result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        public static double AssignmentCost(Matrix<double> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out double result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        public static float AssignmentCost(Matrix<float> cost, IEnumerable<long> assignment)
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>(assignment))
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.assignment_cost(type,
                                                        cost.NativePtr,
                                                        vector.NativePtr,
                                                        out float result);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return result;
            }
        }

        #endregion

        public static IEnumerable<long> MaxCostAssignment<T>(Matrix<T> cost)
            where T : struct
        {
            if (cost == null)
                throw new ArgumentNullException(nameof(cost));
            if (cost.Rows != cost.Columns)
                throw new ArgumentException($"{cost.Rows} must equal to {cost.Columns}");

            using (var vector = new StdVector<long>())
            {
                var type = cost.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.max_cost_assignment(type,
                                                            cost.NativePtr,
                                                            vector.NativePtr);
                if (ret == NativeMethods.ErrorType.MatrixElementTypeNotSupport)
                    throw new ArgumentException($"{cost.MatrixElementType} is not supported.");

                return vector.ToArray();
            }
        }
        
        #endregion

    }

}

#endif
