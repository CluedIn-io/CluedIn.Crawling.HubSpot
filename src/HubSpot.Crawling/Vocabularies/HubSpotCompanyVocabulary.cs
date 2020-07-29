// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotCompanyVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotCompanyVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot company vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotCompanyVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubSpotCompanyVocabulary"/> class.
        /// </summary>
        public HubSpotCompanyVocabulary()
        {
            VocabularyName = "HubSpot Company";
            KeyPrefix      = "hubspot.company";
            KeySeparator   = ".";
            Grouping       = EntityType.Organization;

            CompanyId               = Add(new VocabularyKey("CompanyId", VocabularyKeyVisibility.Hidden));
            IsDeleted               = Add(new VocabularyKey("IsDeleted"));
            PortalId                = Add(new VocabularyKey("PortalId", VocabularyKeyVisibility.Hidden));
            Zip                     = Add(new VocabularyKey("Zip"));
            Country                 = Add(new VocabularyKey("Country", VocabularyKeyDataType.GeographyCountry));
            Website                 = Add(new VocabularyKey("Website", VocabularyKeyDataType.Uri));
            Address                 = Add(new VocabularyKey("Address"));
            City                    = Add(new VocabularyKey("City", VocabularyKeyDataType.GeographyCity));
            Phone                   = Add(new VocabularyKey("Phone", VocabularyKeyDataType.PhoneNumber));
            State                   = Add(new VocabularyKey("State"));
            FirstName               = Add(new VocabularyKey("FirstName"));
            LastName                = Add(new VocabularyKey("LastName"));
            Company                 = Add(new VocabularyKey("Company"));
            Salutation              = Add(new VocabularyKey("Salutation"));
            IpAddress               = Add(new VocabularyKey("IPAddress", VocabularyKeyDataType.IPAddress, VocabularyKeyVisibility.Hidden));
            Fax                     = Add(new VocabularyKey("Fax", VocabularyKeyDataType.PhoneNumber));
            OwnerName               = Add(new VocabularyKey("OwnerName"));
            OwnerEmail              = Add(new VocabularyKey("OwnerEmail", VocabularyKeyDataType.Email));
            Message                 = Add(new VocabularyKey("Message"));
            JobTitle                = Add(new VocabularyKey("JobTitle"));
            AnalyticsSource         = Add(new VocabularyKey("AnalyticsSource"));
            Integrations            = Add(new VocabularyKey("Integrations"));
            EmailDomain             = Add(new VocabularyKey("EmailDomain"));
            IsPublic                = Add(new VocabularyKey("IsPublic"));
            AssociateContactsCounts = Add(new VocabularyKey("AssociateContactsCounts"));
            Notes                   = Add(new VocabularyKey("Notes"));

            Properties              = Add(new VocabularyKey("Properties", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));

            AddMapping(EmailDomain, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.EmailDomainNames);   

            AddMapping(Fax, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Fax);
            AddMapping(JobTitle, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.JobTitle);

            AddMapping(FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.FirstName);
            AddMapping(LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.LastName);
            AddMapping(Salutation, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Title);
            AddMapping(Zip, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressZipCode);
            AddMapping(Country, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCountryCode);
            AddMapping(Website, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website);
            AddMapping(Address, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Address);
            AddMapping(City, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCity);
          
            AddMapping(Phone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.PhoneNumber);
            AddMapping(State, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressState);
            //this.AddMapping(this.LinkedInBio, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressZipCode);
            // TODO: map keys to CluedIn vocabulary

            LastMeetingBooked                         = Add(new VocabularyKey("LastMeetingBooked"));
            LastMeetingBookedCampaign                 = Add(new VocabularyKey("LastMeetingBookedCampaign"));
            LastMeetingBookedMedium                   = Add(new VocabularyKey("LastMeetingBookedMedium"));
            LastMeetingBookedSource                   = Add(new VocabularyKey("LastMeetingBookedSource"));

            CompanyInformationAboutUs                 = Add(new VocabularyKey("AboutUs"));
            CompanyInformationFirstDealCreatedDate    = Add(new VocabularyKey("FirstDealCreatedDate"));
            CompanyInformationYearFounded             = Add(new VocabularyKey("YearFounded"));
            CompanyInformationAvatarFileManagerkey    = Add(new VocabularyKey("AvatarFileManagerkey"));
            CompanyInformationLastModifiedDate        = Add(new VocabularyKey("LastModifiedDate"));
            CompanyInformationOwnerAssignedDate       = Add(new VocabularyKey("OwnerAssignedDate"));
            CompanyInformationAssociatedContacts      = Add(new VocabularyKey("AssociatedContacts"));
            CompanyInformationAssociatedDeals         = Add(new VocabularyKey("AssociatedDeals"));
            CompanyInformationRecentDealAmount        = Add(new VocabularyKey("RecentDealAmount"));
            CompanyInformationRecentDealCloseDate     = Add(new VocabularyKey("RecentDealCloseDate"));
            CompanyInformationTimeZone                = Add(new VocabularyKey("TimeZone"));
            CompanyInformationTotalMoneyRaised        = Add(new VocabularyKey("TotalMoneyRaised"));
            CompanyInformationTotalRevenue            = Add(new VocabularyKey("TotalRevenue"));
            CompanyInformationStreetAddress           = Add(new VocabularyKey("StreetAddress"));
            CompanyInformationStreetAddress2          = Add(new VocabularyKey("StreetAddress2"));
            CompanyInformationStateRegion             = Add(new VocabularyKey("State/Region"));
            CompanyInformationHubSpotOwner            = Add(new VocabularyKey("HubSpotOwner"));
            CompanyInformationLastContacted           = Add(new VocabularyKey("LastContacted"));
            CompanyInformationLastActivityDate        = Add(new VocabularyKey("LastActivityDate"));
            CompanyInformationNextActivityDate        = Add(new VocabularyKey("NextActivityDate"));
            CompanyInformationNumberoftimescontacted  = Add(new VocabularyKey("Numberoftimescontacted"));
            CompanyInformationNumberofSalesActivities = Add(new VocabularyKey("NumberofSalesActivities"));
            CompanyInformationPostalCode              = Add(new VocabularyKey("PostalCode"));
            CompanyInformationHubSpotTeam             = Add(new VocabularyKey("HubSpotTeam"));
            CompanyInformationWebsiteURL              = Add(new VocabularyKey("WebsiteURL"));
            CompanyInformationCompanyDomainName       = Add(new VocabularyKey("CompanyDomainName"));
            CompanyInformationNumberofEmployees       = Add(new VocabularyKey("NumberofEmployees"));
            CompanyInformationIndustry                = Add(new VocabularyKey("Industry"));
            CompanyInformationAnnualRevenue           = Add(new VocabularyKey("AnnualRevenue"));
            CompanyInformationLifecycleStage          = Add(new VocabularyKey("LifecycleStage"));
            CompanyInformationLeadStatus              = Add(new VocabularyKey("LeadStatus"));
            CompanyInformationParentCompany           = Add(new VocabularyKey("ParentCompany"));
            CompanyInformationType                    = Add(new VocabularyKey("Type"));
            CompanyInformationNumberOfChildCompanies  = Add(new VocabularyKey("Numberofchildcompanies"));
            CompanyInformationCreateDate              = Add(new VocabularyKey("CreateDate"));
            CompanyInformationCloseDate               = Add(new VocabularyKey("CloseDate"));
            CompanyInformationFirstContactCreateDate  = Add(new VocabularyKey("FirstContactCreateDate"));
            CompanyInformationWebTechnologies         = Add(new VocabularyKey("WebTechnologies"));
            CompanyInformationCity                    = Add(new VocabularyKey("Information.City"));

            AddMapping(CompanyInformationTimeZone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.TimeZone);
            AddMapping(CompanyInformationIndustry, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Industry);
            AddMapping(CompanyInformationAnnualRevenue, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AnnualRevenue);

            AddGroup("HubSpot Company Analytics Information Details", group =>
            {
                AnalyticsInformationFirstTouchConvertingCampaign = group.Add(new VocabularyKey("FirstTouchConvertingCampaign"));
                AnalyticsInformationLastTouchConvertingCampaign  = group.Add(new VocabularyKey("LastTouchConvertingCampaign"));
                AnalyticsInformationNumberofPageviews            = group.Add(new VocabularyKey("NumberOfPageviews"));
                AnalyticsInformationNumberofVisits               = group.Add(new VocabularyKey("NumberOfVisits"));
                AnalyticsInformationTimeFirstSeen                = group.Add(new VocabularyKey("TimeFirstSeen"));
                AnalyticsInformationTimeofFirstVisit             = group.Add(new VocabularyKey("TimeOfFirstVisit"));
                AnalyticsInformationTimeLastSeen                 = group.Add(new VocabularyKey("TimeLastSeen"));
                AnalyticsInformationTimeofLastSession            = group.Add(new VocabularyKey("TimeOfLastSession"));              
                AnalyticsInformationOriginalSourceType           = group.Add(new VocabularyKey("OriginalSourceType"));
                AnalyticsInformationOriginalSourceData1          = group.Add(new VocabularyKey("OriginalSourceData1"));
                AnalyticsInformationOriginalSourceData2          = group.Add(new VocabularyKey("OriginalSourceData2"));
                AnalyticsInformationDaystoClose                  = group.Add(new VocabularyKey("DaysToClose"));
            });

            AddGroup("HubSpot Company Social Media Details", group =>
            {
                SocialMediaInformationLinkedInBio         = group.Add(new VocabularyKey("LinkedInBio"));
                SocialMediaInformationTwitterBio          = group.Add(new VocabularyKey("TwitterBio"));
                SocialMediaInformationFacebookFans        = group.Add(new VocabularyKey("FacebookFans"));
                SocialMediaInformationTwitterHandle       = group.Add(new VocabularyKey("TwitterHandle"));              
                SocialMediaInformationTwitterFollowers    = group.Add(new VocabularyKey("TwitterFollowers"));
                SocialMediaInformationFacebookCompanyPage = group.Add(new VocabularyKey("FacebookCompanyPage"));
                SocialMediaInformationLinkedInCompanyPage = group.Add(new VocabularyKey("LinkedInCompanyPage"));
                SocialMediaInformationGooglePlusPage      = group.Add(new VocabularyKey("GooglePlusPage"));
            });

            AddMapping(SocialMediaInformationTwitterHandle, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Twitter);
            AddMapping(SocialMediaInformationFacebookCompanyPage, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialFacebook);
            AddMapping(SocialMediaInformationLinkedInCompanyPage, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialLinkedIn);

        }



        public VocabularyKey AnalyticsSource { get; set; }
        public VocabularyKey Integrations { get; set; }
        public VocabularyKey EmailDomain { get; set; }
        public VocabularyKey IsPublic { get; set; }
        public VocabularyKey AssociateContactsCounts { get; set; }
        public VocabularyKey Notes { get; set; }

        public VocabularyKey Fax { get; set; }
        public VocabularyKey OwnerName { get; set; }
        public VocabularyKey OwnerEmail { get; set; }
        public VocabularyKey Message { get; set; }
        public VocabularyKey JobTitle { get; set; }


        public VocabularyKey FirstName { get; set; }
        public VocabularyKey LastName { get; set; }
        public VocabularyKey Company { get; set; }
        public VocabularyKey Salutation { get; set; }
        public VocabularyKey IpAddress { get; set; }

        public VocabularyKey CompanyId { get; private set; }

        public VocabularyKey Zip { get; private set; }

        public VocabularyKey IsDeleted { get; private set; }

        public VocabularyKey PortalId { get; private set; }

        public VocabularyKey Properties { get; private set; }
        public VocabularyKey Country { get; private set; }
        public VocabularyKey Website { get; private set; }
        public VocabularyKey Address { get; private set; }
        public VocabularyKey City { get; private set; }
        public VocabularyKey Phone { get; private set; }
        public VocabularyKey State { get; private set; }

        public VocabularyKey CompanyInformationAboutUs { get; private set; }

        public VocabularyKey SocialMediaInformationFacebookFans { get; private set; }

        public VocabularyKey CompanyInformationFirstDealCreatedDate { get; private set; }

        public VocabularyKey CompanyInformationYearFounded { get; private set; }

        public VocabularyKey AnalyticsInformationTimeFirstSeen { get; private set; }

        public VocabularyKey AnalyticsInformationFirstTouchConvertingCampaign { get; private set; }

        public VocabularyKey AnalyticsInformationTimeofFirstVisit { get; private set; }

        public VocabularyKey AnalyticsInformationTimeLastSeen { get; private set; }

        public VocabularyKey AnalyticsInformationLastTouchConvertingCampaign { get; private set; }

        public VocabularyKey AnalyticsInformationTimeofLastSession { get; private set; }

        public VocabularyKey AnalyticsInformationNumberofPageviews { get; private set; }

        public VocabularyKey AnalyticsInformationNumberofVisits { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceType { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceData1 { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceData2 { get; private set; }

        public VocabularyKey CompanyInformationAvatarFileManagerkey { get; private set; }

        public VocabularyKey CompanyInformationLastModifiedDate { get; private set; }

        public VocabularyKey CompanyInformationOwnerAssignedDate { get; private set; }

        public VocabularyKey CompanyInformationAssociatedContacts { get; private set; }

        public VocabularyKey CompanyInformationAssociatedDeals { get; private set; }

        public VocabularyKey CompanyInformationRecentDealAmount { get; private set; }

        public VocabularyKey CompanyInformationRecentDealCloseDate { get; private set; }

        public VocabularyKey CompanyInformationTimeZone { get; private set; }

        public VocabularyKey CompanyInformationTotalMoneyRaised { get; private set; }

        public VocabularyKey CompanyInformationTotalRevenue { get; private set; }

        public VocabularyKey SocialMediaInformationTwitterHandle { get; private set; }

        public VocabularyKey SocialMediaInformationTwitterBio { get; private set; }

        public VocabularyKey SocialMediaInformationTwitterFollowers { get; private set; }

        public VocabularyKey CompanyInformationStreetAddress { get; private set; }

        public VocabularyKey CompanyInformationStreetAddress2 { get; private set; }

        public VocabularyKey SocialMediaInformationFacebookCompanyPage { get; private set; }

        public VocabularyKey CompanyInformationCity { get; private set; }

        public VocabularyKey SocialMediaInformationLinkedInCompanyPage { get; private set; }

        public VocabularyKey SocialMediaInformationLinkedInBio { get; private set; }

        public VocabularyKey CompanyInformationStateRegion { get; private set; }

        public VocabularyKey SocialMediaInformationGooglePlusPage { get; private set; }

        public VocabularyKey LastMeetingBooked { get; private set; }

        public VocabularyKey LastMeetingBookedCampaign { get; private set; }

        public VocabularyKey LastMeetingBookedMedium { get; private set; }

        public VocabularyKey LastMeetingBookedSource { get; private set; }

        public VocabularyKey CompanyInformationHubSpotOwner { get; private set; }

        public VocabularyKey CompanyInformationLastContacted { get; private set; }

        public VocabularyKey CompanyInformationLastActivityDate { get; private set; }

        public VocabularyKey CompanyInformationNextActivityDate { get; private set; }

        public VocabularyKey CompanyInformationNumberoftimescontacted { get; private set; }

        public VocabularyKey CompanyInformationNumberofSalesActivities { get; private set; }

        public VocabularyKey CompanyInformationPostalCode { get; private set; }

        public VocabularyKey CompanyInformationHubSpotTeam { get; private set; }

        public VocabularyKey CompanyInformationWebsiteURL { get; private set; }

        public VocabularyKey CompanyInformationCompanyDomainName { get; private set; }

        public VocabularyKey CompanyInformationNumberofEmployees { get; private set; }

        public VocabularyKey CompanyInformationIndustry { get; private set; }

        public VocabularyKey CompanyInformationAnnualRevenue { get; private set; }

        public VocabularyKey CompanyInformationLifecycleStage { get; private set; }

        public VocabularyKey CompanyInformationLeadStatus { get; private set; }

        public VocabularyKey CompanyInformationParentCompany { get; private set; }

        public VocabularyKey CompanyInformationType { get; private set; }

        public VocabularyKey CompanyInformationNumberOfChildCompanies { get; private set; }

        public VocabularyKey CompanyInformationCreateDate { get; private set; }

        public VocabularyKey CompanyInformationCloseDate { get; private set; }

        public VocabularyKey CompanyInformationFirstContactCreateDate { get; private set; }

        public VocabularyKey AnalyticsInformationDaystoClose { get; private set; }

        public VocabularyKey CompanyInformationWebTechnologies { get; private set; }

    }
}
