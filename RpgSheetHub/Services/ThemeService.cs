using Microsoft.JSInterop;

namespace RpgSheetHub.Services
{
    /// <summary>
    /// Define os temas visuais disponíveis por sistema de RPG.
    /// </summary>
    public enum RpgTheme
    {
        Hub,        // Neutro — página principal
        DnD,        // Dourado, pergaminho
        Gurps,      // Metálico, cinza aço (futuro)
        T20,        // Verde floresta (futuro)
        OrdemParanormal, // Roxo, sombrio (futuro)
        Pathfinder  // Laranja/bronze (futuro)
    }

    /// <summary>
    /// Gerencia o tema ativo do site. Aplica uma classe CSS no <body>
    /// que ativa as variáveis de cor do sistema correspondente.
    /// </summary>
    public class ThemeService
    {
        private readonly IJSRuntime _js;

        public RpgTheme CurrentTheme { get; private set; } = RpgTheme.Hub;

        /// <summary>
        /// Disparado sempre que o tema muda — componentes podem se inscrever
        /// para re-renderizar quando necessário.
        /// </summary>
        public event Action? OnThemeChanged;

        public ThemeService(IJSRuntime js)
        {
            _js = js;
        }

        /// <summary>
        /// Retorna a classe CSS correspondente ao tema.
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
        /// Retorna o tema correspondente a um slug de sistema (ex: "dnd55", "gurps").
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
        /// Aplica um tema, atualiza a classe do body via JS e notifica os componentes.
        /// </summary>
        public async Task SetThemeAsync(RpgTheme theme)
        {
            if (CurrentTheme == theme) return;

            CurrentTheme = theme;
            var cssClass = GetThemeClass(theme);

            // Remove todas as classes de tema e aplica a nova
            await _js.InvokeVoidAsync("rpgTheme.apply", cssClass);

            OnThemeChanged?.Invoke();
        }

        /// <summary>
        /// Atalho para aplicar pelo slug do sistema.
        /// </summary>
        public Task SetThemeBySlugAsync(string? slug)
            => SetThemeAsync(FromSystemSlug(slug));

        /// <summary>
        /// Volta ao tema neutro do hub.
        /// </summary>
        public Task ResetThemeAsync()
            => SetThemeAsync(RpgTheme.Hub);
    }
}
