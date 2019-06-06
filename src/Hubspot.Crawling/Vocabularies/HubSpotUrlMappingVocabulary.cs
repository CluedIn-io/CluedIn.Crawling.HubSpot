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
            this.VocabularyName = "HubSpot Url Mapping";
            this.KeyPrefix      = "hubspot.url.mapping";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Note;

            this.ContentGroupId      = this.Add(new VocabularyKey("ContentGroupId", VocabularyKeyVisiblity.Hidden));
            this.Created             = this.Add(new VocabularyKey("Created"));
            this.DeletedAt           = this.Add(new VocabularyKey("DeletedAt"));
            this.Destination         = this.Add(new VocabularyKey("Destination"));
            this.IsMatchFullUrl      = this.Add(new VocabularyKey("IsMatchFullUrl"));
            this.IsMatchQueryString  = this.Add(new VocabularyKey("IsMatchQueryString"));
            this.IsOnlyAfterNotFound = this.Add(new VocabularyKey("IsOnlyAfterNotFound"));
            this.IsPattern           = this.Add(new VocabularyKey("IsPattern"));
            this.IsRegex             = this.Add(new VocabularyKey("IsRegex"));
            this.PortalId            = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.Precedence          = this.Add(new VocabularyKey("Precedence"));
            this.RedirectStyle       = this.Add(new VocabularyKey("RedirectStyle"));
            this.RoutePrefix         = this.Add(new VocabularyKey("RoutePrefix"));
            this.Updated             = this.Add(new VocabularyKey("Updated"));

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