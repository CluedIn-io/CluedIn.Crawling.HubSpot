using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class CallClueProducer : BaseClueProducer<Call>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<CallClueProducer> _log;

        public CallClueProducer(IClueFactory factory, ILogger<CallClueProducer> log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override Clue MakeClueImpl(Call input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.PhoneCall, input.engagement.id.ToString(), accountId);

            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_002_Unknown_VocabularyKey_Used);

            var data = clue.Data.EntityData;
            if (input.associations != null)
            {
                if (input.associations.contactIds != null)
                {
                    foreach (var contactId in input.associations.contactIds)
                        _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Contact, EntityEdgeType.Parent, input, contactId.ToString());
                }
                if (input.associations.dealIds != null)
                {
                    foreach (var dealId in input.associations.dealIds)
                        _factory.CreateOutgoingEntityReference(clue, EntityType.Sales.Deal, EntityEdgeType.Parent, input, dealId.ToString());
                }
                if (input.associations.ownerIds != null)
                {
                    foreach (var ownerId in input.associations.ownerIds)
                        _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, input, ownerId.ToString());
                }
                if (input.associations.workflowIds != null)
                {
                    foreach (var workflowId in input.associations.workflowIds)
                        _factory.CreateOutgoingEntityReference(clue, EntityType.Process, EntityEdgeType.Parent, input, workflowId.ToString());
                }
                if (input.associations.companyIds != null)
                {
                    if (data.OutgoingEdges.Count == 0)
                        foreach (var companyId in input.associations.companyIds)
                            _factory.CreateOutgoingEntityReference(clue, EntityType.Organization, EntityEdgeType.Parent, input, companyId.ToString());
                }
            }

            if (input.engagement != null)
            {

                if (input.engagement.portalId != null && input.engagement.id != null && input.engagement.ownerId != null)
                {
                    string url = string.Format("https://app.hubspot.com/sales/{0}/tasks?taskId={1}&ownerId={2}", input.engagement.portalId, input.engagement.id, input.engagement.ownerId);
                    data.Uri = new Uri(url);
                }
                if (input.engagement.createdAt != null)
                {
                    if (long.TryParse(input.engagement.createdAt.ToString(), out long date))
                        data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                }
                if (input.engagement.lastUpdated != null)
                {
                    if (long.TryParse(input.engagement.lastUpdated.ToString(), out long date))
                        data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                }
                if (input.engagement.timestamp != null)
                {
                    if (data.CreatedDate == null)
                    {
                        if (long.TryParse(input.engagement.createdAt.ToString(), out long date))
                            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                    }
                }
                if (data.CreatedDate != null)
                    data.Name = "Call at " + data.CreatedDate.Value.ToString("MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                if (input.engagement.active != null)
                {
                    data.Properties[HubSpotVocabulary.Email.Active] = input.engagement.active.Value.ToString();
                }
                if (input.engagement.createdBy != null)
                {
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.CreatedBy, input, input.engagement.createdBy.ToString());
                }
                if (input.engagement.modifiedBy != null)
                {
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.ModifiedBy, input, input.engagement.modifiedBy.ToString());
                }
                if (input.engagement.ownerId != null)
                {
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, input,  input.engagement.ownerId.ToString());
                }
                if (input.engagement.portalId != null)
                {
                    if (data.OutgoingEdges.Count == 0)
                        _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.Parent, input, input.engagement.portalId.ToString());
                }
            }

            try
            {
                var metadata = JsonUtility.Deserialize<Dictionary<string, object>>(JsonUtility.Serialize(input.metadata));

                if (metadata != null)
                {
                    foreach (var property in metadata)
                    {
                        if (property.Key == "toNumber" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Properties[HubSpotVocabulary.Call.ToNumber] = property.Value.ToString();
                        }
                        else if (property.Key == "fromNumber" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Properties[HubSpotVocabulary.Call.FromNumber] = property.Value.ToString();
                        }
                        else if (property.Key == "status" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Properties[HubSpotVocabulary.Call.Status] = property.Value.ToString();
                        }
                        else if (property.Key == "externalId" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Properties[HubSpotVocabulary.Call.ExternalId] = property.Value.ToString();
                        }
                        else if (property.Key == "durationMilliseconds" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Properties[HubSpotVocabulary.Call.Duration] = property.Value.ToString();
                        }
                        else if (property.Key == "externalAccountId" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.Parent, input, property.Value.ToString());
                            data.Properties[HubSpotVocabulary.Call.ExternalAccountId] = property.Value.ToString();
                        }
                        else if (property.Key == "recordingUrl" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Properties[HubSpotVocabulary.Call.RecordingUrl] = property.Value.ToString();
                        }
                        else if (property.Key == "body" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Name = Regex.Replace(property.Value.ToString(), "<.*?>", String.Empty);
                            data.Properties[HubSpotVocabulary.Call.Body] = Regex.Replace(property.Value.ToString(), "<.*?>", String.Empty);
                        }
                        else if (property.Key == "disposition" && property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            data.Properties[HubSpotVocabulary.Call.Disposition] = property.Value.ToString();
                        }
                        else
                        {
                            if (property.Value != null && !string.IsNullOrEmpty(property.Value.ToString()))
                                data.Properties[string.Format("hubspot.call.custom-{0}", property.Key)] = property.Value.ToString();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _log.LogError(exception, "Failed to parse metadata for HubSpot Call");
            }
            if (data.Name == null)
                data.Name = input.engagement.type + " at " + data.CreatedDate.Value.ToString("MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);

            return clue;
        }
    }
}
