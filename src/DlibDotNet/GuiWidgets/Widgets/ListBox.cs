using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ListBox : ScrollableRegion
    {

        #region Constructors

        public ListBox(DrawableWindow window) :
            base(window)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.NativePtr = NativeMethods.list_box_new(window.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Properties

        public bool MultipleSelectEnabled
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();

                return NativeMethods.list_box_multiple_select_enabled(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
            set
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();

                if (value)
                    NativeMethods.list_box_enable_multiple_select(this.NativePtr);
                else
                    NativeMethods.list_box_disable_multiple_select(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        public ulong Size
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.list_box_size(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        #endregion

        #region Methods

        public Queue<uint>.Kernel1A GetSelected()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            var ret = NativeMethods.list_box_get_selected(this.NativePtr);
            return new Queue<uint>.Kernel1A(ret);
#else
                throw new NotSupportedException();
#endif
        }

        public void Load(IEnumerable<string> list)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            using (var disposer = new EnumerableDisposer<StdString>(list.Select(s => new StdString(s))))
            using (var vector = new StdVector<StdString>(disposer.Collection))
                NativeMethods.list_box_load_stdstring(this.NativePtr, vector.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void Select(uint index)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (!(index < this.Size))
                throw new ArgumentOutOfRangeException();

            this.ThrowIfDisposed();
            NativeMethods.list_box_select(this.NativePtr, index);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetClickHandler(SelectIndexedActionMediator mediator)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            mediator.ThrowIfDisposed();

            NativeMethods.list_box_set_click_handler(this.NativePtr, mediator.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }
        
        public void Unselect(uint index)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (!(index < this.Size))
                throw new ArgumentOutOfRangeException();

            this.ThrowIfDisposed();
            NativeMethods.list_box_unselect(this.NativePtr, index);
#else
            throw new NotSupportedException();
#endif
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
#if !DLIB_NO_GUI_SUPPORT
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.list_box_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}