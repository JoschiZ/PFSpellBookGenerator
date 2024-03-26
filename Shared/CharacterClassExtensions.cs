namespace Shared;

public static class CharacterClassExtensions
{
    public static Tradition GetTradition(this CharacterClass.Pathfinder2 characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Pathfinder2.Bard => Tradition.Occult,
            CharacterClass.Pathfinder2.Champion => Tradition.NoTradition,
            CharacterClass.Pathfinder2.Cleric => Tradition.Divine,
            CharacterClass.Pathfinder2.Druid => Tradition.Primal,
            CharacterClass.Pathfinder2.Magus => Tradition.Arcane,
            CharacterClass.Pathfinder2.Monk => Tradition.NoTradition,
            CharacterClass.Pathfinder2.Oracle => Tradition.Divine,
            CharacterClass.Pathfinder2.Psychic => Tradition.Occult,
            CharacterClass.Pathfinder2.Ranger => Tradition.NoTradition,
            CharacterClass.Pathfinder2.Sorcerer => Tradition.NoTradition,
            CharacterClass.Pathfinder2.Summoner => Tradition.NoTradition,
            CharacterClass.Pathfinder2.Witch => Tradition.NoTradition,
            CharacterClass.Pathfinder2.Wizard => Tradition.Arcane,
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }
}