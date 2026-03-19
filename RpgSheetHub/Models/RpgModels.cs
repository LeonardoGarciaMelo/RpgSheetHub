using System;
using System.Collections.Generic;

namespace RpgSheetHub.Models
{
    /// <summary>
    /// Represents the template of an RPG system (e.g., D&D 5.5e, GURPS, Ordem Paranormal).
    /// Defines the rules and which fields the sheets based on this system must contain.
    /// </summary>
    public class SystemTemplate
    {
        /// <summary>
        /// Unique identifier for the system template.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name of the RPG system (e.g., "Dungeons & Dragons 5.5e").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// List containing all field definitions required by this system.
        /// </summary>
        public List<FieldDefinition> Fields { get; set; } = new();
    }

    /// <summary>
    /// Defines the structure of an individual field within an RPG sheet.
    /// Acts as the blueprint for generating text boxes and numbers on the screen.
    /// </summary>
    public class FieldDefinition
    {
        /// <summary>
        /// Unique identification key for the field in the data dictionary (e.g., "strength", "max_hp").
        /// Must not contain spaces or special characters.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// The user-friendly name of the field displayed on the screen (e.g., "Strength", "Hit Points").
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Defines the data type and how it will be rendered in HTML.
        /// Common values: "text", "number", "textarea", "checkbox".
        /// </summary>
        public string InputType { get; set; } = "text";

        /// <summary>
        /// Category or tab to visually group the field on the sheet (e.g., "Main Attributes", "Inventory").
        /// </summary>
        public string Category { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a character sheet filled out by a player.
    /// Stores data dynamically based on the chosen <see cref="SystemTemplate"/>.
    /// </summary>
    public class CharacterSheet
    {
        /// <summary>
        /// Unique identifier for the character sheet.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Character's name.
        /// </summary>
        public string CharacterName { get; set; } = string.Empty;

        /// <summary>
        /// Name of the player who owns the sheet.
        /// </summary>
        public string PlayerName { get; set; } = string.Empty;

        /// <summary>
        /// Reference to the <see cref="SystemTemplate"/> ID that dictates the rules of this sheet.
        /// </summary>
        public Guid SystemTemplateId { get; set; }

        /// <summary>
        /// Flexible dictionary that stores the actual values entered by the player.
        /// The key (string) corresponds to the 'Key' property of 'FieldDefinition'.
        /// The value (object) stores what was typed (text, number, boolean, etc.).
        /// </summary>
        public Dictionary<string, object> Attributes { get; set; } = new();
    }
}