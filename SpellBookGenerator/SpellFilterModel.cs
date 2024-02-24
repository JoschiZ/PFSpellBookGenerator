namespace SpellBookGenerator;


public class SpellFilterModel
{
    public int MinimumSpellLevel { get; set; }
    public int MaximumSpellLevel { get; set; } = 9;
    public string FilterExpression { get; set; } = "";
    public IEnumerable<SearchField> SearchFields = [];
}