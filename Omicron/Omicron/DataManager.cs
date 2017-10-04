using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Omicron
{
    public static class DataManager
    {
        static XmlSerializer serializer;

        public static string SaveLocation = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void Serialize(object o, string filename)
        {
            serializer = new XmlSerializer(o.GetType());
            using (Stream s = File.Create(SaveLocation + @"\" + filename))
                serializer.Serialize(s, o);
        }

        public static T Deserialize<T>(string filename)
            where T : new()
        {
            serializer = new XmlSerializer(typeof(T));
            using (Stream s = File.Open(SaveLocation + @"\" + filename, FileMode.Open))
                return (T)serializer.Deserialize(s);
        }
    }
}
