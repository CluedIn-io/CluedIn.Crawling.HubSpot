using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Crawling.HubSpot.Test.Common
{
    public static class ResourceHelper
    {
        public static Stream GetFile(string name)
        {
            Assembly a = Assembly.GetExecutingAssembly();

            var @namespace = a.FullName.Split(',')[0];
            Stream s = a.GetManifestResourceStream(@namespace + "." + name);

            return s;
        }

        public static IEnumerable<string> GetFilenames(string partialPath)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            var @namespace = a.FullName.Split(',')[0] + ".";
            var files = a.GetManifestResourceNames().Where(n => n.StartsWith(partialPath))
                .Select(n => n.Replace(@namespace, "")).AsEnumerable();

            return files;
        }
    }
}
