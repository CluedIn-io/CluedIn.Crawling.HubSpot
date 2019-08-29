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
            VocabularyName = "HubSpot Channel";
            KeyPrefix      = "hubspot.channel";
            KeySeparator   = ".";
            Grouping       = EntityType.Channel;

            AddGroup("Hubspot Channel Details", group =>
            {
                AccountId = group.Add(new VocabularyKey("AccountId", VocabularyKeyVisiblity.Hidden));
                ChannelId = group.Add(new VocabularyKey("ChannelId", VocabularyKeyVisiblity.Hidden));
                DataMap   = group.Add(new VocabularyKey("DataMap", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                Type      = group.Add(new VocabularyKey("Type"));
                Email = group.Add(new VocabularyKey("Email"));
                FirstName = group.Add(new VocabularyKey("FirstName"));
                FullName = group.Add(new VocabularyKey("FullName"));
                LastName = group.Add(new VocabularyKey("LastName"));
                PageCategory = group.Add(new VocabularyKey("PageCategory"));
                PageId = group.Add(new VocabularyKey("PageId", VocabularyKeyVisiblity.Hidden));
                PageName = group.Add(new VocabularyKey("PageName"));
                Picture = group.Add(new VocabularyKey("Picture"));
                ProfileUrl = group.Add(new VocabularyKey("ProfileUrl"));
                UserId = group.Add(new VocabularyKey("UserId", VocabularyKeyVisiblity.Hidden));
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
