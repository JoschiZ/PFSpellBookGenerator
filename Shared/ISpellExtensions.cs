using System.Text;

namespace Shared;

public static class SpellExtensions
{
    public static string GetRangeDisplay(this ISpell spellBase)
    {
        var sb = new StringBuilder();
        sb.Append(spellBase.Range);

        if (!string.IsNullOrWhiteSpace(spellBase.Targets))
        {
            sb.Append($" ({spellBase.Targets})");
        }

        return sb.ToString();
    }
}