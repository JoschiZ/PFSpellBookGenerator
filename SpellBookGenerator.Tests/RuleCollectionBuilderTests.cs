using SpellBookGenerator.Core;
using SpellBookGenerator.Core.RuleEngine;

namespace SpellBookGenerator.Tests;

public class RuleCollectionBuilderTests
{
    private record Person(string Name, string LastName, int Age = 18);
    
    
    [Fact]
    public void ShouldValidateAny()
    {
        var rulesBuilder = RuleCollection.Any<Person>();

        rulesBuilder.Add(person => person.Name).Should().Equal("Frank");
        rulesBuilder.Add(person => person.LastName).Should().Contain("Meier");

        var rule = rulesBuilder.Build();

        var person = new Person("Frank", "Müller");
        Assert.True(rule(person));
        
        var anotherPerson = new Person("Hans", "Meier");
        Assert.True(rule(anotherPerson));

        var invalidPerson = new Person("Melina", "Anderson");
        Assert.False(rule(invalidPerson));
    }

    [Fact]
    public void ShouldValidateAll()
    {
        var rulesBuilder = RuleCollection.All<Person>();

        rulesBuilder.Add(person => person.Name).Should().Equal("Frank");
        rulesBuilder.Add(person => person.LastName).Should().Contain("Meier");
        rulesBuilder.Add(person => person.LastName).Should().Not().Contain("x");

        var rule = rulesBuilder.Build();

        var person = new Person("Frank", "Meier");
        Assert.True(rule(person));

        var invalidPerson = new Person("Frank", "Anderson");
        Assert.False(rule(invalidPerson));
    }

    [Fact]
    public void ShouldValidateNestedCollections()
    {
        var rulesBuilder = RuleCollection.All<Person>();

        var anyRuleSet = rulesBuilder.Any();
        anyRuleSet.Add(person => person.Name).Equal("Frank");
        anyRuleSet.Add(person => person.Name).Equal("Peter");

        var allRuleSet = rulesBuilder.All();
        allRuleSet.Add(person => person.Name).Should().Equal("Frank");
        allRuleSet.Add(person => person.LastName).Should().Equal("Ghimli");

        var rule = rulesBuilder.Build();
        
        var validPerson = new Person("Frank", "Ghimli");
        Assert.True(rule(validPerson));

        var invalidPerson = new Person("Karsten", "Meier");
        Assert.False(rule(invalidPerson));
    }

    [Fact]
    public void ShouldValidateDeeplyNestedCollections()
    {
        var rulesBuilder = RuleCollection.All<Person>();
        var subBuilder = rulesBuilder.All().All().All().All().All().All().All().Any();
        subBuilder.Add(p => p.Name).Should().Equal("Frank");
        subBuilder.Add(p => p.Name).Should().Equal("Peter");

        var rule = rulesBuilder.Build();
        
        var validPerson = new Person(Name: "Frank", "");
        Assert.True(rule(validPerson));
        
        var invalidPerson = new Person(Name: "Hans", "");
        Assert.False(rule(invalidPerson));
    }
}