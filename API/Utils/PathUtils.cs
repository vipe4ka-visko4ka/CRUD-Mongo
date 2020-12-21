using System.IO;

namespace API.Utils
{
    public static class PathUtils
    {
        public static string SeedsDataPath(string path) => Path.Combine("Data", "Seeds", "Data", path);
    }
}
