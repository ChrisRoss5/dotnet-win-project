using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class Paths
    {
        public static readonly string SolutionFolderPath = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), @"..\"));

        // path generator
        public static string GetPath(string folderName, string fileName)
        {
            return Path.Combine(SolutionFolderPath, folderName, fileName);
        }
    }
}
