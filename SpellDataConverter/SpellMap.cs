using System.Globalization;
using CsvHelper.Configuration;
using Shared;

namespace SpellDataConverter;

public sealed class SpellMap: ClassMap<Pathfinder1Spell>
{
    public SpellMap()
    {
        Map(s => s.Name).Name("name");
        Map(s => s.School).Name("school");
        Map(s => s.SubSchool).Name("subschool");
        Map(s => s.Descriptor).Name("descriptor");
        Map(s => s.Components).Name("components");
        Map(s => s.Range).Name("range");
        Map(s => s.Area).Name("area");
        Map(s => s.Targets).Name("targets");
        Map(s => s.Duration).Name("duration");
        Map(s => s.Id).Name("id");
        
        Map(s => s.Levels.Sorcerer).Name("sor").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Wizard).Name("wiz").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Cleric).Name("cleric").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Druid).Name("druid").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Ranger).Name("ranger").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Bard).Name("bard").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Paladin).Name("paladin").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Alchemist).Name("alchemist").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Summoner).Name("summoner").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Witch).Name("witch").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Inquisitor).Name("inquisitor").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Oracle).Name("oracle").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.AntiPaladin).Name("antipaladin").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Magus).Name("magus").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Adept).Name("adept").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.BloodRager).Name("bloodrager").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Shaman).Name("shaman").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Psychic).Name("psychic").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Medium).Name("medium").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Mesmerist).Name("mesmerist").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Occultist).Name("occultist").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Spiritualist).Name("spiritualist").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Skald).Name("skald").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Investigator).Name("investigator").TypeConverterOption.NullValues("NULL");
        Map(s => s.Levels.Hunter).Name("hunter").TypeConverterOption.NullValues("NULL");
        
        
        Map(s => s.Source).Name("source");

        
        
        
        Map(s => s.CastingTime).Name("casting_time");
        Map(s => s.SavingThrow).Name("saving_throw");
        Map(s => s.SpellResistance).Name("spell_resistance");
        Map(s => s.DescriptionFormatted).Name("description_formatted");
        Map(s => s.ShortDescription).Name("short_description");
        
    }
}