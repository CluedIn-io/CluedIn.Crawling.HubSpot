// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotEmailVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotEmailVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot email vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotEmailVocabulary : SimpleVocabulary
    {
        public HubSpotEmailVocabulary()
        {
            VocabularyName = "HubSpot Email";
            KeyPrefix      = "hubspot.email";
            KeySeparator   = ".";
            Grouping       = EntityType.Mail.Task;

            FromEmail                     = Add(new VocabularyKey("FromEmail", VocabularyKeyDataType.Email));
            FromFirstName                 = Add(new VocabularyKey("FromFirstName", VocabularyKeyDataType.PersonName));
            FromLastName                  = Add(new VocabularyKey("FromLastName", VocabularyKeyDataType.PersonName));
            ToEmail                       = Add(new VocabularyKey("ToEmail", VocabularyKeyDataType.Email));
            Cc                            = Add(new VocabularyKey("Cc"));
            Bcc                           = Add(new VocabularyKey("Bcc"));
            Body                          = Add(new VocabularyKey("Body", VocabularyKeyDataType.Html));
            Active                        = Add(new VocabularyKey("Active", VocabularyKeyDataType.Boolean));
            HtmlBody                      = Add(new VocabularyKey("HtmlBody"));
            Text                          = Add(new VocabularyKey("Text"));
            SentVia                       = Add(new VocabularyKey("SentVia"));

            ToFirstName = Add(new VocabularyKey("ToFirstName"));
            ToLastName = Add(new VocabularyKey("ToLastName"));

            AddMapping(Body, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.Body);
            AddMapping(Cc, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.Cc);
            AddMapping(Bcc, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.Bcc);
            AddMapping(FromEmail, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.From);
            AddMapping(ToEmail, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.To);
        }

        public VocabularyKey Body { get; private set; }
        public VocabularyKey Cc { get; private set; }
        public VocabularyKey Bcc { get; private set; }
        public VocabularyKey Active { get; private set; }
        public VocabularyKey FromEmail { get; private set; }
        public VocabularyKey ToEmail { get; private set; }
        public VocabularyKey FromFirstName { get; private set; }
        public VocabularyKey FromLastName { get; private set; }
        public VocabularyKey ToFirstName { get; private set; }
        public VocabularyKey ToLastName { get; private set; }
        public VocabularyKey SentVia { get; private set; }
        public VocabularyKey HtmlBody { get; private set; }
        public VocabularyKey Text { get; private set; }

    }
}
