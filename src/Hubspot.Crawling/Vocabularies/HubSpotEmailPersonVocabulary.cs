// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotEmailPersonVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotEmailPersonVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot email person vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotEmailPersonVocabulary : SimpleVocabulary
    {
        public HubSpotEmailPersonVocabulary()
        {
            this.VocabularyName = "HubSpot EmailPerson";
            this.KeyPrefix      = "hubspot.emailperson";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Person;

            this.Email     = this.Add(new VocabularyKey("Email", VocabularyKeyDataType.Email));
            this.FirstName = this.Add(new VocabularyKey("FirstName", VocabularyKeyDataType.PersonName));
            this.LastName  = this.Add(new VocabularyKey("LastName", VocabularyKeyDataType.PersonName));

            this.AddMapping(this.Email, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email);
            this.AddMapping(this.FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.FirstName);
            this.AddMapping(this.LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.LastName);
        }

        public VocabularyKey Email { get; private set; }

        public VocabularyKey FirstName { get; private set; }

        public VocabularyKey LastName { get; private set; }
    }
}
