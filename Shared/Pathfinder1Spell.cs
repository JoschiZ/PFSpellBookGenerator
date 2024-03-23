using System.Text.Json.Serialization;

namespace Shared;

public sealed class Pathfinder1Spell : SpellBase
{
    public string School { get; set; } = "";
    public string SubSchool { get; set; } = "";
    public string Descriptor { get; set; } = "";
    public string SpellResistance { get; set; } = "";
    
    [JsonPropertyName("SpellGrades")]
    public Levels Levels { get; set; } = new();
    public string Components { get; set; } = "";
    public override string NethysUrl { get; set; }
}