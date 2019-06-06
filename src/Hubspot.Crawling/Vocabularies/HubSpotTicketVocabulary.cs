// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotTicketVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotTicketVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot ticket vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotTicketVocabulary : SimpleVocabulary
    {
        public HubSpotTicketVocabulary()
        {
            this.VocabularyName = "HubSpot Ticket";
            this.KeyPrefix      = "hubspot.ticket";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Support.Ticket;

            this.AddGroup("Hubspot Ticket Details", group =>
            {
                this.TicketName                      = group.Add(new VocabularyKey("TicketName"));
                this.TicketDescription               = group.Add(new VocabularyKey("TicketDescription"));
                this.ClosedDate                      = group.Add(new VocabularyKey("ClosedDate", VocabularyKeyDataType.DateTime));
                this.CreatedBy                       = group.Add(new VocabularyKey("CreatedBy"));
                this.CreatedDate                     = group.Add(new VocabularyKey("CreatedDate", VocabularyKeyDataType.DateTime));
                this.FirstAgentResponseDate          = group.Add(new VocabularyKey("FirstAgentResponseDate", VocabularyKeyDataType.DateTime));
                this.CustomInboxID                   = group.Add(new VocabularyKey("CustomInboxID"));
                this.LastActivityDate                = group.Add(new VocabularyKey("LastActivityDate", VocabularyKeyDataType.DateTime));
                this.LastContactedDate               = group.Add(new VocabularyKey("LastContactedDate", VocabularyKeyDataType.DateTime));
                this.LastModifiedDate                = group.Add(new VocabularyKey("LastModifiedDate", VocabularyKeyDataType.DateTime));
                this.NextActivityDate                = group.Add(new VocabularyKey("NextActivityDate", VocabularyKeyDataType.DateTime));
                this.NumberOfTimesContacted          = group.Add(new VocabularyKey("NumberOfTimesContacted", VocabularyKeyDataType.Integer));
                this.Pipeline                        = group.Add(new VocabularyKey("Pipeline"));
                this.TicketStatus                    = group.Add(new VocabularyKey("TicketStatus"));
                this.Resolution                      = group.Add(new VocabularyKey("Resolution"));
                this.Category                        = group.Add(new VocabularyKey("Category"));
                this.TicketId                        = group.Add(new VocabularyKey("TicketId"));
                this.Priority                        = group.Add(new VocabularyKey("Priority"));
                this.OwnerAssignedDate               = group.Add(new VocabularyKey("OwnerAssignedDate", VocabularyKeyDataType.DateTime));
                this.DateOfLastEngagement            = group.Add(new VocabularyKey("DateOfLastEngagement", VocabularyKeyDataType.DateTime));
                this.LastCustomerReplyDate           = group.Add(new VocabularyKey("LastCustomerReplyDate", VocabularyKeyDataType.DateTime));
                this.NPSfollowup                     = group.Add(new VocabularyKey("NPSfollowp"));
                this.NPSfollowupQuestion             = group.Add(new VocabularyKey("NPSfollowupQuestion"));
                this.ConversationNPSscore            = group.Add(new VocabularyKey("ConversationNPSscore"));
                this.ReferenceToeEmailThread         = group.Add(new VocabularyKey("ReferenceToeEmailThread"));
                this.TimeToClose                     = group.Add(new VocabularyKey("TimeToClose"));
                this.TimeToFirstAgentReply           = group.Add(new VocabularyKey("TimeToFirstAgentReply"));
                this.Source                          = group.Add(new VocabularyKey("Source"));
                this.ReferenceToSourceSpecificObject = group.Add(new VocabularyKey("ReferenceToSourceSpecificObject"));
                this.Tags                            = group.Add(new VocabularyKey("Tags"));
                this.TicketOwner                     = group.Add(new VocabularyKey("TicketOwner"));
                this.HubSpotTeam                     = group.Add(new VocabularyKey("HubSpotTeam"));
                this.AllOwnerIds                     = group.Add(new VocabularyKey("AllOwnerIds"));
                this.AllTeamIds                      = group.Add(new VocabularyKey("AllTeamIds"));
            });
        }

        public VocabularyKey ClosedDate { get; private set; }
        public VocabularyKey CreatedBy { get; private set; }
        public VocabularyKey CreatedDate { get; private set; }
        public VocabularyKey FirstAgentResponseDate { get; private set; }
        public VocabularyKey CustomInboxID { get; private set; }
        public VocabularyKey LastActivityDate { get; private set; }
        public VocabularyKey LastContactedDate { get; private set; }
        public VocabularyKey LastModifiedDate { get; private set; }
        public VocabularyKey NextActivityDate { get; private set; }
        public VocabularyKey NumberOfTimesContacted { get; private set; }
        public VocabularyKey Pipeline { get; private set; }
        public VocabularyKey TicketStatus { get; private set; }
        public VocabularyKey Resolution { get; private set; }
        public VocabularyKey Category { get; private set; }
        public VocabularyKey TicketId { get; private set; }
        public VocabularyKey Priority { get; private set; }
        public VocabularyKey OwnerAssignedDate { get; private set; }
        public VocabularyKey DateOfLastEngagement { get; private set; }
        public VocabularyKey LastCustomerReplyDate { get; private set; }
        public VocabularyKey NPSfollowup { get; private set; }
        public VocabularyKey NPSfollowupQuestion { get; private set; }
        public VocabularyKey ConversationNPSscore { get; private set; }
        public VocabularyKey ReferenceToeEmailThread { get; private set; }
        public VocabularyKey TimeToClose { get; private set; }
        public VocabularyKey TimeToFirstAgentReply { get; private set; }
        public VocabularyKey TicketName { get; private set; }
        public VocabularyKey TicketDescription { get; private set; }
        public VocabularyKey Source { get; private set; }
        public VocabularyKey ReferenceToSourceSpecificObject { get; private set; }
        public VocabularyKey Tags { get; private set; }
        public VocabularyKey TicketOwner { get; private set; }
        public VocabularyKey HubSpotTeam { get; private set; }
        public VocabularyKey AllOwnerIds { get; private set; }
        public VocabularyKey AllTeamIds { get; private set; }
    }
}