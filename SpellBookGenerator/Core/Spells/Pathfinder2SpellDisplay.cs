using System.Text.Json.Serialization;
using Shared;

namespace SpellBookGenerator.Core.Spells;

public sealed class Pathfinder2SpellDisplay: ISpellDisplay<Pathfinder2Spell>, IHasQueryableStrings<Pathfinder2SpellDisplay>, IHasQueryableInters<Pathfinder2SpellDisplay>
{
    public Pathfinder2SpellDisplay(Pathfinder2Spell spell)
    {
        Spell = spell;
        ArchivesOfNethysUrl = spell.NethysUrl;
        RangeDisplay = spell.Range;
        CurrentSpellLevel = spell.Level;
        RangeDisplay = spell.Range;
    }

    public Pathfinder2Spell Spell { get; set; }
    public int CurrentSpellLevel { get; set; }
    public string RangeDisplay { get; set; }
    public string ArchivesOfNethysUrl { get; init; }
    
    public static IEnumerable<QueryableInfo<Pathfinder2SpellDisplay, string>> QueryableStrings { get; } = 
    [
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Name", display => display.Spell.Name),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Casting Time", display => display.Spell.CastingTime),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Range", display => display.Spell.Range),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Area", display => display.Spell.Area),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Targets", display => display.Spell.Targets),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Duration", display => display.Spell.Duration),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Saving Throw", display => display.Spell.SavingThrow),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Description", display => display.Spell.DescriptionFormatted),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Source", display => display.Spell.Source),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Short Description", display => display.Spell.ShortDescription),
        
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Traits", display => string.Join(" ", display.Spell.Traits)),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Components", display => string.Join(" ", display.Spell.Components)),
        
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Bloodline", display => display.Spell.Bloodline ?? ""),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Patron", display => display.Spell.PatronTheme ?? ""),
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Traditions", display => string.Join(" ", display.Spell.Traditions)),
        
        new QueryableInfo<Pathfinder2SpellDisplay, string>("Rarity", display => display.Spell.Rarity),
    ];

    public static IEnumerable<QueryableInfo<Pathfinder2SpellDisplay, int>> QueryableIntegers { get; } =
    [
        new QueryableInfo<Pathfinder2SpellDisplay, int>("Level", display => display.CurrentSpellLevel),
    ];
}