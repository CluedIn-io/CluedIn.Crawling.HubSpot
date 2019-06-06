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
            this.VocabularyName = "HubSpot Call";
            this.KeyPrefix      = "hubspot.call";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.PhoneCall;

            this.FromNumber         = this.Add(new VocabularyKey("FromNumber", VocabularyKeyDataType.PhoneNumber));
            this.ToNumber           = this.Add(new VocabularyKey("ToNumber", VocabularyKeyDataType.PhoneNumber));
            this.Duration           = this.Add(new VocabularyKey("Duration", VocabularyKeyDataType.Time));
            this.Status             = this.Add(new VocabularyKey("Status", VocabularyKeyDataType.Text));
            this.Body               = this.Add(new VocabularyKey("Notes", VocabularyKeyDataType.Text));
            this.Disposition        = this.Add(new VocabularyKey("Disposition", VocabularyKeyVisiblity.HiddenInFrontendUI));
            this.ExternalAccountId  = this.Add(new VocabularyKey("ExternalAccountId", VocabularyKeyDataType.DateTime, VocabularyKeyVisiblity.HiddenInFrontendUI));
            this.ExternalId         = this.Add(new VocabularyKey("ExternalId", VocabularyKeyDataType.DateTime, VocabularyKeyVisiblity.HiddenInFrontendUI));
            this.RecordingUrl       = this.Add(new VocabularyKey("RecordingUrl", VocabularyKeyDataType.Uri));

            AddMapping(this.Duration, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInAudio.Duration);
         
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