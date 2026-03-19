using RpgSheetHub.Models;

namespace RpgSheetHub.Services
{
    /// <summary>
    /// Service responsible for providing the RPG system templates.
    /// Acts as an in-memory repository until a real database is implemented.
    /// </summary>
    public class TemplateService
    {
        private readonly List<SystemTemplate> _templates;

        public TemplateService()
        {
            // Initializes with some base systems so we can test dynamic generation
            _templates = new List<SystemTemplate>
            {
                CreateDnDTemplate(),
                CreateOrdemParanormalTemplate()
            };
        }

        /// <summary>
        /// Returns all registered system templates.
        /// </summary>
        public List<SystemTemplate> GetAll() => _templates;

        /// <summary>
        /// Fetches a specific system based on its URL slug.
        /// </summary>
        public SystemTemplate? GetBySlug(string slug)
        {
            return slug.ToLower() switch
            {
                "dnd55" => _templates.FirstOrDefault(t => t.Name.Contains("D&D")),
                "op" => _templates.FirstOrDefault(t => t.Name.Contains("Ordem")),
                _ => null
            };
        }

        private SystemTemplate CreateDnDTemplate()
        {
            return new SystemTemplate
            {
                Name = "D&D 5.5e",
                Fields = new List<FieldDefinition>
                {
                    new() { Key = "class", Label = "Class & Level", Category = "Header", InputType = "text" },
                    new() { Key = "race", Label = "Species/Race", Category = "Header", InputType = "text" },
                    new() { Key = "ac", Label = "Armor Class", Category = "Combat", InputType = "number" },
                    new() { Key = "max_hp", Label = "Max Hit Points", Category = "Combat", InputType = "number" },
                    new() { Key = "strength", Label = "Strength", Category = "Attributes", InputType = "number" },
                    new() { Key = "dexterity", Label = "Dexterity", Category = "Attributes", InputType = "number" }
                }
            };
        }

        private SystemTemplate CreateOrdemParanormalTemplate()
        {
            return new SystemTemplate
            {
                Name = "Ordem Paranormal",
                Fields = new List<FieldDefinition>
                {
                    new() { Key = "origin", Label = "Origin", Category = "Header", InputType = "text" },
                    new() { Key = "class", Label = "Class", Category = "Header", InputType = "text" },
                    new() { Key = "nex", Label = "NEX (%)", Category = "Header", InputType = "number" },
                    new() { Key = "hp", Label = "Hit Points", Category = "Status", InputType = "number" },
                    new() { Key = "sanity", Label = "Sanity", Category = "Status", InputType = "number" },
                    new() { Key = "agility", Label = "Agility", Category = "Attributes", InputType = "number" }
                }
            };
        }
    }
}