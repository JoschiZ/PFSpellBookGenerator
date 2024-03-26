using System.Text;
using System.Text.Json.Serialization;
using Shared;

namespace SpellBookGenerator.Core.Spells;

public sealed class Pathfinder1SpellDisplay: ISpellDisplay<Pathfinder1Spell>, IHasQueryableStrings<Pathfinder1SpellDisplay>, IHasQueryableInters<Pathfinder1SpellDisplay>
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
    
    public static IEnumerable<QueryableInfo<Pathfinder1SpellDisplay, string>> QueryableStrings { get; } = 
    [
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Name", display => display.Spell.Name),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Casting Time", display => display.Spell.CastingTime),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Range", display => display.Spell.Range),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Area", display => display.Spell.Area),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Targets", display => display.Spell.Targets),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Duration", display => display.Spell.Duration),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Saving Throw", display => display.Spell.SavingThrow),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Description", display => display.Spell.DescriptionFormatted),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Source", display => display.Spell.Source),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Short Description", display => display.Spell.ShortDescription),
        
        new QueryableInfo<Pathfinder1SpellDisplay, string>("School", display => display.Spell.School),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Sub school", display => display.Spell.SubSchool),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Descriptor", display => display.Spell.Descriptor),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Spell resistance", display => display.Spell.SpellResistance),
        new QueryableInfo<Pathfinder1SpellDisplay, string>("Components", display => display.Spell.Components),
    ];

    public static IEnumerable<QueryableInfo<Pathfinder1SpellDisplay, int>> QueryableIntegers { get; } =
    [
        new QueryableInfo<Pathfinder1SpellDisplay, int>("Level", display => display.CurrentSpellLevel),
    ];
    
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