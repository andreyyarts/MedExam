using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MedExam.Common
{
    public class LocalSettingsService
    {
        private const string ConfigPath = "Settings";

        public static T Load<T>(T settingsDefault = default(T)) where T : new()
        {
            var type = typeof(T);
            var fileName = type.Name;

            var fullFileName = String.Format("{0}/{1}.xml", ConfigPath, fileName);

            if (!File.Exists(fullFileName))
            {
                return SaveDefaultSettings(settingsDefault);
            }
            using (var stream = File.OpenRead(fullFileName))
            {
                var serializer = new XmlSerializer(type);
                var settings = (T)serializer.Deserialize(stream);
                stream.Close();
                return settings;
            }
        }

        private static T SaveDefaultSettings<T>(T settingsDefault = default(T)) where T : new()
        {
            var settings = Equals(settingsDefault, default(T)) ? new T() : settingsDefault;

            return Save(settings);
        }

        private static T Save<T>(T settings) where T : new()
        {
            if (!Directory.Exists(ConfigPath))
            {
                Directory.CreateDirectory(ConfigPath);
            }
            var type = typeof(T);
            var fileName = type.Name;
            var fullFileName = string.Format("{0}/{1}.xml", ConfigPath, fileName);
            using (var stream = File.Create(fullFileName))
            {
                new XmlSerializer(type).Serialize(stream, settings,
                    new XmlSerializerNamespaces(new[]
                    {
                        new XmlQualifiedName(string.Empty)
                    }));
                stream.Close();
                return settings;
            }
        }
    }
}