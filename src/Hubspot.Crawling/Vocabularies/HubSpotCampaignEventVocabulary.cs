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
            this.VocabularyName = "HubSpot Campaign Event";
            this.KeyPrefix      = "hubspot.campaign.event";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Calendar.Event;

            this.AddGroup("Hubspot Campaign Event Details", group =>
            {
                this.AppName         = group.Add(new VocabularyKey("AppName"));
                this.Browser         = group.Add(new VocabularyKey("Browser", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.EmailCampaignId = group.Add(new VocabularyKey("EmailCampaignId", VocabularyKeyVisiblity.Hidden));
                this.Hmid            = group.Add(new VocabularyKey("Hmid", VocabularyKeyVisiblity.Hidden));
                this.IpAddress = group.Add(new VocabularyKey("IpAddress", VocabularyKeyVisiblity.Hidden));
                this.Location        = group.Add(new VocabularyKey("Location", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.Recipient       = group.Add(new VocabularyKey("Recipient"));
                this.Type            = group.Add(new VocabularyKey("Type"));
                this.UserAgent = group.Add(new VocabularyKey("UserAgent", VocabularyKeyVisiblity.Hidden));

                this.City = group.Add(new VocabularyKey("City", VocabularyKeyDataType.GeographyCity));
                this.Country = group.Add(new VocabularyKey("Country", VocabularyKeyDataType.GeographyCountry));
                this.State = group.Add(new VocabularyKey("State", VocabularyKeyDataType.GeographyLocation));
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
