using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IdentityMVC.Services
{
    public class XMLParserService<T>
    {
        public T DeserializeData(string xml_data)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml_data);
            XmlNodeReader xmlNodeReader = new XmlNodeReader(xmlDoc.DocumentElement);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            var rawData = xmlSerializer.Deserialize(xmlNodeReader);
            T result = (T)rawData;
            return result;
        }

        public string SerializeData(T data)
        {
            StringBuilder stringBuilder= new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringWriter strWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(strWriter, data);
            return stringBuilder.ToString();
        }
    }
}
