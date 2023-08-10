using System.Diagnostics;
using System.Reflection;

namespace ClassLibrary
{
    public static class Settings
    {
        private const char delimiter = ',';
        private const string settingsFileName = "settings.txt";
        public const bool ConfirmDialogsEnabled = false;

        public static readonly string solutionFolderPath =
            Environment.GetEnvironmentVariable("APP_ENV") == "Development"
                ? Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName
                : Environment.CurrentDirectory;

        // Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location):
        // C:\Users\user\AppData\Local\Temp\.net\WpfApp\{random key}

        public static readonly string playerImagesPath = solutionFolderPath + "/PlayerImages/";

        public static string ChampionshipPath { get; set; } = "";

        public static bool SettingsExist(string fileName = settingsFileName, int length = 0)
        {
            return File.Exists($"{solutionFolderPath}/{fileName}")
                && LoadSettings(fileName).Length >= length;
        }

        public static string[] LoadSettings(string fileName = settingsFileName)
        {
            var s = File.ReadLines($"{solutionFolderPath}/{fileName}");
            return !s.Any() ? Array.Empty<string>() : s.First().Split(delimiter);
        }

        public static void SaveSettings(string fileName = settingsFileName, params string[] settings)
        {
            File.WriteAllText($"{solutionFolderPath}/{fileName}", string.Join(delimiter, settings));
        }

        public static string GetPlayerImagePath(string playerName)
        {
            if (!Directory.Exists(playerImagesPath)) return "";
            var files = Directory.GetFiles(playerImagesPath, playerName + ".*");
            return files.Length > 0 ? files[0] : "";
        }
    }
}
