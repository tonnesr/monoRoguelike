using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace mono2.src.loading 
{
  public class DataLoader {
    public T loadXmlFile<T>(string fileName) {
      using (StreamReader reader = new StreamReader(fileName)) {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(reader);
      }
    }

    public T loadJsonFile<T>(string fileName) {
      using (StreamReader reader = new StreamReader(fileName)) {
        JsonSerializer serializer = new JsonSerializer();
        return (T)serializer.Deserialize(reader, typeof(T));
      }
    }
  }
}