using Pure.Linq.Conditions.Tests.Fakes;

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

    [Fact]
    public void NotEvaluateWithoutAccess()
    {
        EnumerableWithEvaluationMarker collection = new EnumerableWithEvaluationMarker();

        _ = new EmptyCondition<int>(collection);

        Assert.False(collection.Evaluated);
    }
}
