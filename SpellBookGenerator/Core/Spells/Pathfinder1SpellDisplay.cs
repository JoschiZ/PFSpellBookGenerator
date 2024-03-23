using System.Text;
using System.Text.Json.Serialization;
using Shared;

namespace SpellBookGenerator.Core.Spells;

public sealed class Pathfinder1SpellDisplay: SpellDisplay<Pathfinder1Spell>
{
    
    public string SchoolDisplay { get; set; }

    public Pathfinder1SpellDisplay(Pathfinder1Spell spell, CharacterClass mainCharacterClass) : base(spell, mainCharacterClass)
    {
        CurrentSpellLevel = GetCurrentSpellLevel(spell);
        SchoolDisplay = GetSchoolDisplay(spell);
        RangeDisplay = GetRangeDisplay(spell);
        SpellResistDisplay = GetSpellResistDisplay(spell);
        ArchivesOfNethysUrl = $"https://aonprd.com/SpellDisplay.aspx?ItemName={spell.Name}";
    }

    public string SpellResistDisplay { get; set; }
    public override string RangeDisplay { get; set; }
    public override string ArchivesOfNethysUrl { get; init; }

    public override int GetCurrentSpellLevel(Pathfinder1Spell spell)
    {
        return spell.Levels.GetGradeForClass(MainCharacterClass) ?? spell.Levels.GetLowestSpellGrade();
    }
    
    private static string GetSchoolDisplay(Pathfinder1Spell spellBase)
    {
        var sb = new StringBuilder();
        sb.Append(spellBase.School);

        if (!string.IsNullOrWhiteSpace(spellBase.SubSchool))
        {
            sb.Append(' ');
            sb.Append($"({spellBase.SubSchool})");
        }
        
        if (!string.IsNullOrWhiteSpace(spellBase.Descriptor))
        {
            sb.Append($" [{spellBase.Descriptor}]");
        }

        return sb.ToString();
    }


    private protected override string GetRangeDisplay(Pathfinder1Spell spellBase)
    {
        var sb = new StringBuilder();
        sb.Append(spellBase.Range);

        if (!string.IsNullOrWhiteSpace(spellBase.Targets))
        {
            sb.Append($" ({spellBase.Targets})");
        }

        return sb.ToString();
    }

    private static string GetSpellResistDisplay(Pathfinder1Spell spellBase)
    {
        var sb = new StringBuilder();
        sb.Append(spellBase.SavingThrow);

        if (!string.IsNullOrWhiteSpace(spellBase.SpellResistance))
        {
            sb.Append($" (SR: {spellBase.SpellResistance})");
        }

        return sb.ToString();
    }
}