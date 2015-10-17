using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MedExam.Common.Local
{
    public class LocalSettingsRepository
    {
        private const string ConfigPath = "Settings";

        public static T LoadSetting<T>(string configPath = null, string fileName = null) where T : new()
        {
            return Load(new T(), configPath, fileName);
        }

        public static T[] LoadSettings<T>(string configPath = null, string fileName = null) where T : new()
        {
            return Load(new[] { new T() }, configPath, fileName);
        }

        private static T Load<T>(T defaultSetting, string configPath, string fileName)
        {
            SetDefaultPathIfNull(ref configPath);
            var type = typeof(T);
            var fullFileName = GetFullFileName(configPath, fileName ?? type.Name);

            if (!File.Exists(fullFileName))
            {
                return Save(defaultSetting, configPath);
            }
            using (var stream = File.OpenRead(fullFileName))
            {
                var serializer = new XmlSerializer(type);
                var settings = (T)serializer.Deserialize(stream);
                stream.Close();
                return settings;
            }
        }

        public static bool Exists<T>(string configPath = null)
        {
            return Exists(typeof(T), configPath);
        }

        public static bool Exists(Type type, string configPath = null)
        {
            SetDefaultPathIfNull(ref configPath);
            var fullFileName = GetFullFileName(configPath, type.Name);
            return File.Exists(fullFileName);
        }

        protected static string GetFullFileName(string configPath, string fileName)
        {
            return String.Format("{0}/{1}.xml", configPath, fileName);
        }

        /*private static T SaveDefaultSettings<T>(T settingsDefault = default(T)) where T : new()
        {
            var settings = Equals(settingsDefault, default(T)) ? new T() : settingsDefault;

            return Save(settings);
        }*/

        protected static T Save<T>(T settings, string configPath = null, string fileName = null)
        {
            SetDefaultPathIfNull(ref configPath);
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
            var type = typeof(T);
            var fullFileName = GetFullFileName(configPath, fileName ?? type.Name);
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

        private static void SetDefaultPathIfNull(ref string configPath)
        {
            configPath = configPath ?? ConfigPath;
        }
    }
}