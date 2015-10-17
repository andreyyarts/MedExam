using System.IO;

namespace MedExam.Common.Local
{
    public class CacheViewsRepository : LocalSettingsRepository
    {
        private const string ConfigPath = "Cache/Views";

        public static ViewSetting Load(string fileName)
        {
            return LoadSetting<ViewSetting>(ConfigPath, fileName);
        }

        public static void Save(ViewSetting settings, string fileName)
        {
            LocalSettingsRepository.Save(settings, ConfigPath, fileName);
        }

        public static bool Exists(string fileName)
        {
            var fullFileName = GetFullFileName(ConfigPath, fileName);
            return File.Exists(fullFileName);
        }
    }
}