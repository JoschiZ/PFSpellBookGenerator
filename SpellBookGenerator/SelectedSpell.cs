using Shared;

namespace SpellBookGenerator;

public class SelectedSpell
{
    public SelectedSpell(Spell spell, Classes mainClass)
    {
        MainClass = mainClass;
        Spell = spell;
        CurrentSpellGrade = Spell.SpellGrades.GetGradeForClass(mainClass) ?? Spell.SpellGrades.GetLowestSpellGrade();
    }
    
    public Classes MainClass { get; set; }
    public Spell Spell { get; set; }
    public int CurrentSpellGrade { get; set; }
}