using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class SamplePair : DlibObject
    {

        #region Constructors

        public SamplePair() :
            this(0, 0, 1)
        {
        }

        public SamplePair(uint index1, uint index2) :
            this(index1, index2, 1)
        {
        }

        public SamplePair(uint index1, uint index2, double distance)
        {
            this.NativePtr = Native.sample_pair_new(index1, index2, distance);
        }

        internal SamplePair(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public uint Index1
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.sample_pair_get_index1(this.NativePtr);
            }
        }

        public uint Index2
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.sample_pair_get_index2(this.NativePtr);
            }
        }

        public double Distance
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.sample_pair_get_distance(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.sample_pair_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr sample_pair_new(uint idx1, uint idx2, double distance);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint sample_pair_get_index1(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint sample_pair_get_index2(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double sample_pair_get_distance(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void sample_pair_delete(IntPtr obj);

        }

    }

}
