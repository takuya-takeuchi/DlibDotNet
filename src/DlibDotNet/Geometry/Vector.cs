#if !LITE
using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Vector<TType> : VectorBase<TType>
        where TType : struct
    {

        #region Fields

        private readonly VectorElementTypes _VectorElementTypes;

        private readonly NativeMethods.VectorElementType _ElementType;

        private static readonly Dictionary<Type, VectorElementTypes> SupportTypes = new Dictionary<Type, VectorElementTypes>();

        private readonly VectorImp<TType> _Imp;

        #endregion

        #region Constructors

        static Vector()
        {
            var types = new[]
            {
                new {Type = typeof(byte),   ElementType = VectorElementTypes.UInt8 },
                new {Type = typeof(ushort), ElementType = VectorElementTypes.UInt16 },
                new {Type = typeof(uint),   ElementType = VectorElementTypes.UInt32 },
                new {Type = typeof(sbyte),  ElementType = VectorElementTypes.Int8 },
                new {Type = typeof(short),  ElementType = VectorElementTypes.Int16 },
                new {Type = typeof(int),    ElementType = VectorElementTypes.Int32 },
                new {Type = typeof(float),  ElementType = VectorElementTypes.Float },
                new {Type = typeof(double), ElementType = VectorElementTypes.Double }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public Vector(TType x, TType y, TType z)
            : this()
        {
            this._Imp.X = x;
            this._Imp.Y = y;
            this._Imp.Z = z;
        }

        public Vector()
        {
            if (!SupportTypes.TryGetValue(typeof(TType), out var type))
                throw new NotSupportedException($"{typeof(TType).Name} does not support");

            this._VectorElementTypes = type;
            this._ElementType = type.ToNativeVectorElementType();

            this._Imp = this.CreateVectorImp(this._ElementType);

            this.NativePtr = NativeMethods.vector_new(this._ElementType);
        }

        internal Vector(IntPtr ptr, bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            if (!SupportTypes.TryGetValue(typeof(TType), out var type))
                throw new NotSupportedException($"{typeof(TType).Name} does not support");

            this.NativePtr = ptr;

            this._VectorElementTypes = type;
            this._ElementType = type.ToNativeVectorElementType();

            this._Imp = this.CreateVectorImp(this._ElementType);

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public override double Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double LengthSquared
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override TType X
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.X;
            }
            //set
            //{
            //    this.ThrowIfDisposed();
            //    this._Imp.X = value;
            //}
        }

        public override TType Y
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.Y;
            }
            //set
            //{
            //    this.ThrowIfDisposed();
            //    this._Imp.Y = value;
            //}
        }

        public override TType Z
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.Z;
            }
            //set
            //{
            //    this.ThrowIfDisposed();
            //    this._Imp.Z = value;
            //}
        }

        #endregion

        #region Methods

        #region Overrides

        public static Vector<TType> operator +(Vector<TType> left, Vector<TType> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));
            if (right == null)
                throw new ArgumentNullException(nameof(right));

            left.ThrowIfDisposed();
            right.ThrowIfDisposed();

            return left._Imp.OperatorAdd(left, right);
        }

        public static Vector<TType> operator /(Vector<TType> vector, TType div)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector));

            vector.ThrowIfDisposed();

            return vector._Imp.OperatorDiv(vector, div);
        }

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.vector_delete(this._ElementType, this.NativePtr);
        }

        public override string ToString()
        {
            var ofstream = IntPtr.Zero;
            var stdstr = IntPtr.Zero;
            string str = null;

            try
            {
                ofstream = NativeMethods.ostringstream_new();
                var ret = NativeMethods.vector_operator_left_shift(this._ElementType, this.NativePtr, ofstream);
                switch (ret)
                {
                    case NativeMethods.ErrorType.OK:
                        stdstr = NativeMethods.ostringstream_str(ofstream);
                        str = StringHelper.FromStdString(stdstr);
                        break;
                    case NativeMethods.ErrorType.VectorTypeNotSupport:
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

        #region Helpers

        private VectorImp<TType> CreateVectorImp(NativeMethods.VectorElementType types)
        {
            switch (types)
            {
                case NativeMethods.VectorElementType.UInt8:
                    return new VectorUInt8Imp(this, types) as VectorImp<TType>;
                case NativeMethods.VectorElementType.UInt16:
                    return new VectorUInt16Imp(this, types) as VectorImp<TType>;
                case NativeMethods.VectorElementType.UInt32:
                    return new VectorUInt32Imp(this, types) as VectorImp<TType>;
                case NativeMethods.VectorElementType.Int8:
                    return new VectorInt8Imp(this, types) as VectorImp<TType>;
                case NativeMethods.VectorElementType.Int16:
                    return new VectorInt16Imp(this, types) as VectorImp<TType>;
                case NativeMethods.VectorElementType.Int32:
                    return new VectorInt32Imp(this, types) as VectorImp<TType>;
                case NativeMethods.VectorElementType.Float:
                    return new VectorFloatImp(this, types) as VectorImp<TType>;
                case NativeMethods.VectorElementType.Double:
                    return new VectorDoubleImp(this, types) as VectorImp<TType>;
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }

        #endregion

        #endregion

        internal abstract class VectorImp<T>
            where T : struct
        {

            #region Fields 

            protected readonly NativeMethods.VectorElementType _Type;

            protected readonly DlibObject _Parent;

            #endregion

            #region Constructors 

            internal VectorImp(DlibObject parent, NativeMethods.VectorElementType type)
            {
                this._Parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this._Type = type;
            }

            #endregion

            #region Properties

            public abstract T X
            {
                get;
                set;
            }

            public abstract T Y
            {
                get;
                set;
            }

            public abstract T Z
            {
                get;
                set;
            }

            #endregion

            #region Method

            public abstract Vector<T> OperatorAdd(Vector<T> left, Vector<T> right);

            public abstract Vector<T> OperatorDiv(Vector<T> vector, T div);

            #endregion

        }

        internal sealed class VectorUInt8Imp : VectorImp<byte>
        {

            #region Constructors

            internal VectorUInt8Imp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override byte X
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint8_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override byte Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint8_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override byte Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint8_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<byte> OperatorAdd(Vector<byte> left, Vector<byte> right)
            {
                NativeMethods.vector_operator_add_uint8_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<byte>(ret);
            }

            public override Vector<byte> OperatorDiv(Vector<byte> vector, byte div)
            {
                NativeMethods.vector_operator_div_uint8_t(vector.NativePtr, div, out var ret);
                return new Vector<byte>(ret);
            }

            #endregion

        }

        internal sealed class VectorUInt16Imp : VectorImp<ushort>
        {

            #region Constructors

            internal VectorUInt16Imp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override ushort X
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint16_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override ushort Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint16_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override ushort Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint16_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<ushort> OperatorAdd(Vector<ushort> left, Vector<ushort> right)
            {
                NativeMethods.vector_operator_add_uint16_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<ushort>(ret);
            }

            public override Vector<ushort> OperatorDiv(Vector<ushort> vector, ushort div)
            {
                NativeMethods.vector_operator_div_uint16_t(vector.NativePtr, div, out var ret);
                return new Vector<ushort>(ret);
            }

            #endregion

        }

        internal sealed class VectorUInt32Imp : VectorImp<uint>
        {

            #region Constructors

            internal VectorUInt32Imp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override uint X
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint32_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override uint Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint32_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override uint Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_uint32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_uint32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_uint32_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<uint> OperatorAdd(Vector<uint> left, Vector<uint> right)
            {
                NativeMethods.vector_operator_add_uint32_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<uint>(ret);
            }

            public override Vector<uint> OperatorDiv(Vector<uint> vector, uint div)
            {
                NativeMethods.vector_operator_div_uint32_t(vector.NativePtr, div, out var ret);
                return new Vector<uint>(ret);
            }

            #endregion

        }

        internal sealed class VectorInt8Imp : VectorImp<sbyte>
        {

            #region Constructors

            internal VectorInt8Imp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override sbyte X
            {
                get
                {
                    NativeMethods.vector_get_xyz_int8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int8_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override sbyte Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_int8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int8_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override sbyte Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_int8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int8_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int8_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<sbyte> OperatorAdd(Vector<sbyte> left, Vector<sbyte> right)
            {
                NativeMethods.vector_operator_add_int8_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<sbyte>(ret);
            }

            public override Vector<sbyte> OperatorDiv(Vector<sbyte> vector, sbyte div)
            {
                NativeMethods.vector_operator_div_int8_t(vector.NativePtr, div, out var ret);
                return new Vector<sbyte>(ret);
            }

            #endregion

        }

        internal sealed class VectorInt16Imp : VectorImp<short>
        {

            #region Constructors

            internal VectorInt16Imp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override short X
            {
                get
                {
                    NativeMethods.vector_get_xyz_int16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int16_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override short Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_int16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int16_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override short Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_int16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int16_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int16_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<short> OperatorAdd(Vector<short> left, Vector<short> right)
            {
                NativeMethods.vector_operator_add_int16_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<short>(ret);
            }

            public override Vector<short> OperatorDiv(Vector<short> vector, short div)
            {
                NativeMethods.vector_operator_div_int16_t(vector.NativePtr, div, out var ret);
                return new Vector<short>(ret);
            }

            #endregion

        }

        internal sealed class VectorInt32Imp : VectorImp<int>
        {

            #region Constructors

            internal VectorInt32Imp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override int X
            {
                get
                {
                    NativeMethods.vector_get_xyz_int32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int32_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override int Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_int32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int32_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override int Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_int32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_int32_t(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_int32_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<int> OperatorAdd(Vector<int> left, Vector<int> right)
            {
                NativeMethods.vector_operator_add_int32_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<int>(ret);
            }

            public override Vector<int> OperatorDiv(Vector<int> vector, int div)
            {
                NativeMethods.vector_operator_div_int32_t(vector.NativePtr, div, out var ret);
                return new Vector<int>(ret);
            }

            #endregion

        }

        internal sealed class VectorFloatImp : VectorImp<float>
        {

            #region Constructors

            internal VectorFloatImp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override float X
            {
                get
                {
                    NativeMethods.vector_get_xyz_float(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_float(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_float(this._Parent.NativePtr, value, y, z);
                }
            }

            public override float Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_float(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_float(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_float(this._Parent.NativePtr, x, value, z);
                }
            }

            public override float Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_float(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_float(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_float(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<float> OperatorAdd(Vector<float> left, Vector<float> right)
            {
                NativeMethods.vector_operator_add_float(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<float>(ret);
            }

            public override Vector<float> OperatorDiv(Vector<float> vector, float div)
            {
                NativeMethods.vector_operator_div_float(vector.NativePtr, div, out var ret);
                return new Vector<float>(ret);
            }

            #endregion

        }

        internal sealed class VectorDoubleImp : VectorImp<double>
        {

            #region Constructors

            internal VectorDoubleImp(DlibObject parent, NativeMethods.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override double X
            {
                get
                {
                    NativeMethods.vector_get_xyz_double(this._Parent.NativePtr, out var x, out var y, out var z);
                    return x;
                }
                set
                {
                    NativeMethods.vector_get_xyz_double(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_double(this._Parent.NativePtr, value, y, z);
                }
            }

            public override double Y
            {
                get
                {
                    NativeMethods.vector_get_xyz_double(this._Parent.NativePtr, out var x, out var y, out var z);
                    return y;
                }
                set
                {
                    NativeMethods.vector_get_xyz_double(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_double(this._Parent.NativePtr, x, value, z);
                }
            }

            public override double Z
            {
                get
                {
                    NativeMethods.vector_get_xyz_double(this._Parent.NativePtr, out var x, out var y, out var z);
                    return z;
                }
                set
                {
                    NativeMethods.vector_get_xyz_double(this._Parent.NativePtr, out var x, out var y, out var z);
                    NativeMethods.vector_set_xyz_double(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<double> OperatorAdd(Vector<double> left, Vector<double> right)
            {
                NativeMethods.vector_operator_add_double(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<double>(ret);
            }

            public override Vector<double> OperatorDiv(Vector<double> vector, double div)
            {
                NativeMethods.vector_operator_div_double(vector.NativePtr, div, out var ret);
                return new Vector<double>(ret);
            }

            #endregion

        }

    }

}

#endif
