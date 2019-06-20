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
            VocabularyName = "HubSpot EmailPerson";
            KeyPrefix      = "hubspot.emailperson";
            KeySeparator   = ".";
            Grouping       = EntityType.Person;

            Email     = Add(new VocabularyKey("Email", VocabularyKeyDataType.Email));
            FirstName = Add(new VocabularyKey("FirstName", VocabularyKeyDataType.PersonName));
            LastName  = Add(new VocabularyKey("LastName", VocabularyKeyDataType.PersonName));

            AddMapping(Email, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email);
            AddMapping(FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.FirstName);
            AddMapping(LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.LastName);
        }

        public VocabularyKey Email { get; private set; }

        public VocabularyKey FirstName { get; private set; }

        public VocabularyKey LastName { get; private set; }
    }
}
