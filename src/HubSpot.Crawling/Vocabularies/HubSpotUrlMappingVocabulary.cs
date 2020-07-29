// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotUrlMappingVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotUrlMappingVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot URL mapping vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotUrlMappingVocabulary : SimpleVocabulary
    {
        public HubSpotUrlMappingVocabulary()
        {
            VocabularyName = "HubSpot Url Mapping";
            KeyPrefix      = "hubspot.url.mapping";
            KeySeparator   = ".";
            Grouping       = EntityType.Note;

            ContentGroupId      = Add(new VocabularyKey("ContentGroupId", VocabularyKeyVisibility.Hidden));
            Created             = Add(new VocabularyKey("Created"));
            DeletedAt           = Add(new VocabularyKey("DeletedAt"));
            Destination         = Add(new VocabularyKey("Destination"));
            IsMatchFullUrl      = Add(new VocabularyKey("IsMatchFullUrl"));
            IsMatchQueryString  = Add(new VocabularyKey("IsMatchQueryString"));
            IsOnlyAfterNotFound = Add(new VocabularyKey("IsOnlyAfterNotFound"));
            IsPattern           = Add(new VocabularyKey("IsPattern"));
            IsRegex             = Add(new VocabularyKey("IsRegex"));
            PortalId            = Add(new VocabularyKey("PortalId", VocabularyKeyVisibility.Hidden));
            Precedence          = Add(new VocabularyKey("Precedence"));
            RedirectStyle       = Add(new VocabularyKey("RedirectStyle"));
            RoutePrefix         = Add(new VocabularyKey("RoutePrefix"));
            Updated             = Add(new VocabularyKey("Updated"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey ContentGroupId { get; private set; }
        public VocabularyKey Created { get; private set; }
        public VocabularyKey DeletedAt { get; private set; }
        public VocabularyKey Destination { get; private set; }
        public VocabularyKey IsMatchFullUrl { get; private set; }
        public VocabularyKey IsMatchQueryString { get; private set; }
        public VocabularyKey IsOnlyAfterNotFound { get; private set; }
        public VocabularyKey IsPattern { get; private set; }
        public VocabularyKey IsRegex { get; private set; }
        public VocabularyKey PortalId { get; private set; }
        public VocabularyKey Precedence { get; private set; }
        public VocabularyKey RedirectStyle { get; private set; }
        public VocabularyKey RoutePrefix { get; private set; }
        public VocabularyKey Updated { get; private set; }
    }
}
