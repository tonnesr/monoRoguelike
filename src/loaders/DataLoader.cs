using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System;

namespace mono2.src.loading 
{
  public class DataLoader {
    private string basePath;
    
    public DataLoader(string basePath) {
      this.basePath = $"{Environment.CurrentDirectory}\\{basePath}";
    }

    public T loadXmlFile<T>(string fileName) {
      using (StreamReader reader = new StreamReader($"{basePath}\\{fileName}")) {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(reader);
      }
    }

    public T loadJsonFile<T>(string fileName) {
      using (StreamReader reader = new StreamReader($"{basePath}\\{fileName}")) {
        JsonSerializer serializer = new JsonSerializer();
        return (T)serializer.Deserialize(reader, typeof(T));
      }
    }
  }
}