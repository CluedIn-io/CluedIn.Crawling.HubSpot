// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotCampaignVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotCampaignVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot campaign vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotCampaignVocabulary : SimpleVocabulary
    {
        public HubSpotCampaignVocabulary()
        {
            this.VocabularyName = "HubSpot Campaign";
            this.KeyPrefix      = "hubspot.campaign";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Marketing.Campaign;

            this.AddGroup("Hubspot Campaign Details", group =>
            {
                this.AppName     = group.Add(new VocabularyKey("AppName"));
                this.ContentId   = group.Add(new VocabularyKey("ContentId", VocabularyKeyVisiblity.Hidden));
                this.Counters    = group.Add(new VocabularyKey("Counters", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.NumIncluded = group.Add(new VocabularyKey("NumIncluded", VocabularyKeyDataType.Number));
                this.NumQueued   = group.Add(new VocabularyKey("NumQueued", VocabularyKeyDataType.Number));
                this.SubType     = group.Add(new VocabularyKey("SubType"));
                this.Type        = group.Add(new VocabularyKey("Type"));
                this.Delivered = group.Add(new VocabularyKey("Delivered"));
                this.Open = group.Add(new VocabularyKey("Open"));
                this.Processed = group.Add(new VocabularyKey("Processed"));
                this.Sent = group.Add(new VocabularyKey("Sent"));
            });
            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey AppName { get; private set; }
        public VocabularyKey ContentId { get; private set; }
        public VocabularyKey Counters { get; private set; }
        public VocabularyKey Delivered { get; internal set; }
        public VocabularyKey NumIncluded { get; private set; }
        public VocabularyKey NumQueued { get; private set; }
        public VocabularyKey Open { get; internal set; }
        public VocabularyKey Processed { get; internal set; }
        public VocabularyKey Sent { get; internal set; }
        public VocabularyKey SubType { get; private set; }
        public VocabularyKey Type { get; private set; }

    }
}
