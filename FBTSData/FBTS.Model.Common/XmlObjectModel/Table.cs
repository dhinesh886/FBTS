using System.Collections.Generic;
using System.Xml.Serialization;

namespace FBTS.Model.Common.XmlObjectModel
{
    public class Table
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlArray("Fields"), XmlArrayItem("Field", Type = typeof(Field))]
        public List<Field> Fields { get; set; }
    }
}
