using System.Xml.Serialization;

namespace ImgLab
{

    [XmlRoot("object")]
    public sealed class Object
    {

        [XmlElement("name")]
        public string Name
        {
            get;
            set;
        }

        [XmlElement("pose")]
        public string Pose
        {
            get;
            set;
        }

        [XmlElement("truncated")]
        public string Truncated
        {
            get;
            set;
        }

        [XmlElement("occluded")]
        public string Occluded
        {
            get;
            set;
        }

        [XmlElement("difficult")]
        public string Difficult
        {
            get;
            set;
        }

        [XmlElement("bndbox")]
        public BoundingBox BoundingBox
        {
            get;
            set;
        }

    }

}