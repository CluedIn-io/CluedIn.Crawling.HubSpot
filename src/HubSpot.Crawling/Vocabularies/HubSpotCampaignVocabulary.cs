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
            VocabularyName = "HubSpot Campaign";
            KeyPrefix      = "hubspot.campaign";
            KeySeparator   = ".";
            Grouping       = EntityType.Marketing.Campaign;

            AddGroup("Hubspot Campaign Details", group =>
            {
                AppName     = group.Add(new VocabularyKey("AppName"));
                ContentId   = group.Add(new VocabularyKey("ContentId", VocabularyKeyVisiblity.Hidden));
                Counters    = group.Add(new VocabularyKey("Counters", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                NumIncluded = group.Add(new VocabularyKey("NumIncluded", VocabularyKeyDataType.Number));
                NumQueued   = group.Add(new VocabularyKey("NumQueued", VocabularyKeyDataType.Number));
                SubType     = group.Add(new VocabularyKey("SubType"));
                Type        = group.Add(new VocabularyKey("Type"));
                Delivered = group.Add(new VocabularyKey("Delivered"));
                Open = group.Add(new VocabularyKey("Open"));
                Processed = group.Add(new VocabularyKey("Processed"));
                Sent = group.Add(new VocabularyKey("Sent"));
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
