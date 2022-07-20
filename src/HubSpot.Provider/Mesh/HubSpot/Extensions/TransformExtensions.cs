using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Mesh;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions
{
    public static class TransformExtensions
    {
        private const string VocabPrefix = "hubspot";

        public static bool HasValidVocabularyKey(this List<Transform> transforms)
        {
            return transforms?.Any(transform => transform.key.StartsWith(VocabPrefix, StringComparison.OrdinalIgnoreCase)) ?? false;
        }
    }
}
