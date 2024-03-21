using SpellBookGenerator.Core;
using SpellBookGenerator.Core.RuleEngine;

namespace SpellBookGenerator.Tests;

public class StringBuilderTests
{
    [Fact]
    public void ShouldContain()
    {
        var value = "HelloWorld";
        var valueToContain = "Hello";
        var rb = new StringRuleBuilder<string?>(objectToTest => objectToTest, Guid.NewGuid()).Should().Contain(valueToContain).Build();
        Assert.True(rb(value));
    }
    
    [Fact]
    public void ShouldNotContain()
    {
        var value = "HelloWorld";
        var valueToContain = "x";
        var rb = new StringRuleBuilder<string?>((o => o), Guid.NewGuid()).Should().Not().Contain(valueToContain).Build();
        Assert.True(rb(value));
    }
    
    [Fact]
    public void ShouldEqual()
    {
        var value = "HelloWorld";
        var valueToContain = "HelloWorld";
        var rb = new StringRuleBuilder<string?>((o => o), Guid.NewGuid()).Should().Equal(valueToContain).Build();
        Assert.True(rb(value));
    }

    [Fact]
    public void ShouldNotEqual()
    {
        var value = "HelloWorld";
        var rb = new StringRuleBuilder<string?>((o => o), Guid.NewGuid()).Should().Not().Equal("Peter").Build();
        Assert.True(rb(value));
    }

}