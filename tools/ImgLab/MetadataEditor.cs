using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DlibDotNet;
using DlibDotNet.ImageDatasetMetadata;

namespace ImgLab
{

    public sealed class MetadataEditor : CustomDrawableWindow
    {

        #region Events
        #endregion

        #region Fields

        private uint _KeyboardJumpPos;

        private long _LastKeyboardJumpPosUpdate;

        private readonly ColorMapper _ColorMapper = new ColorMapper();

        private bool _DisplayEquializedImage;

        private uint _ImagePos;

        private readonly Dataset _Metadata;

        private readonly string _FileName;

        private readonly MenuBar _MenuBar;

        private readonly ListBox _ListBoxImages;

        private readonly Label _OverlayLabelName;

        private readonly TextField _OverlayLabel;

        private readonly ImageDisplay _Display;

        private readonly MenuItemText _ItemText1;

        private readonly MenuItemText _ItemText2;

        private readonly MenuItemText _ItemText3;

        private readonly MenuItemText _ItemText4;

        private readonly MenuItemText _ItemText5;

        private readonly MenuItemSeparator _ItemSeparator1;

        private readonly MenuItemSeparator _ItemSeparator2;

        private readonly StringActionMediator _SaveMetadataToFileMediator;

        private readonly VoidActionMediator _FileSaveMediator;

        private readonly VoidActionMediator _FileSaveAsMediator;

        private readonly VoidActionMediator _RemoveSelectedImagesMediator;

        private readonly VoidActionMediator _CloseWindowMediator;

        private readonly VoidActionMediator _DisplayAboutMediator;

        private readonly ClickActionMediator _ClickActionMediator;

        private readonly VoidActionMediator _OverlayLabelChangedMediator;

        private readonly VoidActionMediator _OverlayRectsChanged;

        private readonly ImageDisplayOverlayRectActionMediator _OverlayRectSelected;

        private readonly SelectIndexedActionMediator _ListBoxImagesClickedMediator;

        #endregion

        #region Constructors

        public MetadataEditor(string fileName)
        {
            var filename = Path.GetFullPath(fileName);
            this._FileName = filename;
            // Make our current directory be the one that contains the metadata file.  We 
            // do this because that file might contain relative paths to the image files
            // we are supposed to be loading.
            Environment.CurrentDirectory = Path.GetDirectoryName(filename) ?? Path.GetPathRoot(filename);

            this._Metadata = Dlib.ImageDatasetMetadata.LoadImageDatasetMetadata(filename);
            var files = this._Metadata.Images.Select(image => image.FileName).ToArray();

            this._ListBoxImages = new ListBox(this);
            this._ListBoxImages.Load(files);
            this._ListBoxImages.MultipleSelectEnabled = true;
            this._ListBoxImagesClickedMediator = new SelectIndexedActionMediator(this.OnListBoxImagesClicked);
            this._ListBoxImages.SetClickHandler(this._ListBoxImagesClickedMediator);

            this._OverlayLabelName = new Label(this);
            this._OverlayLabelName.SetText("Next Label: ");
            this._OverlayLabel = new TextField(this);
            this._OverlayLabel.SetWidth(200);

            this._ClickActionMediator = new ClickActionMediator(this.OnImageClicked);
            this._OverlayLabelChangedMediator = new VoidActionMediator(this.OnOverlayLabelChanged);
            this._OverlayRectsChanged = new VoidActionMediator(this.OnOverlayRectsChanged);
            this._OverlayRectSelected = new ImageDisplayOverlayRectActionMediator(this.OnOverlayRectSelected);
            this._Display = new ImageDisplay(this);
            this._Display.SetImageClickedHandler(this._ClickActionMediator);
            this._Display.SetOverlayRectsChangedHandler(this._OverlayRectsChanged);
            this._Display.SetOverlayRectSelectedHandler(this._OverlayRectSelected);
            this._OverlayLabel.SetTextModifiedHandler(this._OverlayLabelChangedMediator);

            this._MenuBar = new MenuBar(this);
            this._MenuBar.SetNumberOfMenus(2);
            this._MenuBar.SetMenuName(0, "File", 'F');
            this._MenuBar.SetMenuName(1, "Help", 'H');

            this._FileSaveMediator = new VoidActionMediator(this.FileSave);
            this._ItemText1 = new MenuItemText("Save", this._FileSaveMediator, 'S');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText1);
            this._FileSaveAsMediator = new VoidActionMediator(this.FileSaveAs);
            this._ItemText2 = new MenuItemText("Save As", this._FileSaveAsMediator, 'A');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText2);
            this._ItemSeparator1 = new MenuItemSeparator();
            this._MenuBar.Menu(0).AddMenuItem(this._ItemSeparator1);
            this._RemoveSelectedImagesMediator = new VoidActionMediator(this.RemoveSelectedImages);
            this._ItemText3 = new MenuItemText("Remove Selected Images", this._RemoveSelectedImagesMediator, 'R');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText3);
            this._ItemSeparator2 = new MenuItemSeparator();
            this._MenuBar.Menu(0).AddMenuItem(this._ItemSeparator2);
            this._CloseWindowMediator = new VoidActionMediator(this.CloseWindow);
            this._ItemText4 = new MenuItemText("Exit", this._CloseWindowMediator, 'x');
            this._MenuBar.Menu(0).AddMenuItem(this._ItemText4);

            this._DisplayAboutMediator = new VoidActionMediator(this.DisplayAbout);
            this._ItemText5 = new MenuItemText("About", this._DisplayAboutMediator, 'x');
            this._MenuBar.Menu(1).AddMenuItem(this._ItemText5);

            // set the size of this window.
            this.OnWindowResized();
            this.LoadImageAndSetSize(0);
            this.OnWindowResized();
            if (this._ImagePos < this._ListBoxImages.Size)
                this._ListBoxImages.Select(this._ImagePos);

            // make sure the window is centered on the screen.
            this.GetSize(out var width, out var height);
            this.GetDisplaySize(out var screenWidth, out var screenHeight);
            this.SetPos((int)((screenWidth - width) / 2), (int)((screenHeight - height) / 2));

            this.Show();

            this._SaveMetadataToFileMediator = new StringActionMediator(SaveMetadataToFile);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void AddLabelablePartName(string name)
        {
            this._Display.AddLabelablePartName(name);
        }

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
            this._Display.SetPos(this._ListBoxImages.Right, this._OverlayLabel.Bottom + 3);

            this._Display.SetSize((uint)(width - this._Display.Left), (uint)(height - this._Display.Top));
        }

        protected override void OnKeyDown(uint key, bool isPrintable, uint state)
        {
            base.OnKeyDown(key, isPrintable, state);

            if (isPrintable)
            {
                if (key == '\t')
                {
                    this._OverlayLabel.GiveInputFocus();
                    this._OverlayLabel.SelectAllText();
                }

                // If the user types a number then jump to that image.
                if ('0' <= key && key <= '9' && this._Metadata.Images.Count != 0 && !this._OverlayLabel.HasInputFocus)
                {
                    long curtime = 0;
                    // If it's been a while since the user typed numbers then forget the last jump
                    // position and start accumulating numbers over again.
                    if (curtime - this._LastKeyboardJumpPosUpdate >= 2)
                        this._KeyboardJumpPos = 0;
                    this._LastKeyboardJumpPosUpdate = curtime;

                    this._KeyboardJumpPos *= 10;
                    this._KeyboardJumpPos += key - '0';
                    if (this._KeyboardJumpPos >= this._Metadata.Images.Count)
                        this._KeyboardJumpPos = (uint)(this._Metadata.Images.Count - 1);

                    this._ImagePos = this._KeyboardJumpPos;
                    this.SelectImage(this._ImagePos);
                }
                else
                {
                    this._LastKeyboardJumpPosUpdate = 0;
                }

                if (key == 'd' && (state & (ulong)KeyboardStateMasks.KBD_MOD_ALT) > 0)
                {
                    this.RemoveSelectedImages();
                }

                if (key == 'e' && !this._OverlayLabel.HasInputFocus)
                {
                    this._DisplayEquializedImage = !this._DisplayEquializedImage;
                    this.SelectImage(this._ImagePos);
                }

                // Make 'w' and 's' act like KEY_UP and KEY_DOWN
                if ((key == 'w' || key == 'W') && !this._OverlayLabel.HasInputFocus)
                {
                    key = (uint)NonPrintableKeyboardKeys.KEY_UP;
                }
                else if ((key == 's' || key == 'S') && !this._OverlayLabel.HasInputFocus)
                {
                    key = (uint)NonPrintableKeyboardKeys.KEY_DOWN;
                }
                else
                {
                    return;
                }
            }

            if (key == (uint)NonPrintableKeyboardKeys.KEY_UP)
            {
                if ((state & (ulong)KeyboardStateMasks.KBD_MOD_CONTROL) > 0 && (state & (ulong)KeyboardStateMasks.KBD_MOD_SHIFT) > 0)
                {
                    // Don't do anything if there are no boxes in the current image.
                    if (this._Metadata.Images[(int)this._ImagePos].Boxes.Count == 0)
                        return;
                    // Also don't do anything if there *are* boxes in the next image.
                    if (this._ImagePos > 1 && this._Metadata.Images[(int)(this._ImagePos - 1)].Boxes.Count != 0)
                        return;

                    PropagateBoxes(this._Metadata, this._ImagePos, this._ImagePos - 1);
                }
                else if ((state & (ulong)KeyboardStateMasks.KBD_MOD_CONTROL) > 0)
                {
                    // If the label we are supposed to propagate doesn't exist in the current image
                    // then don't advance.
                    if (!HasLabelOrAllBoxesLabeled(this._Display.GetDefaultOverlayRectLabel(), this._Metadata.Images[(int)this._ImagePos]))
                        return;

                    // if the next image is going to be empty then fast forward to the next one
                    while (this._ImagePos > 1 && this._Metadata.Images[(int)(this._ImagePos - 1)].Boxes.Count == 0)
                        --this._ImagePos;

                    PropagateLabels(this._Display.GetDefaultOverlayRectLabel(), this._Metadata, this._ImagePos, this._ImagePos - 1);
                }
                this.SelectImage(this._ImagePos - 1);
            }
            else if (key == (uint)NonPrintableKeyboardKeys.KEY_DOWN)
            {
                if ((state & (ulong)KeyboardStateMasks.KBD_MOD_CONTROL) > 0 && (state & (ulong)KeyboardStateMasks.KBD_MOD_SHIFT) > 0)
                {
                    // Don't do anything if there are no boxes in the current image.
                    if (this._Metadata.Images[(int)this._ImagePos].Boxes.Count == 0)
                        return;
                    // Also don't do anything if there *are* boxes in the next image.
                    if (this._ImagePos + 1 < (ulong)this._Metadata.Images.Count && this._Metadata.Images[(int)(this._ImagePos + 1)].Boxes.Count != 0)
                        return;

                    PropagateBoxes(this._Metadata, this._ImagePos, this._ImagePos + 1);
                }
                else if ((state & (ulong)KeyboardStateMasks.KBD_MOD_CONTROL) > 0)
                {
                    // If the label we are supposed to propagate doesn't exist in the current image
                    // then don't advance.
                    if (!HasLabelOrAllBoxesLabeled(this._Display.GetDefaultOverlayRectLabel(), this._Metadata.Images[(int)this._ImagePos]))
                        return;

                    // if the next image is going to be empty then fast forward to the next one
                    while (this._ImagePos + 1 < this._Metadata.Images.Count && this._Metadata.Images[(int)(this._ImagePos + 1)].Boxes.Count == 0)
                        ++this._ImagePos;

                    PropagateLabels(this._Display.GetDefaultOverlayRectLabel(), this._Metadata, this._ImagePos, this._ImagePos + 1);
                }

                this.SelectImage(this._ImagePos + 1);
            }
        }

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            this._ClickActionMediator.Dispose();
            this._Metadata.Dispose();
            this._Display.Dispose();
            this._OverlayRectSelected.Dispose();
            this._OverlayLabelName.Dispose();
            this._ListBoxImages.Dispose();
            this._MenuBar.Dispose();
            this._ItemSeparator1.Dispose();
            this._ItemSeparator2.Dispose();
            this._ItemText1.Dispose();
            this._ItemText2.Dispose();
            this._ItemText3.Dispose();
            this._ItemText4.Dispose();
            this._ItemText5.Dispose();
            this._ListBoxImagesClickedMediator.Dispose();
            this._SaveMetadataToFileMediator.Dispose();
            this._OverlayLabel.Dispose();
            this._CloseWindowMediator.Dispose();
            this._DisplayAboutMediator.Dispose();
            this._FileSaveAsMediator.Dispose();
            this._FileSaveMediator.Dispose();
            this._OverlayLabelChangedMediator.Dispose();
            this._OverlayRectsChanged.Dispose();
            this._RemoveSelectedImagesMediator.Dispose();
        }

        #endregion

        #region Helpers

        private void DisplayAbout()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Dlib.WrapString($"Image Labeler v{Program.Version}.", 0, 0));
            sb.AppendLine();
            sb.AppendLine(Dlib.WrapString("This program is a tool for labeling images with rectangles. ", 0, 0));
            sb.AppendLine();

            sb.AppendLine(Dlib.WrapString("You can add a new rectangle by holding the shift key, left clicking " +
                                          "the mouse, and dragging it.  New rectangles are given the label from the \"Next Label\" " +
                                          "field at the top of the application.  You can quickly edit the contents of the Next Label field " +
                                          "by hitting the tab key. Double clicking " +
                                          "a rectangle selects it and the delete key removes it.  You can also mark " +
                                          "a rectangle as ignored by hitting the i or END keys when it is selected.  Ignored " +
                                          "rectangles are visually displayed with an X through them.  You can remove an image " +
                                          "entirely by selecting it in the list on the left and pressing alt+d."
                                          , 0, 0));
            sb.AppendLine();

            sb.AppendLine(Dlib.WrapString("It is also possible to label object parts by selecting a rectangle and " +
                                          "then right clicking.  A popup menu will appear and you can select a part label. " +
                                          "Note that you must define the allowable part labels by giving --parts on the " +
                                          "command line.  An example would be '--parts \"leye reye nose mouth\"'. " +
                                          "Alternatively, if you don't give --parts you can simply select a rectangle and shift+left " +
                                          "click to add parts. Parts added this way will be labeled with integer labels starting from 0. " +
                                          "You can only use this simpler part adding mode if all the parts in a rectangle are already " +
                                          "labeled with integer labels or the rectangle has no parts at all."
                                          , 0, 0));
            sb.AppendLine();

            sb.AppendLine(Dlib.WrapString("Press the down or s key to select the next image in the list and the up or w " +
                                          "key to select the previous one.", 0, 0));
            sb.AppendLine();

            sb.AppendLine(Dlib.WrapString("Additionally, you can hold ctrl and then scroll the mouse wheel to zoom.  A normal left click " +
                                          "and drag allows you to navigate around the image.  Holding ctrl and " +
                                          "left clicking a rectangle will give it the label from the Next Label field. " +
                                          "Holding shift + right click and then dragging allows you to move things around. " +
                                          "Holding ctrl and pressing the up or down keyboard keys will propagate " +
                                          "rectangle labels from one image to the next and also skip empty images. " +
                                          "Similarly, holding ctrl+shift will propagate entire boxes via a visual tracking " +
                                          "algorithm from one image to the next. " +
                                          "Finally, typing a number on the keyboard will jump you to a specific image.", 0, 0));
            sb.AppendLine();

            sb.AppendLine(Dlib.WrapString("You can also toggle image histogram equalization by pressing the e key.", 0, 0));

            Dlib.MessageBox("About Image Labeler", sb.ToString());
        }

        private void FileSave()
        {
            SaveMetadataToFile(this._FileName);
        }

        private void FileSaveAs()
        {
            Dlib.SaveFileBox(this._SaveMetadataToFileMediator);
        }

        private IEnumerable<ImageDisplay.OverlayRect> GetOverlays(Image data, ColorMapper stringToColor)
        {
            using (var disposer = new EnumerableDisposer<Box>(data.Boxes))
            {
                var boxes = disposer.Collection.ToArray();
                var temp = new ImageDisplay.OverlayRect[boxes.Length];
                for (var i = 0; i < temp.Length; ++i)
                {
                    temp[i] = new ImageDisplay.OverlayRect();
                    temp[i].Rect = boxes[i].Rect;
                    temp[i].Label = boxes[i].Label;
                    temp[i].Parts = boxes[i].Parts;
                    temp[i].CrossedOut = boxes[i].Ignore;
                    temp[i].Color = stringToColor.Operator(boxes[i].Label);
                }

                return temp;
            }
        }

        private static bool HasLabelOrAllBoxesLabeled(string label, Image img)
        {
            if (string.IsNullOrEmpty(label))
                return true;

            var allBoxesLabeled = true;
            for (int i = 0, count = img.Boxes.Count; i < count; ++i)
            {
                if (img.Boxes[i].Label == label)
                    return true;
                if (img.Boxes[i].Label.Length == 0)
                    allBoxesLabeled = false;
            }

            return allBoxesLabeled;
        }

        private void LoadImage(uint index)
        {
            if (index >= this._Metadata.Images.Count)
                return;

            this._ImagePos = index;

            Array2D<RgbPixel> img = null;
            try
            {
                this._Display.ClearOverlay();

                try
                {
                    img = Dlib.LoadImage<RgbPixel>(this._Metadata.Images[(int)index].FileName);
                    this.Title = $"{this._Metadata.Name} #{index}: {this._Metadata.Images[(int)index].FileName}";
                }
                catch (Exception e)
                {
                    Dlib.MessageBox("Error loading image", e.Message);
                }

                if (this._DisplayEquializedImage)
                    Dlib.EqualizeHistogram(img);
                this._Display.SetImage(img);
                using (var overlays = new EnumerableDisposer<ImageDisplay.OverlayRect>(this.GetOverlays(this._Metadata.Images[(int)index], this._ColorMapper)))
                    this._Display.AddOverlay(overlays.Collection);
            }
            finally
            {
                img?.Dispose();
            }
        }

        private void LoadImageAndSetSize(uint index)
        {
            if (index >= this._Metadata.Images.Count)
                return;

            this._ImagePos = index;

            Array2D<RgbPixel> img = null;
            try
            {
                this._Display.ClearOverlay();

                try
                {
                    img = Dlib.LoadImage<RgbPixel>(this._Metadata.Images[(int)index].FileName);
                    this.Title = $"{this._Metadata.Name} #{index}: {this._Metadata.Images[(int)index].FileName}";
                }
                catch (Exception e)
                {
                    Dlib.MessageBox("Error loading image", e.Message);
                    return;
                }

                this.GetDisplaySize(out var screenWidth, out var screenHeight);

                var neededWidth = this._Display.Left + img.Columns + 4;
                var neededHeight = this._Display.Top + img.Rows + 4;
                if (neededWidth < 300) neededWidth = 300;
                if (neededHeight < 300) neededHeight = 300;

                if (neededWidth > 100 + screenWidth)
                    neededWidth = (int)(screenWidth - 100);
                if (neededHeight > 100 + screenHeight)
                    neededHeight = (int)(screenHeight - 100);

                this.SetSize(neededWidth, neededHeight);

                if (this._DisplayEquializedImage)
                    Dlib.EqualizeHistogram(img);
                this._Display.SetImage(img);
                using (var overlays = new EnumerableDisposer<ImageDisplay.OverlayRect>(this.GetOverlays(this._Metadata.Images[(int)index], this._ColorMapper)))
                    this._Display.AddOverlay(overlays.Collection);
            }
            finally
            {
                img?.Dispose();
            }
        }

        private void OnImageClicked(Point p, bool isDoubleClick, uint button)
        {
            this._Display.SetDefaultOverlayRectColor(this._ColorMapper.Operator(Trim(this._OverlayLabel.Text)));
        }

        private void OnOverlayLabelChanged()
        {
            this._Display.SetDefaultOverlayRectLabel(Trim(this._OverlayLabel.Text));
        }

        private void OnOverlayRectsChanged()
        {
            if (this._ImagePos >= (ulong)this._Metadata.Images.Count)
                return;

            using (var disposer = new EnumerableDisposer<ImageDisplay.OverlayRect>(this._Display.GetOverlayRects()))
            {
                var rects = disposer.Collection.ToArray();

                var boxes = this._Metadata.Images[(int)this._ImagePos].Boxes;
                boxes.Clear();

                for (var i = 0; i < rects.Length; ++i)
                {
                    var temp = new Box();
                    temp.Label = rects[i].Label;
                    temp.Rect = rects[i].Rect;
                    temp.Parts = rects[i].Parts;
                    temp.Ignore = rects[i].CrossedOut;
                    boxes.Add(temp);
                }
            }
        }

        private void OnOverlayRectSelected(ImageDisplay.OverlayRect rect)
        {
            this._OverlayLabel.Text = rect.Label;
            this._Display.SetDefaultOverlayRectLabel(rect.Label);
            this._Display.SetDefaultOverlayRectColor(this._ColorMapper.Operator(rect.Label));
        }

        private void OnListBoxImagesClicked(uint index)
        {
            this.LoadImage(index);
        }

        private static void PropagateBoxes(Dataset data, uint prev, uint next)
        {
            if (prev == next || next >= data.Images.Count)
                return;

            using (var img1 = Dlib.LoadImage<RgbPixel>(data.Images[(int)prev].FileName))
            using (var img2 = Dlib.LoadImage<RgbPixel>(data.Images[(int)next].FileName))
                for (int i = 0, count = data.Images[(int)prev].Boxes.Count; i < count; ++i)
                {
                    using (var tracker = new CorrelationTracker())
                    {
                        tracker.StartTrack(img1, data.Images[(int)prev].Boxes[i].Rect);
                        tracker.Update(img2);
                        var box = data.Images[(int)prev].Boxes[i];
                        box.Rect = (Rectangle)tracker.GetPosition();
                        data.Images[(int)next].Boxes.Add(box);
                    }
                }
        }

        private static void PropagateLabels(string label, Dataset data, uint prev, uint next)
        {
            if (prev == next || next >= data.Images.Count)
                return;

            for (int i = 0, count = data.Images[(int)prev].Boxes.Count; i < count; ++i)
            {
                if (data.Images[(int)prev].Boxes[i].Label != label)
                    continue;

                // figure out which box in the next image matches the current one the best
                var cur = data.Images[(int)prev].Boxes[i].Rect;
                double bestOverlap = 0;
                var bestIndex = 0;
                for (var j = 0; j < count; ++j)
                {
                    var nextBox = data.Images[(int)prev].Boxes[j].Rect;
                    var overlap = cur.Intersect(nextBox).Area / (double)(cur + nextBox).Area;
                    if (overlap > bestOverlap)
                    {
                        bestOverlap = overlap;
                        bestIndex = j;
                    }
                }

                // If we found a matching rectangle in the next image and the best match doesn't
                // already have a label.
                if (bestOverlap > 0.5 && data.Images[(int)prev].Boxes[bestIndex].Label == "")
                {
                    data.Images[(int)prev].Boxes[bestIndex].Label = label;
                }
            }
        }

        private void RemoveSelectedImages()
        {
            ulong minIndex;
            using (var list = this._ListBoxImages.GetSelected())
            {
                list.Reset();
                minIndex = this._ListBoxImages.Size;
                while (list.MoveNext)
                {
                    this._ListBoxImages.Unselect(list.Element());
                    minIndex = Math.Min(minIndex, list.Element());
                }

                // remove all the selected items from metadata.images
                var toRemove = new HashSet<uint>();
                list.Reset();
                while (list.MoveNext)
                    toRemove.Add(list.Element());

                var images = new List<Image>();
                for (uint i = 0, count = (uint)this._Metadata.Images.Count; i < count; ++i)
                {
                    if (!toRemove.Contains(i))
                    {
                        images.Add(this._Metadata.Images[(int)i]);
                    }
                }

                for (int i = 0, count = this._Metadata.Images.Count; i < count; ++i)
                {
                    if (!images.Contains(this._Metadata.Images[i]))
                        this._Metadata.Images[i].Dispose();
                }

                for (var i = this._Metadata.Images.Count - 1; i >= 0; i--)
                {
                    if (images.Contains(this._Metadata.Images[i]))
                        continue;

                    this._Metadata.Images[i].Dispose();
                    this._Metadata.Images.RemoveAt(i);
                }
            }

            // reload metadata into lb_images
            var files = this._Metadata.Images.Select(image => image.FileName).ToArray();
            this._ListBoxImages.Load(files);

            if (minIndex != 0)
                minIndex--;

            this.SelectImage((uint)minIndex);
        }

        private void SaveMetadataToFile(string file)
        {
            try
            {
                Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(this._Metadata, file);
            }
            catch (IOException e)
            {
                Dlib.MessageBox("Error saving file", e.Message);
            }
        }

        private void SelectImage(uint index)
        {
            if (index < this._ListBoxImages.Size)
            {
                // unselect all currently selected images
                using (var list = this._ListBoxImages.GetSelected())
                {
                    list.Reset();
                    while (list.MoveNext)
                    {
                        this._ListBoxImages.Unselect(list.Element());
                    }
                }

                this._ListBoxImages.Select(index);
                this.LoadImage(index);
            }
            else if (this._ListBoxImages.Size == 0)
            {
                this._Display.ClearOverlay();
                using (var emptyImg = new Array2D<byte>())
                    this._Display.SetImage(emptyImg);
            }
        }

        private static string Trim(string text)
        {
            return text.Trim(' ', '\t', '\r', '\n');
        }

        #endregion

        #endregion

        internal sealed class ColorMapper
        {

            #region Fields

            private readonly Dictionary<string, RgbAlphaPixel> _Colors = new Dictionary<string, RgbAlphaPixel>();

            #endregion

            #region Methods

            public RgbAlphaPixel Operator(string str)
            {
                if (this._Colors.TryGetValue(str, out var value))
                    return value;

                var pix = new HsiPixel
                {
                    H = Reverse((byte)this._Colors.Count),
                    S = byte.MaxValue,
                    I = 150
                };

                var result = new RgbAlphaPixel();
                Dlib.AssignPixel(ref result, pix);
                this._Colors[str] = result;
                return result;
            }

            #region Helpers

            // We use a bit reverse here because it causes us to evenly spread the colors as we
            // allocated them. First the colors are maximally different, then become interleaved
            // and progressively more similar as they are allocated.
            private static byte Reverse(ulong b)
            {
                // reverse the order of the bits in b.
                b = ((b * 0x0802LU & 0x22110LU) | (b * 0x8020LU & 0x88440LU)) * 0x10101LU >> 16;
                return (byte)b;
            }

            #endregion

            #endregion

        }

    }

}
