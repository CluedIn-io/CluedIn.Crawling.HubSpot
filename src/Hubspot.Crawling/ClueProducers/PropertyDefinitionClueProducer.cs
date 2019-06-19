using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class PropertyDefinitionClueProducer : BaseClueProducer<PropertyDefinition>
    {
        private readonly IClueFactory _factory;

        public PropertyDefinitionClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(PropertyDefinition input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Note, input.Groupname + input.Name, accountId);
            
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            data.Description = input.Description;
            data.Name = input.Name;

            if (input.Createdat.HasValue)
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.Createdat.Value);
            if (data.CreatedDate != null)
                data.Properties[HubSpotVocabulary.PropertyDefinition.CreatedAt] = data.CreatedDate.Value.ToString("o");

            if (input.Updatedat.HasValue)
                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.Updatedat.Value);
            if (data.ModifiedDate != null)
                data.Properties[HubSpotVocabulary.PropertyDefinition.UpdatedAt] = data.ModifiedDate.Value.ToString("o");

            if (input.Createduserid != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.CreatedBy, input, c => input.Createduserid.Value.ToString());

            if (input.Updateduserid != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.ModifiedBy, input, c => input.Updateduserid.Value.ToString());

            data.Properties[HubSpotVocabulary.PropertyDefinition.Name] = input.Name.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Label] = input.Label.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Description] = input.Description.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.GroupName] = input.Groupname.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Type] = input.Type.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.FieldType] = input.Fieldtype.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Hidden] = input.Hidden.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Options] = input.Options.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Calculated] = input.Calculated.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.ExternalOptions] = input.Externaloptions.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.HubspotDefined] = input.Hubspotdefined.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.FormField] = input.Formfield.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.DisplayOrder] = input.Displayorder.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.ReadonlyValue] = input.Readonlyvalue.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.ReadonlyDefinition] = input.Readonlydefinition.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Deleted] = input.Deleted.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.MutableDefinitionNotDeletable] = input.Mutabledefinitionnotdeletable.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.Favorited] = input.Favorited.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.FavoritedOrder] = input.Favoritedorder.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.DisplayMode] = input.Displaymode.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.ShowCurrencySymbol] = input.Showcurrencysymbol.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.CreatedUserId] = input.Createduserid.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.TextDisplayHint] = input.Textdisplayhint.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.NumberDisplayHint] = input.Numberdisplayhint.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.OptionsAreMutable] = input.Optionsaremutable.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.ReferencedObjectType] = input.Referencedobjecttype.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.IsCustomizedDefault] = input.Iscustomizeddefault.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.PropertyDefinition.UpdatedUserId] = input.Updateduserid.PrintIfAvailable();


            return clue;
        }
    }
}
