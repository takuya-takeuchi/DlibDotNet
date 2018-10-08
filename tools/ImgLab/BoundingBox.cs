using System.Xml.Serialization;

namespace ImgLab
{

    [XmlRoot("bndbox")]
    public sealed class BoundingBox
    {

        [XmlElement("xmin")]
        public int XMin
        {
            get;
            set;
        }

        [XmlElement("ymin")]
        public int YMin
        {
            get;
            set;
        }

        [XmlElement("xmax")]
        public int XMax
        {
            get;
            set;
        }

        [XmlElement("ymax")]
        public int YMax
        {
            get;
            set;
        }


    }

}