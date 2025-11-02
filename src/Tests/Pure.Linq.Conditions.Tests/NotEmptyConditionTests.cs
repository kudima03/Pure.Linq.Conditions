using Pure.Linq.Conditions.Tests.Fakes;

namespace Pure.Linq.Conditions.Tests;

public sealed record NotEmptyConditionTests
{
    [Fact]
    public void ProduceCorrectResultOnEmptyCollection()
    {
        Assert.False(new NotEmptyCondition<int>([]).BoolValue);
    }

    [Fact]
    public void ProduceCorrectResultOnNotEmptyCollection()
    {
        Assert.True(new NotEmptyCondition<int>(Enumerable.Range(0, 10)).BoolValue);
    }

    [Fact]
    public void NotEvaluateWithoutAccess()
    {
        EnumerableWithEvaluationMarker collection = new EnumerableWithEvaluationMarker();

        _ = new NotEmptyCondition<int>(collection);

        Assert.False(collection.Evaluated);
    }
}
