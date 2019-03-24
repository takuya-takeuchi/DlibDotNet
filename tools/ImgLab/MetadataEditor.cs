using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DlibDotNet;

namespace ImgLab
{

    public sealed class MetadataEditor : CustomDrawableWindow
    {

        #region Events
        #endregion

        #region Fields

        private readonly string _FileName;

        private MenuBar _MenuBar;

        private ListBox _ListBoxImages;

        private readonly Label _OverlayLabelName;

        private readonly TextField _OverlayLabel;

        #endregion

        #region Constructors

        public MetadataEditor(string fileName)
        {
            this._FileName = fileName;

            this._OverlayLabelName = new Label(this);
            this._OverlayLabelName.SetText("Next Label: ");
            this._OverlayLabel = new TextField(this);
            this._OverlayLabel.SetWidth(200);

            this._MenuBar = new MenuBar(this);
            this._MenuBar.SetNumberOfMenus(2);
            this._MenuBar.SetMenuName(0, "File", 'F');
            this._MenuBar.SetMenuName(1, "Help", 'H');

            this._ListBoxImages = new ListBox(this);

            this.Show();
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        #region Overrids

        protected override void OnConstructor(IntPtr window)
        {
        }

        protected override void OnDestructor()
        {
            //this.CloseWindow();
        }

        protected override void OnWindowResized()
        {
            this.GetSize(out var width, out var height);

            this._ListBoxImages.SetPos(0, this._MenuBar.Bottom + 1);
            this._ListBoxImages.SetSize(180, height - this._MenuBar.Height);

            this._OverlayLabelName.SetPos(this._ListBoxImages.Right + 10, (int)(this._MenuBar.Bottom + (this._OverlayLabel.Height - this._OverlayLabelName.Height) / 2 + 1));
            this._OverlayLabel.SetPos(this._OverlayLabelName.Right, this._MenuBar.Bottom + 1);
        }

        protected override void OnKeyDown(uint key, bool isPrintable, uint state)
        {
        }

        ///// <summary>
        ///// Releases all unmanaged resources.
        ///// </summary>
        //protected override void DisposeUnmanaged()
        //{

        //    base.DisposeUnmanaged();

        //    if (this.NativePtr == IntPtr.Zero)
        //        return;
        //}

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

    }

}
