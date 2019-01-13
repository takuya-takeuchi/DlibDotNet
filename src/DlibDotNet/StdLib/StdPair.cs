using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdPair<TFirst, TSecond> : DlibObject
    {

        #region Fields

        private static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        private readonly StdPairImp<TFirst, TSecond> _Imp;

        #endregion

        #region Constructors

        static StdPair()
        {
            var types = new[]
            {
                new { Type = typeof(Point),     ElementType = ElementTypes.Point }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public StdPair(TFirst first, TSecond second)
        {
            this._Imp = CreateImp();
            this.NativePtr = this._Imp.Create(first, second);

            this._Second = second;
            this._First = first;
        }

        public StdPair(IntPtr first, IntPtr second)
        {
            this._Imp = CreateImp();
            this.NativePtr = this._Imp.Create(first, second);

            this._First = this._Imp.CreateFirst(first);
            this._Second = this._Imp.CreateSecond(second);
        }

        internal StdPair(IntPtr ptr)
        {
            this._Imp = CreateImp();
            this.NativePtr = ptr;

            this._First = this._Imp.GetFirst(ptr);
            this._Second = this._Imp.GetSecond(ptr);
        }

        #endregion

        #region Properties

        private TFirst _First;

        public TFirst First
        {
            get
            {
                return this._First;
            }
            set
            {
                this.ThrowIfDisposed();
                this._First = value;
                this._Imp.SetFirst(this.NativePtr, value);
            }
        }

        private TSecond _Second;

        public TSecond Second
        {
            get
            {
                return this._Second;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Second = value;
                this._Imp.SetSecond(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        #region Helpers

        private static StdPairImp<TFirst, TSecond> CreateImp()
        {
            if (!SupportTypes.TryGetValue(typeof(TFirst), out var first))
                throw new ArgumentOutOfRangeException(nameof(first), first, null);

            if (!SupportTypes.TryGetValue(typeof(TSecond), out var second))
                throw new ArgumentOutOfRangeException(nameof(second), second, null);

            if (first == ElementTypes.Point && second == ElementTypes.Point)
                return new StdPairPointPointImp() as StdPairImp<TFirst, TSecond>;

            throw new ArgumentOutOfRangeException();
        }

        #endregion

        #endregion

        private enum ElementTypes
        {

            Point

        }

        #region StdPairImp

        private abstract class StdPairImp<T, U>
        {

            #region Methods

            public abstract IntPtr Create(T first, U second);

            public abstract IntPtr Create(IntPtr first, IntPtr second);

            public abstract T CreateFirst(IntPtr first);

            public abstract U CreateSecond(IntPtr second);

            public abstract T GetFirst(IntPtr ptr);

            public abstract U GetSecond(IntPtr ptr);

            public abstract void SetFirst(IntPtr ptr, T value);

            public abstract void SetSecond(IntPtr ptr, U value);

            #endregion

        }

        private sealed class StdPairPointPointImp : StdPairImp<Point, Point>
        {

            #region Methods

            public override IntPtr Create(Point first, Point second)
            {
                using (var f = first.ToNative())
                using (var n = second.ToNative())
                    return NativeMethods.stdpair_point_point_new(f.NativePtr, n.NativePtr);
            }

            public override IntPtr Create(IntPtr first, IntPtr second)
            {
                return NativeMethods.stdpair_point_point_new(first, second);
            }

            public override Point CreateFirst(IntPtr first)
            {
                return new Point(first);
            }

            public override Point CreateSecond(IntPtr second)
            {
                return new Point(second);
            }

            public override Point GetFirst(IntPtr ptr)
            {
                using (var native = new Point.NativePoint(NativeMethods.stdpair_point_point_get_first(ptr)))
                    return native.ToManaged();
            }

            public override Point GetSecond(IntPtr ptr)
            {
                using (var native = new Point.NativePoint(NativeMethods.stdpair_point_point_get_second(ptr)))
                    return native.ToManaged();
            }

            public override void SetFirst(IntPtr ptr, Point value)
            {
                using (var native = value.ToNative())
                    NativeMethods.stdpair_point_point_set_first(ptr, native.NativePtr);
            }

            public override void SetSecond(IntPtr ptr, Point value)
            {
                using (var native = value.ToNative())
                    NativeMethods.stdpair_point_point_set_second(ptr, native.NativePtr);
            }

            #endregion

        }

        #endregion

    }

}