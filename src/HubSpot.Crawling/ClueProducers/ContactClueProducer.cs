using System;
using System.Collections.Generic;
using System.Globalization;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class ContactClueProducer : BaseClueProducer<Contact>
    {
        private readonly IClueFactory _factory;
        private readonly IHubSpotFileFetcher _fileFetcher;

        public ContactClueProducer(IClueFactory factory, IHubSpotFileFetcher fileFetcher)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _fileFetcher = fileFetcher ?? throw new ArgumentNullException(nameof(fileFetcher));
        }

        protected override Clue MakeClueImpl(Contact input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Infrastructure.Contact, input.Vid.ToString(), accountId);

            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_001_Name_MustBeSet);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_001_MustExist);

            var data = clue.Data.EntityData;

            data.Name = input.ProfileUrl;

            if (input.AddedAt != null)
            {
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.AddedAt.Value);
            }

            if (input.CanonicalVid != null)
                data.Properties[HubSpotVocabulary.Contact.CanonicalVid] = input.CanonicalVid.Value.ToString(CultureInfo.InvariantCulture);
            if (input.FormSubmissions != null)
            {
                data.Properties[HubSpotVocabulary.Contact.FormSubmissions] = JsonUtility.Serialize(input.FormSubmissions);
            }

            if (input.IdentityProfiles != null)
            {
                try
                {
                    var identity = JsonUtility.Deserialize<IEnumerable<IdentityProfile>>(JsonUtility.Serialize(input.IdentityProfiles));

                    foreach (var identiy in identity)
                    {
                        foreach (var property in identiy.identities)
                        {
                            if (property.type == "EMAIL")
                            {
                                data.Properties[HubSpotVocabulary.Contact.Email] = property.value;
                                data.Name = property.value;
                            }
                            else if (property.type == "name")
                            {
                                data.Name = property.value;
                            }
                            else if (property.type == "zip")
                            {
                                data.Properties[HubSpotVocabulary.Company.Zip] = property.value;
                            }
                            else if (property.type == "name")
                            {
                                data.Name = property.value;
                            }
                            else if (property.type == "photo" && ! string.IsNullOrWhiteSpace(property.value))
                            {
                                var previewImagePart = _fileFetcher.FetchAsRawDataPart(property.value, "/RawData/PreviewImage", "preview_{0}".FormatWith(clue.OriginEntityCode.Key));
                                if (previewImagePart != null)
                                {
                                    clue.Details.RawData.Add(previewImagePart);
                                    clue.Data.EntityData.PreviewImage = new ImageReferencePart(previewImagePart);
                                }
                            }
                            else if (property.type == "zip")
                            {
                                data.Properties[HubSpotVocabulary.Company.Zip] = property.value;
                            }
                            else if (property.type == "firstname")
                            {
                                if (string.IsNullOrEmpty(input.FirstName))
                                    input.FirstName = property.value;
                                data.Properties[HubSpotVocabulary.Company.FirstName] = property.value;
                            }
                            else if (property.type == "lastname")
                            {
                                if (string.IsNullOrEmpty(input.LastName))
                                    input.LastName = property.value;
                                data.Properties[HubSpotVocabulary.Company.LastName] = property.value;
                            }
                            else if (property.type == "company")
                            {
                                data.Properties[HubSpotVocabulary.Company.Company] = property.value;
                            }
                            else if (property.type == "salutation")
                            {
                                data.Properties[HubSpotVocabulary.Company.Salutation] = property.value;
                            }
                            else if (property.type == "ipaddress")
                            {
                                data.Properties[HubSpotVocabulary.Company.IpAddress] = property.value;
                            }
                            else if (property.type == "country")
                            {
                                data.Properties[HubSpotVocabulary.Company.Country] = property.value;
                            }
                            else if (property.type == "website")
                            {
                                data.Properties[HubSpotVocabulary.Company.Website] = property.value;
                            }
                            else if (property.type == "address")
                            {
                                data.Properties[HubSpotVocabulary.Company.Address] = property.value;
                            }
                            else if (property.type == "city")
                            {
                                data.Properties[HubSpotVocabulary.Company.City] = property.value;
                            }
                            else if (property.type == "salutation")
                            {
                                data.Properties[HubSpotVocabulary.Company.Salutation] = property.value;
                            }
                            else if (property.type == "ipaddress")
                            {
                                data.Properties[HubSpotVocabulary.Company.IpAddress] = property.value;
                            }
                            else if (property.type == "country")
                            {
                                data.Properties[HubSpotVocabulary.Company.Country] = property.value;
                            }
                            else if (property.type == "website")
                            {
                                data.Properties[HubSpotVocabulary.Company.Website] = property.value;
                            }
                            else if (property.type == "address")
                            {
                                data.Properties[HubSpotVocabulary.Company.Address] = property.value;
                            }
                            else if (property.type == "city")
                            {
                                data.Properties[HubSpotVocabulary.Company.City] = property.value;
                            }
                            else if (property.type == "industry")
                            {
                                data.Properties[HubSpotVocabulary.Company.CompanyInformationIndustry] = property.value;
                            }
                            else if (property.type == "twitterhandle")
                            {
                                data.Properties[HubSpotVocabulary.Company.CompanyInformationIndustry] = property.value;
                            }
                            else if (property.type == "phone")
                            {
                                data.Properties[HubSpotVocabulary.Company.Phone] = property.value;
                            }
                            else if (property.type == "fax")
                            {
                                data.Properties[HubSpotVocabulary.Company.Fax] = property.value;
                            }
                            else if (property.type == "ownername")
                            {
                                data.Properties[HubSpotVocabulary.Company.OwnerName] = property.value;
                            }
                            else if (property.type == "owneremail")
                            {
                                data.Properties[HubSpotVocabulary.Company.OwnerEmail] = property.value;
                            }
                            else if (property.type == "annualrevenue")
                            {
                                data.Properties[HubSpotVocabulary.Company.CompanyInformationAnnualRevenue] = property.value;
                            }
                            else if (property.type == "lifecyclestage")
                            {
                                data.Properties[HubSpotVocabulary.Company.CompanyInformationLifecycleStage] = property.value;
                            }
                            else if (property.type == "message")
                            {
                                data.Properties[HubSpotVocabulary.Company.Message] = property.value;
                            }
                            else if (property.type == "state")
                            {
                                data.Properties[HubSpotVocabulary.Company.State] = property.value;
                            }
                            else if (property.type == "linkedinbio")
                            {
                                data.Properties[HubSpotVocabulary.Company.SocialMediaInformationLinkedInBio] = property.value;
                            }
                            else if (property.type == "jobtitle")
                            {
                                data.Properties[HubSpotVocabulary.Company.JobTitle] = property.value;
                            }
                            else if (property.type == "createddate")
                            {
                                if (long.TryParse(property.value, out long date))
                                    data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                            }
                            else if (property.type == "lastmodifieddate")
                            {
                                if (long.TryParse(property.value, out long date))
                                    data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                            }
                            else if (property.type == "LEAD_GUID")
                            {
                                // factory.CreateEntityReference(clue, EntityType.Sales.Lead, EntityEdgeType.PartOf, value, s => property.value);
                            }
                            else
                            {
                                data.Properties[string.Format("hubspot.contact.custom-{0}", property.type)] = property.value;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    var identity = JsonUtility.Deserialize<IdentityHook>(JsonUtility.Serialize(input.IdentityProfiles));
                    foreach (var property in identity.identities.values)
                    {
                        if (property.type == "EMAIL")
                        {
                            data.Properties[HubSpotVocabulary.Contact.Email] = property.value;
                            data.Name = property.value;
                        }
                        else if (property.type == "name")
                        {
                            data.Name = property.value;
                        }
                        else if (property.type == "photo" && ! string.IsNullOrWhiteSpace(property.value))
                        {
                            var previewImagePart = _fileFetcher.FetchAsRawDataPart(property.value, "/RawData/PreviewImage", "preview_{0}".FormatWith(clue.OriginEntityCode.Key));
                            if (previewImagePart != null)
                            {
                                clue.Details.RawData.Add(previewImagePart);
                                clue.Data.EntityData.PreviewImage = new ImageReferencePart(previewImagePart);
                            }
                        }
                        else if (property.type == "zip")
                        {
                            data.Properties[HubSpotVocabulary.Company.Zip] = property.value;
                        }
                        else if (property.type == "firstname")
                        {
                            if (string.IsNullOrEmpty(input.FirstName))
                                input.FirstName = property.value;
                            data.Properties[HubSpotVocabulary.Company.FirstName] = property.value;
                        }
                        else if (property.type == "lastname")
                        {
                            if (string.IsNullOrEmpty(input.LastName))
                                input.LastName = property.value;
                            data.Properties[HubSpotVocabulary.Company.LastName] = property.value;
                        }
                        else if (property.type == "company")
                        {
                            data.Properties[HubSpotVocabulary.Company.Company] = property.value;
                        }
                        else if (property.type == "salutation")
                        {
                            data.Properties[HubSpotVocabulary.Company.Salutation] = property.value;
                        }
                        else if (property.type == "ipaddress")
                        {
                            data.Properties[HubSpotVocabulary.Company.IpAddress] = property.value;
                        }
                        else if (property.type == "country")
                        {
                            data.Properties[HubSpotVocabulary.Company.Country] = property.value;
                        }
                        else if (property.type == "website")
                        {
                            data.Properties[HubSpotVocabulary.Company.Website] = property.value;
                        }
                        else if (property.type == "address")
                        {
                            data.Properties[HubSpotVocabulary.Company.Address] = property.value;
                        }
                        else if (property.type == "city")
                        {
                            data.Properties[HubSpotVocabulary.Company.City] = property.value;
                        }
                        else if (property.type == "industry")
                        {
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationIndustry] = property.value;
                        }
                        else if (property.type == "twitterhandle")
                        {
                            data.Properties[HubSpotVocabulary.Company.SocialMediaInformationTwitterHandle] = property.value;
                        }
                        else if (property.type == "phone")
                        {
                            data.Properties[HubSpotVocabulary.Company.Phone] = property.value;
                        }
                        else if (property.type == "fax")
                        {
                            data.Properties[HubSpotVocabulary.Company.Fax] = property.value;
                        }
                        else if (property.type == "ownername")
                        {
                            data.Properties[HubSpotVocabulary.Company.OwnerName] = property.value;
                        }
                        else if (property.type == "owneremail")
                        {
                            data.Properties[HubSpotVocabulary.Company.OwnerEmail] = property.value;
                        }
                        else if (property.type == "annualrevenue")
                        {
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationAnnualRevenue] = property.value;
                        }
                        else if (property.type == "lifecyclestage")
                        {
                            data.Properties[HubSpotVocabulary.Company.CompanyInformationLifecycleStage] = property.value;
                        }
                        else if (property.type == "message")
                        {
                            data.Properties[HubSpotVocabulary.Company.Message] = property.value;
                        }
                        else if (property.type == "state")
                        {
                            data.Properties[HubSpotVocabulary.Company.State] = property.value;
                        }
                        else if (property.type == "linkedinbio")
                        {
                            data.Properties[HubSpotVocabulary.Company.SocialMediaInformationLinkedInBio] = property.value;
                        }
                        else if (property.type == "jobtitle")
                        {
                            data.Properties[HubSpotVocabulary.Company.JobTitle] = property.value;
                        }
                        else if (property.type == "createddate")
                        {
                            if (long.TryParse(property.value, out long created))
                                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(created);
                        }
                        else if (property.type == "lastmodifieddate")
                        {
                            if (long.TryParse(property.value, out long modified))
                                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(modified);
                        }
                        else
                        {
                            data.Properties[string.Format("hubspot.contact.custom-{0}", property.type)] = property.value;
                        }
                    }
                }
            }

            if (input.IsContact == true && input.Properties != null)
            {
                JObject allProperties = JObject.Parse(JsonUtility.Serialize(input.Properties));

                foreach (var property in allProperties)
                {
                    string val = null;
                    DateTimeOffset? date = null;
                    if (property.Value["value"] != null && property.Value["value"].ToString() != null)
                    {
                        val = property.Value["value"].ToString();
                        if (long.TryParse(val, out long epoch))
                        {
                            try
                            {
                                date = DateUtilities.EpochRef.AddMilliseconds(epoch);
                            }
                            catch
                            {

                            }
                        }
                    }

                    if (val == null)
                    {
                        continue;
                    }

                    if (property.Key == "days_to_close")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationDaysToClose] = val;
                    }
                    else if (property.Key == "first_conversion_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ConversionInformationFirstConversionDate] = date.Value.ToString();
                    }
                    else if (property.Key == "first_conversion_event_name")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationFirstConversion] = val;
                    }
                    else if (property.Key == "first_deal_created_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationFirstDealCreatedDate] = date.Value.ToString();
                    }
                    else if (property.Key == "hs_additional_emails")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationAdditionalemailaddresses] = val;
                    }
                    else if (property.Key == "hs_analytics_first_touch_converting_campaign")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationFirstTouchConvertingCampaign] = val;
                    }
                    else if (property.Key == "hs_analytics_last_touch_converting_campaign")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationLastTouchConvertingCampaign] = val;
                    }
                    else if (property.Key == "hs_avatar_filemanager_key")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationAvatarFileManagerkey] = val;
                    }
                    else if (property.Key == "hs_calculated_mobile_number")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedMobileNumberinInternationalFormat] = val;
                    }
                    else if (property.Key == "hs_calculated_phone_number")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedPhoneNumberinInternationalFormat] = val;
                    }
                    else if (property.Key == "hs_calculated_phone_number_area_code")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedPhoneNumberAreaCode] = val;
                    }
                    else if (property.Key == "hs_calculated_phone_number_country_code")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedPhoneNumberCountryCode] = val;
                    }
                    else if (property.Key == "hs_calculated_phone_number_region_code")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedPhoneNumberRegion] = val;
                    }
                    else if (property.Key == "hs_email_domain")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationEmailDomain] = val;
                    }
                    else if (property.Key == "hs_email_quarantined")
                    {
                        data.Properties[HubSpotVocabulary.Contact.EmailInformationEmailAddressQuarantined] = val;
                    }
                    else if (property.Key == "hs_email_sends_since_last_engagement")
                    {
                        data.Properties[HubSpotVocabulary.Contact.EmailInformationSendsSinceLastEngagement] = val;
                    }
                    else if (property.Key == "hs_facebookid")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationFacebookID] = val;
                    }
                    else if (property.Key == "hs_googleplusid")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationgoogleplusID] = val;
                    }
                    else if (property.Key == "hs_ip_timezone")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPTimezone] = val;
                    }
                    else if (property.Key == "hs_lead_status")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLeadStatus] = val;
                    }
                    else if (property.Key == "hs_linkedinid")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationLinkedinID] = val;
                    }
                    else if (property.Key == "hs_sales_email_last_clicked")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationRecentSalesEmailClickedDate] = val;
                    }
                    else if (property.Key == "hs_sales_email_last_opened")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationRecentSalesEmailOpenedDate] = val;
                    }
                    else if (property.Key == "hs_sales_email_last_replied")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationRecentSalesEmailRepliedDate] = val;
                    }
                    else if (property.Key == "hs_searchable_calculated_international_mobile_number")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedMobileNumberwithcountrycode] = val;
                    }
                    else if (property.Key == "hs_searchable_calculated_international_phone_number")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedPhoneNumberwithcountrycode] = val;
                    }
                    else if (property.Key == "hs_searchable_calculated_mobile_number")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedMobileNumberwithoutcountrycode] = val;
                    }
                    else if (property.Key == "hs_searchable_calculated_phone_number")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCalculatedPhoneNumberwithoutcountrycode] = val;
                    }
                    else if (property.Key == "hs_sequences_is_enrolled")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCurrentlyinSequence] = val;
                    }
                    else if (property.Key == "hs_twitterid")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationTwitterID] = val;
                        data.Aliases.Add((string)property.Value.First);
                    }
                    else if (property.Key == "hubspot_owner_assigneddate")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationOwnerAssignedDate] = date.Value.ToString();
                    }
                    else if (property.Key == "ip_city")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPCity] = val;
                    }
                    else if (property.Key == "ip_country")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPCountry] = val;
                    }
                    else if (property.Key == "ip_country_code")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPCountryCode] = val;
                    }
                    else if (property.Key == "ip_latlon")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPLatitudeLongitude] = val;
                    }
                    else if (property.Key == "ip_state")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPStateRegion] = val;
                    }
                    else if (property.Key == "ip_state_code")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPStateCodeRegionCode] = val;
                    }
                    else if (property.Key == "ip_zipcode")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationIPZipcode] = val;
                    }
                    else if (property.Key == "lastmodifieddate")
                    {
                        if (date.HasValue)
                            data.ModifiedDate = date.Value;
                    }
                    else if (property.Key == "num_associated_deals")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationAssociatedDeals] = val;
                    }
                    else if (property.Key == "num_conversion_events")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationNumberofFormSubmissions] = val;
                    }
                    else if (property.Key == "num_unique_conversion_events")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationNumberofUniqueFormsSubmitted] = val;
                    }
                    else if (property.Key == "recent_conversion_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ConversionInformationRecentConversionDate] = date.Value.ToString();
                    }
                    else if (property.Key == "recent_conversion_event_name")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ConversionInformationRecentConversion] = val;
                    }
                    else if (property.Key == "recent_deal_amount")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationRecentDealAmount] = val;
                    }
                    else if (property.Key == "recent_deal_close_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationRecentDealCloseDate] = date.Value.ToString();
                    }
                    else if (property.Key == "total_revenue")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationTotalRevenue] = val;
                    }
                    else if (property.Key == "firstname")
                    {
                        if (string.IsNullOrEmpty(input.FirstName))
                            input.FirstName = (string)property.Value.First;
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationFirstName] = val;
                    }
                    else if (property.Key == "lastname")
                    {
                        if (string.IsNullOrEmpty(input.LastName))
                            input.LastName = (string)property.Value.First;
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLastName] = val;
                    }
                    else if (property.Key == "hs_analytics_first_url")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationFirstPageSeen] = val;
                    }
                    else if (property.Key == "twitterhandle")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationTwitterUsername] = val;
                    }
                    else if (property.Key == "followercount")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationFollowerCount] = val;
                    }
                    else if (property.Key == "hs_analytics_last_url")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationLastPageSeen] = val;
                    }
                    else if (property.Key == "hs_analytics_num_page_views")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationNumberofPageviews] = val;
                    }
                    else if (property.Key == "salutation")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationSalutation] = val;
                    }
                    else if (property.Key == "twitterprofilephoto")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationTwitterProfilePhoto] = val;
                    }
                    else if (property.Key == "email")
                    {
                        if (!string.IsNullOrEmpty((string)property.Value.First))
                            data.Properties[HubSpotVocabulary.Contact.Email] = val;
                        data.Aliases.Add((string)property.Value.First);
                    }
                    else if (property.Key == "hs_analytics_num_visits")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationNumberofVisits] = val;
                    }
                    else if (property.Key == "hs_persona")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationPersona] = val;
                    }
                    else if (property.Key == "hs_analytics_num_event_completions")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationNumberofeventcompletions] = val;
                    }
                    else if (property.Key == "hs_email_optout")
                    {
                        data.Properties[HubSpotVocabulary.Contact.EmailInformationOptedoutofallemail] = val;
                    }
                    else if (property.Key == "mobilephone")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationMobilePhoneNumber] = val;
                    }
                    else if (property.Key == "phone")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationPhoneNumber] = val;
                    }
                    else if (property.Key == "fax")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationFaxNumber] = val;
                    }
                    else if (property.Key == "hs_analytics_first_timestamp")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationTimeFirstSeen] = date.Value.ToString();
                    }
                    else if (property.Key == "address")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationStreetAddress] = val;
                    }
                    else if (property.Key == "engagements_last_meeting_booked")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLastMeetingBooked] = val;
                    }
                    else if (property.Key == "engagements_last_meeting_booked_campaign")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLastMeetingBookedCampaign] = val;
                    }
                    else if (property.Key == "engagements_last_meeting_booked_medium")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLastMeetingBookedMedium] = val;
                    }
                    else if (property.Key == "engagements_last_meeting_booked_source")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLastMeetingBookedSource] = val;
                    }
                    else if (property.Key == "hs_analytics_first_visit_timestamp")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationTimeofFirstVisit] = date.Value.ToString();
                    }
                    else if (property.Key == "hubspot_owner_id")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationHubSpotOwner] = val;
                        if (!string.IsNullOrEmpty((string)property.Value.First))
                            _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, input, s => (string)property.Value.First);
                    }
                    else if (property.Key == "notes_last_contacted")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationLastContacted] = date.Value.ToString();
                    }
                    else if (property.Key == "notes_last_updated")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLastActivityDate] = val;
                    }
                    else if (property.Key == "notes_next_activity_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationNextActivityDate] = date.Value.ToString();
                    }
                    else if (property.Key == "num_contacted_notes")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationNumberoftimescontacted] = date.Value.ToString();
                    }
                    else if (property.Key == "num_notes")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationNumberofSalesActivities] = val;
                    }
                    else if (property.Key == "surveymonkeyeventlastupdated")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationSurveyMonkeyEventLastUpdated] = val;
                    }
                    else if (property.Key == "webinareventlastupdated")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationWebinarEventLastUpdated] = val;
                    }
                    else if (property.Key == "city")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCity] = val;
                    }
                    else if (property.Key == "hs_analytics_last_timestamp")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationTimeLastSeen] = date.Value.ToString();
                    }
                    else if (property.Key == "hubspot_team_id")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationHubSpotTeam] = val;

                        if (!string.IsNullOrEmpty((string)property.Value.First))
                        {
                            _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Group, EntityEdgeType.Parent, input, s => (string)property.Value.First);
                        }
                    }
                    else if (property.Key == "linkedinbio")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationLinkedInBio] = val;
                    }
                    else if (property.Key == "twitterbio")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationTwitterBio] = val;
                    }
                    else if (property.Key == "hs_analytics_last_visit_timestamp")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationTimeofLastVisit] = date.Value.ToString();
                    }
                    else if (property.Key == "state")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationStateRegion] = val;
                    }
                    else if (property.Key == "hs_analytics_source")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationOriginalSource] = val;
                    }
                    else if (property.Key == "zip")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationPostalCode] = val;
                    }
                    else if (property.Key == "country")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCountry] = val;
                    }
                    else if (property.Key == "hs_analytics_source_data_1")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationOriginalSourceDrillDown1] = val;
                    }
                    else if (property.Key == "linkedinconnections")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationLinkedInConnections] = val;
                    }
                    else if (property.Key == "hs_analytics_source_data_2")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationOriginalSourceDrillDown2] = val;
                    }
                    else if (property.Key == "jobtitle")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationJobTitle] = val;
                    }
                    else if (property.Key == "kloutscoregeneral")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationKloutScore] = val;
                    }
                    else if (property.Key == "hs_analytics_first_referrer")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationFirstReferringSite] = val;
                    }
                    else if (property.Key == "message")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationMessage] = val;
                    }
                    else if (property.Key == "photo")
                    {
                        data.Properties[HubSpotVocabulary.Contact.SocialMediaInformationPhoto] = val;
                        input.Photo = (string)property.Value.First;
                    }
                    else if (property.Key == "closedate")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationCloseDate] = date.Value.ToString();
                    }
                    else if (property.Key == "hs_analytics_last_referrer")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationLastReferringSite] = val;
                    }
                    else if (property.Key == "hs_analytics_average_page_views")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationAveragePageviews] = val;
                    }
                    else if (property.Key == "lifecyclestage")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationLifecycleStage] = val;
                    }
                    else if (property.Key == "hs_analytics_revenue")
                    {
                        data.Properties[HubSpotVocabulary.Contact.AnalyticsInformationEventRevenue] = val;
                    }
                    else if (property.Key == "hs_lifecyclestage_lead_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameaLeadDate] = date.Value.ToString();
                    }
                    else if (property.Key == "hs_lifecyclestage_marketingqualifiedlead_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameaMarketingQualifiedLeadDate] = date.Value.ToString();
                    }
                    else if (property.Key == "hs_lifecyclestage_opportunity_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameanOpportunityDate] = date.Value.ToString();
                    }
                    else if (property.Key == "ipaddress")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationIPAddress] = val;
                    }
                    else if (property.Key == "createdate")
                    {
                        if (date.HasValue)
                            data.CreatedDate = date.Value;
                    }
                    else if (property.Key == "hs_lifecyclestage_salesqualifiedlead_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameaSalesQualifiedLeadDate] = date.Value.ToString();
                    }
                    else if (property.Key == "hs_lifecyclestage_evangelist_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameanEvangelistDate] = date.Value.ToString();
                    }
                    else if (property.Key == "hubspotscore")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationHubSpotScore] = val;
                    }
                    else if (property.Key == "company")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationCompanyName] = val;
                    }
                    else if (property.Key == "hs_lifecyclestage_customer_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameaCustomerDate] = date.Value.ToString();
                    }
                    else if (property.Key == "hs_lifecyclestage_subscriber_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameaSubscriberDate] = date.Value.ToString();
                    }
                    else if (property.Key == "website")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationWebsiteURL] = val;
                    }
                    else if (property.Key == "hs_lifecyclestage_other_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationBecameanOtherLifecycleDate] = date.Value.ToString();
                    }
                    else if (property.Key == "numemployees")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationNumberofEmployees] = val;
                    }
                    else if (property.Key == "annualrevenue")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationAnnualRevenue] = val;
                    }
                    else if (property.Key == "industry")
                    {
                        data.Properties[HubSpotVocabulary.Contact.ContactInformationIndustry] = val;
                    }
                    else if (property.Key == "associatedcompanyid")
                    {
                        if (!string.IsNullOrEmpty((string)property.Value.First))
                        {
                            _factory.CreateIncomingEntityReference(clue, EntityType.Organization, EntityEdgeType.PartOf, input, s => val);
                        }
                    }
                    else if (property.Key == "associatedcompanylastupdated")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Contact.ContactInformationAssociatedCompanyLastUpdated] = date.Value.ToString();
                    }
                    else if (property.Key == "createddate")
                    {
                        if (date.HasValue)
                            data.CreatedDate = date.Value;
                    }
                    else if (property.Key == "lastmodifieddate")
                    {
                        if (date.HasValue)
                            data.ModifiedDate = date.Value;
                    }
                    else
                        data.Properties[string.Format("hubspot.contact.custom-{0}", property.Key)] = val;

                }
                
                //Loop over results
                //if (property.type == "hubspot_analytics_source")
                //{

                //    DateTimeOffset modifiedDate;
                //    if (DateTimeOffset.TryParse(property.value, out modifiedDate))
                //    {
                //        data.ModifiedDate = modifiedDate;
                //    }
                //}
                ////else
                //{
                //    data.Properties[string.Format("hubspot.contact.custom-{0}", property.type)] = property.value;
                //}
            }
            

            return clue;
            
        }
    }
}
