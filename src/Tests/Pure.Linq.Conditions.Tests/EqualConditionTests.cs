using Pure.Linq.Conditions.Tests.Fakes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Bool;

namespace Pure.Linq.Conditions.Tests;

public sealed record EqualConditionTests
{
    [Fact]
    public void ThrowsExceptionOnEmptyCollection()
    {
        _ = Assert.Throws<ArgumentException>(() =>
            new EqualCondition<int>((a, b) => new False(), []).BoolValue
        );
    }

    [Fact]
    public void ProducePositiveResultOnEqualCollections()
    {
        Assert.True(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(),
                [
                    new IntRange(),
                    new IntRange(),
                    new IntRange(),
                    new IntRange(),
                    new IntRange(),
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void NotEvaluateWithoutAccess()
    {
        EnumerableWithEvaluationMarker[] collections =
        [
            new EnumerableWithEvaluationMarker(),
            new EnumerableWithEvaluationMarker(),
            new EnumerableWithEvaluationMarker(),
            new EnumerableWithEvaluationMarker(),
        ];

        _ = new EqualCondition<int>(
            (a, b) => a == b ? new True() : new False(),
            collections
        );

        Assert.DoesNotContain(collections, x => x.Evaluated);
    }
}
