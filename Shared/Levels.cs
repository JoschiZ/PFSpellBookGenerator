using System.Reflection;

namespace Shared;

public class Levels
{
    public int? Sorcerer { get; set; }
    public int? Wizard { get; set; }
    public int? Cleric { get; set; }
    public int? Druid { get; set; }
    public int? Ranger { get; set; }
    public int? Bard { get; set; }
    public int? Paladin { get; set; }
    public int? Alchemist { get; set; }
    public int? Summoner { get; set; }
    public int? Witch { get; set; }
    public int? Inquisitor { get; set; }
    public int? Oracle { get; set; }
    public int? AntiPaladin { get; set; }
    public int? Magus { get; set; }
    public int? Adept { get; set; }
    public int? BloodRager { get; set; }
    public int? Shaman { get; set; }
    public int? Psychic { get; set; }
    public int? Medium { get; set; }
    public int? Mesmerist { get; set; }
    public int? Occultist { get; set; }
    public int? Spiritualist { get; set; }
    public int? Skald { get; set; }
    public int? Investigator { get; set; }
    public int? Hunter { get; set; }

    public int GetLowestSpellGrade()
    {
        var grades = typeof(Levels).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        return (int)grades
            .Select(info => (int?)info.GetValue(this))
            .Where(grade => grade.HasValue)
            .Min()!; // There is always at least on class with a non null value
    }

    public int? GetGradeForClass(CharacterClass.Pathfinder1 characterClass) => characterClass switch
    {
        CharacterClass.Pathfinder1.AllSpells => GetLowestSpellGrade(),
        CharacterClass.Pathfinder1.Sorcerer => Sorcerer,
        CharacterClass.Pathfinder1.Wizard => Wizard,
        CharacterClass.Pathfinder1.Cleric => Cleric,
        CharacterClass.Pathfinder1.Druid => Druid,
        CharacterClass.Pathfinder1.Ranger => Ranger,
        CharacterClass.Pathfinder1.Bard => Bard,
        CharacterClass.Pathfinder1.Paladin => Paladin,
        CharacterClass.Pathfinder1.Alchemist => Alchemist,
        CharacterClass.Pathfinder1.Summoner => Summoner,
        CharacterClass.Pathfinder1.Witch => Witch,
        CharacterClass.Pathfinder1.Inquisitor => Inquisitor,
        CharacterClass.Pathfinder1.Oracle => Oracle,
        CharacterClass.Pathfinder1.AntiPaladin => AntiPaladin,
        CharacterClass.Pathfinder1.Magus => Magus,
        CharacterClass.Pathfinder1.Adept => Adept,
        CharacterClass.Pathfinder1.BloodRager => BloodRager,
        CharacterClass.Pathfinder1.Shaman => Shaman,
        CharacterClass.Pathfinder1.Psychic => Psychic,
        CharacterClass.Pathfinder1.Medium => Medium,
        CharacterClass.Pathfinder1.Mesmerist => Mesmerist,
        CharacterClass.Pathfinder1.Occultist => Occultist,
        CharacterClass.Pathfinder1.Spiritualist => Spiritualist,
        CharacterClass.Pathfinder1.Skald => Skald,
        CharacterClass.Pathfinder1.Investigator => Investigator,
        CharacterClass.Pathfinder1.Hunter => Hunter,
        _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
    };
}