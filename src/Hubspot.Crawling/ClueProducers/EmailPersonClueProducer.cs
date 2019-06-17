using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class EmailPersonClueProducer : BaseClueProducer<EmailPerson>
    {
        private readonly IClueFactory _factory;

        public EmailPersonClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(EmailPerson input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Person, input.email, accountId);
            
            var data = clue.Data.EntityData;

            data.Name = input.email;
            data.Aliases.Add(input.email);

            data.Properties[HubSpotVocabulary.EmailPerson.Email] = input.email;
            data.Properties[HubSpotVocabulary.EmailPerson.FirstName] = input.firstName;
            data.Properties[HubSpotVocabulary.EmailPerson.LastName] = input.lastName;

            if (input.lastName != null)
            {
                data.Name = input.firstName != null ? $"{input.firstName} {input.lastName}" : input.lastName;
            }
            else
            {
                data.Name = input.email;
            }

            return clue;
        }
    }
}
