using Shared;

namespace SpellBookGenerator;

public class SelectedSpell
{
    public SelectedSpell(Spell spell, CharacterClass mainCharacterClass)
    {
        MainCharacterClass = mainCharacterClass;
        Spell = spell;
        CurrentSpellGrade = Spell.SpellGrades.GetGradeForClass(mainCharacterClass) ?? Spell.SpellGrades.GetLowestSpellGrade();
    }
    
    public CharacterClass MainCharacterClass { get; set; }
    public Spell Spell { get; set; }
    public int CurrentSpellGrade { get; set; }
}