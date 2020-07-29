using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core;
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
    public class LineItemClueProducer : BaseClueProducer<LineItem>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<LineItemClueProducer> _log;

        public LineItemClueProducer(IClueFactory factory, ILogger<LineItemClueProducer> log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override Clue MakeClueImpl(LineItem input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Sales.Order, input.ObjectId.ToString(), accountId);

            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_001_Name_MustBeSet);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);

            var data = clue.Data.EntityData;

            data.Properties[HubSpotVocabulary.LineItem.Version] = input.Version.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.LineItem.IsDeleted] = input.IsDeleted.PrintIfAvailable();

            if (input.Properties != null)
            {
                try
                {
                    var jobject = JObject.Parse(JsonUtility.Serialize(input.Properties));
                    var properties = new List<Property>();
                    if (jobject?.Properties() != null)
                    {
                        jobject.Properties().ForEach(c =>
                            properties.Add(new Property
                            {
                                Name = c.Name,
                                Value = c.Value["value"]?.ToString(),
                                Timestamp = c.Value["timestamp"]?.ToString(),
                                Source = c.Value["source"]?.ToString(),
                                SourceId = c.Value["sourceId"]?.ToString()
                            }));
                    }

                    foreach (var r in properties)
                    {
                        if (r.Name == "createdate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(epoch);
                            if (data.CreatedDate != null)
                                data.Properties[HubSpotVocabulary.LineItem.CreateDate] = DateTimeFormatter.ToIso8601(data.CreatedDate.Value);
                            if (r.SourceId != null)
                                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.CreatedBy, input, r.SourceId);
                        }

                        else if (r.Name == "hs_lastmodifieddate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(epoch);
                            if (data.ModifiedDate != null)
                                data.Properties[HubSpotVocabulary.LineItem.LastModifiedDate] = DateTimeFormatter.ToIso8601(data.ModifiedDate.Value);
                        }

                        else if (r.Name == "hs_recurring_billing_end_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.LineItem.EndDate] = DateTimeFormatter.ToIso8601(DateUtilities.EpochRef.AddMilliseconds(epoch));
                        }

                        else if (r.Name == "hs_recurring_billing_start_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.LineItem.StartDate] = DateTimeFormatter.ToIso8601(DateUtilities.EpochRef.AddMilliseconds(epoch));
                        }

                        else if (r.Name == "hs_deal_closed_won_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.LineItem.Dealclosedwondate] = DateTimeFormatter.ToIso8601(DateUtilities.EpochRef.AddMilliseconds(epoch));
                        }

                        else if (r.Name == "hs_product_id")
                        {
                            if (r.Value != null && r.Value.ToString() != string.Empty)
                                _factory.CreateOutgoingEntityReference(clue, EntityType.Product, EntityEdgeType.Parent, input, r.Value.ToString());

                            data.Properties[HubSpotVocabulary.LineItem.ProductID] = r.Value.PrintIfAvailable();
                        }

                        else if (r.Name == "name")
                            data.Properties[HubSpotVocabulary.LineItem.Name] = data.Name = r.Value.PrintIfAvailable();
                        else if (r.Name == "description")
                            data.Properties[HubSpotVocabulary.LineItem.Productdescription] = data.Description = r.Value.PrintIfAvailable();
                        else if (r.Name == "amount")
                            data.Properties[HubSpotVocabulary.LineItem.Amount] = r.Value.PrintIfAvailable();
                        else if (r.Name == "price")
                            data.Properties[HubSpotVocabulary.LineItem.Productprice] = r.Value.PrintIfAvailable();
                        else if (r.Name == "quantity")
                            data.Properties[HubSpotVocabulary.LineItem.Quantity] = r.Value.PrintIfAvailable();
                        else if (r.Name == "recurringbillingfrequency")
                            data.Properties[HubSpotVocabulary.LineItem.Recurringbillingfrequency] = r.Value.PrintIfAvailable();
                        else if (r.Name == "discount")
                            data.Properties[HubSpotVocabulary.LineItem.DiscountAmount] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_discount_percentage")
                            data.Properties[HubSpotVocabulary.LineItem.DiscountPercentage] = r.Value.PrintIfAvailable();
                        else if (r.Name == "tax")
                            data.Properties[HubSpotVocabulary.LineItem.Tax] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_recurring_billing_period")
                            data.Properties[HubSpotVocabulary.LineItem.Term] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_cost_of_goods_sold")
                            data.Properties[HubSpotVocabulary.LineItem.Costofgoodssold] = r.Value.PrintIfAvailable();
                        else
                            data.Properties[string.Format("hubspot.lineitem.custom-{0}", r.Name)] = r.Value.PrintIfAvailable();
                    }
                }
                catch (Exception exception)
                {
                    _log.LogError(exception, "Could not parse HubSpot Line Item Properies");
                }

                if (input.Associations.Any())
                    foreach (var association in input.Associations)
                        _factory.CreateOutgoingEntityReference(clue, EntityType.Sales.Deal, EntityEdgeType.PartOf, input, association.ToString());

                if (!data.OutgoingEdges.Any() && input.PortalId != null)
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.PortalId.ToString(), s => "HubSpot");
            }

            return clue;

        }
    }
}
