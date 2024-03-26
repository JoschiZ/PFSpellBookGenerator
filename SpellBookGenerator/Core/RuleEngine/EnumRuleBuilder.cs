using System.Runtime.InteropServices;

namespace SpellBookGenerator.Core.RuleEngine;

internal sealed class EnumRuleBuilder<TObject, TEnum>(Func<TObject, TEnum> selector, Guid ruleId) 
    : RuleBuilder<TObject, TEnum>(selector, ruleId) where TEnum: Enum
{
    public override EnumRuleBuilder<TObject, TEnum> Not()
    {
        BaseCase = !BaseCase;
        return this;
    }

    public override EnumRuleBuilder<TObject, TEnum> Should()
    {
        return this;
    }

    public EnumRuleBuilder<TObject, TEnum> Equal(TEnum value)
    {
        Rule = o => BaseCase == Selector(o).Equals(value);
        return this;
    }
    
}