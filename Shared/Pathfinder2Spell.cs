using System.Text.Json.Serialization;

namespace Shared;

public sealed class Pathfinder2Spell: ISpell
{
    public string NethysUrl { get; set; } = "";
    public IEnumerable<string> Traits { get; set; } = [];
    
    public string Type { get; set; } = "";
    public int Level { get; set; }
    
    public IEnumerable<Tradition> Traditions { get; set; } = [];

    /// <summary>
    /// An arbitrary, but unique ID for each spell.
    /// Do not assume that this ID is consistent between versions!
    /// It very likely changes, when data is regenerated!
    /// </summary>
    public int Id { get; set; }
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
    
    public Bloodline? Bloodline { get; set; }
    public PatronTheme? PatronTheme { get; set; }

    [JsonPropertyName("legacy content")]
    public bool IsLegacyContent { get; set; }
}