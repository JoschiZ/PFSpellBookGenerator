using System.Globalization;
using System.Text;

namespace Shared;

public interface ISpell
{
    public string Name { get; set; }
    public string CastingTime { get; set; }
    public string Range { get; set; }
    public string Area { get; set; }
    public string Targets { get; set; }
    public string Duration { get; set; }
    public string SavingThrow { get; set; }
    public string DescriptionFormatted { get; set; }
    public string Source { get; set; }
    public string ShortDescription { get; set; }
}