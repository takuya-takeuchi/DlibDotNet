namespace DlibDotNet
{

    // The PASCAL VOC2012 dataset contains 20 ground-truth classes + background.  Each class
    // is represented using an RGB color value.  We associate each class also to an index in the
    // range [0, 20], used internally by the network. To convert the ground-truth data to
    // something that the network can efficiently digest, we need to be able to map the RGB
    // values to the corresponding indexes.
    public class Voc2012Class
    {

        #region Constructors

        public Voc2012Class(ushort index, RgbPixel rgbLabel, string classLabel)
        {
            this.Index = index;
            this.RgbLabel = rgbLabel;
            this.ClassLabel = classLabel;
        }

        #endregion

        #region Properties

        // The index of the class. In the PASCAL VOC 2012 dataset, indexes from 0 to 20 are valid.
        public ushort Index
        {
            get;
        }

        // The corresponding RGB representation of the class.
        public RgbPixel RgbLabel
        {
            get;
        }

        // The label of the class in plain text.
        public string ClassLabel
        {
            get;
        }

        #endregion

    }

}