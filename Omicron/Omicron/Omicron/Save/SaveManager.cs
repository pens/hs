using System;
using System.Xml.Serialization;
using System.IO;

namespace Omicron
{
    public static class SaveManager
    {
        public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void Save(object obj, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (FileStream stream = new FileStream(Path + @"\" + filename + ".xml", FileMode.Create))
            {
                serializer.Serialize(stream, obj);
            }
        }
        public static T Load<T>(string filename)
           where T : new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(Path + @"\" + filename + ".xml", FileMode.OpenOrCreate))
            {
                try
                {
                    return (T)serializer.Deserialize(stream);
                }
                catch
                {
                    return new T();
                }
            }
        }
    }
}
