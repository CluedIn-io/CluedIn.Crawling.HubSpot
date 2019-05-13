using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Hubspot.Core;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.Hubspot.Factories
{
  public class HubspotClueFactory : ClueFactory
  {
    public HubspotClueFactory()
        : base(HubspotConstants.CodeOrigin, HubspotConstants.ProviderRootCodeValue)
    {
    }

    protected override Clue ConfigureProviderRoot([NotNull] Clue clue)
    {
      if (clue == null) throw new ArgumentNullException(nameof(clue));

      var data = clue.Data.EntityData;
      data.Name = HubspotConstants.CrawlerName;
      data.Uri = new Uri(HubspotConstants.Uri);
      data.Description = HubspotConstants.CrawlerDescription;

      // TODO this should not be necessary
      clue.ValidationRuleSuppressions.AddRange(new[]
          {
            RuleConstants.METADATA_002_Uri_MustBeSet
          });

      return clue;
    }
  }
}
