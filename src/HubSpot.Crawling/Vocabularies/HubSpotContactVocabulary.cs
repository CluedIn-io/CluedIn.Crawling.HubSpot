// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotContactVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotContactVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot contact vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotContactVocabulary : SimpleVocabulary
    {
        public HubSpotContactVocabulary()
        {
            VocabularyName = "HubSpot Contact";
            KeyPrefix      = "hubspot.contact";
            KeySeparator   = ".";
            Grouping       = EntityType.Infrastructure.Contact;

            CanonicalVid     = Add(new VocabularyKey("CanonicalVid",                                  VocabularyKeyVisibility.Hidden));
            FormSubmissions  = Add(new VocabularyKey("FormSubmissions",   VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
       
            MergeAudits      = Add(new VocabularyKey("MergeAudits",       VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
            IsContact        = Add(new VocabularyKey("IsContact",                                     VocabularyKeyVisibility.Hidden));
            ProfileUrl       = Add(new VocabularyKey("ProfileUrl",                                    VocabularyKeyVisibility.Hidden));
            Vid              = Add(new VocabularyKey("Vid",                                           VocabularyKeyVisibility.Hidden));
            Email            = Add(new VocabularyKey("Email").WithDataAnnotations(
                                                                k => k.PrimaryKey()
                                                              , k => k.CanEdit()
                                                              , k => k.Nullable()
                                                              , k => k.Required()
                                                              , k => k.MaxLength(100)
                                                            )
                                                            .ModelProperty("email", typeof(string))
                                                            .ModelDictionary("Properties", "email")
                                                            .ContainsIdentifiableData(IdentifiableDataTypes.EmailAddress));

            AddMapping(Email, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Email);

            // TODO: map keys to CluedIn vocabulary


            ContactInformationDaysToClose                                 = Add(new VocabularyKey("DaysToClose", VocabularyKeyDataType.Text));
            ContactInformationFirstDealCreatedDate                        = Add(new VocabularyKey("FirstDealCreatedDate", VocabularyKeyDataType.Text));
            ContactInformationAdditionalemailaddresses                    = Add(new VocabularyKey("Additionalemailaddresses", VocabularyKeyDataType.Text));
            ContactInformationAvatarFileManagerkey                        = Add(new VocabularyKey("AvatarFileManagerkey", VocabularyKeyDataType.Text));
            ContactInformationCalculatedMobileNumberinInternationalFormat = Add(new VocabularyKey("CalculatedMobileNumberinInternationalFormat", VocabularyKeyDataType.Text));
            ContactInformationCalculatedPhoneNumberinInternationalFormat  = Add(new VocabularyKey("CalculatedPhoneNumberinInternationalFormat", VocabularyKeyDataType.Text));
            ContactInformationCalculatedPhoneNumberAreaCode               = Add(new VocabularyKey("CalculatedPhoneNumberAreaCode", VocabularyKeyDataType.Text));
            ContactInformationCalculatedPhoneNumberCountryCode            = Add(new VocabularyKey("CalculatedPhoneNumberCountryCode", VocabularyKeyDataType.Text));
            ContactInformationCalculatedPhoneNumberRegion                 = Add(new VocabularyKey("CalculatedPhoneNumberRegion", VocabularyKeyDataType.Text));
            ContactInformationEmailDomain                                 = Add(new VocabularyKey("EmailDomain", VocabularyKeyDataType.Text));
            ContactInformationLeadStatus                                  = Add(new VocabularyKey("LeadStatus", VocabularyKeyDataType.Text));
            ContactInformationRecentSalesEmailClickedDate                 = Add(new VocabularyKey("RecentSalesEmailClickedDate", VocabularyKeyDataType.Text));
            ContactInformationRecentSalesEmailOpenedDate                  = Add(new VocabularyKey("RecentSalesEmailOpenedDate", VocabularyKeyDataType.Text));
            ContactInformationRecentSalesEmailRepliedDate                 = Add(new VocabularyKey("RecentSalesEmailRepliedDate", VocabularyKeyDataType.Text));
            ContactInformationCalculatedMobileNumberwithcountrycode       = Add(new VocabularyKey("CalculatedMobileNumberwithcountrycode", VocabularyKeyDataType.Text));
            ContactInformationCalculatedPhoneNumberwithcountrycode        = Add(new VocabularyKey("CalculatedPhoneNumberwithcountrycode", VocabularyKeyDataType.Text));
            ContactInformationCalculatedMobileNumberwithoutcountrycode    = Add(new VocabularyKey("CalculatedMobileNumberwithoutcountrycode", VocabularyKeyDataType.Text));
            ContactInformationCalculatedPhoneNumberwithoutcountrycode     = Add(new VocabularyKey("CalculatedPhoneNumberwithoutcountrycode", VocabularyKeyDataType.Text));
            ContactInformationCurrentlyinSequence                         = Add(new VocabularyKey("CurrentlyinSequence", VocabularyKeyDataType.Text));
            ContactInformationOwnerAssignedDate                           = Add(new VocabularyKey("OwnerAssignedDate", VocabularyKeyDataType.Text));
            ContactInformationLastModifiedDate                            = Add(new VocabularyKey("LastModifiedDate", VocabularyKeyDataType.Text));
            ContactInformationAssociatedDeals                             = Add(new VocabularyKey("AssociatedDeals", VocabularyKeyDataType.Text));
            ContactInformationRecentDealAmount                            = Add(new VocabularyKey("RecentDealAmount", VocabularyKeyDataType.Text));
            ContactInformationRecentDealCloseDate                         = Add(new VocabularyKey("RecentDealCloseDate", VocabularyKeyDataType.Text));
            ContactInformationTotalRevenue                                = Add(new VocabularyKey("TotalRevenue", VocabularyKeyDataType.Number));
            ContactInformationFirstName                                   = Add(new VocabularyKey("FirstName", VocabularyKeyDataType.PersonName));
            ContactInformationLastName                                    = Add(new VocabularyKey("LastName", VocabularyKeyDataType.PersonName));
            ContactInformationSalutation                                  = Add(new VocabularyKey("Salutation", VocabularyKeyDataType.Text));
            ContactInformationPersona                                     = Add(new VocabularyKey("Persona", VocabularyKeyDataType.Text));
            ContactInformationMobilePhoneNumber                           = Add(new VocabularyKey("MobilePhoneNumber", VocabularyKeyDataType.PhoneNumber));
            ContactInformationPhoneNumber                                 = Add(new VocabularyKey("PhoneNumber", VocabularyKeyDataType.Text));
            ContactInformationFaxNumber                                   = Add(new VocabularyKey("FaxNumber", VocabularyKeyDataType.Text));
            ContactInformationStreetAddress                               = Add(new VocabularyKey("StreetAddress", VocabularyKeyDataType.Text));
            ContactInformationLastMeetingBooked                           = Add(new VocabularyKey("LastMeetingBooked", VocabularyKeyDataType.Text));
            ContactInformationLastMeetingBookedCampaign                   = Add(new VocabularyKey("LastMeetingBookedCampaign", VocabularyKeyDataType.Text));
            ContactInformationLastMeetingBookedMedium                     = Add(new VocabularyKey("LastMeetingBookedMedium", VocabularyKeyDataType.Text));
            ContactInformationLastMeetingBookedSource                     = Add(new VocabularyKey("LastMeetingBookedSource", VocabularyKeyDataType.Text));
            ContactInformationHubSpotOwner                                = Add(new VocabularyKey("HubSpotOwner", VocabularyKeyDataType.Text));
            ContactInformationLastContacted                               = Add(new VocabularyKey("LastContacted", VocabularyKeyDataType.Text));
            ContactInformationLastActivityDate                            = Add(new VocabularyKey("LastActivityDate", VocabularyKeyDataType.DateTime));
            ContactInformationNextActivityDate                            = Add(new VocabularyKey("NextActivityDate", VocabularyKeyDataType.DateTime));
            ContactInformationNumberoftimescontacted                      = Add(new VocabularyKey("Numberoftimescontacted", VocabularyKeyDataType.Text));
            ContactInformationNumberofSalesActivities                     = Add(new VocabularyKey("NumberofSalesActivities", VocabularyKeyDataType.Text));
            ContactInformationSurveyMonkeyEventLastUpdated                = Add(new VocabularyKey("SurveyMonkeyEventLastUpdated", VocabularyKeyDataType.Text));
            ContactInformationWebinarEventLastUpdated                     = Add(new VocabularyKey("WebinarEventLastUpdated", VocabularyKeyDataType.Text));
            ContactInformationCity                                        = Add(new VocabularyKey("City", VocabularyKeyDataType.Text));
            ContactInformationHubSpotTeam                                 = Add(new VocabularyKey("HubSpotTeam", VocabularyKeyDataType.Text));
            ContactInformationStateRegion                                 = Add(new VocabularyKey("State/Region", VocabularyKeyDataType.Text));
            ContactInformationPostalCode                                  = Add(new VocabularyKey("PostalCode", VocabularyKeyDataType.Text));
            ContactInformationCountry                                     = Add(new VocabularyKey("Country", VocabularyKeyDataType.Text));
            ContactInformationJobTitle                                    = Add(new VocabularyKey("JobTitle", VocabularyKeyDataType.Text));
            ContactInformationMessage                                     = Add(new VocabularyKey("Message", VocabularyKeyDataType.Text));
            ContactInformationCloseDate                                   = Add(new VocabularyKey("CloseDate", VocabularyKeyDataType.DateTime));
            ContactInformationLifecycleStage                              = Add(new VocabularyKey("LifecycleStage", VocabularyKeyDataType.Text));
            ContactInformationBecameaLeadDate                             = Add(new VocabularyKey("BecameaLeadDate", VocabularyKeyDataType.Text));
            ContactInformationBecameaMarketingQualifiedLeadDate           = Add(new VocabularyKey("BecameaMarketingQualifiedLeadDate", VocabularyKeyDataType.Text));
            ContactInformationBecameanOpportunityDate                     = Add(new VocabularyKey("BecameanOpportunityDate", VocabularyKeyDataType.Text));
            ContactInformationIPAddress                                   = Add(new VocabularyKey("IPAddress", VocabularyKeyDataType.IPAddress));
            ContactInformationCreateDate                                  = Add(new VocabularyKey("CreateDate", VocabularyKeyDataType.DateTime));
            ContactInformationBecameaSalesQualifiedLeadDate               = Add(new VocabularyKey("BecameaSalesQualifiedLeadDate", VocabularyKeyDataType.Text));
            ContactInformationBecameanEvangelistDate                      = Add(new VocabularyKey("BecameanEvangelistDate", VocabularyKeyDataType.Text));
            ContactInformationHubSpotScore                                = Add(new VocabularyKey("HubSpotScore", VocabularyKeyDataType.Text));
            ContactInformationCompanyName                                 = Add(new VocabularyKey("CompanyName", VocabularyKeyDataType.Text));
            ContactInformationBecameaCustomerDate                         = Add(new VocabularyKey("BecameaCustomerDate", VocabularyKeyDataType.Text));
            ContactInformationBecameaSubscriberDate                       = Add(new VocabularyKey("BecameaSubscriberDate", VocabularyKeyDataType.Text));
            ContactInformationWebsiteURL                                  = Add(new VocabularyKey("WebsiteURL", VocabularyKeyDataType.Uri));
            ContactInformationBecameanOtherLifecycleDate                  = Add(new VocabularyKey("BecameanOtherLifecycleDate", VocabularyKeyDataType.Text));
            ContactInformationNumberofEmployees                           = Add(new VocabularyKey("NumberOfEmployees", VocabularyKeyDataType.Text));
            ContactInformationAnnualRevenue                               = Add(new VocabularyKey("AnnualRevenue", VocabularyKeyDataType.Text));
            ContactInformationIndustry                                    = Add(new VocabularyKey("Industry", VocabularyKeyDataType.Text));
            ContactInformationAssociatedCompanyLastUpdated                = Add(new VocabularyKey("AssociatedCompanyLastUpdated", VocabularyKeyDataType.Text));

            AddMapping(ContactInformationPhoneNumber, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.PhoneNumber);
            AddMapping(ContactInformationMobilePhoneNumber, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.MobileNumber);
            AddMapping(ContactInformationStreetAddress, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.HomeAddress);
            AddMapping(ContactInformationJobTitle, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.JobTitle);
            AddMapping(ContactInformationFirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.FirstName);
            AddMapping(ContactInformationLastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.LastName);

            AddGroup("HubSpot Contact Email Information Details", group =>
            {
                EmailInformationEmailAddressQuarantined  = group.Add(new VocabularyKey("EmailAddressQuarantined", VocabularyKeyDataType.Text));
                EmailInformationOptedoutofallemail       = group.Add(new VocabularyKey("OptedOutOfAllEmail", VocabularyKeyDataType.Text));
                EmailInformationSendsSinceLastEngagement = group.Add(new VocabularyKey("EmailInformationSendsSinceLastEngagement", VocabularyKeyDataType.Integer));           
            });

            AddGroup("HubSpot Contact Social Media Details", group =>
            {
                SocialMediaInformationFacebookID          = group.Add(new VocabularyKey("FacebookID", VocabularyKeyDataType.Text));
                SocialMediaInformationgoogleplusID        = group.Add(new VocabularyKey("GoogleplusID", VocabularyKeyDataType.Text));
                SocialMediaInformationLinkedinID          = group.Add(new VocabularyKey("LinkedinID", VocabularyKeyDataType.Text));
                SocialMediaInformationTwitterID           = group.Add(new VocabularyKey("TwitterID", VocabularyKeyDataType.Text));
                SocialMediaInformationTwitterUsername     = group.Add(new VocabularyKey("TwitterUsername", VocabularyKeyDataType.Text));
                SocialMediaInformationFollowerCount       = group.Add(new VocabularyKey("FollowerCount", VocabularyKeyDataType.Text));
                SocialMediaInformationTwitterProfilePhoto = group.Add(new VocabularyKey("TwitterProfilePhoto", VocabularyKeyDataType.Text));
                SocialMediaInformationLinkedInBio         = group.Add(new VocabularyKey("LinkedInBio", VocabularyKeyDataType.Text));
                SocialMediaInformationTwitterBio          = group.Add(new VocabularyKey("TwitterBio", VocabularyKeyDataType.Text));
                SocialMediaInformationLinkedInConnections = group.Add(new VocabularyKey("LinkedInConnections", VocabularyKeyDataType.Text));
                SocialMediaInformationKloutScore          = group.Add(new VocabularyKey("KloutScore", VocabularyKeyDataType.Text));
                SocialMediaInformationPhoto               = group.Add(new VocabularyKey("Photo", VocabularyKeyDataType.Text));
            });

            AddMapping(SocialMediaInformationFacebookID, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialFacebook);
            AddMapping(SocialMediaInformationgoogleplusID, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialGooglePlus);
            AddMapping(SocialMediaInformationLinkedinID, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialLinkedIn);
            AddMapping(SocialMediaInformationTwitterUsername, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialTwitter);

            AddGroup("HubSpot Contact Analytics Information Details", group =>
            {
                AnalyticsInformationFirstTouchConvertingCampaign = group.Add(new VocabularyKey("FirstTouchConvertingCampaign", VocabularyKeyDataType.Text));
                AnalyticsInformationLastTouchConvertingCampaign  = group.Add(new VocabularyKey("LastTouchConvertingCampaign", VocabularyKeyDataType.Text));
                AnalyticsInformationFirstPageSeen                = group.Add(new VocabularyKey("FirstPageSeen", VocabularyKeyDataType.Text));
                AnalyticsInformationLastPageSeen                 = group.Add(new VocabularyKey("LastPageSeen", VocabularyKeyDataType.Text));
                AnalyticsInformationNumberofPageviews            = group.Add(new VocabularyKey("NumberOfPageviews", VocabularyKeyDataType.Text));
                AnalyticsInformationNumberofVisits               = group.Add(new VocabularyKey("NumberOfVisits", VocabularyKeyDataType.Text));
                AnalyticsInformationNumberofeventcompletions     = group.Add(new VocabularyKey("NumberOfEventCompletions", VocabularyKeyDataType.Text));
                AnalyticsInformationTimeFirstSeen                = group.Add(new VocabularyKey("TimeFirstSeen", VocabularyKeyDataType.Text));
                AnalyticsInformationTimeofFirstVisit             = group.Add(new VocabularyKey("TimeofFirstVisit", VocabularyKeyDataType.Text));
                AnalyticsInformationTimeLastSeen                 = group.Add(new VocabularyKey("TimeLastSeen", VocabularyKeyDataType.Text));
                AnalyticsInformationTimeofLastVisit              = group.Add(new VocabularyKey("TimeofLastVisit", VocabularyKeyDataType.Text));
                AnalyticsInformationOriginalSource               = group.Add(new VocabularyKey("OriginalSource", VocabularyKeyDataType.Text));
                AnalyticsInformationOriginalSourceDrillDown1     = group.Add(new VocabularyKey("OriginalSourceDrill-Down1", VocabularyKeyDataType.Text));
                AnalyticsInformationOriginalSourceDrillDown2     = group.Add(new VocabularyKey("OriginalSourceDrill-Down2", VocabularyKeyDataType.Text));
                AnalyticsInformationFirstReferringSite           = group.Add(new VocabularyKey("FirstReferringSite", VocabularyKeyDataType.Text));
                AnalyticsInformationLastReferringSite            = group.Add(new VocabularyKey("LastReferringSite", VocabularyKeyDataType.Text));
                AnalyticsInformationAveragePageviews             = group.Add(new VocabularyKey("AveragePageviews", VocabularyKeyDataType.Text));
                AnalyticsInformationEventRevenue                 = group.Add(new VocabularyKey("EventRevenue", VocabularyKeyDataType.Text));
            });


            AddGroup("HubSpot Contact Conversion Information Details", group =>
            {
                ConversionInformationFirstConversion              = group.Add(new VocabularyKey("FirstConversion", VocabularyKeyDataType.Text));
                ConversionInformationFirstConversionDate          = group.Add(new VocabularyKey("FirstConversionDate", VocabularyKeyDataType.DateTime));
                ConversionInformationIPCity                       = group.Add(new VocabularyKey("IPCity", VocabularyKeyDataType.GeographyCity));
                ConversionInformationIPCountry                    = group.Add(new VocabularyKey("IPCountry", VocabularyKeyDataType.GeographyCountry));
                ConversionInformationIPCountryCode                = group.Add(new VocabularyKey("IPCountryCode", VocabularyKeyDataType.GeographyCountry));
                ConversionInformationIPLatitudeLongitude          = group.Add(new VocabularyKey("IPLatitudeLongitude", VocabularyKeyDataType.GeographyCoordinates));
                ConversionInformationIPStateCodeRegionCode        = group.Add(new VocabularyKey("IPStateCodeRegionCode", VocabularyKeyDataType.GeographyLocation));
                ConversionInformationIPStateRegion                = group.Add(new VocabularyKey("IPStateRegion", VocabularyKeyDataType.GeographyLocation));
                ConversionInformationIPTimezone                   = group.Add(new VocabularyKey("IPTimezone", VocabularyKeyDataType.Text));
                ConversionInformationIPZipcode                    = group.Add(new VocabularyKey("IPZipCode", VocabularyKeyDataType.Text));
                ConversionInformationNumberofFormSubmissions      = group.Add(new VocabularyKey("NumberOfFormSubmissions", VocabularyKeyDataType.Text));
                ConversionInformationNumberofUniqueFormsSubmitted = group.Add(new VocabularyKey("NumberOfUniqueFormsSubmitted", VocabularyKeyDataType.Text));
                ConversionInformationRecentConversion             = group.Add(new VocabularyKey("RecentConversion", VocabularyKeyDataType.Text));
                ConversionInformationRecentConversionDate         = group.Add(new VocabularyKey("RecentConversionDate", VocabularyKeyDataType.DateTime));
            });
        }

        public VocabularyKey CanonicalVid { get; private set; }

        public VocabularyKey FormSubmissions { get; private set; }

        public VocabularyKey MergeAudits { get; private set; }

        public VocabularyKey Email { get; private set; }

        public VocabularyKey IsContact { get; private set; }
        
        public VocabularyKey ProfileUrl { get; private set; }

        public VocabularyKey Vid { get; private set; }

        public VocabularyKey ContactInformationDaysToClose { get; private set; }

        public VocabularyKey ConversionInformationFirstConversionDate { get; private set; }

        public VocabularyKey ConversionInformationFirstConversion { get; private set; }

        public VocabularyKey ContactInformationFirstDealCreatedDate { get; private set; }

        public VocabularyKey ContactInformationAdditionalemailaddresses { get; private set; }

        public VocabularyKey AnalyticsInformationFirstTouchConvertingCampaign { get; private set; }

        public VocabularyKey AnalyticsInformationLastTouchConvertingCampaign { get; private set; }

        public VocabularyKey ContactInformationAvatarFileManagerkey { get; private set; }

        public VocabularyKey ContactInformationCalculatedMobileNumberinInternationalFormat { get; private set; }

        public VocabularyKey ContactInformationCalculatedPhoneNumberinInternationalFormat { get; private set; }

        public VocabularyKey ContactInformationCalculatedPhoneNumberAreaCode { get; private set; }

        public VocabularyKey ContactInformationCalculatedPhoneNumberCountryCode { get; private set; }

        public VocabularyKey ContactInformationCalculatedPhoneNumberRegion { get; private set; }

        public VocabularyKey ContactInformationEmailDomain { get; private set; }

        public VocabularyKey EmailInformationEmailAddressQuarantined { get; private set; }

        public VocabularyKey EmailInformationSendsSinceLastEngagement { get; private set; }

        public VocabularyKey SocialMediaInformationFacebookID { get; private set; }

        public VocabularyKey SocialMediaInformationgoogleplusID { get; private set; }

        public VocabularyKey ConversionInformationIPTimezone { get; private set; }

        public VocabularyKey ContactInformationLeadStatus { get; private set; }

        public VocabularyKey SocialMediaInformationLinkedinID { get; private set; }

        public VocabularyKey ContactInformationRecentSalesEmailClickedDate { get; private set; }

        public VocabularyKey ContactInformationRecentSalesEmailOpenedDate { get; private set; }

        public VocabularyKey ContactInformationRecentSalesEmailRepliedDate { get; private set; }

        public VocabularyKey ContactInformationCalculatedMobileNumberwithcountrycode { get; private set; }

        public VocabularyKey ContactInformationCalculatedPhoneNumberwithcountrycode { get; private set; }

        public VocabularyKey ContactInformationCalculatedMobileNumberwithoutcountrycode { get; private set; }

        public VocabularyKey ContactInformationCalculatedPhoneNumberwithoutcountrycode { get; private set; }

        public VocabularyKey ContactInformationCurrentlyinSequence { get; private set; }

        public VocabularyKey SocialMediaInformationTwitterID { get; private set; }

        public VocabularyKey ContactInformationOwnerAssignedDate { get; private set; }

        public VocabularyKey ConversionInformationIPCity { get; private set; }

        public VocabularyKey ConversionInformationIPCountry { get; private set; }

        public VocabularyKey ConversionInformationIPCountryCode { get; private set; }

        public VocabularyKey ConversionInformationIPLatitudeLongitude { get; private set; }

        public VocabularyKey ConversionInformationIPStateRegion { get; private set; }

        public VocabularyKey ConversionInformationIPStateCodeRegionCode { get; private set; } 

        public VocabularyKey ConversionInformationIPZipcode { get; private set; }

        public VocabularyKey ContactInformationLastModifiedDate { get; private set; }

        public VocabularyKey ContactInformationAssociatedDeals { get; private set; }

        public VocabularyKey ConversionInformationNumberofFormSubmissions { get; private set; }

        public VocabularyKey ConversionInformationNumberofUniqueFormsSubmitted { get; private set; }

        public VocabularyKey ConversionInformationRecentConversionDate { get; private set; }

        public VocabularyKey ConversionInformationRecentConversion { get; private set; }

        public VocabularyKey ContactInformationRecentDealAmount { get; private set; }

        public VocabularyKey ContactInformationRecentDealCloseDate { get; private set; }

        public VocabularyKey ContactInformationTotalRevenue { get; private set; }

        public VocabularyKey ContactInformationFirstName { get; private set; }

        public VocabularyKey AnalyticsInformationFirstPageSeen { get; private set; }

        public VocabularyKey SocialMediaInformationTwitterUsername { get; private set; }
        
        public VocabularyKey SocialMediaInformationFollowerCount { get; private set; }

        public VocabularyKey AnalyticsInformationLastPageSeen { get; private set; }

        public VocabularyKey ContactInformationLastName { get; private set; }

        public VocabularyKey AnalyticsInformationNumberofPageviews { get; private set; }

        public VocabularyKey ContactInformationSalutation { get; private set; }

        public VocabularyKey SocialMediaInformationTwitterProfilePhoto { get; private set; }

        public VocabularyKey AnalyticsInformationNumberofVisits { get; private set; }

        public VocabularyKey ContactInformationPersona { get; private set; }

        public VocabularyKey AnalyticsInformationNumberofeventcompletions { get; private set; }

        public VocabularyKey EmailInformationOptedoutofallemail { get; private set; }

        public VocabularyKey ContactInformationMobilePhoneNumber { get; private set; }

        public VocabularyKey ContactInformationPhoneNumber { get; private set; }

        public VocabularyKey ContactInformationFaxNumber { get; private set; }

        public VocabularyKey AnalyticsInformationTimeFirstSeen { get; private set; }

        public VocabularyKey ContactInformationStreetAddress { get; private set; }

        public VocabularyKey ContactInformationLastMeetingBooked { get; private set; }

        public VocabularyKey ContactInformationLastMeetingBookedCampaign { get; private set; }

        public VocabularyKey ContactInformationLastMeetingBookedMedium { get; private set; }

        public VocabularyKey ContactInformationLastMeetingBookedSource { get; private set; }

        public VocabularyKey AnalyticsInformationTimeofFirstVisit { get; private set; }

        public VocabularyKey ContactInformationHubSpotOwner { get; private set; }

        public VocabularyKey ContactInformationLastContacted { get; private set; }

        public VocabularyKey ContactInformationLastActivityDate { get; private set; }

        public VocabularyKey ContactInformationNextActivityDate { get; private set; }

        public VocabularyKey ContactInformationNumberoftimescontacted { get; private set; }

        public VocabularyKey ContactInformationNumberofSalesActivities { get; private set; }

        public VocabularyKey ContactInformationSurveyMonkeyEventLastUpdated { get; private set; }

        public VocabularyKey ContactInformationWebinarEventLastUpdated { get; private set; }

        public VocabularyKey ContactInformationCity { get; private set; }

        public VocabularyKey AnalyticsInformationTimeLastSeen { get; private set; }

        public VocabularyKey ContactInformationHubSpotTeam { get; private set; }

        public VocabularyKey SocialMediaInformationLinkedInBio { get; private set; }

        public VocabularyKey SocialMediaInformationTwitterBio { get; private set; }

        public VocabularyKey AnalyticsInformationTimeofLastVisit { get; private set; }

        public VocabularyKey ContactInformationStateRegion { get; private set; } 

        public VocabularyKey AnalyticsInformationOriginalSource { get; private set; }

        public VocabularyKey ContactInformationPostalCode { get; private set; }

        public VocabularyKey ContactInformationCountry { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceDrillDown1 { get; private set; } 

        public VocabularyKey SocialMediaInformationLinkedInConnections { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceDrillDown2 { get; private set; } 

        public VocabularyKey ContactInformationJobTitle { get; private set; }

        public VocabularyKey SocialMediaInformationKloutScore { get; private set; }

        public VocabularyKey AnalyticsInformationFirstReferringSite { get; private set; }

        public VocabularyKey ContactInformationMessage { get; private set; }

        public VocabularyKey SocialMediaInformationPhoto { get; private set; }

        public VocabularyKey ContactInformationCloseDate { get; private set; }

        public VocabularyKey AnalyticsInformationLastReferringSite { get; private set; }

        public VocabularyKey AnalyticsInformationAveragePageviews { get; private set; }

        public VocabularyKey ContactInformationLifecycleStage { get; private set; }

        public VocabularyKey AnalyticsInformationEventRevenue { get; private set; }

        public VocabularyKey ContactInformationBecameaLeadDate { get; private set; }

        public VocabularyKey ContactInformationBecameaMarketingQualifiedLeadDate { get; private set; }

        public VocabularyKey ContactInformationBecameanOpportunityDate { get; private set; }

        public VocabularyKey ContactInformationIPAddress { get; private set; }

        public VocabularyKey ContactInformationCreateDate { get; private set; }

        public VocabularyKey ContactInformationBecameaSalesQualifiedLeadDate { get; private set; }

        public VocabularyKey ContactInformationBecameanEvangelistDate { get; private set; }

        public VocabularyKey ContactInformationHubSpotScore { get; private set; }

        public VocabularyKey ContactInformationCompanyName { get; private set; }

        public VocabularyKey ContactInformationBecameaCustomerDate { get; private set; }

        public VocabularyKey ContactInformationBecameaSubscriberDate { get; private set; }

        public VocabularyKey ContactInformationWebsiteURL { get; private set; }

        public VocabularyKey ContactInformationBecameanOtherLifecycleDate { get; private set; }

        public VocabularyKey ContactInformationNumberofEmployees { get; private set; }

        public VocabularyKey ContactInformationAnnualRevenue { get; private set; }

        public VocabularyKey ContactInformationIndustry { get; private set; }

        public VocabularyKey ContactInformationAssociatedCompanyLastUpdated { get; private set; } 
    }
}
