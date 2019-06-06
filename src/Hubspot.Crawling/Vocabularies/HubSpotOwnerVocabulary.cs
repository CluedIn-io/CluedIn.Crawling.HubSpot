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
            this.VocabularyName = "HubSpot Owner";
            this.KeyPrefix      = "hubspot.owner";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Person;

            this.CreatedAt      = this.Add(new VocabularyKey("CreatedAt"));
            this.RemoteList     = this.Add(new VocabularyKey("RemoteList", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.Type           = this.Add(new VocabularyKey("Type"));
            this.Email          = this.Add(new VocabularyKey("Email"));
            this.FirstName      = this.Add(new VocabularyKey("FirstName"));
            this.LastName       = this.Add(new VocabularyKey("LastName"));

            // TODO: map keys to CluedIn vocabulary
            this.AddMapping(this.Email, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Email);
            this.AddMapping(this.FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.FirstName);
            this.AddMapping(this.LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.LastName);
            
            this.AddMapping(this.CreatedAt, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
        }

        public VocabularyKey CreatedAt { get; private set; }

        public VocabularyKey RemoteList { get; private set; }

        public VocabularyKey Type { get; private set; }

        public VocabularyKey Email { get; private set; }

        public VocabularyKey FirstName { get; private set; }

        public VocabularyKey LastName { get; private set; }
    }
}
