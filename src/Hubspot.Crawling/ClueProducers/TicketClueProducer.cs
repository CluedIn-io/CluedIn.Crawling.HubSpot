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
    public class TicketClueProducer : BaseClueProducer<Ticket>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger _log;

        public TicketClueProducer(IClueFactory factory, ILogger log)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override Clue MakeClueImpl(Ticket input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Support.Ticket, input.ObjectId.ToString(), accountId);
            
            var data = clue.Data.EntityData;

            data.Uri = new Uri(string.Format("https://app.hubspot.com/tickets/{0}/tickets?ticketId={1}", input.PortalId.Value, input.ObjectId));
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
                        if (r.Name == "closed_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.ClosedDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "created_by")
                        {
                            if (r.Value != null)
                                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.CreatedBy, input, p => r.Value);
                            data.Properties[HubSpotVocabulary.Ticket.CreatedBy] = r.Value.PrintIfAvailable();
                        }
                        else if (r.Name == "createdate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(epoch);
                            if (data.CreatedDate != null)
                                data.Properties[HubSpotVocabulary.Ticket.CreatedDate] = data.CreatedDate.Value.ToString("o");
                            if (r.SourceId != null)
                                this._factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.CreatedBy, input, c => r.SourceId);
                        }
                        else if (r.Name == "first_agent_reply_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.FirstAgentResponseDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "hs_lastactivitydate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.LastActivityDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "hs_lastcontacted")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.LastContactedDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "hs_lastmodifieddate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(epoch);
                            if (data.ModifiedDate != null)
                                data.Properties[HubSpotVocabulary.Ticket.LastModifiedDate] = data.ModifiedDate.Value.ToString("o");
                        }
                        else if (r.Name == "hs_nextactivitydate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.NextActivityDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "last_engagement_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.DateOfLastEngagement] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "last_reply_date")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.LastCustomerReplyDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "hubspot_owner_assigneddate")
                        {
                            long epoch = long.MinValue;
                            if (!long.TryParse(r.Timestamp, out epoch))
                                long.TryParse(r.Value, out epoch);
                            if (epoch != long.MinValue)
                                if (DateUtilities.EpochRef.AddMilliseconds(epoch) != null)
                                    data.Properties[HubSpotVocabulary.Ticket.OwnerAssignedDate] = DateUtilities.EpochRef.AddMilliseconds(epoch).ToString("o");
                        }
                        else if (r.Name == "hubspot_owner_id")
                        {
                            if (r.Value != null)
                                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.OwnedBy, input, p => r.Value);
                            data.Properties[HubSpotVocabulary.Ticket.TicketOwner] = r.Value.PrintIfAvailable();
                        }
                        else if (r.Name == "hs_custom_inbox")
                            data.Properties[HubSpotVocabulary.Ticket.CustomInboxID] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_num_times_contacted")
                            data.Properties[HubSpotVocabulary.Ticket.NumberOfTimesContacted] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_pipeline")
                            data.Properties[HubSpotVocabulary.Ticket.Pipeline] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_pipeline_stage")
                            data.Properties[HubSpotVocabulary.Ticket.TicketStatus] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_resolution")
                            data.Properties[HubSpotVocabulary.Ticket.Resolution] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_ticket_category")
                            data.Properties[HubSpotVocabulary.Ticket.Category] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_ticket_id")
                            data.Properties[HubSpotVocabulary.Ticket.TicketId] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_ticket_priority")
                            data.Properties[HubSpotVocabulary.Ticket.Priority] = r.Value.PrintIfAvailable();
                        else if (r.Name == "nps_follow_up_answer")
                            data.Properties[HubSpotVocabulary.Ticket.NPSfollowup] = r.Value.PrintIfAvailable();
                        else if (r.Name == "nps_follow_up_question_version")
                            data.Properties[HubSpotVocabulary.Ticket.NPSfollowupQuestion] = r.Value.PrintIfAvailable();
                        else if (r.Name == "nps_score")
                            data.Properties[HubSpotVocabulary.Ticket.ConversationNPSscore] = r.Value.PrintIfAvailable();
                        else if (r.Name == "source_thread_id")
                            data.Properties[HubSpotVocabulary.Ticket.ReferenceToeEmailThread] = r.Value.PrintIfAvailable();
                        else if (r.Name == "time_to_close")
                            data.Properties[HubSpotVocabulary.Ticket.TimeToClose] = r.Value.PrintIfAvailable();
                        else if (r.Name == "time_to_first_agent_reply")
                            data.Properties[HubSpotVocabulary.Ticket.TimeToFirstAgentReply] = r.Value.PrintIfAvailable();
                        else if (r.Name == "subject")
                            data.Properties[HubSpotVocabulary.Ticket.TicketName] = data.Name = r.Value.PrintIfAvailable();
                        else if (r.Name == "content")
                            data.Properties[HubSpotVocabulary.Ticket.TicketDescription] = data.Description = r.Value.PrintIfAvailable();
                        else if (r.Name == "source_type")
                            data.Properties[HubSpotVocabulary.Ticket.Source] = r.Value.PrintIfAvailable();
                        else if (r.Name == "source_ref")
                            data.Properties[HubSpotVocabulary.Ticket.ReferenceToSourceSpecificObject] = r.Value.PrintIfAvailable();
                        else if (r.Name == "tags")
                            data.Properties[HubSpotVocabulary.Ticket.Tags] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hubspot_team_id")
                            data.Properties[HubSpotVocabulary.Ticket.HubSpotTeam] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_all_owner_ids")
                            data.Properties[HubSpotVocabulary.Ticket.AllOwnerIds] = r.Value.PrintIfAvailable();
                        else if (r.Name == "hs_all_team_ids")
                            data.Properties[HubSpotVocabulary.Ticket.AllTeamIds] = r.Value.PrintIfAvailable();
                        else
                            data.Properties[string.Format("hubspot.ticket.custom-{0}", r.Name)] = r.Value.PrintIfAvailable();
                    }
                }
                catch (Exception exception)
                {
                    _log.Error(() => "Failed to parse properties for Hubspot Ticket", exception);
                }
            }

            if (input.Associations != null)
            {
                if (input.Associations.Companies.Any())
                    foreach (var t in input.Associations.Companies)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Organization, EntityEdgeType.Involves, input, p => t.ToString());

                if (input.Associations.Contacts.Any())
                    foreach (var t in input.Associations.Contacts)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Contact, EntityEdgeType.Involves, input, p => t.ToString());

                if (input.Associations.Engagements.Any())
                    foreach (var t in input.Associations.Engagements)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Activity, EntityEdgeType.Involves, input, p => t.ToString());
            }

            return clue;
        }
    }
}
