namespace ClassLibrary
{
    public static class Settings
    {
        private const char delimiter = ',';

        public static readonly string SolutionFolderPath =
            Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName;

        public static string ChampionshipPath { get; set; } = "";

        public static bool SettingsExist(string fileName)
        {
            return File.Exists(SolutionFolderPath + "/" + fileName);
        }

        public static string[] LoadSettings(string fileName)
        {
            var s = File.ReadLines(SolutionFolderPath + "/" + fileName);
            return !s.Any() ? Array.Empty<string>() : s.First().Split(delimiter);
        }

        public static void SaveSettings(string fileName, params string[] settings)
        {
            File.WriteAllText(SolutionFolderPath + "/" + fileName,
                string.Join(delimiter, settings));
        }
    }
}
