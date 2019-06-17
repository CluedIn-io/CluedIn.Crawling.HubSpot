using System;
using System.Collections.Generic;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class CompanyClueProducer : BaseClueProducer<Company>
    {
        private readonly IClueFactory _factory;
        private readonly IHubSpotImageFetcher _imageFetcher;

        public CompanyClueProducer(IClueFactory factory, IHubSpotImageFetcher imageFetcher)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _imageFetcher = imageFetcher ?? throw new ArgumentNullException(nameof(imageFetcher));
        }

        protected override Clue MakeClueImpl(Company input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Organization, input.companyId.ToString(), accountId);
            
            var data = clue.Data.EntityData;

            data.Name = input.companyId.PrintIfAvailable();

            if (input.portalId != null)
                data.Uri = new Uri($"https://app.hubspot.com/sales/{input.portalId}/company/{input.companyId}/");

            data.Properties[HubSpotVocabulary.Company.CompanyId] = input.companyId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Company.IsDeleted] = input.isDeleted.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Company.PortalId] = input.portalId.PrintIfAvailable();

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => input.portalId.Value.ToString(), s => "HubSpot");

            if (input.properties != null)
            {
                var properties = JsonUtility.Deserialize<Dictionary<string, Property>>(JsonUtility.Serialize(input.properties));

                foreach (var property in properties)
                {
                    if (property.Key == null || property.Value?.Value == null)
                        continue;

                    if (long.TryParse(property.Value.Value, out long d))
                    {
                        try
                        {
                            DateUtilities.EpochRef.AddMilliseconds(d);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    if (property.Key == "name")
                    {
                        data.Name = property.Value.Value;
                    }
                    else if (property.Key == "description")
                    {
                        data.Description = property.Value.Value;
                    }
                    else if (property.Key == "zip")
                    {
                        data.Properties[HubSpotVocabulary.Company.Zip] = property.Value.Value;
                    }
                    else if (property.Key == "country")
                    {
                        data.Properties[HubSpotVocabulary.Company.Country] = property.Value.Value;
                    }
                    else if (property.Key == "website")
                    {
                        data.Properties[HubSpotVocabulary.Company.Website] = property.Value.Value;
                    }
                    else if (property.Key == "days_to_close")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationDaystoClose] = property.Value.Value;
                    }
                    else if (property.Key == "address")
                    {
                        data.Properties[HubSpotVocabulary.Company.Address] = property.Value.Value;
                    }
                    else if (property.Key == "city")
                    {
                        data.Properties[HubSpotVocabulary.Company.City] = property.Value.Value;
                    }
                    else if (property.Key == "twitterhandle")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationTwitterHandle] = property.Value.Value;
                    }
                    else if (property.Key == "phone")
                    {
                        data.Properties[HubSpotVocabulary.Company.Phone] = property.Value.Value;
                    }
                    else if (property.Key == "state")
                    {
                        data.Properties[HubSpotVocabulary.Company.State] = property.Value.Value;
                    }
                    else if (property.Key == "company_notes")
                    {
                        data.Properties[HubSpotVocabulary.Company.Notes] = property.Value.Value;
                    }
                    else if (property.Key == "num_associated_contacts")
                    {
                        data.Properties[HubSpotVocabulary.Company.AssociateContactsCounts] = property.Value.Value;
                    }
                    else if (property.Key == "is_public")
                    {
                        data.Properties[HubSpotVocabulary.Company.IsPublic] = property.Value.Value;
                    }
                    else if (property.Key == "domain")
                    {
                        data.Properties[HubSpotVocabulary.Company.EmailDomain] = property.Value.Value;
                    }
                    else if (property.Key == "integrations")
                    {
                        data.Properties[HubSpotVocabulary.Company.Integrations] = property.Value.Value;
                    }
                    else if (property.Key == "createdate")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                        else
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationCreateDate] = property.Value.Value;
                    }
                    else if (property.Key == "createddate")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                        else
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationCreateDate] = property.Value.Value;
                    }
                    else if (property.Key == "hs_lastmodifieddate")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                        else
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationLastModifiedDate] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_source")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsSource] = property.Value.Value;
                    }
                    else if (property.Key == "about_us")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationAboutUs] = property.Value.Value;
                    }
                    else if (property.Key == "facebookfans")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationFacebookFans] = property.Value.Value;
                    }
                    else if (property.Key == "photo")
                    {
                        var previewImagePart = _imageFetcher.FetchAsRawDataPart(property.Value.Value, "/RawData/PreviewImage", "preview_{0}".FormatWith(clue.OriginEntityCode.Key));
                        if (previewImagePart != null)
                        {
                            clue.Details.RawData.Add(previewImagePart);
                            clue.Data.EntityData.PreviewImage = new ImageReferencePart(previewImagePart);
                        }
                    }
                    else if (property.Key == "first_deal_created_date")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationFirstDealCreatedDate] = property.Value.Value;
                    }
                    else if (property.Key == "founded_year")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationYearFounded] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_first_timestamp")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.Properties[HubSpotVocabulary.Company.AnalyticsInformationTimeFirstSeen] = DateUtilities.EpochRef.AddMilliseconds(date).ToString();
                    }
                    else if (property.Key == "hs_analytics_first_touch_converting_campaign")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationFirstTouchConvertingCampaign] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_first_visit_timestamp")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationTimeofFirstVisit] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_last_timestamp")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationTimeLastSeen] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_last_touch_converting_campaign")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationLastTouchConvertingCampaign] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_last_visit_timestamp")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationTimeofLastSession] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_num_page_views")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationNumberofPageviews] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_num_visits")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationNumberofVisits] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_source")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationOriginalSourceType] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_source_data_1")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationOriginalSourceData1] = property.Value.Value;
                    }
                    else if (property.Key == "hs_analytics_source_data_2")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationOriginalSourceData2] = property.Value.Value;
                    }
                    else if (property.Key == "hs_avatar_filemanager_key")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationAvatarFileManagerkey] = property.Value.Value;
                    }
                    else if (property.Key == "hubspot_owner_assigneddate")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationOwnerAssignedDate] = DateUtilities.EpochRef.AddMilliseconds(date).ToString();
                    }
                    else if (property.Key == "num_associated_contacts")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationAssociatedContacts] = property.Value.Value;
                    }
                    else if (property.Key == "num_associated_deals")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationAssociatedDeals] = property.Value.Value;
                    }
                    else if (property.Key == "recent_deal_amount")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationRecentDealAmount] = property.Value.Value;
                    }
                    else if (property.Key == "recent_deal_close_date")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationRecentDealCloseDate] = DateUtilities.EpochRef.AddMilliseconds(date).ToString();
                    }
                    else if (property.Key == "timezone")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationTimeZone] = property.Value.Value;
                    }
                    else if (property.Key == "total_money_raised")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationTotalMoneyRaised] = property.Value.Value;
                    }
                    else if (property.Key == "total_revenue")
                    {
                        if (input.Currency != null && !string.IsNullOrEmpty(property.Value.Value))
                            property.Value.Value = property.Value.Value + input.Currency;
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationTotalRevenue] = property.Value.Value;
                    }
                    else if (property.Key == "name")
                    {
                        if (!string.IsNullOrEmpty(property.Value.Value))
                            data.Name = property.Value.Value;
                    }
                    else if (property.Key == "twitterhandle")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationTwitterHandle] = property.Value.Value;
                    }
                    else if (property.Key == "phone")
                    {
                        if (!string.IsNullOrEmpty(property.Value.Value))
                            data.Properties[HubSpotVocabulary.Company.Phone] = property.Value.Value;
                    }
                    else if (property.Key == "twitterbio")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationTwitterBio] = property.Value.Value;
                    }
                    else if (property.Key == "twitterfollowers")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationTwitterFollowers] = property.Value.Value;
                    }
                    else if (property.Key == "address")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationStreetAddress] = property.Value.Value;
                    }
                    else if (property.Key == "address2")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationStreetAddress2] = property.Value.Value;
                    }
                    else if (property.Key == "facebook_company_page")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationFacebookCompanyPage] = property.Value.Value;
                    }
                    else if (property.Key == "city")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationCity] = property.Value.Value;
                    }
                    else if (property.Key == "linkedin_company_page")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationLinkedInCompanyPage] = property.Value.Value;
                    }
                    else if (property.Key == "linkedinbio")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationLinkedInBio] = property.Value.Value;
                    }
                    else if (property.Key == "state")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationStateRegion] = property.Value.Value;
                    }
                    else if (property.Key == "googleplus_page")
                    {
                        data.Properties[HubSpotVocabulary.Company.SocialMediaInformationGooglePlusPage] = property.Value.Value;
                    }
                    else if (property.Key == "engagements_last_meeting_booked")
                    {
                        data.Properties[HubSpotVocabulary.Company.LastMeetingBooked] = property.Value.Value;
                    }
                    else if (property.Key == "engagements_last_meeting_booked_campaign")
                    {
                        data.Properties[HubSpotVocabulary.Company.LastMeetingBookedCampaign] = property.Value.Value;
                    }
                    else if (property.Key == "engagements_last_meeting_booked_medium")
                    {
                        data.Properties[HubSpotVocabulary.Company.LastMeetingBookedMedium] = property.Value.Value;
                    }
                    else if (property.Key == "engagements_last_meeting_booked_source")
                    {
                        data.Properties[HubSpotVocabulary.Company.LastMeetingBookedSource] = property.Value.Value;
                    }
                    else if (property.Key == "hubspot_owner_id")
                    {
                        if (property.Value != null)
                            _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, input, s => property.Value.Value.ToString());
                    }
                    else if (property.Key == "notes_last_contacted")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationLastContacted] = property.Value.Value;
                    }
                    else if (property.Key == "notes_last_updated")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationLastActivityDate] = property.Value.Value;
                    }
                    else if (property.Key == "notes_next_activity_date")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationNextActivityDate] = DateUtilities.EpochRef.AddMilliseconds(date).ToString();
                    }
                    else if (property.Key == "num_contacted_notes")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationNumberoftimescontacted] = property.Value.Value;
                    }
                    else if (property.Key == "num_notes")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationNumberofSalesActivities] = property.Value.Value;
                    }
                    else if (property.Key == "zip")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationPostalCode] = property.Value.Value;
                    }
                    else if (property.Key == "country")
                    {
                        if (!string.IsNullOrEmpty(property.Value.Value))
                            data.Properties[HubSpotVocabulary.Company.Country] = property.Value.Value;
                    }
                    else if (property.Key == "hubspot_team_id")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationHubSpotTeam] = property.Value.Value;
                    }
                    else if (property.Key == "website")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationWebsiteURL] = property.Value.Value;
                    }
                    else if (property.Key == "domain")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationCompanyDomainName] = property.Value.Value;
                    }
                    else if (property.Key == "numberofemployees")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationNumberofEmployees] = property.Value.Value;
                    }
                    else if (property.Key == "industry")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationIndustry] = property.Value.Value;
                    }
                    else if (property.Key == "annualrevenue")
                    {
                        if (input.Currency != null && !string.IsNullOrEmpty(property.Value.Value))
                            property.Value.Value = property.Value.Value + input.Currency;
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationAnnualRevenue] = property.Value.Value;
                    }
                    else if (property.Key == "lifecyclestage")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationLifecycleStage] = property.Value.Value;
                    }
                    else if (property.Key == "hs_lead_status")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationLeadStatus] = property.Value.Value;
                    }
                    else if (property.Key == "hs_parent_company_id")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationParentCompany] = property.Value.Value;
                    }
                    else if (property.Key == "type")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationType] = property.Value.Value;
                    }
                    else if (property.Key == "description")
                    {
                        if (!string.IsNullOrEmpty(property.Value.Value))
                            data.Description = property.Value.Value;
                    }
                    else if (property.Key == "hs_num_child_companies")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationNumberOfChildCompanies] = property.Value.Value;
                    }
                    else if (property.Key == "closedate")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationCloseDate] = DateUtilities.EpochRef.AddMilliseconds(date).ToString();
                    }
                    else if (property.Key == "first_contact_createdate")
                    {
                        if (long.TryParse(property.Value.Value, out long date))
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationFirstContactCreateDate] = DateUtilities.EpochRef.AddMilliseconds(date).ToString();
                    }
                    else if (property.Key == "days_to_close")
                    {
                        data.Properties[HubSpotVocabulary.Company.AnalyticsInformationDaystoClose] = property.Value.Value;
                    }
                    else if (property.Key == "web_technologies")
                    {
                        data.Properties[HubSpotVocabulary.Company.CompanyInformationWebTechnologies] = property.Value.Value;
                    }
                    else if (property.Key == "company_address")
                    {
                        if (!string.IsNullOrEmpty(property.Value.Value))
                            data.Properties[HubSpotVocabulary.Company.Address] = property.Value.Value;
                    }
                    else
                    {
                        data.Properties[string.Format("hubspot.company.custom-{0}", property.Key)] = property.Value.Value;
                    }

                    if (properties.TryGetValue("photo", out Property photo))
                    {
                        var previewImagePart = _imageFetcher.FetchAsRawDataPart(photo.Value, "/RawData/PreviewImage", "preview_{0}".FormatWith(data.Name));
                        if (previewImagePart != null)
                        {
                            clue.Details.RawData.Add(previewImagePart);
                            clue.Data.EntityData.PreviewImage = new ImageReferencePart(previewImagePart, 255, 255);
                        }
                    }
                }
            }

            return clue;
        }
    }
}
