using ClassLibrary.Repo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class AppSettings
    {
        public static bool IsProduction { get; } = true;
        public static bool ConfirmDialogsEnabled { get; } = false;
        public static IRepo DefaultRepo { get; } = new FileRepo();
        public static bool ForceDefaultRepo { get; } = true;
        public static string SolutionPath { get; } = !IsProduction
            ? Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName
            : Environment.CurrentDirectory;

        // Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location):
        // C:\Users\user\AppData\Local\Temp\.net\WpfApp\{random key}
    }
}
