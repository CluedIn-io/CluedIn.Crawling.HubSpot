using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;

using CluedIn.Crawling.Hubspot.Vocabularies;
using CluedIn.Crawling.Hubspot.Core.Models;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.Hubspot.ClueProducers
{
  public class _SampleFile_ClueProducer : BaseClueProducer<_SampleFile_>
  {
    private readonly IClueFactory _factory;

    public _SampleFile_ClueProducer(IClueFactory factory)
    {
      _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    protected override Clue MakeClueImpl(_SampleFile_ input, Guid accountId)
    {
      if (input == null) throw new ArgumentNullException(nameof(input));

      // TODO: Create clue specifying the type of entity it is and ID
      var clue = _factory.Create(EntityType.Files.File, input.Id.ToString(), accountId);

      // TODO: Populate clue data
      var data = clue.Data.EntityData;

      var vocab = new _SampleFile_Vocabulary();
      data.Properties[vocab.Id] = input.Id.PrintIfAvailable();

      data.Name = input.Name.PrintIfAvailable();
      data.Properties[vocab.Name] = input.Name.PrintIfAvailable();

      data.Uri = input.Uri;
      data.Properties[vocab.Uri] = input.Uri.ToString();

      _factory.CreateOutgoingEntityReference(clue, EntityType.Files.Directory, EntityEdgeType.PartOf, input.FolderId.ToString(), input.FolderId.ToString());

      clue.ValidationRuleSuppressions.AddRange(new[]
      {
          RuleConstants.DATA_001_File_MustBeIndexed,
          //TODO this should not be necessary - we are setting the URI
          RuleConstants.METADATA_002_Uri_MustBeSet,
          RuleConstants.METADATA_003_Author_Name_MustBeSet
      });

      return clue;
    }
  }
}
