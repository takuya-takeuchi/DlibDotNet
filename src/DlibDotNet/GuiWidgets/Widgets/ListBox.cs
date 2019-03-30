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
            this.NativePtr = NativeMethods.list_box_new(window.NativePtr);
        }

        #endregion

        #region Properties

        public bool MultipleSelectEnabled
        {
            get
            {
                this.ThrowIfDisposed();

                return NativeMethods.list_box_multiple_select_enabled(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();

                if (value)
                    NativeMethods.list_box_enable_multiple_select(this.NativePtr);
                else
                    NativeMethods.list_box_disable_multiple_select(this.NativePtr);
            }
        }

        public ulong Size
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.list_box_size(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public Queue<uint>.Kernel1A GetSelected()
        {
            this.ThrowIfDisposed();
            var ret = NativeMethods.list_box_get_selected(this.NativePtr);
            return new Queue<uint>.Kernel1A(ret);
        }

        public void Load(IEnumerable<string> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            using (var disposer = new EnumerableDisposer<StdString>(list.Select(s => new StdString(s))))
            using (var vector = new StdVector<StdString>(disposer.Collection))
                NativeMethods.list_box_load_stdstring(this.NativePtr, vector.NativePtr);
        }

        public void Select(uint index)
        {
            if (!(index < this.Size))
                throw new ArgumentOutOfRangeException();

            this.ThrowIfDisposed();
            NativeMethods.list_box_select(this.NativePtr, index);
        }

        public void SetClickHandler(SelectIndexedActionMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            mediator.ThrowIfDisposed();

            NativeMethods.list_box_set_click_handler(this.NativePtr, mediator.NativePtr);
        }
        
        public void Unselect(uint index)
        {
            if (!(index < this.Size))
                throw new ArgumentOutOfRangeException();

            this.ThrowIfDisposed();
            NativeMethods.list_box_unselect(this.NativePtr, index);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.list_box_delete(this.NativePtr);
        }

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

    }

}