// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotCampaignEventVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotCampaignEventVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot campaign event vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotCampaignEventVocabulary : SimpleVocabulary
    {
        public HubSpotCampaignEventVocabulary()
        {
            VocabularyName = "HubSpot Campaign Event";
            KeyPrefix      = "hubspot.campaign.event";
            KeySeparator   = ".";
            Grouping       = EntityType.Calendar.Event;

            AddGroup("Hubspot Campaign Event Details", group =>
            {
                AppName         = group.Add(new VocabularyKey("AppName"));
                Browser         = group.Add(new VocabularyKey("Browser", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                EmailCampaignId = group.Add(new VocabularyKey("EmailCampaignId", VocabularyKeyVisiblity.Hidden));
                Hmid            = group.Add(new VocabularyKey("Hmid", VocabularyKeyVisiblity.Hidden));
                IpAddress = group.Add(new VocabularyKey("IpAddress", VocabularyKeyVisiblity.Hidden));
                Location        = group.Add(new VocabularyKey("Location", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                Recipient       = group.Add(new VocabularyKey("Recipient"));
                Type            = group.Add(new VocabularyKey("Type"));
                UserAgent = group.Add(new VocabularyKey("UserAgent", VocabularyKeyVisiblity.Hidden));

                City = group.Add(new VocabularyKey("City", VocabularyKeyDataType.GeographyCity));
                Country = group.Add(new VocabularyKey("Country", VocabularyKeyDataType.GeographyCountry));
                State = group.Add(new VocabularyKey("State", VocabularyKeyDataType.GeographyLocation));
            });

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey AppName { get; private set; }
        public VocabularyKey Browser { get; private set; }
        public VocabularyKey City { get; internal set; }
        public VocabularyKey Country { get; internal set; }
        public VocabularyKey EmailCampaignId { get; private set; }
        public VocabularyKey Hmid { get; private set; }
        public VocabularyKey IpAddress { get; private set; }
        public VocabularyKey Location { get; private set; }
        public VocabularyKey Recipient { get; private set; }
        public VocabularyKey State { get; internal set; }
        public VocabularyKey Type { get; private set; }
        public VocabularyKey UserAgent { get; private set; }

    }
}
