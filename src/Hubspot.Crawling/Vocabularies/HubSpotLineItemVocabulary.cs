// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotLineItemVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotLineItemVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot line item vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotLineItemVocabulary : SimpleVocabulary
    {
        public HubSpotLineItemVocabulary()
        {
            this.VocabularyName = "HubSpot Line Item";
            this.KeyPrefix      = "hubspot.lineitem";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Sales.Order;

            this.Name                        = this.Add(new VocabularyKey("Name"));
            this.Productdescription          = this.Add(new VocabularyKey("ProductDescription"));
            this.IsDeleted                   = this.Add(new VocabularyKey("IsDeleted", VocabularyKeyDataType.Boolean));
            this.Version                     = this.Add(new VocabularyKey("Version"));
            this.CreateDate                  = this.Add(new VocabularyKey("CreateDate"));
            this.LastModifiedDate            = this.Add(new VocabularyKey("LastModifiedDate"));
            this.Amount                      = this.Add(new VocabularyKey("Amount"));
            this.ProductID                   = this.Add(new VocabularyKey("ProductID"));
            this.Productprice                = this.Add(new VocabularyKey("Productprice"));
            this.Quantity                    = this.Add(new VocabularyKey("Quantity"));
            this.Dealclosedwondate           = this.Add(new VocabularyKey("DealClosedWonDate"));
            this.StartDate                   = this.Add(new VocabularyKey("StartDate"));
            this.EndDate                     = this.Add(new VocabularyKey("EndDate"));
            this.Recurringbillingfrequency   = this.Add(new VocabularyKey("RecurringBillingFrequency"));
            this.DiscountAmount              = this.Add(new VocabularyKey("DiscountAmount"));
            this.DiscountPercentage          = this.Add(new VocabularyKey("DiscountPercentage"));
            this.Tax                         = this.Add(new VocabularyKey("Tax"));
            this.Term                        = this.Add(new VocabularyKey("Term"));
            this.Costofgoodssold             = this.Add(new VocabularyKey("CostOfGoodsSold"));
        }

        public VocabularyKey IsDeleted { get; private set; }
        public VocabularyKey Version { get; private set; }
        public VocabularyKey CreateDate { get; private set; }
        public VocabularyKey LastModifiedDate { get; private set; }
        public VocabularyKey Name { get; private set; }
        public VocabularyKey Productdescription { get; private set; }
        public VocabularyKey Amount { get; private set; }
        public VocabularyKey ProductID { get; private set; }
        public VocabularyKey Productprice { get; private set; }
        public VocabularyKey Quantity { get; private set; }
        public VocabularyKey Dealclosedwondate { get; private set; }
        public VocabularyKey Recurringbillingfrequency { get; private set; }
        public VocabularyKey DiscountAmount { get; private set; }
        public VocabularyKey EndDate { get; private set; }
        public VocabularyKey DiscountPercentage { get; private set; }
        public VocabularyKey Tax { get; private set; }
        public VocabularyKey Term { get; private set; }
        public VocabularyKey StartDate { get; private set; }
        public VocabularyKey Costofgoodssold { get; private set; }
    }
}