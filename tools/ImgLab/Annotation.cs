using System.Xml.Serialization;

namespace ImgLab
{

    [XmlRoot("annotation")]
    public sealed class Annotation
    {

        [XmlElement("filename")]
        public string FileName
        {
            get;
            set;
        }

        [XmlElement("source")]
        public Source Source
        {
            get;
            set;
        }

        [XmlElement("object")]
        public Object[] Objects
        {
            get;
            set;
        }

    }

}