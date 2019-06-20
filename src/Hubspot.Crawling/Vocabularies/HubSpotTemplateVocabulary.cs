// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotTemplateVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotTemplateVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot template vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotTemplateVocabulary : SimpleVocabulary
    {
        public HubSpotTemplateVocabulary()
        {
            VocabularyName = "HubSpot Template";
            KeyPrefix      = "hubspot.template";
            KeySeparator   = ".";
            Grouping       = EntityType.Template;

            CategoryId               = Add(new VocabularyKey("CategoryId", VocabularyKeyVisiblity.Hidden));
            CdnMinifiedUrl           = Add(new VocabularyKey("CdnMinifiedUrl"));
            CdnUrl                   = Add(new VocabularyKey("CdnUrl"));
            DeletedAt                = Add(new VocabularyKey("DeletedAt"));
            Folder                   = Add(new VocabularyKey("Folder"));
            GeneratedFromLayoutId    = Add(new VocabularyKey("GeneratedFromLayoutId", VocabularyKeyVisiblity.Hidden));
            IsAvailableForNewContent = Add(new VocabularyKey("IsAvailableForNewContent"));
            IsFromLayout = Add(new VocabularyKey("IsFromLayout", VocabularyKeyVisiblity.Hidden));
            IsReadOnly               = Add(new VocabularyKey("IsReadOnly"));
            LinkedInStyleLayout      = Add(new VocabularyKey("LinkedInStyleLayout"));
            Path                     = Add(new VocabularyKey("Path"));
            PortalId                 = Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            TemplateType             = Add(new VocabularyKey("TemplateType"));
            ThumbnailWidth           = Add(new VocabularyKey("ThumbnailWidth"));
            Type                     = Add(new VocabularyKey("Type"));
            UpdatedBy                = Add(new VocabularyKey("UpdatedBy"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey CategoryId { get; private set; }
        public VocabularyKey CdnMinifiedUrl { get; private set; }
        public VocabularyKey CdnUrl { get; private set; }
        public VocabularyKey DeletedAt { get; private set; }
        public VocabularyKey Folder { get; private set; }
        public VocabularyKey GeneratedFromLayoutId { get; private set; }
        public VocabularyKey IsAvailableForNewContent { get; private set; }
        public VocabularyKey IsFromLayout { get; private set; }
        public VocabularyKey IsReadOnly { get; private set; }
        public VocabularyKey LinkedInStyleLayout { get; private set; }
        public VocabularyKey Path { get; private set; }
        public VocabularyKey PortalId { get; private set; }
        public VocabularyKey TemplateType { get; private set; }
        public VocabularyKey ThumbnailWidth { get; private set; }
        public VocabularyKey Type { get; private set; }
        public VocabularyKey UpdatedBy { get; private set; }
    }
}
