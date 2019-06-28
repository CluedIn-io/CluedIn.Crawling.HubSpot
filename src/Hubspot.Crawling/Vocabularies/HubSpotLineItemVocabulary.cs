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
            VocabularyName = "HubSpot Line Item";
            KeyPrefix      = "hubspot.lineitem";
            KeySeparator   = ".";
            Grouping       = EntityType.Sales.Order;

            Name                        = Add(new VocabularyKey("Name"));
            Productdescription          = Add(new VocabularyKey("ProductDescription"));
            IsDeleted                   = Add(new VocabularyKey("IsDeleted", VocabularyKeyDataType.Boolean));
            Version                     = Add(new VocabularyKey("Version"));
            CreateDate                  = Add(new VocabularyKey("CreateDate"));
            LastModifiedDate            = Add(new VocabularyKey("LastModifiedDate"));
            Amount                      = Add(new VocabularyKey("Amount"));
            ProductID                   = Add(new VocabularyKey("ProductID"));
            Productprice                = Add(new VocabularyKey("Productprice"));
            Quantity                    = Add(new VocabularyKey("Quantity"));
            Dealclosedwondate           = Add(new VocabularyKey("DealClosedWonDate"));
            StartDate                   = Add(new VocabularyKey("StartDate"));
            EndDate                     = Add(new VocabularyKey("EndDate"));
            Recurringbillingfrequency   = Add(new VocabularyKey("RecurringBillingFrequency"));
            DiscountAmount              = Add(new VocabularyKey("DiscountAmount"));
            DiscountPercentage          = Add(new VocabularyKey("DiscountPercentage"));
            Tax                         = Add(new VocabularyKey("Tax"));
            Term                        = Add(new VocabularyKey("Term"));
            Costofgoodssold             = Add(new VocabularyKey("CostOfGoodsSold"));
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
