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
            VocabularyName = "HubSpot Ticket";
            KeyPrefix      = "hubspot.ticket";
            KeySeparator   = ".";
            Grouping       = EntityType.Support.Ticket;

            AddGroup("Hubspot Ticket Details", group =>
            {
                TicketName                      = group.Add(new VocabularyKey("TicketName"));
                TicketDescription               = group.Add(new VocabularyKey("TicketDescription"));
                ClosedDate                      = group.Add(new VocabularyKey("ClosedDate", VocabularyKeyDataType.DateTime));
                CreatedBy                       = group.Add(new VocabularyKey("CreatedBy"));
                CreatedDate                     = group.Add(new VocabularyKey("CreatedDate", VocabularyKeyDataType.DateTime));
                FirstAgentResponseDate          = group.Add(new VocabularyKey("FirstAgentResponseDate", VocabularyKeyDataType.DateTime));
                CustomInboxID                   = group.Add(new VocabularyKey("CustomInboxID"));
                LastActivityDate                = group.Add(new VocabularyKey("LastActivityDate", VocabularyKeyDataType.DateTime));
                LastContactedDate               = group.Add(new VocabularyKey("LastContactedDate", VocabularyKeyDataType.DateTime));
                LastModifiedDate                = group.Add(new VocabularyKey("LastModifiedDate", VocabularyKeyDataType.DateTime));
                NextActivityDate                = group.Add(new VocabularyKey("NextActivityDate", VocabularyKeyDataType.DateTime));
                NumberOfTimesContacted          = group.Add(new VocabularyKey("NumberOfTimesContacted", VocabularyKeyDataType.Integer));
                Pipeline                        = group.Add(new VocabularyKey("Pipeline"));
                TicketStatus                    = group.Add(new VocabularyKey("TicketStatus"));
                Resolution                      = group.Add(new VocabularyKey("Resolution"));
                Category                        = group.Add(new VocabularyKey("Category"));
                TicketId                        = group.Add(new VocabularyKey("TicketId"));
                Priority                        = group.Add(new VocabularyKey("Priority"));
                OwnerAssignedDate               = group.Add(new VocabularyKey("OwnerAssignedDate", VocabularyKeyDataType.DateTime));
                DateOfLastEngagement            = group.Add(new VocabularyKey("DateOfLastEngagement", VocabularyKeyDataType.DateTime));
                LastCustomerReplyDate           = group.Add(new VocabularyKey("LastCustomerReplyDate", VocabularyKeyDataType.DateTime));
                NPSfollowup                     = group.Add(new VocabularyKey("NPSfollowp"));
                NPSfollowupQuestion             = group.Add(new VocabularyKey("NPSfollowupQuestion"));
                ConversationNPSscore            = group.Add(new VocabularyKey("ConversationNPSscore"));
                ReferenceToeEmailThread         = group.Add(new VocabularyKey("ReferenceToeEmailThread"));
                TimeToClose                     = group.Add(new VocabularyKey("TimeToClose"));
                TimeToFirstAgentReply           = group.Add(new VocabularyKey("TimeToFirstAgentReply"));
                Source                          = group.Add(new VocabularyKey("Source"));
                ReferenceToSourceSpecificObject = group.Add(new VocabularyKey("ReferenceToSourceSpecificObject"));
                Tags                            = group.Add(new VocabularyKey("Tags"));
                TicketOwner                     = group.Add(new VocabularyKey("TicketOwner"));
                HubSpotTeam                     = group.Add(new VocabularyKey("HubSpotTeam"));
                AllOwnerIds                     = group.Add(new VocabularyKey("AllOwnerIds"));
                AllTeamIds                      = group.Add(new VocabularyKey("AllTeamIds"));
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
