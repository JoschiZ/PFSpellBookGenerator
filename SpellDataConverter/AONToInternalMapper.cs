using Shared;

namespace SpellDataConverter;

internal sealed class AONToInternalMapper
{
    public static Pathfinder2Spell ToPf2Spell(AONPf2Spell aonSpell)
    {

        if (!PatronThemeExtensions.TryParse(aonSpell.PatronTheme, out var patronTheme)) ;
        if (!BloodlineExtensions.TryParse(aonSpell.Bloodline, out var bloodline)) ;

        List<Tradition> parsedTraditions = [];
        foreach (var traditionString in aonSpell.Tradition.Split(',').Select(x => x.Trim()))
        {
            if (Enum.TryParse<Tradition>(traditionString, out var tradition))
            {
                parsedTraditions.Add(tradition);
            }
        }

        if (parsedTraditions.Count == 0)
        {
            parsedTraditions.Add(Tradition.NoTradition);
        }
        
        var newSpell = new Pathfinder2Spell()
        {
            NethysUrl = "https://2e.aonprd.com" + aonSpell.Url,
            Traits = aonSpell.Trait.Split(',').Select(x => x.Trim()),
            Type = aonSpell.SpellType,
            Level = int.Parse(aonSpell.Level),
            Traditions = parsedTraditions.ToHashSet(),
            Name = aonSpell.Name,
            CastingTime = aonSpell.Actions,
            Range = aonSpell.Range,
            Area = aonSpell.Area,
            Targets = aonSpell.Target,
            Duration = aonSpell.Duration,
            SavingThrow = aonSpell.SavingThrow,
            ShortDescription = aonSpell.Summary,
            DescriptionFormatted = aonSpell.Description,
            Components = aonSpell.Component.Split(',').Select(x => x.Trim()),
            Bloodline = aonSpell.Bloodline,
            PatronTheme = aonSpell.PatronTheme,
            Rarity = aonSpell.Rarity,
            IsRemastered = IsRemastered(aonSpell.Source),
            Source = aonSpell.Source,
        };

        return newSpell;
    }

    private static bool IsRemastered(string source)
    {
        return source.Contains("Player Core", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("GM Core", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("Player Core 2", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("Howl of the Wild", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("Tian Xia World Guide", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("Monster Core", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("Pathfinder #200: Seven Dooms for Sandpoint", StringComparison.InvariantCultureIgnoreCase) ||
               source.Contains("Wake the Dead #5", StringComparison.InvariantCultureIgnoreCase);
    }
}