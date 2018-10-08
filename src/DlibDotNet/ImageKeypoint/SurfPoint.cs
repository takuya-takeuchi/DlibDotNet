using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

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
                return Native.surf_point_get_angle(this.NativePtr);
            }
        }

        public InterestPoint P
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = Native.surf_point_get_p(this.NativePtr);

                // Can not dispose because this unmanged data is in surf_point object
                return new InterestPoint(ret, false);
            }
        }

        public Matrix<double> Des
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = Native.surf_point_get_des(this.NativePtr);
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

            Native.surf_point_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double surf_point_get_angle(IntPtr surfpoint);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr surf_point_get_p(IntPtr surfpoint);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr surf_point_get_des(IntPtr surfpoint);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void surf_point_delete(IntPtr surfpoint);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType surf_point_des_matrix_operator_left_shift(IntPtr matrix, IntPtr ofstream);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType surf_point_des_matrix_nc(IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType surf_point_des_matrix_nr(IntPtr matrix, out int ret);


        }

        private sealed class SurfPointMatrix<T> : Matrix<T>
            where T : struct
        {

            #region Fields

            private readonly Dlib.Native.MatrixElementType _MatrixElementType;

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
                    Native.surf_point_des_matrix_nc(this.NativePtr, out var ret);
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
                    Native.surf_point_des_matrix_nr(this.NativePtr, out var ret);
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
                    ofstream = Dlib.Native.ostringstream_new();
                    Native.surf_point_des_matrix_operator_left_shift(this.NativePtr, ofstream);
                    stdstr = Dlib.Native.ostringstream_str(ofstream);
                    str = StringHelper.FromStdString(stdstr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    if (stdstr != IntPtr.Zero)
                        Dlib.Native.string_delete(stdstr);
                    if (ofstream != IntPtr.Zero)
                        Dlib.Native.ostringstream_delete(ofstream);
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

                Dlib.Native.matrix_delete(this._MatrixElementType, this.NativePtr, 64, 1);
            }

            #endregion

            #endregion

        }

    }

}
