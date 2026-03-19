using Microsoft.JSInterop;

namespace RpgSheetHub.Services
{
    /// <summary>
    /// Defines the available visual themes per RPG system.
    /// </summary>
    public enum RpgTheme
    {
        Hub,             // Neutral — main page
        DnD,             // Golden, parchment
        Gurps,           // Metallic, steel gray (future)
        T20,             // Forest green (future)
        OrdemParanormal, // Purple, dark (future)
        Pathfinder       // Orange/bronze (future)
    }

    /// <summary>
    /// Manages the active site theme. Applies a CSS class to the <body>
    /// that triggers the corresponding system's color variables.
    /// </summary>
    public class ThemeService
    {
        private readonly IJSRuntime _js;

        public RpgTheme CurrentTheme { get; private set; } = RpgTheme.Hub;

        /// <summary>
        /// Triggered whenever the theme changes — components can subscribe
        /// to re-render when necessary.
        /// </summary>
        public event Action? OnThemeChanged;

        public ThemeService(IJSRuntime js)
        {
            _js = js;
        }

        /// <summary>
        /// Returns the CSS class corresponding to the theme.
        /// </summary>
        public static string GetThemeClass(RpgTheme theme) => theme switch
        {
            RpgTheme.DnD => "theme-dnd",
            RpgTheme.Gurps => "theme-gurps",
            RpgTheme.T20 => "theme-t20",
            RpgTheme.OrdemParanormal => "theme-op",
            RpgTheme.Pathfinder => "theme-pathfinder",
            _ => "theme-hub"
        };

        /// <summary>
        /// Returns the theme corresponding to a system slug (e.g., "dnd55", "gurps").
        /// </summary>
        public static RpgTheme FromSystemSlug(string? slug) => slug?.ToLower() switch
        {
            "dnd55" or "dnd35" or "dnd" => RpgTheme.DnD,
            "gurps" => RpgTheme.Gurps,
            "t20" => RpgTheme.T20,
            "op" => RpgTheme.OrdemParanormal,
            "pathfinder" or "pf" => RpgTheme.Pathfinder,
            _ => RpgTheme.Hub
        };

        /// <summary>
        /// Applies a theme, updates the body class via JS, and notifies components.
        /// </summary>
        public async Task SetThemeAsync(RpgTheme theme)
        {
            if (CurrentTheme == theme) return;

            CurrentTheme = theme;
            var cssClass = GetThemeClass(theme);

            // Removes all theme classes and applies the new one
            await _js.InvokeVoidAsync("rpgTheme.apply", cssClass);

            OnThemeChanged?.Invoke();
        }

        /// <summary>
        /// Shortcut to apply the theme via the system slug.
        /// </summary>
        public Task SetThemeBySlugAsync(string? slug)
            => SetThemeAsync(FromSystemSlug(slug));

        /// <summary>
        /// Resets to the neutral hub theme.
        /// </summary>
        public Task ResetThemeAsync()
            => SetThemeAsync(RpgTheme.Hub);
    }
}