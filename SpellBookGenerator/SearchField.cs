using System.ComponentModel.DataAnnotations;
using NetEscapades.EnumGenerators;

namespace SpellBookGenerator;

[EnumExtensions]
public enum SearchField
{
    Name,
    [Display(Name = "Short Description")]
    ShortDescription,
    [Display(Name = "Full Description")]
    FullDescription,
    Range
}