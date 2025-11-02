namespace Pure.Linq.Conditions.Tests;

public sealed record EmptyConditionTests
{
    [Fact]
    public void ProduceCorrectResultOnEmptyCollection()
    {
        Assert.True(new EmptyCondition<int>([]).BoolValue);
    }

    [Fact]
    public void ProduceCorrectResultOnNotEmptyCollection()
    {
        Assert.False(new EmptyCondition<int>(Enumerable.Range(0, 10)).BoolValue);
    }
}
