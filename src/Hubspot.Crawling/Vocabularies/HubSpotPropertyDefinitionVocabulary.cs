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
            this.VocabularyName = "HubSpot Property Definition";
            this.KeyPrefix      = "hubspot.propertydefinition";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Note;

            this.AddGroup("Hubspot Ticket Details", group =>
            {

                this.Name                          = group.Add(new VocabularyKey("Name"));
                this.Label                         = group.Add(new VocabularyKey("Label"));
                this.Description                   = group.Add(new VocabularyKey("Description"));
                this.GroupName                     = group.Add(new VocabularyKey("GroupName"));
                this.Type                          = group.Add(new VocabularyKey("Type"));
                this.FieldType                     = group.Add(new VocabularyKey("FieldType"));
                this.Hidden                        = group.Add(new VocabularyKey("Hidden"));
                this.Options                       = group.Add(new VocabularyKey("Options", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.Calculated                    = group.Add(new VocabularyKey("Calculated"));
                this.ExternalOptions               = group.Add(new VocabularyKey("ExternalOptions"));
                this.HubspotDefined                = group.Add(new VocabularyKey("HubspotDefined"));
                this.FormField                     = group.Add(new VocabularyKey("FormField"));
                this.DisplayOrder                  = group.Add(new VocabularyKey("DisplayOrder"));
                this.ReadonlyValue                 = group.Add(new VocabularyKey("ReadonlyValue"));
                this.ReadonlyDefinition            = group.Add(new VocabularyKey("ReadonlyDefinition"));
                this.Deleted                       = group.Add(new VocabularyKey("Deleted"));
                this.MutableDefinitionNotDeletable = group.Add(new VocabularyKey("MutableDefinitionNotDeletable"));
                this.Favorited                     = group.Add(new VocabularyKey("Favorited"));
                this.FavoritedOrder                = group.Add(new VocabularyKey("FavoritedOrder"));
                this.DisplayMode                   = group.Add(new VocabularyKey("DisplayMode"));
                this.ShowCurrencySymbol            = group.Add(new VocabularyKey("ShowCurrencySymbol"));
                this.CreatedUserId                 = group.Add(new VocabularyKey("CreatedUserId"));
                this.TextDisplayHint               = group.Add(new VocabularyKey("TextDisplayHint"));
                this.NumberDisplayHint             = group.Add(new VocabularyKey("NumberDisplayHint"));
                this.OptionsAreMutable             = group.Add(new VocabularyKey("OptionsAreMutable"));
                this.ReferencedObjectType          = group.Add(new VocabularyKey("ReferencedObjectType"));
                this.IsCustomizedDefault           = group.Add(new VocabularyKey("IsCustomizedDefault"));
                this.CreatedAt                     = group.Add(new VocabularyKey("CreatedAt"));
                this.UpdatedAt                     = group.Add(new VocabularyKey("UpdatedAt"));
                this.UpdatedUserId                 = group.Add(new VocabularyKey("UpdatedUserId"));
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