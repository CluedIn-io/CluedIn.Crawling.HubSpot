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
            this.VocabularyName = "HubSpot Email";
            this.KeyPrefix      = "hubspot.email";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Mail.Task;

            this.FromEmail                     = this.Add(new VocabularyKey("FromEmail", VocabularyKeyDataType.Email));
            this.FromFirstName                 = this.Add(new VocabularyKey("FromFirstName", VocabularyKeyDataType.PersonName));
            this.FromLastName                  = this.Add(new VocabularyKey("FromLastName", VocabularyKeyDataType.PersonName));
            this.ToEmail                       = this.Add(new VocabularyKey("ToEmail", VocabularyKeyDataType.Email));
            this.Cc                            = this.Add(new VocabularyKey("Cc"));
            this.Bcc                           = this.Add(new VocabularyKey("Bcc"));
            this.Body                          = this.Add(new VocabularyKey("Body", VocabularyKeyDataType.Html));
            this.Active                        = this.Add(new VocabularyKey("Active", VocabularyKeyDataType.Boolean));
            this.HtmlBody                      = this.Add(new VocabularyKey("HtmlBody"));
            this.Text                          = this.Add(new VocabularyKey("Text"));
            this.SentVia                       = this.Add(new VocabularyKey("SentVia"));

            this.ToFirstName = this.Add(new VocabularyKey("ToFirstName"));
            this.ToLastName = this.Add(new VocabularyKey("ToLastName"));

            AddMapping(this.Body, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.Body);
            AddMapping(this.Cc, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.Cc);
            AddMapping(this.Bcc, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.Bcc);
            AddMapping(this.FromEmail, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.From);
            AddMapping(this.ToEmail, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInMail.To);
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