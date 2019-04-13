using DlibDotNet;

namespace DnnSemanticSegmentation
{

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