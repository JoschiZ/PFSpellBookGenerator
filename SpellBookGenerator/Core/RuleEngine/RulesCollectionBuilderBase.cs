using System.Numerics;
using MudBlazor;

namespace SpellBookGenerator.Core.RuleEngine;

public abstract class RulesCollectionBuilderBase<TObject>(Guid ruleId) : IRuleBuilder<TObject>
{
    public Guid RuleId { get; } = ruleId;
    private readonly Dictionary<Guid, IRuleBuilder<TObject>> _rulesDictionary = [];

    public IEnumerable<IRuleBuilder<TObject>> Rules => _rulesDictionary.Values;
    
    public StringRuleBuilder<TObject> Add(Func<TObject, string?> selector)
    {
        var ruleId = Guid.NewGuid();
        var srb = new StringRuleBuilder<TObject>(selector, ruleId);
        _rulesDictionary.Add(ruleId, srb);
        return srb;
    }

    public NumericRuleBuilder<TObject, TNumber> Add<TNumber>(Func<TObject, TNumber> selector) where TNumber: INumber<TNumber>
    {
        var ruleId = Guid.NewGuid();
        var nrb = new NumericRuleBuilder<TObject, TNumber>(selector, ruleId);
        _rulesDictionary.Add(ruleId, nrb);
        return nrb;
    }

    public AnyRulesCollectionBuilder<TObject> Any()
    {
        var ruleId = Guid.NewGuid();
        var anyBuilder = new AnyRulesCollectionBuilder<TObject>(ruleId);
        _rulesDictionary.Add(ruleId, anyBuilder);
        return anyBuilder;
    }

    public AllRulesCollectionBuilder<TObject> All()
    {
        var ruleId = Guid.NewGuid();
        var allBuilder = new AllRulesCollectionBuilder<TObject>(ruleId);
        _rulesDictionary.Add(ruleId, allBuilder);
        return allBuilder;
    }

    public RulesCollectionBuilderBase<TObject> RemoveRule(Guid ruleId)
    {
        _rulesDictionary.Remove(ruleId);
        return this;
    }
    
    public abstract Func<TObject, bool> Build();

}