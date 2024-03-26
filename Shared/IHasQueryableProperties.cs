namespace Shared;



public interface IHasQueryableStrings<TSelf>
{
    public static abstract IEnumerable<QueryableInfo<TSelf, string>> QueryableStrings { get; }
}


public interface IHasQueryableInters<TSelf>
{
    public static abstract IEnumerable<QueryableInfo<TSelf, int>> QueryableIntegers { get; }
}

public interface IHasQueryableEnums<TSelf>
{
    public static abstract IEnumerable<QueryableInfo<TSelf, Enum>> QueryableEnums { get; }
}
public record QueryableInfo<TSelf, TProperty>(string Name, Func<TSelf, TProperty> Selector);