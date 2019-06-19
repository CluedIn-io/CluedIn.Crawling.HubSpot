namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public enum AssociationType
    {
        ContactToCompany = 1,
        CompanyToContact = 2,
        DealToContact = 3,
        ContactToDeal = 4,
        DealToCompany = 5,
        CompanyToDeal = 6,
        CompanyToEngagement = 7,
        EngagementToCompany = 8,
        ContactToEngagement = 9,
        EngagementToContact = 10,
        DealToEngagement = 11,
        EngagementToDeal = 12,
        ParentCompanyToChildCompany = 13,
        ChildCompanyToParentCompany = 14,
        ContactToTicket = 15,
        TicketToContact = 16,
        TicketToEngagement = 17,
        EngagementToTicket = 18,
        DealToLineItem = 19,
        LineItemToDeal = 20,
        CompanyToTicket = 25,
        TicketToCompany = 26,
        DealToTicket = 27,
        TicketToDeal = 28,
    }
}
