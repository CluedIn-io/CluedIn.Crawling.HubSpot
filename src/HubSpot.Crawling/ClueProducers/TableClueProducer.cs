using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class TableClueProducer : BaseClueProducer<Table>
    {
        private readonly IClueFactory _factory;

        public TableClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Table input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.List, input.id.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.Description = input.rowCount + " rows";
            data.Uri = new Uri("http://app.hubspot.com/l/hubdb");  // TODO take from configuration

            if (input.createdAt != null && long.TryParse(input.createdAt.ToString(), out long date))
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);

            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updatedAt);

            data.Properties[HubSpotVocabulary.Table.CreatedAt] = data.CreatedDate.PrintIfAvailable(v => v.Value.DateTime);
            data.Properties[HubSpotVocabulary.Table.UpdatedAt] = data.ModifiedDate.PrintIfAvailable(v => v.Value.DateTime);
            data.Properties[HubSpotVocabulary.Table.CreatedAt] = input.publishedAt.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v.Value));

            data.Properties[HubSpotVocabulary.Table.RowCount] = input.rowCount.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Table.Deleted] = input.deleted.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Table.ColumnCount] = input.columnCount.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Table.Id] = input.id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Table.CreatedBy] = input.createdBy.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Table.UseForPages] = input.useForPages.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Table.Name] = input.name.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Table.UpdatedBy] = input.updatedBy.PrintIfAvailable();

            if (input.createdBy != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.CreatedBy, input,  input.createdBy.ToString());

            if (input.updatedBy != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.Modified, input, input.updatedBy.ToString());

            if (input.PortalId != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.Parent, input, input.PortalId.ToString());


            return clue;
        }
    }
}
