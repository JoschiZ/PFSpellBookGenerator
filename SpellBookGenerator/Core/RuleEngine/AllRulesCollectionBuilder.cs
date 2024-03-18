namespace SpellBookGenerator.Core.RuleEngine;

public class AllRulesCollectionBuilder<TObject>: RulesCollectionBuilderBase<TObject>
{
    public override Func<TObject, bool> Build()
    {
        var rules = Rules.Select(rb => rb.Build());
        return o => rules.All(rule => rule(o));
    }
}