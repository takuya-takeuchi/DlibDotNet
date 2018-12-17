﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class RectDetection : DlibObject
    {

        #region Constructors

        internal RectDetection(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public double DetectionConfidence
        {
            get
            {
                this.ThrowIfDisposed();
                var detectionConfidence = Native.rect_detection_get_detection_confidence(this.NativePtr);
                return detectionConfidence;
            }
            set
            {
                this.ThrowIfDisposed();
                Native.rect_detection_set_detection_confidence(this.NativePtr, value);
            }
        }

        public Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                var rect = Native.rect_detection_get_rect(this.NativePtr);
                return new Rectangle(rect);
            }
            set
            {
                this.ThrowIfDisposed();
                using(var native = value.ToNative())
                    Native.rect_detection_set_rect(this.NativePtr, native.NativePtr);
            }
        }

        public ulong WeightIndex
        {
            get
            {
                this.ThrowIfDisposed();
                var weightIndex = Native.rect_detection_get_weight_index(this.NativePtr);
                return weightIndex;
            }
            set
            {
                this.ThrowIfDisposed();
                Native.rect_detection_set_weight_index(this.NativePtr, value);
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

            Native.rect_detection_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double rect_detection_get_detection_confidence(IntPtr detection);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rect_detection_set_detection_confidence(IntPtr detection, double detection_confidence);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rect_detection_get_rect(IntPtr detection);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rect_detection_set_rect(IntPtr detection, IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ulong rect_detection_get_weight_index(IntPtr detection);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rect_detection_set_weight_index(IntPtr detection, ulong weight_index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rect_detection_delete(IntPtr detection);

        }

    }
}
