using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PyramidDown : Pyramid
    {

        #region Constructors

        public PyramidDown(uint pyramidRate)
        {
            this.PyramidRate = pyramidRate;

            var err = NativeMethods.pyramid_down_new(pyramidRate, out var ret);
            switch (err)
            {
                case NativeMethods.ErrorType.PyramidNotSupportRate:
                    throw new NotSupportedException();
            }

            this.NativePtr = ret;
        }

        #endregion

        #region Properties

        public uint PyramidRate
        {
            get;
        }

        #endregion

        #region Methods

        public override Rectangle RectDown(Rectangle rect)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_down_rectangle(this.NativePtr,
                                                                         this.PyramidRate,
                                                                         p.NativePtr,
                                                                         out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new Rectangle(ret);
            }
        }

        public override Rectangle RectDown(Rectangle rect, uint levels)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_down2_rectangle(this.NativePtr,
                                                                          this.PyramidRate,
                                                                          p.NativePtr,
                                                                          levels,
                                                                          out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new Rectangle(ret);
            }
        }

        public override DRectangle RectDown(DRectangle rect)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_down(this.NativePtr,
                                                        this.PyramidRate,
                                                        p.NativePtr,
                                                        out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new DRectangle(ret);
            }
        }

        public override DRectangle RectDown(DRectangle rect, uint levels)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_down2(this.NativePtr,
                                                         this.PyramidRate,
                                                         p.NativePtr,
                                                         levels,
                                                         out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new DRectangle(ret);
            }
        }

        public override Rectangle RectUp(Rectangle rect)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_up_rectangle(this.NativePtr,
                                                                this.PyramidRate,
                                                                p.NativePtr,
                                                                out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new Rectangle(ret);
            }
        }

        public override Rectangle RectUp(Rectangle rect, uint levels)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_up2_rectangle(this.NativePtr,
                                                                 this.PyramidRate,
                                                                 p.NativePtr,
                                                                 levels,
                                                                 out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new Rectangle(ret);
            }
        }

        public override DRectangle RectUp(DRectangle rect)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_up(this.NativePtr,
                                                      this.PyramidRate,
                                                      p.NativePtr,
                                                      out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new DRectangle(ret);
            }
        }

        public override DRectangle RectUp(DRectangle rect, uint levels)
        {
            using (var p = rect.ToNative())
            {
                var err = NativeMethods.pyramid_down_rect_up2(this.NativePtr,
                                                       this.PyramidRate,
                                                       p.NativePtr,
                                                       levels,
                                                       out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                        throw new NotSupportedException();
                }

                return new DRectangle(ret);
            }
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

            NativeMethods.pyramid_down_delete(this.PyramidRate, this.NativePtr);
        }

        #endregion

        #endregion

    }

}
