using System.Xml.Serialization;

namespace ImgLab
{

    [XmlRoot("source")]
    public sealed class Source
    {

        [XmlElement("database")]
        public string Database
        {
            get;
            set;
        }

    }

}