using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;
using Vocabs = CluedIn.Core.Data.Vocabularies.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
  public class _SampleFile_Vocabulary : SimpleVocabulary
  {
    public _SampleFile_Vocabulary()
    {
      VocabularyName = "HubSpot [SampleFile]"; // TODO: Set value
      KeyPrefix = "hubspot.[samplefile]"; // TODO: Set value
      KeySeparator = ".";
      Grouping = EntityType.Unknown; // TODO: Set value

      AddGroup("HubSpot Details", group =>
      {
        Id = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Text, VocabularyKeyVisiblity.Visible));
        Name = group.Add(new VocabularyKey("Name", VocabularyKeyDataType.Text, VocabularyKeyVisiblity.Visible));
        Uri = group.Add(new VocabularyKey("Uri", VocabularyKeyDataType.Uri, VocabularyKeyVisiblity.Visible));
        FolderId = group.Add(new VocabularyKey("ParentId", VocabularyKeyDataType.Text, VocabularyKeyVisiblity.Hidden));
      });

      // Mappings to Common Vocabulary
      AddMapping(Uri, Vocabs.CluedInFile.DownloadUrl);
    }

    public VocabularyKey Id { get; private set; }
    public VocabularyKey Name { get; private set; }
    public VocabularyKey FolderId { get; private set; }
    public VocabularyKey Uri { get; private set; }
  }
}
