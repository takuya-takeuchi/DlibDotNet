using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdPair<T, U> : DlibObject
    {

        #region Fields

        private static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        private readonly StdPairImp<T, U> _Imp;

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

        public StdPair(T first, U second)
        {
            this._Imp = Create<T, U>();
            this.NativePtr = this._Imp.Create(first, second);

            this._Second = second;
            this._First = first;
        }

        public StdPair(IntPtr first, IntPtr second)
        {
            this._Imp = Create<T, U>();
            this.NativePtr = this._Imp.Create(first, second);

            this._First = this._Imp.CreateFirst(first);
            this._Second = this._Imp.CreateSecond(second);
        }

        internal StdPair(IntPtr ptr)
        {
            this._Imp = Create<T, U>();
            this.NativePtr = ptr;

            this._First = this._Imp.GetFirst(ptr);
            this._Second = this._Imp.GetSecond(ptr);
        }

        #endregion

        #region Properties

        private T _First;

        public T First
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

        private U _Second;

        public U Second
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

        private static StdPairImp<T, U> Create<T, U>()
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var first))
                throw new ArgumentOutOfRangeException(nameof(first), first, null);

            if (!SupportTypes.TryGetValue(typeof(U), out var second))
                throw new ArgumentOutOfRangeException(nameof(second), second, null);

            if (first == ElementTypes.Point && second == ElementTypes.Point)
                return new StdPairPointPointImp() as StdPairImp<T, U>;

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
                if (first != null)
                    first.ThrowIfDisposed();
                if (second != null)
                    second.ThrowIfDisposed();

                var firstPtr = first == null ? IntPtr.Zero : first.NativePtr;
                var secondPtr = second == null ? IntPtr.Zero : second.NativePtr;
                return Dlib.Native.stdpair_point_point_new(firstPtr, secondPtr);
            }

            public override IntPtr Create(IntPtr first, IntPtr second)
            {
                return Dlib.Native.stdpair_point_point_new(first, second);
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
                return new Point(Dlib.Native.stdpair_point_point_get_first(ptr));
            }

            public override Point GetSecond(IntPtr ptr)
            {
                return new Point(Dlib.Native.stdpair_point_point_get_second(ptr));
            }

            public override void SetFirst(IntPtr ptr, Point value)
            {
                if (value != null)
                    value.ThrowIfDisposed();
                Dlib.Native.stdpair_point_point_set_first(ptr, value == null ? IntPtr.Zero : value.NativePtr);
            }

            public override void SetSecond(IntPtr ptr, Point value)
            {
                if (value != null)
                    value.ThrowIfDisposed();
                Dlib.Native.stdpair_point_point_set_second(ptr, value == null ? IntPtr.Zero : value.NativePtr);
            }

            #endregion

        }

        #endregion

    }

}