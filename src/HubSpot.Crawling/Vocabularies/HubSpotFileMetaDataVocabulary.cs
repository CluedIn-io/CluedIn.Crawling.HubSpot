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
            VocabularyName = "HubSpot File Meta Data";
            KeyPrefix      = "hubspot.file.metadata";
            KeySeparator   = ".";
            Grouping       = EntityType.Files.File;

            AltKey     = Add(new VocabularyKey("AltKey"));
            AltKeyHash = Add(new VocabularyKey("AltKeyHash", VocabularyKeyVisibility.Hidden));
            AltUrl     = Add(new VocabularyKey("AltUrl"));
            Archived   = Add(new VocabularyKey("Archived"));
            DeletedAt  = Add(new VocabularyKey("DeletedAt"));
            Extension  = Add(new VocabularyKey("Extension"));
            FolderId   = Add(new VocabularyKey("FolderId", VocabularyKeyVisibility.Hidden));
            Height     = Add(new VocabularyKey("Height"));
            IsCtaImage = Add(new VocabularyKey("IsCtaImage", VocabularyKeyVisibility.Hidden));
            Meta       = Add(new VocabularyKey("Meta", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
            PortalId   = Add(new VocabularyKey("PortalId", VocabularyKeyVisibility.Hidden));
            Title      = Add(new VocabularyKey("Title"));
            Type       = Add(new VocabularyKey("Type"));
            Updated    = Add(new VocabularyKey("Updated"));
            Version    = Add(new VocabularyKey("Version"));
            Width      = Add(new VocabularyKey("Width"));
            EmbedUrl = Add(new VocabularyKey("EmbedUrl"));
            EditUrl = Add(new VocabularyKey("EditUrl"));

            // TODO: map keys to CluedIn vocabulary
            AddMapping(EmbedUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInFile.EmbedUrl);
            AddMapping(EditUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInFile.EditUrl);
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
