using System.Text.RegularExpressions;

namespace SpellBookGenerator.Core.RuleEngine;

public class StringRuleBuilder<TObject>(Func<TObject, string> selector, Guid ruleId) : RuleBuilder<TObject, string>(selector, ruleId)
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

    public StringRuleBuilder<TObject> Contain(string valueToContain) => Contain(valueToContain, StringComparison.OrdinalIgnoreCase);
    public StringRuleBuilder<TObject> Contain(string valueToContain, StringComparison comparisonType)
    {
        Rule = o => BaseCase == Selector(o).Contains(valueToContain, comparisonType);
        return this;
    }
    
    public StringRuleBuilder<TObject> Equal(string valueToEqual) => Equal(valueToEqual, StringComparison.OrdinalIgnoreCase);
    public StringRuleBuilder<TObject> Equal(string valueToEqual, StringComparison comparisonType)
    {
        Rule = o => BaseCase == Selector(o).Equals(valueToEqual, comparisonType);
        return this;
    }

    public StringRuleBuilder<TObject> Match(string pattern) =>
        Match(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    public StringRuleBuilder<TObject> Match(string pattern, RegexOptions regexOptions)
    {
        try
        {
            var regex = new Regex(pattern, regexOptions);
            Rule = o => BaseCase == regex.IsMatch(Selector(o));
        }
        catch (RegexParseException)
        {
        }
        
        
        
        return this;
    }
}