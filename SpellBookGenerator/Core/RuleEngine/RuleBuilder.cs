namespace SpellBookGenerator.Core.RuleEngine;

public class RuleBuilder<TObject, TProperty>: IRuleBuilder<TObject>, IHasBuilderMethods<RuleBuilder<TObject, TProperty>, TObject>
{
    private protected readonly Func<TObject, TProperty> Selector;
    private protected Func<TObject, bool> Rule;
    private protected bool BaseCase = true;

    protected RuleBuilder(Func<TObject, TProperty> selector)
    {
        Selector = selector;
        Rule = (o => BaseCase);
    }

    public RuleBuilder<TObject, TProperty> Custom(Func<TObject, bool> func)
    {
        Rule = (o => BaseCase && func(o));
        return this;
    }

    public Func<TObject, bool> Build()
    {
        return Rule;
    }

    public virtual RuleBuilder<TObject, TProperty> Should()
    {
        return this;
    }

    public virtual RuleBuilder<TObject, TProperty> Not()
    {
        BaseCase = !BaseCase;
        return this;
    }
}