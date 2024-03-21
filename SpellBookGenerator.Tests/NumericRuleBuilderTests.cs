using SpellBookGenerator.Core.RuleEngine;

namespace SpellBookGenerator.Tests;

public class NumericRuleBuilderTests
{
    private record Person(int Age);
    
    [Fact]
    public void ShouldBeBigger()
    {
        var rb = new NumericRuleBuilder<Person, int>(p => p.Age, Guid.NewGuid());
        rb.Should().BeBigger(18);
        var rule = rb.Build();

        var valid = new Person(20);
        Assert.True(rule(valid));

        var invalid = new Person(10);
        Assert.False(rule(invalid));
    }
    
    [Fact]
    public void ShouldBeSmaller()
    {
        var rb = new NumericRuleBuilder<Person, int>(p => p.Age, Guid.NewGuid());
        rb.Should().BeSmaller(18);
        var rule = rb.Build();

        var valid = new Person(10);
        Assert.True(rule(valid));

        var invalid = new Person(20);
        Assert.False(rule(invalid));
    }
    
    
    [Fact]
    public void Between()
    {
        var rb = new NumericRuleBuilder<Person, int>(p => p.Age, Guid.NewGuid());
        rb.Should().BeBetween(14, 18);
        var rule = rb.Build();

        var valid = new Person(15);
        Assert.True(rule(valid));

        var invalid = new Person(14);
        Assert.False(rule(invalid));
    }

    [Fact]
    public void ShouldNotBetween()
    {
        var rb = new NumericRuleBuilder<Person, int>(p => p.Age, Guid.NewGuid());
        rb.Should().Not().BeBetween(14, 18);
        var rule = rb.Build();

        var valid = new Person(99);
        Assert.True(rule(valid));

        var invalid = new Person(15);
        Assert.False(rule(invalid));
    }
}