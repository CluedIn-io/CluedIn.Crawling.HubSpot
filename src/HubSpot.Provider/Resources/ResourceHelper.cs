using System;
using System.IO;
using System.Reflection;
using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Resources
{
    public static class ResourceHelper
    {
        public static Stream GetFile(string name, Assembly assembly = null)
        {
            var a = assembly ?? Assembly.GetExecutingAssembly();

            var @namespace = a.FullName.Split(',')[0];
            var s = a.GetManifestResourceStream(@namespace + "." + name);

            if (s == null)
            {
                throw new NotFoundException($"Could not find resource " + name);
            }

            return s;
        }

        public static string GetFileAsBase64(string name)
        {
            var stream = GetFile(name);
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            var base64 = Convert.ToBase64String(bytes);

            return base64;
        }
    }
}
