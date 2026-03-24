using RpgSheetHub.Models;

namespace RpgSheetHub.Services
{
    /// <summary>
    /// Service responsible for providing the RPG system templates.
    /// reads from a professional JSON static database.
    /// </summary>
    public class TemplateService
    {
        private readonly List<SystemTemplate> _templates;

        public TemplateService(List<SystemTemplate> templates)
        {
            _templates = templates ?? new List<SystemTemplate>();
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
            if (string.IsNullOrEmpty(slug)) return null;

            return slug.ToLower() switch
            {
                "dnd55" => _templates.FirstOrDefault(t => t.Name.Contains("D&D 5.5")),
                "dnd35" => _templates.FirstOrDefault(t => t.Name.Contains("D&D 3.5")),
                "op" or "ordemparanormal" => _templates.FirstOrDefault(t => t.Name.Contains("Ordem")),
                "gurps" => _templates.FirstOrDefault(t => t.Name.Contains("GURPS")),
                "t20" => _templates.FirstOrDefault(t => t.Name.Contains("Tormenta")),
                "pathfinder" or "pf" => _templates.FirstOrDefault(t => t.Name.Contains("Pathfinder")),
                _ => null
            };
        }
    }
}