using System.IO;
using System.Xml.Serialization;

namespace FBTS.Library.Common
{
    public static class XmlUtilities
    {
        public static string ToXml<T>(this T o)
            where T : new()
        {
            string retVal;
            using (var ms = new MemoryStream())
            {
                var xs = new XmlSerializer(typeof (T));
                xs.Serialize(ms, o);
                ms.Flush();
                ms.Position = 0;
                var sr = new StreamReader(ms);
                retVal = sr.ReadToEnd();
            }
            return retVal;
        }
        public static T ToObject<T>(string sourceXml)
           where T : new()
        {
            var deserializer = new XmlSerializer(typeof(T));
            TextReader reader = new StringReader(sourceXml);
            var obj = deserializer.Deserialize(reader);
            var objects = (T)obj;
            reader.Close();
            return objects;
        }
    }
}