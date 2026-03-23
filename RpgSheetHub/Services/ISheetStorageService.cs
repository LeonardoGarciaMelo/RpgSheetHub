using RpgSheetHub.Models;

namespace RpgSheetHub.Services
{
    /// <summary>
    /// Defines the contract for character sheet storage operations.
    /// This allows swapping between LocalStorage and a real Database seamlessly.
    /// </summary>
    public interface ISheetStorageService
    {
        /// <summary>
        /// Saves a new character sheet or updates an existing one.
        /// </summary>
        Task SaveSheetAsync(CharacterSheet sheet);

        /// <summary>
        /// Retrieves all character sheets saved by the user.
        /// </summary>
        Task<List<CharacterSheet>> GetAllSheetsAsync();

        /// <summary>
        /// Retrieves a specific character sheet by its ID.
        /// </summary>
        Task<CharacterSheet?> GetSheetByIdAsync(Guid id);

        /// <summary>
        /// Deletes a specific character sheet.
        /// </summary>
        Task DeleteSheetAsync(Guid id);
    }
}