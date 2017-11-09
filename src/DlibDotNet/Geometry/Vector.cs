using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Vector<T> : VectorBase<T>
        where T : struct
    {

        #region Fields

        private readonly VectorElementTypes _VectorElementTypes;

        private readonly Dlib.Native.VectorElementType _ElementType;

        private static readonly Dictionary<Type, VectorElementTypes> SupportTypes = new Dictionary<Type, VectorElementTypes>();

        private readonly VectorImp<T> _Imp;

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

        public Vector(T x, T y, T z)
            : this()
        {
            this._Imp.X = x;
            this._Imp.Y = y;
            this._Imp.Z = z;
        }

        public Vector()
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._VectorElementTypes = type;
            this._ElementType = type.ToNativeVectorElementType();

            this._Imp = this.CreateVectorImp(this._ElementType);

            this.NativePtr = Dlib.Native.vector_new(this._ElementType);
        }

        internal Vector(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

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

        public override T X
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

        public override T Y
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

        public override T Z
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

        public static Vector<T> operator +(Vector<T> left, Vector<T> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));
            if (right == null)
                throw new ArgumentNullException(nameof(right));

            left.ThrowIfDisposed();
            right.ThrowIfDisposed();

            return left._Imp.OperatorAdd(left, right);
        }

        public static Vector<T> operator /(Vector<T> vector, T div)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector));

            vector.ThrowIfDisposed();

            return vector._Imp.OperatorDiv(vector, div);
        }

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Dlib.Native.vector_delete(this._ElementType, this.NativePtr);
        }

        #endregion

        #region Helpers

        private VectorImp<T> CreateVectorImp(Dlib.Native.VectorElementType types)
        {
            switch (types)
            {
                case Dlib.Native.VectorElementType.UInt8:
                    return new VectorUInt8Imp(this, types) as VectorImp<T>;
                case Dlib.Native.VectorElementType.UInt16:
                    return new VectorUInt16Imp(this, types) as VectorImp<T>;
                case Dlib.Native.VectorElementType.UInt32:
                    return new VectorUInt32Imp(this, types) as VectorImp<T>;
                case Dlib.Native.VectorElementType.Int8:
                    return new VectorInt8Imp(this, types) as VectorImp<T>;
                case Dlib.Native.VectorElementType.Int16:
                    return new VectorInt16Imp(this, types) as VectorImp<T>;
                case Dlib.Native.VectorElementType.Int32:
                    return new VectorInt32Imp(this, types) as VectorImp<T>;
                case Dlib.Native.VectorElementType.Float:
                    return new VectorFloatImp(this, types) as VectorImp<T>;
                case Dlib.Native.VectorElementType.Double:
                    return new VectorDoubleImp(this, types) as VectorImp<T>;
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }

        #endregion

        #endregion

        internal sealed class Native
        {
        }

        internal abstract class VectorImp<T>
            where T : struct
        {

            #region Fields 

            protected readonly Dlib.Native.VectorElementType _Type;

            protected readonly DlibObject _Parent;

            #endregion

            #region Constructors 

            internal VectorImp(DlibObject parent, Dlib.Native.VectorElementType type)
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

            internal VectorUInt8Imp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override byte X
            {
                get
                {
                    byte x, y, z;
                    Dlib.Native.vector_get_xyz_uint8_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    byte x, y, z;
                    Dlib.Native.vector_get_xyz_uint8_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint8_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override byte Y
            {
                get
                {
                    byte x, y, z;
                    Dlib.Native.vector_get_xyz_uint8_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    byte x, y, z;
                    Dlib.Native.vector_get_xyz_uint8_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint8_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override byte Z
            {
                get
                {
                    byte x, y, z;
                    Dlib.Native.vector_get_xyz_uint8_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    byte x, y, z;
                    Dlib.Native.vector_get_xyz_uint8_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint8_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<byte> OperatorAdd(Vector<byte> left, Vector<byte> right)
            {
                Dlib.Native.vector_operator_add_uint8_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<byte>(ret);
            }

            public override Vector<byte> OperatorDiv(Vector<byte> vector, byte div)
            {
                Dlib.Native.vector_operator_div_uint8_t(vector.NativePtr, div, out var ret);
                return new Vector<byte>(ret);
            }

            #endregion

        }

        internal sealed class VectorUInt16Imp : VectorImp<ushort>
        {

            #region Constructors

            internal VectorUInt16Imp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override ushort X
            {
                get
                {
                    ushort x, y, z;
                    Dlib.Native.vector_get_xyz_uint16_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    ushort x, y, z;
                    Dlib.Native.vector_get_xyz_uint16_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint16_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override ushort Y
            {
                get
                {
                    ushort x, y, z;
                    Dlib.Native.vector_get_xyz_uint16_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    ushort x, y, z;
                    Dlib.Native.vector_get_xyz_uint16_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint16_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override ushort Z
            {
                get
                {
                    ushort x, y, z;
                    Dlib.Native.vector_get_xyz_uint16_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    ushort x, y, z;
                    Dlib.Native.vector_get_xyz_uint16_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint16_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<ushort> OperatorAdd(Vector<ushort> left, Vector<ushort> right)
            {
                Dlib.Native.vector_operator_add_uint16_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<ushort>(ret);
            }

            public override Vector<ushort> OperatorDiv(Vector<ushort> vector, ushort div)
            {
                Dlib.Native.vector_operator_div_uint16_t(vector.NativePtr, div, out var ret);
                return new Vector<ushort>(ret);
            }

            #endregion

        }

        internal sealed class VectorUInt32Imp : VectorImp<uint>
        {

            #region Constructors

            internal VectorUInt32Imp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override uint X
            {
                get
                {
                    uint x, y, z;
                    Dlib.Native.vector_get_xyz_uint32_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    uint x, y, z;
                    Dlib.Native.vector_get_xyz_uint32_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint32_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override uint Y
            {
                get
                {
                    uint x, y, z;
                    Dlib.Native.vector_get_xyz_uint32_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    uint x, y, z;
                    Dlib.Native.vector_get_xyz_uint32_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint32_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override uint Z
            {
                get
                {
                    uint x, y, z;
                    Dlib.Native.vector_get_xyz_uint32_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    uint x, y, z;
                    Dlib.Native.vector_get_xyz_uint32_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_uint32_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<uint> OperatorAdd(Vector<uint> left, Vector<uint> right)
            {
                Dlib.Native.vector_operator_add_uint32_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<uint>(ret);
            }

            public override Vector<uint> OperatorDiv(Vector<uint> vector, uint div)
            {
                Dlib.Native.vector_operator_div_uint32_t(vector.NativePtr, div, out var ret);
                return new Vector<uint>(ret);
            }

            #endregion

        }

        internal sealed class VectorInt8Imp : VectorImp<sbyte>
        {

            #region Constructors

            internal VectorInt8Imp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override sbyte X
            {
                get
                {
                    sbyte x, y, z;
                    Dlib.Native.vector_get_xyz_int8_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    sbyte x, y, z;
                    Dlib.Native.vector_get_xyz_int8_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int8_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override sbyte Y
            {
                get
                {
                    sbyte x, y, z;
                    Dlib.Native.vector_get_xyz_int8_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    sbyte x, y, z;
                    Dlib.Native.vector_get_xyz_int8_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int8_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override sbyte Z
            {
                get
                {
                    sbyte x, y, z;
                    Dlib.Native.vector_get_xyz_int8_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    sbyte x, y, z;
                    Dlib.Native.vector_get_xyz_int8_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int8_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<sbyte> OperatorAdd(Vector<sbyte> left, Vector<sbyte> right)
            {
                Dlib.Native.vector_operator_add_int8_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<sbyte>(ret);
            }

            public override Vector<sbyte> OperatorDiv(Vector<sbyte> vector, sbyte div)
            {
                Dlib.Native.vector_operator_div_int8_t(vector.NativePtr, div, out var ret);
                return new Vector<sbyte>(ret);
            }

            #endregion

        }

        internal sealed class VectorInt16Imp : VectorImp<short>
        {

            #region Constructors

            internal VectorInt16Imp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override short X
            {
                get
                {
                    short x, y, z;
                    Dlib.Native.vector_get_xyz_int16_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    short x, y, z;
                    Dlib.Native.vector_get_xyz_int16_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int16_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override short Y
            {
                get
                {
                    short x, y, z;
                    Dlib.Native.vector_get_xyz_int16_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    short x, y, z;
                    Dlib.Native.vector_get_xyz_int16_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int16_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override short Z
            {
                get
                {
                    short x, y, z;
                    Dlib.Native.vector_get_xyz_int16_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    short x, y, z;
                    Dlib.Native.vector_get_xyz_int16_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int16_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<short> OperatorAdd(Vector<short> left, Vector<short> right)
            {
                Dlib.Native.vector_operator_add_int16_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<short>(ret);
            }

            public override Vector<short> OperatorDiv(Vector<short> vector, short div)
            {
                Dlib.Native.vector_operator_div_int16_t(vector.NativePtr, div, out var ret);
                return new Vector<short>(ret);
            }

            #endregion

        }

        internal sealed class VectorInt32Imp : VectorImp<int>
        {

            #region Constructors

            internal VectorInt32Imp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override int X
            {
                get
                {
                    int x, y, z;
                    Dlib.Native.vector_get_xyz_int32_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    int x, y, z;
                    Dlib.Native.vector_get_xyz_int32_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int32_t(this._Parent.NativePtr, value, y, z);
                }
            }

            public override int Y
            {
                get
                {
                    int x, y, z;
                    Dlib.Native.vector_get_xyz_int32_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    int x, y, z;
                    Dlib.Native.vector_get_xyz_int32_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int32_t(this._Parent.NativePtr, x, value, z);
                }
            }

            public override int Z
            {
                get
                {
                    int x, y, z;
                    Dlib.Native.vector_get_xyz_int32_t(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    int x, y, z;
                    Dlib.Native.vector_get_xyz_int32_t(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_int32_t(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<int> OperatorAdd(Vector<int> left, Vector<int> right)
            {
                Dlib.Native.vector_operator_add_int32_t(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<int>(ret);
            }

            public override Vector<int> OperatorDiv(Vector<int> vector, int div)
            {
                Dlib.Native.vector_operator_div_int32_t(vector.NativePtr, div, out var ret);
                return new Vector<int>(ret);
            }

            #endregion

        }

        internal sealed class VectorFloatImp : VectorImp<float>
        {

            #region Constructors

            internal VectorFloatImp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override float X
            {
                get
                {
                    float x, y, z;
                    Dlib.Native.vector_get_xyz_float(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    float x, y, z;
                    Dlib.Native.vector_get_xyz_float(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_float(this._Parent.NativePtr, value, y, z);
                }
            }

            public override float Y
            {
                get
                {
                    float x, y, z;
                    Dlib.Native.vector_get_xyz_float(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    float x, y, z;
                    Dlib.Native.vector_get_xyz_float(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_float(this._Parent.NativePtr, x, value, z);
                }
            }

            public override float Z
            {
                get
                {
                    float x, y, z;
                    Dlib.Native.vector_get_xyz_float(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    float x, y, z;
                    Dlib.Native.vector_get_xyz_float(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_float(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<float> OperatorAdd(Vector<float> left, Vector<float> right)
            {
                Dlib.Native.vector_operator_add_float(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<float>(ret);
            }

            public override Vector<float> OperatorDiv(Vector<float> vector, float div)
            {
                Dlib.Native.vector_operator_div_float(vector.NativePtr, div, out var ret);
                return new Vector<float>(ret);
            }

            #endregion

        }

        internal sealed class VectorDoubleImp : VectorImp<double>
        {

            #region Constructors

            internal VectorDoubleImp(DlibObject parent, Dlib.Native.VectorElementType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override double X
            {
                get
                {
                    double x, y, z;
                    Dlib.Native.vector_get_xyz_double(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    double x, y, z;
                    Dlib.Native.vector_get_xyz_double(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_double(this._Parent.NativePtr, value, y, z);
                }
            }

            public override double Y
            {
                get
                {
                    double x, y, z;
                    Dlib.Native.vector_get_xyz_double(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    double x, y, z;
                    Dlib.Native.vector_get_xyz_double(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_double(this._Parent.NativePtr, x, value, z);
                }
            }

            public override double Z
            {
                get
                {
                    double x, y, z;
                    Dlib.Native.vector_get_xyz_double(this._Parent.NativePtr, out x, out y, out z);
                    return x;
                }
                set
                {
                    double x, y, z;
                    Dlib.Native.vector_get_xyz_double(this._Parent.NativePtr, out x, out y, out z);
                    Dlib.Native.vector_set_xyz_double(this._Parent.NativePtr, x, y, value);
                }
            }

            #endregion

            #region Method

            public override Vector<double> OperatorAdd(Vector<double> left, Vector<double> right)
            {
                Dlib.Native.vector_operator_add_double(left.NativePtr, right.NativePtr, out var ret);
                return new Vector<double>(ret);
            }

            public override Vector<double> OperatorDiv(Vector<double> vector, double div)
            {
                Dlib.Native.vector_operator_div_double(vector.NativePtr, div, out var ret);
                return new Vector<double>(ret);
            }

            #endregion

        }

    }

}
