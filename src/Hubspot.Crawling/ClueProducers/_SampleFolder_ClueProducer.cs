using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Hubspot.Core.Models;
using CluedIn.Crawling.Factories;
using CluedIn.Core;
using CluedIn.Crawling.Hubspot.Vocabularies;
using CluedIn.Crawling.Helpers;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;


namespace CluedIn.Crawling.Hubspot.ClueProducers
{
  public class _SampleFolder_ClueProducer : BaseClueProducer<_SampleFolder_>
  {
    private readonly IClueFactory _factory;

    public _SampleFolder_ClueProducer([NotNull] IClueFactory factory)
    {
      _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    }

    protected override Clue MakeClueImpl([NotNull] _SampleFolder_ input, Guid accountId)
    {
      if (input == null) throw new ArgumentNullException(nameof(input));

      // TODO: Create clue specifying the type of entity it is and ID            
      var clue = _factory.Create(EntityType.Files.Directory, input.Id.ToString(), accountId);

      // TODO: Populate clue data
      var data = clue.Data.EntityData;

      var vocab = new _SampleFolder_Vocabulary();

      data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
      data.Properties[vocab.Name] = input.Name.PrintIfAvailable();

      data.Name = input.Name.PrintIfAvailable();

      clue.ValidationRuleSuppressions.AddRange(new[]
          {
                RuleConstants.METADATA_002_Uri_MustBeSet,
                RuleConstants.METADATA_003_Author_Name_MustBeSet,
                RuleConstants.METADATA_005_PreviewImage_RawData_MustBeSet
                });
      //since all clues need at least one edge
      //folder is connected to the provider itself
      _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);
      return clue;
    }
  }
}
