namespace SpellBookGenerator.Core.RuleEngine;

public interface IRuleBuilder<in TObject>
{
    public Func<TObject, bool> Build();
}

public interface IHasBuilderMethods<out TSelf, TObject> where TSelf : IRuleBuilder<TObject>
{
    public TSelf Should();

    public TSelf Not();
}