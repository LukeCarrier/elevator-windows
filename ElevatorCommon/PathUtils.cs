using System;
using System.IO;
using System.Linq;

namespace ElevatorCommon
{
    public class PathUtils
    {
        public static string FindOnPath(string executable)
        {
            var path = System.Environment.GetEnvironmentVariable("PATH");
            var paths = path.Split(';');

            return paths.Select(x => Path.Combine(x, executable))
                   .Where(x => File.Exists(x))
                   .FirstOrDefault();
        }
    }
}
