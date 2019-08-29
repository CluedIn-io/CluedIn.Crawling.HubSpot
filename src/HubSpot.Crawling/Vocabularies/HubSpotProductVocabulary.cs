// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotProductVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotProductVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot product vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotProductVocabulary : SimpleVocabulary
    {
        public HubSpotProductVocabulary()
        {
            VocabularyName = "HubSpot Product";
            KeyPrefix      = "hubspot.product";
            KeySeparator   = ".";
            Grouping       = EntityType.Product;

            Name                        = Add(new VocabularyKey("Name"));
            Productdescription          = Add(new VocabularyKey("ProductDescription"));
            StartDate                   = Add(new VocabularyKey("StartDate", VocabularyKeyDataType.DateTime));
            CreateDate                  = Add(new VocabularyKey("CreateDate", VocabularyKeyDataType.DateTime));
            LastModifiedDate            = Add(new VocabularyKey("LastModifiedDate", VocabularyKeyDataType.DateTime));
            IsDeleted                   = Add(new VocabularyKey("IsDeleted", VocabularyKeyDataType.Boolean));
            Version                     = Add(new VocabularyKey("Version"));
            AvatarFileManagerkey        = Add(new VocabularyKey("AvatarFileManagerKey"));
            Productprice                = Add(new VocabularyKey("ProductPrice", VocabularyKeyDataType.Money));
            Recurringbillingfrequency   = Add(new VocabularyKey("RecurringBillingFrequency"));
            DiscountAmount              = Add(new VocabularyKey("DiscountAmount"));
            DiscountPercentage          = Add(new VocabularyKey("DiscountPercentage"));
            Tax                         = Add(new VocabularyKey("Tax"));
            Term                        = Add(new VocabularyKey("Term"));
            Costofgoodssold             = Add(new VocabularyKey("CostOfGoodsSold", VocabularyKeyDataType.Money));

            AddMapping(Version, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInProduct.VersionNumber);
        }

        public VocabularyKey IsDeleted { get; private set; }
        public VocabularyKey Version { get; private set; }
        public VocabularyKey CreateDate { get; private set; }
        public VocabularyKey LastModifiedDate { get; private set; }
        public VocabularyKey AvatarFileManagerkey { get; private set; }
        public VocabularyKey Name { get; private set; }
        public VocabularyKey Productdescription { get; private set; }
        public VocabularyKey Productprice { get; private set; }
        public VocabularyKey Recurringbillingfrequency { get; private set; }
        public VocabularyKey DiscountAmount { get; private set; }
        public VocabularyKey DiscountPercentage { get; private set; }
        public VocabularyKey Tax { get; private set; }
        public VocabularyKey Term { get; private set; }
        public VocabularyKey StartDate { get; private set; }
        public VocabularyKey Costofgoodssold { get; private set; }
    }
}
