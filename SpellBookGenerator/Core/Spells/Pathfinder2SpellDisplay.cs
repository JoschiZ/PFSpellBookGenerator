using Shared;

namespace SpellBookGenerator.Core.Spells;

public sealed class Pathfinder2SpellDisplay: SpellDisplay<Pathfinder2Spell>
{
    internal Pathfinder2SpellDisplay(Pathfinder2Spell spell, CharacterClass mainCharacterClass) : base(spell, mainCharacterClass)
    {
    }
    
    public override string RangeDisplay { get; set; }

    public override int GetCurrentSpellLevel(Pathfinder2Spell spell)
    {
        return spell.Level;
    }
    

    private protected override string GetRangeDisplay(Pathfinder2Spell spellBase)
    {
        throw new NotImplementedException();
    }
}