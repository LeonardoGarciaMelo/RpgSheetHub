using System.Text.Json;
using Microsoft.JSInterop;
using RpgSheetHub.Models;

namespace RpgSheetHub.Services
{
    /// <summary>
    /// Implementation of the storage service using the browser's LocalStorage API.
    /// Perfect for offline use and prototyping.
    /// </summary>
    public class BrowserStorageService : ISheetStorageService
    {
        private readonly IJSRuntime _js;
        private const string StorageKey = "rpg_hub_sheets"; // Key where everything will be stored as a JSON string

        public BrowserStorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SaveSheetAsync(CharacterSheet sheet)
        {
            var sheets = await GetAllSheetsAsync();

            // Verify if the sheet already exists (by Id) and remove it before adding the updated version
            var existing = sheets.FirstOrDefault(s => s.Id == sheet.Id);
            if (existing != null)
            {
                sheets.Remove(existing);
            }

            sheets.Add(sheet);

            // Converts the list of sheets to a JSON string and saves it in LocalStorage
            var json = JsonSerializer.Serialize(sheets);
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }

        public async Task<List<CharacterSheet>> GetAllSheetsAsync()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);

            if (string.IsNullOrEmpty(json))
                return new List<CharacterSheet>();

            return JsonSerializer.Deserialize<List<CharacterSheet>>(json) ?? new List<CharacterSheet>();
        }

        public async Task<CharacterSheet?> GetSheetByIdAsync(Guid id)
        {
            var sheets = await GetAllSheetsAsync();
            return sheets.FirstOrDefault(s => s.Id == id);
        }

        public async Task DeleteSheetAsync(Guid id)
        {
            var sheets = await GetAllSheetsAsync();
            var sheetToRemove = sheets.FirstOrDefault(s => s.Id == id);

            if (sheetToRemove != null)
            {
                sheets.Remove(sheetToRemove);
                var json = JsonSerializer.Serialize(sheets);
                await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
            }
        }
    }
}