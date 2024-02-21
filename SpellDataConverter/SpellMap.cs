using System.Globalization;
using CsvHelper.Configuration;
using Shared;

namespace SpellDataConverter;

public sealed class SpellMap: ClassMap<Spell>
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
        
        Map(s => s.SpellGrades.Sorcerer).Name("sor").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Wizard).Name("wiz").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Cleric).Name("cleric").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Druid).Name("druid").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Ranger).Name("ranger").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Bard).Name("bard").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Paladin).Name("paladin").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Alchemist).Name("alchemist").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Summoner).Name("summoner").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Witch).Name("witch").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Inquisitor).Name("inquisitor").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Oracle).Name("oracle").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.AntiPaladin).Name("antipaladin").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Magus).Name("magus").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Adept).Name("adept").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.BloodRager).Name("bloodrager").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Shaman).Name("shaman").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Psychic).Name("psychic").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Medium).Name("medium").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Mesmerist).Name("mesmerist").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Occultist).Name("occultist").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Spiritualist).Name("spiritualist").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Skald).Name("skald").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Investigator).Name("investigator").TypeConverterOption.NullValues("NULL");
        Map(s => s.SpellGrades.Hunter).Name("hunter").TypeConverterOption.NullValues("NULL");
        
        
        Map(s => s.Source).Name("source");

        
        
        
        Map(s => s.CastingTime).Name("casting_time");
        Map(s => s.SavingThrow).Name("saving_throw");
        Map(s => s.SpellResistance).Name("spell_resistance");
        Map(s => s.DescriptionFormatted).Name("description_formatted");
        Map(s => s.ShortDescription).Name("short_description");
        
    }
}