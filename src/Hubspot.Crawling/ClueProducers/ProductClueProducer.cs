using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Logging;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class ProductClueProducer : BaseClueProducer<Product>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger _log;

        public ProductClueProducer(IClueFactory factory, ILogger log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override Clue MakeClueImpl(Product input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Product, input.ObjectId.ToString(), accountId);
            
            var data = clue.Data.EntityData;

            data.Uri = new Uri(string.Format("https://app.hubspot.com/settings/{0}/sales/products", input.PortalId));

            data.Properties[HubSpotVocabulary.Product.Version] = input.Version.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Product.IsDeleted] = input.IsDeleted.PrintIfAvailable();

            if (input.Properties != null)
            {
                try
                {
                    var jobject = JObject.Parse(JsonUtility.Serialize(input.Properties));
                    var properties = new List<Property>();
                    jobject.Properties().ForEach(c =>
                    properties.Add(new Property
                    {
                        Name = c.Name,
                        Value = c.Value["value"]?.ToString(),
                        Timestamp = c.Value["timestamp"]?.ToString(),
                        Source = c.Value["source"]?.ToString(),
                        SourceId = c.Value["sourceId"]?.ToString()
                    }));

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
                                data.Properties[HubSpotVocabulary.Product.CreateDate] = data.CreatedDate.Value.ToString("o");
                            if (r.SourceId != null)
                                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.CreatedBy, input, c => r.SourceId);
                        }

                        else if (r.Name == "hs_lastmodifieddate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(epoch);
                            if (data.ModifiedDate != null)
                                data.Properties[HubSpotVocabulary.Product.LastModifiedDate] = data.ModifiedDate.Value.ToString("o");
                        }

                        else if (r.Name == "hs_recurring_billing_start_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Product.StartDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }

                        else if (r.Name == "name")
                            data.Properties[HubSpotVocabulary.Product.Name] = data.Name = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_avatar_filemanager_key")
                            data.Properties[HubSpotVocabulary.Product.AvatarFileManagerkey] = r.Value.PrintIfAvailable();
                        else if (r.Name == "description")
                            data.Properties[HubSpotVocabulary.Product.Productdescription] = data.Description = r.Value.PrintIfAvailable();
                        else if (r.Name == "price")
                            data.Properties[HubSpotVocabulary.Product.Productprice] = r.Value.PrintIfAvailable();
                        else if (r.Name == "recurringbillingfrequency")
                            data.Properties[HubSpotVocabulary.Product.Recurringbillingfrequency] = r.Value.PrintIfAvailable();
                        else if (r.Name == "discount")
                            data.Properties[HubSpotVocabulary.Product.DiscountAmount] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_discount_percentage")
                            data.Properties[HubSpotVocabulary.Product.DiscountPercentage] = r.Value.PrintIfAvailable();
                        else if (r.Name == "tax")
                            data.Properties[HubSpotVocabulary.Product.Tax] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_recurring_billing_period")
                            data.Properties[HubSpotVocabulary.Product.Term] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_cost_of_goods_sold")
                            data.Properties[HubSpotVocabulary.Product.Costofgoodssold] = r.Value.PrintIfAvailable();
                        else
                            data.Properties[string.Format("hubspot.product.custom-{0}", r.Name)] = r.Value.PrintIfAvailable();
                    }
                }

                catch (Exception exception)
                {
                    _log.Error(() => "Could not parse HubSpot Product Properies", exception);
                }
            }

            if (!data.OutgoingEdges.Any() && input.PortalId != null)
                this._factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.PortalId.ToString(), s => "Hubspot");


            return clue;
        }
    }
}
