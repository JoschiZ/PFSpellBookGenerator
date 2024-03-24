namespace SpellBookGenerator.Core;

internal static class Routes
{
    public const string Home = "/";
    
    internal static class Pathfinder2
    {
        public const string Builder = "pf2";
    }
    
    internal static class Pathfinder1
    {
        public const string Builder = "pf1";
        public const string Print = Builder + "/print";
    }
}