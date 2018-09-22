using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MatrixOp : TwoDimensionObjectBase
    {

        #region Fields

        private readonly ImageTypes _ImageType;

        private readonly Dlib.Native.Array2DType _Array2DType;

        private readonly Dlib.Native.ElementType _ElementType;

        private readonly Dlib.Native.MatrixElementType _MatrixElementType;

        #endregion

        #region Constructors

        internal MatrixOp(Dlib.Native.ElementType elementType, ImageTypes type, IntPtr ptr)
        {
            this._ElementType = elementType;
            this._Array2DType = type.ToNativeArray2DType();
            this.NativePtr = ptr;
            this._ImageType = type;

            this.TemplateRows = -1;
            this.TemplateColumns = -1;
        }

        internal MatrixOp(Dlib.Native.ElementType elementType, MatrixElementTypes type, IntPtr ptr, int templateRows = 0, int temlateColumns = 0)
        {
            this._ElementType = elementType;
            this._MatrixElementType = type.ToNativeMatrixElementType();
            this.NativePtr = ptr;

            this.TemplateRows = templateRows;
            this.TemplateColumns = temlateColumns;
        }

        #endregion

        #region Properties

        internal Dlib.Native.Array2DType Array2DType => this._Array2DType;

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();

                var ret = 0;
                switch (this._ElementType)
                {
                    case Dlib.Native.ElementType.OpHeatmap:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            Native.matrix_op_op_heatmap_nc(this._Array2DType, this.NativePtr, out ret);
                        else
                            Native.matrix_op_op_heatmap_nc_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpJet:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            Native.matrix_op_op_jet_nc(this._Array2DType, this.NativePtr, out ret);
                        else
                            Native.matrix_op_op_jet_nc_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpArray2DToMat:
                        Native.matrix_op_op_array2d_to_mat_nc(this._Array2DType, this.NativePtr, out ret);
                        break;
                    case Dlib.Native.ElementType.OpTrans:
                        Native.matrix_op_op_trans_nc(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpStdVectToMat:
                        Native.matrix_op_op_std_vect_to_mat_nc(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpJoinRows:
                        Native.matrix_op_op_join_rows_nc(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                }

                return ret;
            }
        }

        internal Dlib.Native.ElementType ElementType => this._ElementType;

        internal Dlib.Native.MatrixElementType MatrixElementType => this._MatrixElementType;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();

                var ret = 0;
                switch (this._ElementType)
                {
                    case Dlib.Native.ElementType.OpHeatmap:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            Native.matrix_op_op_heatmap_nr(this._Array2DType, this.NativePtr, out ret);
                        else
                            Native.matrix_op_op_heatmap_nr_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpJet:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            Native.matrix_op_op_jet_nr(this._Array2DType, this.NativePtr, out ret);
                        else
                            Native.matrix_op_op_jet_nr_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpArray2DToMat:
                        Native.matrix_op_op_array2d_to_mat_nr(this._Array2DType, this.NativePtr, out ret);
                        break;
                    case Dlib.Native.ElementType.OpTrans:
                        Native.matrix_op_op_trans_nr(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpStdVectToMat:
                        Native.matrix_op_op_std_vect_to_mat_nr(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case Dlib.Native.ElementType.OpJoinRows:
                        Native.matrix_op_op_join_rows_nr(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                }

                return ret;
            }
        }

        internal int TemplateColumns
        {
            get;
        }

        internal int TemplateRows
        {
            get;
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            switch (this._ElementType)
            {
                case Dlib.Native.ElementType.OpHeatmap:
                    Native.matrix_op_op_heatmap_delete(this._Array2DType, this.NativePtr);
                    break;
                case Dlib.Native.ElementType.OpJet:
                    Native.matrix_op_op_jet_delete(this._Array2DType, this.NativePtr);
                    break;
                case Dlib.Native.ElementType.OpArray2DToMat:
                    Native.matrix_op_op_array2d_to_mat_delete(this._Array2DType, this.NativePtr);
                    break;
                case Dlib.Native.ElementType.OpTrans:
                    Native.matrix_op_op_trans_delete(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
                    break;
                case Dlib.Native.ElementType.OpStdVectToMat:
                    Native.matrix_op_op_std_vect_to_mat_delete(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
                    break;
                case Dlib.Native.ElementType.OpJoinRows:
                    Native.matrix_op_op_join_rows_delete(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
                    break;
            }
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            string str = null;

            try
            {
                ofstream = Dlib.Native.ostringstream_new();

                Dlib.Native.ErrorType ret;
                switch (this._ElementType)
                {
                    case Dlib.Native.ElementType.OpTrans:
                        ret = Native.matrix_op_op_trans_operator_left_shift(this._MatrixElementType,
                                                                            this.NativePtr,
                                                                            this.TemplateRows,
                                                                            this.TemplateColumns,
                                                                            ofstream);
                        break;
                    case Dlib.Native.ElementType.OpStdVectToMat:
                        ret = Native.matrix_op_op_std_vect_to_mat_operator_left_shift(this._MatrixElementType,
                                                                                      this.NativePtr,
                                                                                      this.TemplateRows,
                                                                                      this.TemplateColumns,
                                                                                      ofstream);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (ret)
                {
                    case Dlib.Native.ErrorType.OK:
                        stdstr = Dlib.Native.ostringstream_str(ofstream);
                        str = StringHelper.FromStdString(stdstr);
                        break;
                    case Dlib.Native.ErrorType.InputElementTypeNotSupport:
                        throw new ArgumentException($"Input {this._ElementType} is not supported.");
                    default:
                        throw new ArgumentException();
                }
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

        #endregion

        #endregion

        internal sealed class Native
        {

            #region delete

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_op_op_array2d_to_mat_delete(Dlib.Native.Array2DType type, IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_op_op_heatmap_delete(Dlib.Native.Array2DType type, IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_op_op_jet_delete(Dlib.Native.Array2DType type, IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_op_op_std_vect_to_mat_delete(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_op_op_trans_delete(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_op_op_join_rows_delete(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

            #endregion

            #region nc

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_array2d_to_mat_nc(Dlib.Native.Array2DType type, IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_heatmap_nc(Dlib.Native.Array2DType type, IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_heatmap_nc_matrix(Dlib.Native.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_jet_nc(Dlib.Native.Array2DType type, IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_jet_nc_matrix(Dlib.Native.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_std_vect_to_mat_nc(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_trans_nc(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_join_rows_nc(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

            #endregion

            #region nr

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_array2d_to_mat_nr(Dlib.Native.Array2DType type, IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_heatmap_nr(Dlib.Native.Array2DType type, IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_heatmap_nr_matrix(Dlib.Native.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_jet_nr(Dlib.Native.Array2DType type, IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_jet_nr_matrix(Dlib.Native.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_std_vect_to_mat_nr(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_trans_nr(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_join_rows_nr(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

            #endregion

            #region operator

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_jet_operator(Dlib.Native.Array2DType type, IntPtr matrix, int r, int c, IntPtr rgbPixel);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_heatmap_operator(Dlib.Native.Array2DType type, IntPtr matrix, int r, int c, IntPtr rgbPixel);

            #endregion

            #region operator_left_shift

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_std_vect_to_mat_operator_left_shift(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, IntPtr stream);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType matrix_op_op_trans_operator_left_shift(Dlib.Native.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, IntPtr stream);

            #endregion

        }

    }

}
