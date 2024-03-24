using System.Text.Json.Serialization;
using Shared;

namespace SpellBookGenerator.Core.Spells;



public interface ISpellDisplay<TSpell> where TSpell: ISpell
{
    public TSpell Spell { get; set; }
    public int CurrentSpellLevel { get; set; }
    public abstract string RangeDisplay { get; set; }
    public abstract string ArchivesOfNethysUrl { get; init; }
}