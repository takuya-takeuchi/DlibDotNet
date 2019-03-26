using System;
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

        private readonly MenuItemText _ItemText1;

        private readonly MenuItemText _ItemText2;

        private readonly MenuItemText _ItemText3;

        private readonly MenuItemText _ItemText4;

        private readonly MenuItemText _ItemText5;

        private readonly MenuItemSeparator _ItemSeparator1;

        private readonly MenuItemSeparator _ItemSeparator2;

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

            this._ItemText1 = new MenuItemText("Save", this, this.FileSave, 'S');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText1);
            this._ItemText2 = new MenuItemText("Save As", this, this.FileSaveAs, 'A');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText2);
            this._ItemSeparator1 = new MenuItemSeparator();
            this._MenuBar.Menu(0).AddMenuItem(this._ItemSeparator1);
            this._ItemText3 = new MenuItemText("Remove Selected Images", this, this.RemoveSelectedImages, 'R');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText3);
            this._ItemSeparator2 = new MenuItemSeparator();
            this._MenuBar.Menu(0).AddMenuItem(this._ItemSeparator2);
            this._ItemText4 = new MenuItemText("Exit", this, this.CloseWindow, 'x');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText4);

            this._ItemText5 = new MenuItemText("About", this, this.DisplayAbout, 'x');
            this._MenuBar.Menu(1).AddMenuItem(this._ItemText5);

            this._ListBoxImages = new ListBox(this);

            // make sure the window is centered on the screen.
            this.GetSize(out var width, out var height);
            this.GetDisplaySize(out var screenWidth, out var screenHeight);
            this.SetPos((int)((screenWidth - width) / 2), (int)((screenHeight - height) / 2));

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

        private void DisplayAbout()
        {
            Dlib.MessageBox("About Image Labeler", "");
        }

        private void FileSave()
        {

        }

        private void FileSaveAs()
        {

        }

        private void RemoveSelectedImages()
        {

        }

        #endregion

        #endregion

    }

}
