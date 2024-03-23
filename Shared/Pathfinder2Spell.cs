using System.Text.Json.Serialization;

namespace Shared;

public sealed class Pathfinder2Spell: SpellBase
{
    public override string NethysUrl { get; set; }
    public IEnumerable<string> Traits { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public IEnumerable<string> Traditions { get; set; }
    [JsonPropertyName("action")]
    public new string CastingTime { get; set; }
    public IEnumerable<string> Components { get; set; }
    
}