using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MedExam.Common
{
    public static class LocalSettingsService
    {
        private const string ConfigPath = "Settings";

        public static T Load<T>(T settingsDefault = default(T)) where T : new()
        {
            var type = typeof(T);
            var fullFileName = GetFullFileName(type);

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

        public static bool Exists<T>()
        {
            return Exists(typeof(T));
        }

        public static bool Exists(Type type)
        {
            var fullFileName = GetFullFileName(type);
            return File.Exists(fullFileName);
        }

        private static string GetFullFileName(Type type)
        {
            return String.Format("{0}/{1}.xml", ConfigPath, type.Name);
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
            var fullFileName = GetFullFileName(type);
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