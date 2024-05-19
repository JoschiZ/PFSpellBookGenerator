using Shared;

namespace SpellDataConverter;

internal sealed class AONToInternalMapper
{
    public static Pathfinder2Spell ToPf2Spell(AONPf2Spell aonSpell)
    {

        if (!PatronThemeExtensions.TryParse(aonSpell.PatronTheme, out var patronTheme)) ;
        if (!BloodlineExtensions.TryParse(aonSpell.Bloodline, out var bloodline)) ;
        
        
        var newSpell = new Pathfinder2Spell()
        {
            NethysUrl = aonSpell.Url,
            Traits = aonSpell.Trait.Split(',').Select(x => x.Trim()),
            Type = aonSpell.SpellType,
            Level = int.Parse(aonSpell.Level),
            Traditions = aonSpell.Tradition.Split(',').Select(x => x.Trim()),
            Name = aonSpell.Name,
            CastingTime = aonSpell.Actions,
            Range = aonSpell.Range,
            Area = aonSpell.Area,
            Targets = aonSpell.Target,
            Duration = aonSpell.Duration,
            SavingThrow = aonSpell.SavingThrow,
            ShortDescription = aonSpell.Summary,
            Components = aonSpell.Component.Split(',').Select(x => x.Trim()),
            Bloodline = aonSpell.Bloodline,
            PatronTheme = aonSpell.PatronTheme,
            Rarity = aonSpell.Rarity,
            IsRemastered = IsRemastered(aonSpell.Source)
        };

        return newSpell;
    }

    private static bool IsRemastered(string source)
    {
        return source.Contains("Player Core", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("GM Core", StringComparison.InvariantCultureIgnoreCase);
    }
}