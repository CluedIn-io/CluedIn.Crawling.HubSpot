using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.HubSpot.Factories
{
  public class HubSpotClueFactory : ClueFactory
  {
    public HubSpotClueFactory()
        : base(HubSpotConstants.CodeOrigin, HubSpotConstants.ProviderRootCodeValue)
    {
    }

    protected override Clue ConfigureProviderRoot([NotNull] Clue clue)
    {
      if (clue == null) throw new ArgumentNullException(nameof(clue));

      var data = clue.Data.EntityData;
      data.Name = HubSpotConstants.CrawlerName;
      data.Uri = new Uri(HubSpotConstants.Uri);
      data.Description = HubSpotConstants.CrawlerDescription;

      // TODO this should not be necessary
      clue.ValidationRuleSuppressions.AddRange(new[]
          {
            RuleConstants.METADATA_002_Uri_MustBeSet
          });

      return clue;
    }
  }
}
