using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class RowClueProducer : BaseClueProducer<Row>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<RowClueProducer> _log;

        public RowClueProducer(IClueFactory factory, ILogger<RowClueProducer> log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override Clue MakeClueImpl(Row input, Guid accountId)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var clue = _factory.Create(EntityType.List.Item, input.Id.ToString(), accountId);

            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            if (input.Name != null)
                data.Name = input.Name.ToString();

            if (input.CreatedAt != null && long.TryParse(input.CreatedAt.ToString(), out long date))
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);

            data.Uri = new Uri("http://app.hubspot.com/l/hubdb");  // TODO take from configuration

            data.Properties[HubSpotVocabulary.Row.Name] = input.Name.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Row.Id] = input.Id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Row.Path] = input.Path.PrintIfAvailable();
            if (data.CreatedDate.HasValue)
                data.Properties[HubSpotVocabulary.Row.CreatedAt] = DateTimeFormatter.ToIso8601(data.CreatedDate.Value);

            if (input.Table != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.List, EntityEdgeType.PartOf, input, selector => input.Table.Value.ToString());

            try
            {
                JObject jobject = null;
                if (input.Values != null)
                    jobject = JObject.Parse(input.Values.ToString());

                if (jobject != null)
                {
                    foreach (var p in jobject.Properties())
                    {
                        if (p.Value != null)
                        {
                            if (int.TryParse(p.Name, out int n) && input.Columns.Exists(c => c.id == n))
                            {
                                data.Properties[string.Format("hubspot.product.custom-{0}", input.Columns.Find(c => c.id == n).name)] = p.Value.ToString();
                            }
                            else
                            {
                                data.Properties[string.Format("hubspot.product.custom-{0}", p.Name)] = p.Value.ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                _log.LogError(exception, "Failed to parse columns for Hubspot Row");
            }

            return clue;
        }
    }
}
