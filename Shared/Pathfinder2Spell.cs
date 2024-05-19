using System.Text.Json.Serialization;

namespace Shared;

public sealed class Pathfinder2Spell: ISpell
{
    public string NethysUrl { get; set; } = "";
    public IEnumerable<string> Traits { get; set; } = [];
    
    public string Type { get; set; } = "";
    public int Level { get; set; }
    
    public IEnumerable<string> Traditions { get; set; } = [];

    /// <summary>
    /// An arbitrary, but unique ID for each spell.
    /// Do not assume that this ID is consistent between versions!
    /// It will change, when data is regenerated!
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";

    [JsonPropertyName("action")] 
    public string CastingTime { get; set; } = "";

    public string Range { get; set; } = "";
    public string Area { get; set; } = "";
    public string Targets { get; set; } = "";
    public string Duration { get; set; } = "";
    public string SavingThrow { get; set; } = "";
    
    [JsonPropertyName("description")]
    public string DescriptionFormatted { get; set; } = "";
    public string Source { get; set; } = "";
    public string ShortDescription { get; set; } = "";

    public IEnumerable<string> Components { get; set; } = [];

    public string Bloodline { get; set; } = "";
    public string PatronTheme { get; set; } = "";

    public string Rarity { get; set; } = "";
    
    public bool IsRemastered { get; set; }
}