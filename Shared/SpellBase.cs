using System.Globalization;
using System.Text;

namespace Shared;

public abstract class SpellBase
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string CastingTime { get; set; } = "";
    public string Range { get; set; } = "";
    public string Area { get; set; } = "";
    public string Targets { get; set; } = "";
    public string Duration { get; set; } = "";
    public string SavingThrow { get; set; } = "";
    public string DescriptionFormatted { get; set; } = "";
    public string Source { get; set; } = "";
    
    /// <summary>
    /// A shorter description for display, currently unimplemented for PF2
    /// </summary>
    // TODO: Do some Archives scraping
    public string ShortDescription { get; set; } = "";
    public abstract string NethysUrl { get; set; }
}