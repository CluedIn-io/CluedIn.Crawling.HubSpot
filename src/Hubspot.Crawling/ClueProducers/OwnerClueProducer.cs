using System;
using System.Collections.Generic;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class OwnerClueProducer : BaseClueProducer<Owner>
    {
        private readonly IClueFactory _factory;

        public OwnerClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Owner input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, input.ownerId.ToString(), accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;

            if (input.firstName != null && input.lastName != null)
                data.Name = $"{input.firstName} {input.lastName}";

            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.createdAt);
            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updatedAt);

            if (input.ownerId != null)
                data.Codes.Add(new EntityCode(EntityType.Person, HubSpotNameConstants.CodeOrigin, input.ownerId.ToString()));

            if (input.email != null)
                data.Codes.Add(new EntityCode(EntityType.Person, HubSpotNameConstants.CodeOrigin, input.email));

            if (input.remoteList != null)
            {
                var remoteList = JsonUtility.Deserialize<JArray>(JsonUtility.Serialize(input.remoteList));
                JsonUtility.Deserialize<Dictionary<string, object>>(remoteList.First.ToString()).TryGetValue("remoteId", out var remoteId);

                if (remoteId != null)
                {
                    data.Codes.Add(new EntityCode(EntityType.Person, HubSpotNameConstants.CodeOrigin, remoteId.ToString()));
                }
            }

            if (input.portalId != null)
                data.Uri = new Uri($"https://app.hubspot.com/user-preferences/{input.portalId}/");

            data.Properties[HubSpotVocabulary.Owner.Email] = input.email;
            data.Properties[HubSpotVocabulary.Owner.FirstName] = input.firstName;
            data.Properties[HubSpotVocabulary.Owner.LastName] = input.lastName;
            data.Properties[HubSpotVocabulary.Owner.Type] = input.type;

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.ToString(), s => "HubSpot");

            return clue;
        }
    }
}
