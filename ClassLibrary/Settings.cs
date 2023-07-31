using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary
{
    public static class Settings
    {
        private const char delimiter = ',';

        public static readonly string SolutionFolderPath =
            Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName;

        public static string GenderPath { get; set; } = "";

        public static bool SettingsExist(string fileName)
        {
            return File.Exists(SolutionFolderPath + "/" + fileName);
        }

        public static string[] LoadSettings(string fileName)
        {
            string s = File.ReadLines(SolutionFolderPath + "/" + fileName).First();
            return s.Split(delimiter);
        }

        public static void SaveSettings(string fileName, params string[] settings)
        {
            File.WriteAllText(SolutionFolderPath + "/" + fileName,
                string.Join(delimiter, settings));
        }
    }
}
