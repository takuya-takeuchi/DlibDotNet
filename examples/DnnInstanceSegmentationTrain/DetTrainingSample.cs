using System;
using System.Runtime.InteropServices;
using DlibDotNet;

namespace DnnInstanceSegmentationTrain
{

    // A single training sample for detection. A mini-batch comprises many of these.
    internal sealed class DetTrainingSample : IDisposable
    {

        #region Constructors

        public DetTrainingSample()
        {
            this.NativePtr = Marshal.AllocCoTaskMem(IntPtr.Size * 2);
            Marshal.WriteIntPtr(this.NativePtr, IntPtr.Size * 0, IntPtr.Zero);
            Marshal.WriteIntPtr(this.NativePtr, IntPtr.Size * 1, IntPtr.Zero);
        }

        public DetTrainingSample(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public IntPtr NativePtr
        {
            get;
        }

        public Matrix<RgbPixel> InputImage
        {
            get => this.Read<Matrix<RgbPixel>>(0);
            set => this.Write(value, 0);
        }

        public StdVector<MModRect> MmodRects
        {
            get => this.Read<StdVector<MModRect>>(1);
            set => this.Write(value, 1);
        }

        #endregion

        #region Methods

        #region Helpers

        private T Read<T>(int offset)
        {
            var ret = Marshal.ReadIntPtr(this.NativePtr, IntPtr.Size * offset);
            if (ret == IntPtr.Zero)
                return default(T);
            var bridge = ContainerBridgeRepository.Get<T>();
            return bridge.Create(ret);
        }

        private void Write<T>(T item, int offset)
            where T : DlibObject
        {
            var ptr = item?.NativePtr ?? IntPtr.Zero;
            Marshal.WriteIntPtr(this.NativePtr, IntPtr.Size * offset, ptr);
        }

        #endregion

        #endregion

        #region IDisposable Members

        private bool _IsDisposed;

        /// <summary>
        /// Releases all resources used by this <see cref="EnumerableDisposer{T}"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by this <see cref="EnumerableDisposer{T}"/>.
        /// </summary>
        /// <param name="disposing">Indicate value whether <see cref="IDisposable.Dispose"/> method was called.</param>
        private void Dispose(bool disposing)
        {
            if (this._IsDisposed)
            {
                return;
            }

            this._IsDisposed = true;

            if (disposing)
            {
                Marshal.FreeCoTaskMem(this.NativePtr);
            }
        }

        #endregion

    }

}