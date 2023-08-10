using System.Diagnostics;
using System.Reflection;

namespace ClassLibrary
{
    public static class UserSettings
    {
        private const char delimiter = ',';
        private const string defaultSettings = "settings.txt";

        public static string PlayerImagesPath { get; } = "/PlayerImages/";

        public static string ChampionshipPath { get; set; } = null!;

        public static bool SettingsExist(string fileName = defaultSettings, int length = 0)
        {
            return File.Exists($"{AppSettings.SolutionPath}/{fileName}")
                && LoadSettings(fileName).Length >= length;
        }

        public static string[] LoadSettings(string fileName = defaultSettings)
        {
            var s = File.ReadLines($"{AppSettings.SolutionPath}/{fileName}");
            return !s.Any() ? Array.Empty<string>() : s.First().Split(delimiter);
        }

        public static void SaveSettings(string fileName = defaultSettings, params string[] settings)
        {
            File.WriteAllText($"{AppSettings.SolutionPath}/{fileName}", string.Join(delimiter, settings));
        }

        public static string GetPlayerImagePath(string playerName)
        {
            var path = AppSettings.SolutionPath + PlayerImagesPath;
            if (!Directory.Exists(path)) return "";
            var files = Directory.GetFiles(path, playerName + ".*");
            return files.Length > 0 ? files[0] : "";
        }
    }
}
