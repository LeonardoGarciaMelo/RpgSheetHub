namespace RpgSheetHub.Models
{
    /// <summary>
    /// Representa o molde de um sistema de RPG (ex: D&D 5.5e, GURPS, Ordem Paranormal).
    /// Define as regras e quais campos as fichas baseadas neste sistema deverão conter.
    /// </summary>
    public class SystemTemplate
    {
        /// <summary>
        /// Identificador único do molde do sistema.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome do sistema de RPG (ex: "Dungeons & Dragons 5.5e").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Lista contendo todas as definições de campos exigidas por este sistema.
        /// </summary>
        public List<FieldDefinition> Fields { get; set; } = new();
    }

    /// <summary>
    /// Define a estrutura de um campo individual dentro de uma ficha de RPG.
    /// Funciona como a "planta baixa" para gerar as caixas de texto e números na tela.
    /// </summary>
    public class FieldDefinition
    {
        /// <summary>
        /// Chave de identificação única do campo no dicionário de dados (ex: "forca", "pv_max").
        /// Não deve conter espaços ou caracteres especiais.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// O nome amigável do campo que será exibido para o usuário na tela (ex: "Força", "Pontos de Vida").
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Define o tipo de dado e como ele será renderizado em HTML.
        /// Valores comuns: "text", "number", "textarea", "checkbox".
        /// </summary>
        public string InputType { get; set; } = "text";

        /// <summary>
        /// Categoria ou aba para agrupar visualmente o campo na ficha (ex: "Atributos Principais", "Inventário").
        /// </summary>
        public string Category { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa a ficha de um personagem preenchida por um jogador.
    /// Armazena os dados dinamicamente com base no molde (<see cref="SystemTemplate"/>) escolhido.
    /// </summary>
    public class CharacterSheet
    {
        /// <summary>
        /// Identificador único da ficha do personagem.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome do personagem.
        /// </summary>
        public string CharacterName { get; set; } = string.Empty;

        /// <summary>
        /// Nome do jogador dono da ficha.
        /// </summary>
        public string PlayerName { get; set; } = string.Empty;

        /// <summary>
        /// Referência ao ID do <see cref="SystemTemplate"/> que dita as regras desta ficha.
        /// </summary>
        public Guid SystemTemplateId { get; set; }

        /// <summary>
        /// Dicionário flexível que armazena os valores reais preenchidos pelo jogador.
        /// A chave (string) corresponde à propriedade 'Key' da 'FieldDefinition'.
        /// O valor (object) armazena o que foi digitado (texto, número, booleano, etc).
        /// </summary>
        public Dictionary<string, object> Attributes { get; set; } = new();
    }
}