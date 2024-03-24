using System.Text;
using System.Text.Json.Serialization;
using Shared;

namespace SpellBookGenerator.Core.Spells;

public sealed class Pathfinder1SpellDisplay: ISpellDisplay<Pathfinder1Spell>
{
    
    public string SchoolDisplay { get; set; }

    public Pathfinder1SpellDisplay(Pathfinder1Spell spell, CharacterClass.Pathfinder1 mainCharacterClass)
    {
        CurrentSpellLevel = GetCurrentSpellLevel(spell);
        SchoolDisplay = GetSchoolDisplay(spell);
        RangeDisplay = spell.GetRangeDisplay();
        SpellResistDisplay = GetSpellResistDisplay(spell);
        ArchivesOfNethysUrl = $"https://aonprd.com/SpellDisplay.aspx?ItemName={spell.Name}";
        Spell = spell;
        MainCharacterClass = mainCharacterClass;
    }

    public string SpellResistDisplay { get; set; }
    public Pathfinder1Spell Spell { get; set; }
    public int CurrentSpellLevel { get; set; }
    public string RangeDisplay { get; set; }
    public string ArchivesOfNethysUrl { get; init; }
    
    public CharacterClass.Pathfinder1 MainCharacterClass { get; set; }

    public int GetCurrentSpellLevel(Pathfinder1Spell spell)
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