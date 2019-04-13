using System;
using System.Runtime.InteropServices;
using DlibDotNet;

namespace DnnSemanticSegmentation
{

    // A single training sample. A mini-batch comprises many of these.
    public sealed class TrainingSample : IDisposable
    {

        #region Constructors

        public TrainingSample()
        {
            this.NativePtr = Marshal.AllocCoTaskMem(IntPtr.Size * 2);
            Marshal.WriteIntPtr(this.NativePtr, IntPtr.Size * 0, IntPtr.Zero);
            Marshal.WriteIntPtr(this.NativePtr, IntPtr.Size * 1, IntPtr.Zero);
        }

        public TrainingSample(IntPtr ptr)
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

        public Matrix<ushort> LabelImage // The ground-truth label of each pixel.
        {
            get => this.Read<Matrix<ushort>>(1);
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