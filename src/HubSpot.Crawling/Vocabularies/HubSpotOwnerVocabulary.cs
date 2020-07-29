// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotOwnerVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotOwnerVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot owner vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotOwnerVocabulary : SimpleVocabulary
    {
        public HubSpotOwnerVocabulary()
        {
            VocabularyName = "HubSpot Owner";
            KeyPrefix      = "hubspot.owner";
            KeySeparator   = ".";
            Grouping       = EntityType.Person;

            CreatedAt      = Add(new VocabularyKey("CreatedAt"));
            RemoteList     = Add(new VocabularyKey("RemoteList", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
            Type           = Add(new VocabularyKey("Type"));
            Email          = Add(new VocabularyKey("Email"));
            FirstName      = Add(new VocabularyKey("FirstName"));
            LastName       = Add(new VocabularyKey("LastName"));

            // TODO: map keys to CluedIn vocabulary
            AddMapping(Email, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Email);
            AddMapping(FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.FirstName);
            AddMapping(LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.LastName);
            
            AddMapping(CreatedAt, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
        }

        public VocabularyKey CreatedAt { get; private set; }

        public VocabularyKey RemoteList { get; private set; }

        public VocabularyKey Type { get; private set; }

        public VocabularyKey Email { get; private set; }

        public VocabularyKey FirstName { get; private set; }

        public VocabularyKey LastName { get; private set; }
    }
}
