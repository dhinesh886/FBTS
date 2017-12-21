using System.Xml.Serialization;

namespace FBTS.Model.Common.XmlObjectModel
{
    public class Field
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
