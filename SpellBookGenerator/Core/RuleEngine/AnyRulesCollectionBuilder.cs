namespace SpellBookGenerator.Core.RuleEngine;

public class AnyRulesCollectionBuilder<TObject> : RulesCollectionBuilderBase<TObject>
{
    public override Func<TObject, bool> Build()
    {
        var rules = Rules.Select(rb => rb.Build());
        return o => rules.Any(rule => rule(o));
    }
}