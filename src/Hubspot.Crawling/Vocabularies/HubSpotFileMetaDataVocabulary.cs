// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotFileMetaDataVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotFileMetaDataVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot file meta data vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotFileMetaDataVocabulary : SimpleVocabulary
    {
        public HubSpotFileMetaDataVocabulary()
        {
            this.VocabularyName = "HubSpot File Meta Data";
            this.KeyPrefix      = "hubspot.file.metadata";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Files.File;

            this.AltKey     = this.Add(new VocabularyKey("AltKey"));
            this.AltKeyHash = this.Add(new VocabularyKey("AltKeyHash", VocabularyKeyVisiblity.Hidden));
            this.AltUrl     = this.Add(new VocabularyKey("AltUrl"));
            this.Archived   = this.Add(new VocabularyKey("Archived"));
            this.DeletedAt  = this.Add(new VocabularyKey("DeletedAt"));
            this.Extension  = this.Add(new VocabularyKey("Extension"));
            this.FolderId   = this.Add(new VocabularyKey("FolderId", VocabularyKeyVisiblity.Hidden));
            this.Height     = this.Add(new VocabularyKey("Height"));
            this.IsCtaImage = this.Add(new VocabularyKey("IsCtaImage", VocabularyKeyVisiblity.Hidden));
            this.Meta       = this.Add(new VocabularyKey("Meta", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.PortalId   = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.Title      = this.Add(new VocabularyKey("Title"));
            this.Type       = this.Add(new VocabularyKey("Type"));
            this.Updated    = this.Add(new VocabularyKey("Updated"));
            this.Version    = this.Add(new VocabularyKey("Version"));
            this.Width      = this.Add(new VocabularyKey("Width"));
            this.EmbedUrl = this.Add(new VocabularyKey("EmbedUrl"));
            this.EditUrl = this.Add(new VocabularyKey("EditUrl"));

            // TODO: map keys to CluedIn vocabulary
            this.AddMapping(this.EmbedUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInFile.EmbedUrl);
            this.AddMapping(this.EditUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInFile.EditUrl);
        }

        public VocabularyKey EditUrl { get; private set; }
        public VocabularyKey EmbedUrl { get; private set; }
        public VocabularyKey AltKey { get; private set; }
        public VocabularyKey AltKeyHash { get; private set; }
        public VocabularyKey AltUrl { get; private set; }
        public VocabularyKey Archived { get; private set; }
        public VocabularyKey DeletedAt { get; private set; }
        public VocabularyKey Extension { get; private set; }
        public VocabularyKey FolderId { get; private set; }
        public VocabularyKey Height { get; private set; }
        public VocabularyKey IsCtaImage { get; private set; }
        public VocabularyKey Meta { get; private set; }
        public VocabularyKey PortalId { get; private set; }
        public VocabularyKey Title { get; private set; }
        public VocabularyKey Type { get; private set; }
        public VocabularyKey Updated { get; private set; }
        public VocabularyKey Version { get; private set; }
        public VocabularyKey Width { get; private set; }
    }
}