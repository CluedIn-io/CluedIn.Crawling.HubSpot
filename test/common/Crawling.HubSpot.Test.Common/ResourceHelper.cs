using System.IO;
using System.Reflection;

namespace Crawling.HubSpot.Test.Common
{
    public static class ResourceHelper
    {
        public static Stream GetFile(string name)
        {
            var a = Assembly.GetCallingAssembly();

            var @namespace = a.FullName.Split(',')[0];
            var s = a.GetManifestResourceStream(@namespace + "." + name);

            return s;
        }
    }
}
