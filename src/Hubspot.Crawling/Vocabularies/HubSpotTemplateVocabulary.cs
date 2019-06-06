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
            this.VocabularyName = "HubSpot Template";
            this.KeyPrefix      = "hubspot.template";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Template;

            this.CategoryId               = this.Add(new VocabularyKey("CategoryId", VocabularyKeyVisiblity.Hidden));
            this.CdnMinifiedUrl           = this.Add(new VocabularyKey("CdnMinifiedUrl"));
            this.CdnUrl                   = this.Add(new VocabularyKey("CdnUrl"));
            this.DeletedAt                = this.Add(new VocabularyKey("DeletedAt"));
            this.Folder                   = this.Add(new VocabularyKey("Folder"));
            this.GeneratedFromLayoutId    = this.Add(new VocabularyKey("GeneratedFromLayoutId", VocabularyKeyVisiblity.Hidden));
            this.IsAvailableForNewContent = this.Add(new VocabularyKey("IsAvailableForNewContent"));
            this.IsFromLayout = this.Add(new VocabularyKey("IsFromLayout", VocabularyKeyVisiblity.Hidden));
            this.IsReadOnly               = this.Add(new VocabularyKey("IsReadOnly"));
            this.LinkedInStyleLayout      = this.Add(new VocabularyKey("LinkedInStyleLayout"));
            this.Path                     = this.Add(new VocabularyKey("Path"));
            this.PortalId                 = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.TemplateType             = this.Add(new VocabularyKey("TemplateType"));
            this.ThumbnailWidth           = this.Add(new VocabularyKey("ThumbnailWidth"));
            this.Type                     = this.Add(new VocabularyKey("Type"));
            this.UpdatedBy                = this.Add(new VocabularyKey("UpdatedBy"));

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