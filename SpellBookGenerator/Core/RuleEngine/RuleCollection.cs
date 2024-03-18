namespace SpellBookGenerator.Core.RuleEngine;

public static class RuleCollection
{
    public static AnyRulesCollectionBuilder<TObject> Any<TObject>()
    {
        var anyBuilder = new AnyRulesCollectionBuilder<TObject>();
        return anyBuilder;
    }

    public static AllRulesCollectionBuilder<TObject> All<TObject>()
    {
        var anyBuilder = new AllRulesCollectionBuilder<TObject>();
        return anyBuilder;
    } 
}


