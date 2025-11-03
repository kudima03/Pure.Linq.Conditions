using Pure.Linq.Conditions.Tests.Fakes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Bool;
using Pure.Primitives.Number;

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
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
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

    [Fact]
    public void ReturnsTrueForSingleCollection()
    {
        Assert.True(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange()]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseForDifferentLengths()
    {
        Assert.False(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), new IntRange(new Zero<int>(), new Int(30))]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenSameElementsDifferentOrder()
    {
        Assert.True(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), new IntRange().Reverse()]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenDifferentElements()
    {
        Assert.False(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), new IntRange().SkipLast(1).Append(new Int(100))]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenSameElementsDifferentCounts()
    {
        Assert.False(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(2)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenAllEmptyCollections()
    {
        Assert.True(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [],
                    [],
                    [],
                    [],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenOneCollectionEmptyAndOthersNot()
    {
        Assert.False(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), []]
            ).BoolValue
        );
    }

    [Fact]
    public void WorksWithCustomEquality()
    {
        Assert.True(
            new EqualCondition<int>(
                (a, b) => Math.Abs(a) == Math.Abs(b) ? new True() : new False(),
                [
                    [1, -2, 3],
                    [-1, 2, -3],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenCollectionHasExtraElementNotInFirst()
    {
        Assert.False(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(3)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWithMultipleIdenticalCollections()
    {
        Assert.True(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2), new Int(3), new Int(4)],
                    [new Int(4), new Int(3), new Int(2), new Int(1)],
                    [new Int(2), new Int(1), new Int(3), new Int(4)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseForAllUniqueElements()
    {
        Assert.False(
            new EqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2), new Int(3)],
                    [new Int(4), new Int(5), new Int(6)],
                ]
            ).BoolValue
        );
    }
}
