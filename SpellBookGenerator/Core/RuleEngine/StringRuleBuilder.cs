using System.Text.RegularExpressions;

namespace SpellBookGenerator.Core.RuleEngine;

public class StringRuleBuilder<TObject>(Func<TObject, string> selector) : RuleBuilder<TObject, string>(selector)
{
    public override StringRuleBuilder<TObject> Not()
    {
        BaseCase = !BaseCase;
        return this;
    }

    public override StringRuleBuilder<TObject> Should()
    {
        return this;
    }
    
    public StringRuleBuilder<TObject> Contain(string valueToContain, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
    {
        Rule = o => BaseCase == Selector(o).Contains(valueToContain, comparisonType);
        return this;
    }
    
    public StringRuleBuilder<TObject> Equal(string valueToEqual, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
    {
        Rule = o => BaseCase == Selector(o).Equals(valueToEqual, comparisonType);
        return this;
    }

    public StringRuleBuilder<TObject> Match(string pattern, RegexOptions regexOptions)
    {
        Rule = o => BaseCase == Regex.IsMatch(Selector(o), pattern, regexOptions);
        return this;
    }
}