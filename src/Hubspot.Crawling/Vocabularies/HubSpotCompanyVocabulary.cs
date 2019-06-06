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
            this.VocabularyName = "HubSpot Company";
            this.KeyPrefix      = "hubspot.company";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Organization;

            this.CompanyId               = this.Add(new VocabularyKey("CompanyId", VocabularyKeyVisiblity.Hidden));
            this.IsDeleted               = this.Add(new VocabularyKey("IsDeleted"));
            this.PortalId                = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.Zip                     = this.Add(new VocabularyKey("Zip"));
            this.Country                 = this.Add(new VocabularyKey("Country", VocabularyKeyDataType.GeographyCountry));
            this.Website                 = this.Add(new VocabularyKey("Website", VocabularyKeyDataType.Uri));
            this.Address                 = this.Add(new VocabularyKey("Address"));
            this.City                    = this.Add(new VocabularyKey("City", VocabularyKeyDataType.GeographyCity));
            this.Phone                   = this.Add(new VocabularyKey("Phone", VocabularyKeyDataType.PhoneNumber));
            this.State                   = this.Add(new VocabularyKey("State"));
            this.FirstName               = this.Add(new VocabularyKey("FirstName"));
            this.LastName                = this.Add(new VocabularyKey("LastName"));
            this.Company                 = this.Add(new VocabularyKey("Company"));
            this.Salutation              = this.Add(new VocabularyKey("Salutation"));
            this.IpAddress               = this.Add(new VocabularyKey("IPAddress", VocabularyKeyDataType.IPAddress, VocabularyKeyVisiblity.Hidden));
            this.Fax                     = this.Add(new VocabularyKey("Fax", VocabularyKeyDataType.PhoneNumber));
            this.OwnerName               = this.Add(new VocabularyKey("OwnerName"));
            this.OwnerEmail              = this.Add(new VocabularyKey("OwnerEmail", VocabularyKeyDataType.Email));
            this.Message                 = this.Add(new VocabularyKey("Message"));
            this.JobTitle                = this.Add(new VocabularyKey("JobTitle"));
            this.AnalyticsSource         = this.Add(new VocabularyKey("AnalyticsSource"));
            this.Integrations            = this.Add(new VocabularyKey("Integrations"));
            this.EmailDomain             = this.Add(new VocabularyKey("EmailDomain"));
            this.IsPublic                = this.Add(new VocabularyKey("IsPublic"));
            this.AssociateContactsCounts = this.Add(new VocabularyKey("AssociateContactsCounts"));
            this.Notes                   = this.Add(new VocabularyKey("Notes"));

            this.Properties              = this.Add(new VocabularyKey("Properties", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));

            this.AddMapping(this.EmailDomain, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.EmailDomainNames);   

            this.AddMapping(this.Fax, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Fax);
            this.AddMapping(this.JobTitle, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.JobTitle);

            this.AddMapping(this.FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.FirstName);
            this.AddMapping(this.LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.LastName);
            this.AddMapping(this.Salutation, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Title);
            this.AddMapping(this.Zip, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressZipCode);
            this.AddMapping(this.Country, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCountryCode);
            this.AddMapping(this.Website, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website);
            this.AddMapping(this.Address, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Address);
            this.AddMapping(this.City, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCity);
          
            this.AddMapping(this.Phone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.PhoneNumber);
            this.AddMapping(this.State, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressState);
            //this.AddMapping(this.LinkedInBio, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressZipCode);
            // TODO: map keys to CluedIn vocabulary

            this.LastMeetingBooked                         = this.Add(new VocabularyKey("LastMeetingBooked"));
            this.LastMeetingBookedCampaign                 = this.Add(new VocabularyKey("LastMeetingBookedCampaign"));
            this.LastMeetingBookedMedium                   = this.Add(new VocabularyKey("LastMeetingBookedMedium"));
            this.LastMeetingBookedSource                   = this.Add(new VocabularyKey("LastMeetingBookedSource"));

            this.CompanyInformationAboutUs                 = this.Add(new VocabularyKey("AboutUs"));
            this.CompanyInformationFirstDealCreatedDate    = this.Add(new VocabularyKey("FirstDealCreatedDate"));
            this.CompanyInformationYearFounded             = this.Add(new VocabularyKey("YearFounded"));
            this.CompanyInformationAvatarFileManagerkey    = this.Add(new VocabularyKey("AvatarFileManagerkey"));
            this.CompanyInformationLastModifiedDate        = this.Add(new VocabularyKey("LastModifiedDate"));
            this.CompanyInformationOwnerAssignedDate       = this.Add(new VocabularyKey("OwnerAssignedDate"));
            this.CompanyInformationAssociatedContacts      = this.Add(new VocabularyKey("AssociatedContacts"));
            this.CompanyInformationAssociatedDeals         = this.Add(new VocabularyKey("AssociatedDeals"));
            this.CompanyInformationRecentDealAmount        = this.Add(new VocabularyKey("RecentDealAmount"));
            this.CompanyInformationRecentDealCloseDate     = this.Add(new VocabularyKey("RecentDealCloseDate"));
            this.CompanyInformationTimeZone                = this.Add(new VocabularyKey("TimeZone"));
            this.CompanyInformationTotalMoneyRaised        = this.Add(new VocabularyKey("TotalMoneyRaised"));
            this.CompanyInformationTotalRevenue            = this.Add(new VocabularyKey("TotalRevenue"));
            this.CompanyInformationStreetAddress           = this.Add(new VocabularyKey("StreetAddress"));
            this.CompanyInformationStreetAddress2          = this.Add(new VocabularyKey("StreetAddress2"));
            this.CompanyInformationStateRegion             = this.Add(new VocabularyKey("State/Region"));
            this.CompanyInformationHubSpotOwner            = this.Add(new VocabularyKey("HubSpotOwner"));
            this.CompanyInformationLastContacted           = this.Add(new VocabularyKey("LastContacted"));
            this.CompanyInformationLastActivityDate        = this.Add(new VocabularyKey("LastActivityDate"));
            this.CompanyInformationNextActivityDate        = this.Add(new VocabularyKey("NextActivityDate"));
            this.CompanyInformationNumberoftimescontacted  = this.Add(new VocabularyKey("Numberoftimescontacted"));
            this.CompanyInformationNumberofSalesActivities = this.Add(new VocabularyKey("NumberofSalesActivities"));
            this.CompanyInformationPostalCode              = this.Add(new VocabularyKey("PostalCode"));
            this.CompanyInformationHubSpotTeam             = this.Add(new VocabularyKey("HubSpotTeam"));
            this.CompanyInformationWebsiteURL              = this.Add(new VocabularyKey("WebsiteURL"));
            this.CompanyInformationCompanyDomainName       = this.Add(new VocabularyKey("CompanyDomainName"));
            this.CompanyInformationNumberofEmployees       = this.Add(new VocabularyKey("NumberofEmployees"));
            this.CompanyInformationIndustry                = this.Add(new VocabularyKey("Industry"));
            this.CompanyInformationAnnualRevenue           = this.Add(new VocabularyKey("AnnualRevenue"));
            this.CompanyInformationLifecycleStage          = this.Add(new VocabularyKey("LifecycleStage"));
            this.CompanyInformationLeadStatus              = this.Add(new VocabularyKey("LeadStatus"));
            this.CompanyInformationParentCompany           = this.Add(new VocabularyKey("ParentCompany"));
            this.CompanyInformationType                    = this.Add(new VocabularyKey("Type"));
            this.CompanyInformationNumberOfChildCompanies  = this.Add(new VocabularyKey("Numberofchildcompanies"));
            this.CompanyInformationCreateDate              = this.Add(new VocabularyKey("CreateDate"));
            this.CompanyInformationCloseDate               = this.Add(new VocabularyKey("CloseDate"));
            this.CompanyInformationFirstContactCreateDate  = this.Add(new VocabularyKey("FirstContactCreateDate"));
            this.CompanyInformationWebTechnologies         = this.Add(new VocabularyKey("WebTechnologies"));
            this.CompanyInformationCity                    = this.Add(new VocabularyKey("Information.City"));

            this.AddMapping(this.CompanyInformationTimeZone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.TimeZone);
            this.AddMapping(this.CompanyInformationIndustry, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Industry);
            this.AddMapping(this.CompanyInformationAnnualRevenue, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AnnualRevenue);

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

            this.AddMapping(this.SocialMediaInformationTwitterHandle, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Twitter);
            this.AddMapping(this.SocialMediaInformationFacebookCompanyPage, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialFacebook);
            this.AddMapping(this.SocialMediaInformationLinkedInCompanyPage, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialLinkedIn);

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