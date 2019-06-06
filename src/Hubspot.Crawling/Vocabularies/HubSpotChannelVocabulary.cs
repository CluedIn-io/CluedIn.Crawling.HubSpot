// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotChannelVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotChannelVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot channel vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotChannelVocabulary : SimpleVocabulary
    {
        public HubSpotChannelVocabulary()
        {
            this.VocabularyName = "HubSpot Channel";
            this.KeyPrefix      = "hubspot.channel";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Channel;

            this.AddGroup("Hubspot Channel Details", group =>
            {
                this.AccountId = group.Add(new VocabularyKey("AccountId", VocabularyKeyVisiblity.Hidden));
                this.ChannelId = group.Add(new VocabularyKey("ChannelId", VocabularyKeyVisiblity.Hidden));
                this.DataMap   = group.Add(new VocabularyKey("DataMap", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.Type      = group.Add(new VocabularyKey("Type"));
                this.Email = group.Add(new VocabularyKey("Email"));
                this.FirstName = group.Add(new VocabularyKey("FirstName"));
                this.FullName = group.Add(new VocabularyKey("FullName"));
                this.LastName = group.Add(new VocabularyKey("LastName"));
                this.PageCategory = group.Add(new VocabularyKey("PageCategory"));
                this.PageId = group.Add(new VocabularyKey("PageId", VocabularyKeyVisiblity.Hidden));
                this.PageName = group.Add(new VocabularyKey("PageName"));
                this.Picture = group.Add(new VocabularyKey("Picture"));
                this.ProfileUrl = group.Add(new VocabularyKey("ProfileUrl"));
                this.UserId = group.Add(new VocabularyKey("UserId", VocabularyKeyVisiblity.Hidden));
            });

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey AccountId { get; private set; }

        public VocabularyKey ChannelId { get; private set; }

        public VocabularyKey DataMap { get; private set; }
        public VocabularyKey Email { get; internal set; }
        public VocabularyKey FirstName { get; internal set; }
        public VocabularyKey FullName { get; internal set; }
        public VocabularyKey LastName { get; internal set; }
        public VocabularyKey PageCategory { get; internal set; }
        public VocabularyKey PageId { get; internal set; }
        public VocabularyKey PageName { get; internal set; }
        public VocabularyKey Picture { get; internal set; }
        public VocabularyKey ProfileUrl { get; internal set; }
        public VocabularyKey Type { get; private set; }
        public VocabularyKey UserId { get; internal set; }
    }
}
