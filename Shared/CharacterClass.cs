using System.Text.Json;
using System.Text.Json.Serialization;
using NetEscapades.EnumGenerators;

namespace Shared;

public static class CharacterClass
{
    public enum Pathfinder1
    {
        AllSpells,
        Sorcerer,
        Wizard,
        Cleric,
        Druid,
        Ranger,
        Bard,
        Paladin,
        Alchemist,
        Summoner,
        Witch,
        Inquisitor,
        Oracle,
        AntiPaladin,
        Magus,
        Adept,
        BloodRager,
        Shaman,
        Psychic,
        Medium,
        Mesmerist,
        Occultist,
        Spiritualist,
        Skald,
        Investigator,
        Hunter
    }
    
    [EnumExtensions]
    public enum Pathfinder2
    {
        Bard,
        Champion,
        Cleric,
        Druid,
        Magus,
        Monk,
        Oracle,
        Psychic,
        Ranger,
        Sorcerer,
        Summoner,
        Witch,
        Wizard,
        Other,
    }
}


