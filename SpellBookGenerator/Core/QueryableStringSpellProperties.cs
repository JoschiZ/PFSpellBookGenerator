using System.ComponentModel.DataAnnotations;
using NetEscapades.EnumGenerators;

namespace SpellBookGenerator.Core;

[EnumExtensions]
public enum QueryableStringSpellProperties
{
    [Display(Name = "Area")]
    Area,
    [Display(Name = "Components")]
    Components,
    [Display(Name = "Descriptor")]
    Descriptor,
    [Display(Name = "Duration")]
    Duration,
    [Display(Name = "Name")]
    Name,
    [Display(Name = "Range")]
    Range,
    [Display(Name = "School")]
    School,
    [Display(Name = "Source")]
    Source,
    [Display(Name = "Targets")]
    Targets,
    [Display(Name = "Casting Time")]
    CastingTime,
    [Display(Name = "Description")]
    Description,
    [Display(Name = "Saving Throw")]
    SavingThrow,
    [Display(Name = "Short Description")]
    ShortDescription,
    [Display(Name = "Spell Resistance")]
    SpellResistance,
    [Display(Name = "Sub School")]
    SubSchool
}