#if !LITE
using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MatrixOp : TwoDimensionObjectBase
    {

        #region Fields

        private readonly ImageTypes _ImageType;

        private readonly NativeMethods.Array2DType _Array2DType;

        private readonly NativeMethods.ElementType _ElementType;

        private readonly NativeMethods.MatrixElementType _MatrixElementType;

        private readonly IntPtr _Ref;

        #endregion

        #region Constructors

        internal MatrixOp(NativeMethods.ElementType elementType, ImageTypes type, IntPtr ptr)
        {
            this._ElementType = elementType;
            this._Array2DType = type.ToNativeArray2DType();
            this.NativePtr = ptr;
            this._ImageType = type;

            this.TemplateRows = -1;
            this.TemplateColumns = -1;
        }

        internal MatrixOp(NativeMethods.ElementType elementType, ImageTypes type, IntPtr ptr, IntPtr @ref)
        {
            this._ElementType = elementType;
            this._Array2DType = type.ToNativeArray2DType();
            this.NativePtr = ptr;
            this._ImageType = type;

            this.TemplateRows = -1;
            this.TemplateColumns = -1;

            this._Ref = @ref;
        }

        internal MatrixOp(NativeMethods.ElementType elementType, MatrixElementTypes type, IntPtr ptr, int templateRows = 0, int templateColumns = 0)
        {
            this._ElementType = elementType;
            this._MatrixElementType = type.ToNativeMatrixElementType();
            this.NativePtr = ptr;

            this.TemplateRows = templateRows;
            this.TemplateColumns = templateColumns;
        }

        #endregion

        #region Properties

        internal NativeMethods.Array2DType Array2DType => this._Array2DType;

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();

                var ret = 0;
                switch (this._ElementType)
                {
                    case NativeMethods.ElementType.OpHeatmap:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            NativeMethods.matrix_op_op_heatmap_nc(this._Array2DType, this.NativePtr, out ret);
                        else
                            NativeMethods.matrix_op_op_heatmap_nc_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpJet:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            NativeMethods.matrix_op_op_jet_nc(this._Array2DType, this.NativePtr, out ret);
                        else
                            NativeMethods.matrix_op_op_jet_nc_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpArray2DToMat:
                        NativeMethods.matrix_op_op_array2d_to_mat_nc(this._Array2DType, this.NativePtr, out ret);
                        break;
                    case NativeMethods.ElementType.OpTrans:
                        NativeMethods.matrix_op_op_trans_nc(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpStdVectToMat:
                        NativeMethods.matrix_op_op_std_vect_to_mat_nc(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpStdVectToMatValue:
                        NativeMethods.matrix_op_op_std_vect_to_mat_value_nc(this._Array2DType, this.NativePtr, out ret);
                        break;
                    case NativeMethods.ElementType.OpJoinRows:
                        NativeMethods.matrix_op_op_join_rows_nc(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                }

                return ret;
            }
        }

        internal NativeMethods.ElementType ElementType => this._ElementType;

        internal NativeMethods.MatrixElementType MatrixElementType => this._MatrixElementType;

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();

                var ret = 0;
                switch (this._ElementType)
                {
                    case NativeMethods.ElementType.OpHeatmap:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            NativeMethods.matrix_op_op_heatmap_nr(this._Array2DType, this.NativePtr, out ret);
                        else
                            NativeMethods.matrix_op_op_heatmap_nr_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpJet:
                        if (this.TemplateRows == -1 && this.TemplateColumns == -1)
                            NativeMethods.matrix_op_op_jet_nr(this._Array2DType, this.NativePtr, out ret);
                        else
                            NativeMethods.matrix_op_op_jet_nr_matrix(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpArray2DToMat:
                        NativeMethods.matrix_op_op_array2d_to_mat_nr(this._Array2DType, this.NativePtr, out ret);
                        break;
                    case NativeMethods.ElementType.OpTrans:
                        NativeMethods.matrix_op_op_trans_nr(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpStdVectToMat:
                        NativeMethods.matrix_op_op_std_vect_to_mat_nr(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
                        break;
                    case NativeMethods.ElementType.OpStdVectToMatValue:
                        NativeMethods.matrix_op_op_std_vect_to_mat_value_nr(this._Array2DType, this.NativePtr, out ret);
                        break;
                    case NativeMethods.ElementType.OpJoinRows:
                        NativeMethods.matrix_op_op_join_rows_nr(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns, out ret);
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

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            switch (this._ElementType)
            {
                case NativeMethods.ElementType.OpHeatmap:
                    NativeMethods.matrix_op_op_heatmap_delete(this._Array2DType, this.NativePtr);
                    break;
                case NativeMethods.ElementType.OpJet:
                    NativeMethods.matrix_op_op_jet_delete(this._Array2DType, this.NativePtr);
                    break;
                case NativeMethods.ElementType.OpArray2DToMat:
                    NativeMethods.matrix_op_op_array2d_to_mat_delete(this._Array2DType, this.NativePtr);
                    break;
                case NativeMethods.ElementType.OpTrans:
                    NativeMethods.matrix_op_op_trans_delete(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
                    break;
                case NativeMethods.ElementType.OpStdVectToMat:
                    NativeMethods.matrix_op_op_std_vect_to_mat_delete(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
                    break;
                case NativeMethods.ElementType.OpStdVectToMatValue:
                    DlibObject vector = null;
                    switch (this._Array2DType)
                    {
                        case NativeMethods.Array2DType.UInt8:
                            vector = new StdVector<byte>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.UInt16:
                            vector = new StdVector<ushort>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.UInt32:
                            vector = new StdVector<uint>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.Int8:
                            vector = new StdVector<sbyte>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.Int16:
                            vector = new StdVector<short>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.Int32:
                            vector = new StdVector<int>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.Float:
                            vector = new StdVector<float>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.Double:
                            vector = new StdVector<double>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.RgbPixel:
                            vector = new StdVector<RgbPixel>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.RgbAlphaPixel:
                            vector = new StdVector<RgbAlphaPixel>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.HsiPixel:
                            vector = new StdVector<HsiPixel>(this._Ref);
                            break;
                        case NativeMethods.Array2DType.LabPixel:
                            vector = new StdVector<LabPixel>(this._Ref);
                            break;
                    }
                    vector?.Dispose();
                    NativeMethods.matrix_op_op_std_vect_to_mat_value_delete(this._Array2DType, this.NativePtr);
                    break;
                case NativeMethods.ElementType.OpJoinRows:
                    NativeMethods.matrix_op_op_join_rows_delete(this._MatrixElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
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
                ofstream = NativeMethods.ostringstream_new();

                NativeMethods.ErrorType ret;
                switch (this._ElementType)
                {
                    case NativeMethods.ElementType.OpTrans:
                        ret = NativeMethods.matrix_op_op_trans_operator_left_shift(this._MatrixElementType,
                                                                                   this.NativePtr,
                                                                                   this.TemplateRows,
                                                                                   this.TemplateColumns,
                                                                                   ofstream);
                        break;
                    case NativeMethods.ElementType.OpStdVectToMat:
                        ret = NativeMethods.matrix_op_op_std_vect_to_mat_operator_left_shift(this._MatrixElementType,
                                                                                             this.NativePtr,
                                                                                             this.TemplateRows,
                                                                                             this.TemplateColumns,
                                                                                             ofstream);
                        break;
                    case NativeMethods.ElementType.OpStdVectToMatValue:
                        ret = NativeMethods.matrix_op_op_std_vect_to_mat_value_operator_left_shift(this._ImageType.ToNativeArray2DType(),
                                                                                                   this.NativePtr,
                                                                                                   ofstream);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (ret)
                {
                    case NativeMethods.ErrorType.OK:
                        stdstr = NativeMethods.ostringstream_str(ofstream);
                        str = StringHelper.FromStdString(stdstr);
                        break;
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
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
                    NativeMethods.string_delete(stdstr);
                if (ofstream != IntPtr.Zero)
                    NativeMethods.ostringstream_delete(ofstream);
            }

            return str;
        }

        #endregion

        #endregion

    }

}

#endif
