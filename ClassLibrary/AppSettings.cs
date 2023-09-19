using ClassLibrary.Repo;

namespace ClassLibrary
{
    public static class AppSettings
    {
        public static bool IsProduction { get; } = true;
        public static bool ConfirmDialogsEnabled { get; } = false;
        public static string DefaultRepo { get; } = "FileRepo";
        public static bool ForceDefaultRepo { get; } = true;
        public static string SolutionPath { get; } = !IsProduction
            ? Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName
            : Environment.CurrentDirectory;

        /* Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location):
         * C:\Users\user\AppData\Local\Temp\.net\WpfApp\{random key} */

        /* Svi modeli u /Models generirani su s https://app.quicktype.io/csharp s minimalnim promjenama */
    }
}
