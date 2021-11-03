using System.IO;
using System.Linq;

namespace DotaStat.Data.EntityFramework
{
    public static class DirectoryManager
    {
        public static string GetSolutionRoot(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory?.FullName;
        }

        public static string GetSolutionRootDefault()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        }
    }
}
