using System.Text.Json.Serialization;

namespace Shared;

public sealed class Pathfinder1Spell : ISpell
{
    public string School { get; set; } = "";
    public string SubSchool { get; set; } = "";
    public string Descriptor { get; set; } = "";
    public string SpellResistance { get; set; } = "";
    
    [JsonPropertyName("SpellGrades")]
    public Levels Levels { get; set; } = new();
    public string Components { get; set; } = "";
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string CastingTime { get; set; } = "";
    public string Range { get; set; } = "";
    public string Area { get; set; } = "";
    public string Targets { get; set; } = "";
    public string Duration { get; set; } = "";
    public string SavingThrow { get; set; } = "";
    public string DescriptionFormatted { get; set; } = "";
    public string Source { get; set; } = "";
    public string ShortDescription { get; set; } = "";
}