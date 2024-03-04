using System.Text;
using Microsoft.Extensions.Primitives;
using Shared;

namespace SpellBookGenerator;

public class SelectedSpell
{
    public SelectedSpell(Spell spell, CharacterClass mainCharacterClass)
    {
        MainCharacterClass = mainCharacterClass;
        Spell = spell;
        CurrentSpellLevel = Spell.SpellGrades.GetGradeForClass(mainCharacterClass) ?? Spell.SpellGrades.GetLowestSpellGrade();
        SchoolDisplay = GetSchoolDisplay(spell);
        RangeDisplay = GetRangeDisplay(spell);
        SpellResistDisplay = GetSpellResistDisplay(spell);
    }
    
    public CharacterClass MainCharacterClass { get; set; }
    public Spell Spell { get; set; }
    public int CurrentSpellLevel { get; set; }
    
    public string SchoolDisplay { get; set; }

    private static string GetSchoolDisplay(Spell spell)
    {
        var sb = new StringBuilder();
        sb.Append(spell.School);

        if (!string.IsNullOrWhiteSpace(spell.SubSchool))
        {
            sb.Append(' ');
            sb.Append($"({spell.SubSchool})");
        }
        
        if (!string.IsNullOrWhiteSpace(spell.Descriptor))
        {
            sb.Append($" [{spell.Descriptor}]");
        }

        return sb.ToString();
    }

    public string RangeDisplay { get; set; }
    private static string GetRangeDisplay(Spell spell)
    {
        var sb = new StringBuilder();
        sb.Append(spell.Range);

        if (!string.IsNullOrWhiteSpace(spell.Targets))
        {
            sb.Append($" ({spell.Targets})");
        }

        return sb.ToString();
    }
    
    public string SpellResistDisplay { get; set; }

    private static string GetSpellResistDisplay(Spell spell)
    {
        var sb = new StringBuilder();
        sb.Append(spell.SavingThrow);

        if (!string.IsNullOrWhiteSpace(spell.SpellResistance))
        {
            sb.Append($" (SR: {spell.SpellResistance})");
        }

        return sb.ToString();
    }
}