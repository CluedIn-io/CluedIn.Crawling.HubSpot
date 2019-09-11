// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotSmtpTokenVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotSmtpTokenVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot SMTP token vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotSmtpTokenVocabulary : SimpleVocabulary
    {
        public HubSpotSmtpTokenVocabulary()
        {
            VocabularyName = "HubSpot Token";
            KeyPrefix      = "hubspot.token";
            KeySeparator   = ".";
            Grouping       = EntityType.Activity;

            AppId           = Add(new VocabularyKey("AppId", VocabularyKeyVisibility.Hidden));
            CampaignName    = Add(new VocabularyKey("CampaignName"));
            CreatedAt       = Add(new VocabularyKey("CreatedAt"));
            CreatedBy       = Add(new VocabularyKey("CreatedBy"));
            Deleted         = Add(new VocabularyKey("Deleted"));
            EmailCampaignId = Add(new VocabularyKey("EmailCampaignId", VocabularyKeyVisibility.Hidden));
            PortalId        = Add(new VocabularyKey("PortalId", VocabularyKeyVisibility.Hidden));
            UserName        = Add(new VocabularyKey("UserName"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey AppId { get; private set; }

        public VocabularyKey CampaignName { get; private set; }

        public VocabularyKey CreatedAt { get; private set; }

        public VocabularyKey CreatedBy { get; private set; }

        public VocabularyKey Deleted { get; private set; }

        public VocabularyKey EmailCampaignId { get; private set; }

        public VocabularyKey PortalId { get; private set; }

        public VocabularyKey UserName { get; private set; }
    }
}
