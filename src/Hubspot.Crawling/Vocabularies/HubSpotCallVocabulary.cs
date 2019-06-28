// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotCallVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotCallVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot call vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotCallVocabulary : SimpleVocabulary
    {
        public HubSpotCallVocabulary()
        {
            VocabularyName = "HubSpot Call";
            KeyPrefix      = "hubspot.call";
            KeySeparator   = ".";
            Grouping       = EntityType.PhoneCall;

            FromNumber         = Add(new VocabularyKey("FromNumber", VocabularyKeyDataType.PhoneNumber));
            ToNumber           = Add(new VocabularyKey("ToNumber", VocabularyKeyDataType.PhoneNumber));
            Duration           = Add(new VocabularyKey("Duration", VocabularyKeyDataType.Time));
            Status             = Add(new VocabularyKey("Status", VocabularyKeyDataType.Text));
            Body               = Add(new VocabularyKey("Notes", VocabularyKeyDataType.Text));
            Disposition        = Add(new VocabularyKey("Disposition", VocabularyKeyVisiblity.HiddenInFrontendUI));
            ExternalAccountId  = Add(new VocabularyKey("ExternalAccountId", VocabularyKeyDataType.DateTime, VocabularyKeyVisiblity.HiddenInFrontendUI));
            ExternalId         = Add(new VocabularyKey("ExternalId", VocabularyKeyDataType.DateTime, VocabularyKeyVisiblity.HiddenInFrontendUI));
            RecordingUrl       = Add(new VocabularyKey("RecordingUrl", VocabularyKeyDataType.Uri));

            AddMapping(Duration, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInAudio.Duration);
         
        }

        public VocabularyKey ToNumber { get; private set; }

        public VocabularyKey FromNumber { get; private set; }

        public VocabularyKey Status { get; private set; }

        public VocabularyKey ExternalId { get; private set; }

        public VocabularyKey ExternalAccountId { get; private set; }

        public VocabularyKey RecordingUrl { get; private set; }

        public VocabularyKey Body { get; private set; }

        public VocabularyKey Disposition { get; private set; }

        public VocabularyKey Duration { get; private set; }
    }
}
