using System.Numerics;
using MudBlazor;

namespace SpellBookGenerator.Core.RuleEngine;

public abstract class RulesCollectionBuilderBase<TObject> : IRuleBuilder<TObject>
{
    private readonly Dictionary<Guid, IRuleBuilder<TObject>> _rulesDictionary = [];
    
    private protected IEnumerable<IRuleBuilder<TObject>> Rules => _rulesDictionary.Values;
    
    public StringRuleBuilder<TObject> Add(Func<TObject, string> selector)
    {
        var srb = new StringRuleBuilder<TObject>(selector);
        _rulesDictionary.Add(Guid.NewGuid(), srb);
        return srb;
    }

    public NumericRuleBuilder<TObject, TNumber> Add<TNumber>(Func<TObject, TNumber> selector) where TNumber: INumber<TNumber>
    {
        var nrb = new NumericRuleBuilder<TObject, TNumber>(selector);
        _rulesDictionary.Add(Guid.NewGuid(), nrb);
        return nrb;
    }

    public AnyRulesCollectionBuilder<TObject> Any()
    {
        var anyBuilder = new AnyRulesCollectionBuilder<TObject>();
        _rulesDictionary.Add(Guid.NewGuid(), anyBuilder);
        return anyBuilder;
    }

    public AllRulesCollectionBuilder<TObject> All()
    {
        var allBuilder = new AllRulesCollectionBuilder<TObject>();
        _rulesDictionary.Add(Guid.NewGuid(), allBuilder);
        return allBuilder;
    }
    
    public abstract Func<TObject, bool> Build();
}