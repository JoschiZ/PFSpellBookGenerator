namespace SpellBookGenerator.Core.RuleEngine;

public class RuleBuilder<TObject, TProperty>: IRuleBuilder<TObject>, IHasBuilderMethods<RuleBuilder<TObject, TProperty>, TObject>
{
    private protected Func<TObject, TProperty> Selector;
    private protected Func<TObject, bool> Rule;
    private protected bool BaseCase = true;
    public Guid RuleId { get; } = new Guid();

    protected RuleBuilder(Func<TObject, TProperty> selector, Guid ruleId)
    {
        Selector = selector;
        Rule = (o => BaseCase);
        RuleId = ruleId;
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

    public virtual RuleBuilder<TObject, TProperty> RuleFor(Func<TObject, TProperty> newSelector)
    {
        Selector = newSelector;
        return this;
    }
}