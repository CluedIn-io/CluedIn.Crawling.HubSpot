// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotPropertyDefinitionVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotPropertyDefinitionVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot property definition vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotPropertyDefinitionVocabulary : SimpleVocabulary
    {
        public HubSpotPropertyDefinitionVocabulary()
        {
            VocabularyName = "HubSpot Property Definition";
            KeyPrefix      = "hubspot.propertydefinition";
            KeySeparator   = ".";
            Grouping       = EntityType.Note;

            AddGroup("Hubspot Ticket Details", group =>
            {

                Name                          = group.Add(new VocabularyKey("Name"));
                Label                         = group.Add(new VocabularyKey("Label"));
                Description                   = group.Add(new VocabularyKey("Description"));
                GroupName                     = group.Add(new VocabularyKey("GroupName"));
                Type                          = group.Add(new VocabularyKey("Type"));
                FieldType                     = group.Add(new VocabularyKey("FieldType"));
                Hidden                        = group.Add(new VocabularyKey("Hidden"));
                Options                       = group.Add(new VocabularyKey("Options", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                Calculated                    = group.Add(new VocabularyKey("Calculated"));
                ExternalOptions               = group.Add(new VocabularyKey("ExternalOptions"));
                HubspotDefined                = group.Add(new VocabularyKey("HubspotDefined"));
                FormField                     = group.Add(new VocabularyKey("FormField"));
                DisplayOrder                  = group.Add(new VocabularyKey("DisplayOrder"));
                ReadonlyValue                 = group.Add(new VocabularyKey("ReadonlyValue"));
                ReadonlyDefinition            = group.Add(new VocabularyKey("ReadonlyDefinition"));
                Deleted                       = group.Add(new VocabularyKey("Deleted"));
                MutableDefinitionNotDeletable = group.Add(new VocabularyKey("MutableDefinitionNotDeletable"));
                Favorited                     = group.Add(new VocabularyKey("Favorited"));
                FavoritedOrder                = group.Add(new VocabularyKey("FavoritedOrder"));
                DisplayMode                   = group.Add(new VocabularyKey("DisplayMode"));
                ShowCurrencySymbol            = group.Add(new VocabularyKey("ShowCurrencySymbol"));
                CreatedUserId                 = group.Add(new VocabularyKey("CreatedUserId"));
                TextDisplayHint               = group.Add(new VocabularyKey("TextDisplayHint"));
                NumberDisplayHint             = group.Add(new VocabularyKey("NumberDisplayHint"));
                OptionsAreMutable             = group.Add(new VocabularyKey("OptionsAreMutable"));
                ReferencedObjectType          = group.Add(new VocabularyKey("ReferencedObjectType"));
                IsCustomizedDefault           = group.Add(new VocabularyKey("IsCustomizedDefault"));
                CreatedAt                     = group.Add(new VocabularyKey("CreatedAt"));
                UpdatedAt                     = group.Add(new VocabularyKey("UpdatedAt"));
                UpdatedUserId                 = group.Add(new VocabularyKey("UpdatedUserId"));
            });
        }

        public VocabularyKey Name { get; private set; }
        public VocabularyKey Label { get; private set; }
        public VocabularyKey Description { get; private set; }
        public VocabularyKey GroupName { get; private set; }
        public VocabularyKey Type { get; private set; }
        public VocabularyKey FieldType { get; private set; }
        public VocabularyKey Hidden { get; private set; }
        public VocabularyKey Options { get; private set; }
        public VocabularyKey Calculated { get; private set; }
        public VocabularyKey ExternalOptions { get; private set; }
        public VocabularyKey HubspotDefined { get; private set; }
        public VocabularyKey FormField { get; private set; }
        public VocabularyKey DisplayOrder { get; private set; }
        public VocabularyKey ReadonlyValue { get; private set; }
        public VocabularyKey ReadonlyDefinition { get; private set; }
        public VocabularyKey Deleted { get; private set; }
        public VocabularyKey MutableDefinitionNotDeletable { get; private set; }
        public VocabularyKey Favorited { get; private set; }
        public VocabularyKey FavoritedOrder { get; private set; }
        public VocabularyKey DisplayMode { get; private set; }
        public VocabularyKey ShowCurrencySymbol { get; private set; }
        public VocabularyKey CreatedUserId { get; private set; }
        public VocabularyKey TextDisplayHint { get; private set; }
        public VocabularyKey NumberDisplayHint { get; private set; }
        public VocabularyKey OptionsAreMutable { get; private set; }
        public VocabularyKey ReferencedObjectType { get; private set; }
        public VocabularyKey IsCustomizedDefault { get; private set; }
        public VocabularyKey CreatedAt { get; private set; }
        public VocabularyKey UpdatedAt { get; private set; }
        public VocabularyKey UpdatedUserId { get; private set; }
    }
}
