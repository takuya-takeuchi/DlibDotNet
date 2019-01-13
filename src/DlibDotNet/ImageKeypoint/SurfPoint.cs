using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

using ErrorType = DlibDotNet.NativeMethods.ErrorType;
using MatrixElementType = DlibDotNet.NativeMethods.MatrixElementType;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class SurfPoint : DlibObject
    {

        #region Constructors

        internal SurfPoint(IntPtr ptr)
            : this(ptr, true)
        {
        }

        internal SurfPoint(IntPtr ptr, bool isEnabledDispose)
            : base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public double Angle
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.surf_point_get_angle(this.NativePtr);
            }
        }

        public InterestPoint P
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.surf_point_get_p(this.NativePtr);

                // Can not dispose because this unmanged data is in surf_point object
                return new InterestPoint(ret, false);
            }
        }

        public Matrix<double> Des
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.surf_point_get_des(this.NativePtr);
                // matrix < double,64,1 > des;
                return new SurfPointMatrix<double>(ret, false);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.surf_point_delete(this.NativePtr);
        }

        #endregion

        #endregion

        private sealed class SurfPointMatrix<T> : Matrix<T>
            where T : struct
        {

            #region Fields

            private readonly MatrixElementType _MatrixElementType;

            private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixTypes = new Dictionary<Type, MatrixElementTypes>();

            #endregion

            #region Constructors

            static SurfPointMatrix()
            {
                var matrixTypes = new[]
                {
                    new { Type = typeof(double),        ElementType = MatrixElementTypes.Double }
                };

                foreach (var type in matrixTypes)
                    SupportMatrixTypes.Add(type.Type, type.ElementType);
            }

            internal SurfPointMatrix(IntPtr ptr, bool isEnableDispose)
                : base(ptr, 0, 0, isEnableDispose)
            {
                if (!SupportMatrixTypes.TryGetValue(typeof(T), out var matrixType))
                    throw new NotSupportedException($"{typeof(T).Name} does not support");

                this._MatrixElementType = matrixType.ToNativeMatrixElementType();

                this.NativePtr = ptr;
                this.MatrixElementType = matrixType;
            }

            #endregion

            #region Properties

            public override int Columns
            {
                get
                {
                    this.ThrowIfDisposed();
                    NativeMethods.surf_point_des_matrix_nc(this.NativePtr, out var ret);
                    return ret;
                }
            }

            public override MatrixElementTypes MatrixElementType
            {
                get;
            }

            public override int Rows
            {
                get
                {
                    this.ThrowIfDisposed();
                    NativeMethods.surf_point_des_matrix_nr(this.NativePtr, out var ret);
                    return ret;
                }
            }

            #endregion

            #region Methods 

            #region Overrides 

            public override string ToString()
            {
                var ofstream = IntPtr.Zero;
                var stdstr = IntPtr.Zero;
                string str = null;

                try
                {
                    ofstream = NativeMethods.ostringstream_new();
                    NativeMethods.surf_point_des_matrix_operator_left_shift(this.NativePtr, ofstream);
                    stdstr = NativeMethods.ostringstream_str(ofstream);
                    str = StringHelper.FromStdString(stdstr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    if (stdstr != IntPtr.Zero)
                        NativeMethods.string_delete(stdstr);
                    if (ofstream != IntPtr.Zero)
                        NativeMethods.ostringstream_delete(ofstream);
                }

                return str;
            }

            protected override void DisposeUnmanaged()
            {
                // Do Not call base.DisposeUnmanaged.
                // Because base.DisposeUnmanaged calls array2d_matrix_delete and it corrupts memory
                //base.DisposeUnmanaged();
                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.matrix_delete(this._MatrixElementType, this.NativePtr, 64, 1);
            }

            #endregion

            #endregion

        }

    }

}
