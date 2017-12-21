using System.Collections.Generic;
using System.Xml.Serialization;

namespace FBTS.Model.Common.XmlObjectModel
{
    public class Tables
    {
        [XmlElement("Table")] 
        public List<Table> TableList = new List<Table>();
    }
}
