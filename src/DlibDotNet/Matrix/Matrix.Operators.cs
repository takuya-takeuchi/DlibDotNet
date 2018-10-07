using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public partial class Matrix<TElement>
        where TElement : struct
    {

        public static Matrix<TElement> operator +(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rightTemplateRows = rhs.TemplateRows;
            var rightTemplateColumns = rhs.TemplateColumns;

            // NOTE
            // In C++, the following codes are completely different.
            //
            // 1.
            // matrix<double> left; 
            // matrix<double> right;
            // left += right;
            //
            // 2.
            // matrix<double> left; 
            // matrix<double> right;
            // auto ret = left + right;
            //
            // For 2, dlib checks whether both columns and rows are same.
            // But for 1, dlib resize left operand if both columns and rows are different.
            //

            //var leftRows = lhs.Rows;
            //var leftColumns = lhs.Columns;
            //var rightRows = rhs.Rows;
            //var rightColumns = rhs.Columns;

            //if (!(leftRows == rightRows && leftColumns == rightColumns) &&
            //    !(leftTemplateRows == rightTemplateRows && leftTemplateColumns == rightTemplateColumns))
            //    throw new ArgumentException();

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            var ret = Dlib.Native.matrix_operator_add(type,
                                                      lhs.NativePtr,
                                                      rhs.NativePtr,
                                                      leftTemplateRows,
                                                      leftTemplateColumns,
                                                      rightTemplateRows,
                                                      rightTemplateColumns,
                                                      out var matrix);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
            }

            return new Matrix<TElement>(matrix, leftTemplateRows, leftTemplateColumns);
        }

        public static Matrix<TElement> operator -(Matrix<TElement> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var templateRows = matrix.TemplateRows;
            var templateColumns = matrix.TemplateColumns;

            var type = matrix._MatrixElementTypes.ToNativeMatrixElementType();
            var ret = Dlib.Native.matrix_operator_negative(type,
                                                           matrix.NativePtr,
                                                           templateRows,
                                                           templateColumns,
                                                           out var ptr);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {matrix._MatrixElementTypes} is not supported.");
                case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
            }

            return new Matrix<TElement>(ptr, templateRows, templateColumns);
        }

        public static Matrix<TElement> operator -(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rightTemplateRows = rhs.TemplateRows;
            var rightTemplateColumns = rhs.TemplateColumns;

            // NOTE
            // In C++, the following codes are completely different.
            //
            // 1.
            // matrix<double> left; 
            // matrix<double> right;
            // left -= right;
            //
            // 2.
            // matrix<double> left; 
            // matrix<double> right;
            // auto ret = left - right;
            //
            // For 2, dlib checks whether both columns and rows are same.
            // But for 1, dlib resize left operand if both columns and rows are different.
            //

            //var leftRows = lhs.Rows;
            //var leftColumns = lhs.Columns;
            //var rightRows = rhs.Rows;
            //var rightColumns = rhs.Columns;

            //if (!(leftRows == rightRows && leftColumns == rightColumns) &&
            //    !(leftTemplateRows == rightTemplateRows && leftTemplateColumns == rightTemplateColumns))
            //    throw new ArgumentException();

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            var ret = Dlib.Native.matrix_operator_subtract(type,
                                                           lhs.NativePtr,
                                                           rhs.NativePtr,
                                                           leftTemplateRows,
                                                           leftTemplateColumns,
                                                           rightTemplateRows,
                                                           rightTemplateColumns,
                                                           out var matrix);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
            }

            return new Matrix<TElement>(matrix, leftTemplateRows, leftTemplateColumns);
        }

        public static Matrix<TElement> operator -(Matrix<TElement> lhs, DPoint rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            using (var native = rhs.ToNative())
            {
                var ret = Dlib.Native.matrix_operator_subtract_dpoint(type,
                                                                      lhs.NativePtr,
                                                                      native.NativePtr,
                                                                      templateRows,
                                                                      templateColumns,
                                                                      out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            if (!(lhs.Columns == rhs.Rows && lhs.Size > 0 && rhs.Size > 0))
                throw new ArgumentException();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rightTemplateRows = rhs.TemplateRows;
            var rightTemplateColumns = rhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            var ret = Dlib.Native.matrix_operator_multiply(type,
                                                           lhs.NativePtr,
                                                           rhs.NativePtr,
                                                           leftTemplateRows,
                                                           leftTemplateColumns,
                                                           rightTemplateRows,
                                                           rightTemplateColumns,
                                                           out var matrix);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
            }

            return new Matrix<TElement>(matrix, leftTemplateRows, leftTemplateColumns);
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, DPoint rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            using (var native = rhs.ToNative())
            {
                var ret = Dlib.Native.matrix_operator_multiply_dpoint(type,
                                                                      lhs.NativePtr,
                                                                      native.NativePtr,
                                                                      templateRows,
                                                                      templateColumns,
                                                                      out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                if (templateRows == 0 && templateColumns == 0)
                    return new Matrix<TElement>(matrix);
                return new Matrix<TElement>(matrix, 2, 1);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, byte rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.UInt8,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, ushort rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.UInt16,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, uint rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.UInt32,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, ulong rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.UInt64,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, sbyte rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.Int8,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, short rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.Int16,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, int rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.Int32,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, long rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.Int64,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, float rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.Float,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(Matrix<TElement> lhs, double rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            if (!(lhs.Size > 0))
                throw new ArgumentException();

            var templateRows = lhs.TemplateRows;
            var templateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&rhs);
                var ret = Dlib.Native.matrix_operator_multiply_right_numeric(type,
                                                                             lhs.NativePtr,
                                                                             templateRows,
                                                                             templateColumns,
                                                                             Dlib.Native.NumericType.Double,
                                                                             p,
                                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(byte lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.UInt8,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(ushort lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.UInt16,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(uint lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.UInt32,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(ulong lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.UInt64,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(sbyte lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.Int8,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(short lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.Int16,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(int lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.Int32,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(long lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.Int64,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(float lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.Float,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator *(double lhs, Matrix<TElement> rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rhs.ThrowIfDisposed();

            if (!(rhs.Size > 0))
                throw new ArgumentException();

            var templateRows = rhs.TemplateRows;
            var templateColumns = rhs.TemplateColumns;

            var type = rhs._MatrixElementTypes.ToNativeMatrixElementType();

            unsafe
            {
                var p = (IntPtr)(&lhs);
                var ret = Dlib.Native.matrix_operator_multiply_left_numeric(Dlib.Native.NumericType.Double,
                                                                            p,
                                                                            type,
                                                                            rhs.NativePtr,
                                                                            templateRows,
                                                                            templateColumns,
                                                                            out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {rhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, templateRows, templateColumns);
            }
        }

        public static Matrix<TElement> operator /(Matrix<TElement> lhs, Matrix<TElement> rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            lhs.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            if (!(rhs.Columns == 1 && rhs.Rows == 1))
                throw new ArgumentException();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;
            var rightTemplateRows = rhs.TemplateRows;
            var rightTemplateColumns = rhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            try
            {
                var ret = Dlib.Native.matrix_operator_divide(type,
                                                             lhs.NativePtr,
                                                             rhs.NativePtr,
                                                             leftTemplateRows,
                                                             leftTemplateColumns,
                                                             rightTemplateRows,
                                                             rightTemplateColumns,
                                                             out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, leftTemplateRows, leftTemplateColumns);
            }
            catch (DivideByZeroException)
            {
                // NOTE
                // Hide the source which throw exception
                throw new DivideByZeroException("Right operand may be zero matrix.");
            }
        }

        public static Matrix<TElement> operator /(Matrix<TElement> lhs, double rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException(nameof(lhs));

            lhs.ThrowIfDisposed();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var leftTemplateRows = lhs.TemplateRows;
            var leftTemplateColumns = lhs.TemplateColumns;

            var type = lhs._MatrixElementTypes.ToNativeMatrixElementType();
            try
            {
                var ret = Dlib.Native.matrix_operator_divide_double(type,
                                                                    lhs.NativePtr,
                                                                    rhs,
                                                                    leftTemplateRows,
                                                                    leftTemplateColumns,
                                                                    out var matrix);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {lhs._MatrixElementTypes} is not supported.");
                    case Dlib.Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(TemplateColumns)} or {nameof(TemplateRows)} is not supported.");
                }

                return new Matrix<TElement>(matrix, leftTemplateRows, leftTemplateColumns);
            }
            catch (DivideByZeroException)
            {
                // NOTE
                // Hide the source which throw exception
                throw new DivideByZeroException("Right operand may be zero matrix.");
            }
        }

    }

}