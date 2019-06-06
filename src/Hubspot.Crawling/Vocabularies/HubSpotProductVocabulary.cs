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
            this.VocabularyName = "HubSpot Product";
            this.KeyPrefix      = "hubspot.product";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Product;

            this.Name                        = this.Add(new VocabularyKey("Name"));
            this.Productdescription          = this.Add(new VocabularyKey("ProductDescription"));
            this.StartDate                   = this.Add(new VocabularyKey("StartDate", VocabularyKeyDataType.DateTime));
            this.CreateDate                  = this.Add(new VocabularyKey("CreateDate", VocabularyKeyDataType.DateTime));
            this.LastModifiedDate            = this.Add(new VocabularyKey("LastModifiedDate", VocabularyKeyDataType.DateTime));
            this.IsDeleted                   = this.Add(new VocabularyKey("IsDeleted", VocabularyKeyDataType.Boolean));
            this.Version                     = this.Add(new VocabularyKey("Version"));
            this.AvatarFileManagerkey        = this.Add(new VocabularyKey("AvatarFileManagerKey"));
            this.Productprice                = this.Add(new VocabularyKey("ProductPrice", VocabularyKeyDataType.Money));
            this.Recurringbillingfrequency   = this.Add(new VocabularyKey("RecurringBillingFrequency"));
            this.DiscountAmount              = this.Add(new VocabularyKey("DiscountAmount"));
            this.DiscountPercentage          = this.Add(new VocabularyKey("DiscountPercentage"));
            this.Tax                         = this.Add(new VocabularyKey("Tax"));
            this.Term                        = this.Add(new VocabularyKey("Term"));
            this.Costofgoodssold             = this.Add(new VocabularyKey("CostOfGoodsSold", VocabularyKeyDataType.Money));

            this.AddMapping(this.Version, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInProduct.VersionNumber);
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