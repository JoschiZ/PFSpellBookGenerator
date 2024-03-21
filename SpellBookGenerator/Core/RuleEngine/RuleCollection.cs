namespace SpellBookGenerator.Core.RuleEngine;

public static class RuleCollection
{
    public static AnyRulesCollectionBuilder<TObject> Any<TObject>()
    {
        var anyBuilder = new AnyRulesCollectionBuilder<TObject>(Guid.NewGuid());
        return anyBuilder;
    }

    public static AllRulesCollectionBuilder<TObject> All<TObject>()
    {
        var anyBuilder = new AllRulesCollectionBuilder<TObject>(Guid.NewGuid());
        return anyBuilder;
    } 
}


