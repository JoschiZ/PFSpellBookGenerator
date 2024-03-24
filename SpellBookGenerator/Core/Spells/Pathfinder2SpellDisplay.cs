using Shared;

namespace SpellBookGenerator.Core.Spells;

public sealed class Pathfinder2SpellDisplay: ISpellDisplay<Pathfinder2Spell>
{
    internal Pathfinder2SpellDisplay(Pathfinder2Spell spell)
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
}