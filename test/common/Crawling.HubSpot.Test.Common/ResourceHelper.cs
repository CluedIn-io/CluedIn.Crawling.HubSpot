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
            Assembly a = Assembly.GetCallingAssembly();

            var @namespace = a.FullName.Split(',')[0];
            Stream s = a.GetManifestResourceStream(@namespace + "." + name);

            return s;
        }
    }
}
