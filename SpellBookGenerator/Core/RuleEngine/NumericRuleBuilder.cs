using System.Numerics;

namespace SpellBookGenerator.Core.RuleEngine;


public class NumericRuleBuilder<TObject, TProperty>(Func<TObject, TProperty> selector)
    : RuleBuilder<TObject, TProperty>(selector)
    where TProperty : INumber<TProperty>
{
    public override NumericRuleBuilder<TObject, TProperty> Not()
    {
        BaseCase = !BaseCase;
        return this;
    }

    public override NumericRuleBuilder<TObject, TProperty> Should()
    {
        return this;
    }
    
    public NumericRuleBuilder<TObject, TProperty> BeBigger(TProperty lowerBound)
    {
        Rule = o => BaseCase == Selector(o) > lowerBound;
        return this;
    }
    
    public NumericRuleBuilder<TObject, TProperty> BeSmaller(TProperty upperBound)
    {
        Rule = o => BaseCase == Selector(o) < upperBound;
        return this;
    }

    public NumericRuleBuilder<TObject, TProperty> Equal(TProperty other)
    {
        Rule = o => BaseCase == (Selector(o) == other);
        return this;
    }
    
    /// <summary>
    /// Two sided non inclusive between
    /// </summary>
    /// <param name="upperBound">Exclusive lower bound</param>
    /// <param name="lowerBound">Exclusive upper bound</param>
    /// <returns></returns>
    public NumericRuleBuilder<TObject, TProperty> BeBetween(TProperty lowerBound, TProperty upperBound)
    {
        Rule = o => BaseCase == (Selector(o) < upperBound && Selector(o) > lowerBound);
        return this;
    }
}