using System;
using System.Globalization;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class EmailClueProducer : BaseClueProducer<Email>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger<EmailClueProducer> _log;

        public EmailClueProducer(IClueFactory factory, ILogger<EmailClueProducer> log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override Clue MakeClueImpl(Email input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Mail, input.engagement.id?.ToString(), accountId);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_001_Name_MustBeSet);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_003_Author_Name_MustBeSet);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_004_Invalid_EntityType);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_005_PreviewImage_RawData_MustBeSet);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_006_Created_Modified_Date_InFuture);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_007_Created_Modified_Date_InPast);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_008_Created_Modified_Date_UnixEpoch);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_009_Created_Modified_Date_MinDate);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_010_Created_Modified_Date_MaxDate);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_001_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_002_Unknown_VocabularyKey_Used);
            //clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_003_Value_ShouldNotBeQuoted);

            var data = clue.Data.EntityData;

            if (input.associations != null)
            {
                if (input.associations.companyIds != null)
                {
                    foreach (var companyId in input.associations.companyIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Organization, EntityEdgeType.Parent, input, selector => companyId.ToString());
                }
                if (input.associations.contactIds != null)
                {
                    foreach (var contactId in input.associations.contactIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Contact, EntityEdgeType.Parent, input, selector => contactId.ToString());
                }
                if (input.associations.dealIds != null)
                {
                    foreach (var dealId in input.associations.dealIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Sales.Deal, EntityEdgeType.Parent, input, selector => dealId.ToString());
                }
                if (input.associations.ownerIds != null)
                {
                    foreach (var ownerId in input.associations.ownerIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, input, selector => ownerId.ToString());
                }
                if (input.associations.workflowIds != null)
                {
                    foreach (var workflowId in input.associations.workflowIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Process, EntityEdgeType.Parent, input, selector => workflowId.ToString());
                }
            }

            if (input.engagement != null)
            {

                if (input.engagement.portalId != null && input.engagement.id != null && input.engagement.ownerId != null)
                {
                    var url =
                        $"https://app.hubspot.com/sales/{input.engagement.portalId}/tasks?taskId={input.engagement.id}&ownerId={input.engagement.ownerId}";  // TODO take from configuration
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
                if (input.engagement.active != null)
                {
                    data.Properties[HubSpotVocabulary.Email.Active] = input.engagement.active.ToString();
                }
                if (input.engagement.createdBy != null)
                {
                    _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.CreatedBy, input, selector => input.engagement.createdBy.ToString());
                }
                if (input.engagement.modifiedBy != null)
                {
                    _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.ModifiedBy, input, selector => input.engagement.modifiedBy.ToString());
                }
                if (input.engagement.ownerId != null)
                {
                    _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, input, selector => input.engagement.ownerId.ToString());
                }
                if (input.engagement.portalId != null)
                {
                    _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.Parent, input, selector => input.engagement.portalId.ToString());
                }
            }

            try
            {
                if (input.metadata != null)
                {
                    if (input.metadata.from?.email != null)
                    {
                        if (input.metadata.from.lastName != null)
                        {
                            if (input.metadata.from.firstName != null)
                            {
                                var personReference = new PersonReference(input.metadata.from.firstName + ' ' + input.metadata.from.lastName,
                                                              new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), input.metadata.from.email));

                                data.Authors.Add(personReference);
                                data.LastChangedBy = personReference;
                            }
                            else
                            {
                                var personReference = new PersonReference(input.metadata.from.lastName,
                                                              new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), input.metadata.from.email));

                                data.Authors.Add(personReference);
                                data.LastChangedBy = personReference;
                            }
                        }
                        else
                        {
                            var personReference = new PersonReference(new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), input.metadata.from.email));

                            data.Authors.Add(personReference);
                            data.LastChangedBy = personReference;
                        }

                        data.Properties[HubSpotVocabulary.Email.FromEmail] = input.metadata.from.email;
                        data.Properties[HubSpotVocabulary.Email.FromFirstName] = input.metadata.from.firstName;
                        data.Properties[HubSpotVocabulary.Email.FromLastName] = input.metadata.from.lastName;
                    }
                    if (input.metadata.to != null)
                    {
                        foreach (EmailPerson recipient in input.metadata.to)
                        {
                            if (recipient != null)
                            {
                                if (recipient.email != null && recipient.firstName != null && recipient.lastName != null)
                                {
                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode, recipient.firstName + ' ' + recipient.lastName),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                                if (recipient.email != null && recipient.lastName != null)
                                {
                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode, recipient.lastName),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                                else if (recipient.email != null)
                                {

                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                            }
                        }
                    }
                    if (input.metadata?.bcc != null)
                    {
                        foreach (EmailPerson recipient in input.metadata.bcc)
                        {
                            if (recipient != null)
                            {
                                if (recipient.email != null && recipient.firstName != null && recipient.lastName != null)
                                {

                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode, recipient.firstName + ' ' + recipient.lastName),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                                if (recipient.email != null && recipient.lastName != null)
                                {

                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode, recipient.lastName),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                                else if (recipient.email != null)
                                {

                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                            }
                        }
                    }
                    if (input.metadata.cc != null)
                    {
                        foreach (EmailPerson recipient in input.metadata.cc)
                        {
                            if (recipient != null)
                            {
                                if (recipient.email != null && recipient.firstName != null && recipient.lastName != null)
                                {

                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode, recipient.firstName + ' ' + recipient.lastName),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                                if (recipient.email != null && recipient.lastName != null)
                                {

                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode, recipient.lastName),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                                else if (recipient.email != null)
                                {

                                    var entityCode = new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), recipient.email);

                                    var entityEdge = new EntityEdge(
                                        EntityReference.CreateByKnownCode(clue.OriginEntityCode),
                                        EntityReference.CreateByKnownCode(entityCode),
                                        EntityEdgeType.Recipient);

                                    data.OutgoingEdges.Add(entityEdge);
                                }
                            }
                        }
                    }
                    if (input.metadata.subject != null)
                    {
                        data.Name = input.metadata.subject;
                    }
                    if (input.metadata.html != null)
                    {
                        data.Properties[HubSpotVocabulary.Email.HtmlBody] = input.metadata.html;
                    }
                    if (input.metadata.body != null)
                    {
                        data.Properties[HubSpotVocabulary.Email.Body] = input.metadata.body;
                    }
                    if (input.metadata.text != null)
                    {
                        data.Properties[HubSpotVocabulary.Email.Text] = input.metadata.text;
                    }
                    if (input.metadata.sentVia != null)
                    {
                        data.Properties[HubSpotVocabulary.Email.SentVia] = input.metadata.sentVia;
                    }
                    if (input.metadata.custom != null)
                    {
                        foreach (var p in input.metadata.custom)
                            data.Properties[string.Format("hubspot.company.custom-{0}", p.Key)] = p.Value.ToString();
                    }
                }
            }

            catch (Exception exception)
            {
                _log.LogError(exception, "Failed to parse value.metadata for HubSpot Email");
            }
            if (data.Name == null)
                data.Name = input.engagement.type + " at " + data.CreatedDate.Value.ToString("MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);

            return clue;
        }
    }
}
