using Pure.Linq.Conditions.Tests.Fakes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Bool;
using Pure.Primitives.Number;

namespace Pure.Linq.Conditions.Tests;

public sealed record NotEqualConditionTests
{
    [Fact]
    public void ThrowsExceptionOnEmptyCollection()
    {
        _ = Assert.Throws<ArgumentException>(() =>
            new NotEqualCondition<int>((a, b) => new False(), []).BoolValue
        );
    }

    [Fact]
    public void ProduceNegativeResultOnEqualCollections()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
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

        _ = new NotEqualCondition<int>(
            (a, b) => a == b ? new True() : new False(),
            collections
        );

        Assert.DoesNotContain(collections, x => x.Evaluated);
    }

    [Fact]
    public void ReturnsFalseForSingleCollection()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange()]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueForDifferentLengths()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), new IntRange(new Zero<int>(), new Int(30))]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenSameElementsDifferentOrder()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), new IntRange().Reverse()]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenDifferentElements()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), new IntRange().SkipLast(1).Append(new Int(100))]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenSameElementsDifferentCounts()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(2)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenAllEmptyCollections()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
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
    public void ReturnsTrueWhenOneCollectionEmptyAndOthersNot()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [new IntRange(), []]
            ).BoolValue
        );
    }

    [Fact]
    public void WorksWithCustomEquality()
    {
        Assert.False(
            new NotEqualCondition<int>(
                (a, b) => Math.Abs(a) == Math.Abs(b) ? new True() : new False(),
                [
                    [1, -2, 3],
                    [-1, 2, -3],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenCollectionHasExtraElementNotInFirst()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(3)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWithMultipleIdenticalCollections()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
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
    public void ReturnsTrueForAllUniqueElements()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2), new Int(3)],
                    [new Int(4), new Int(5), new Int(6)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenExtraElementInLaterCollection()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(3)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenExtraElementAppearsOnlyInThirdCollection()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(3)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenAllCollectionsContainSameElementsNoExtras()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2), new Int(3)],
                    [new Int(3), new Int(2), new Int(1)],
                    [new Int(2), new Int(1), new Int(3)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void DetectsExtraElementInSecondCollection()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(3)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void DetectsExtraElementOnlyInThirdCollection()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(99)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void DetectsExtraElementWithDifferentCountsAcrossLaterCollections()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1), new Int(2), new Int(7)],
                    [new Int(1), new Int(2), new Int(7), new Int(7)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void DetectsExtraElementEvenWhenFirstHasDuplicates()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(1), new Int(2)],
                    [new Int(1), new Int(1), new Int(2)],
                    [new Int(1), new Int(1), new Int(2), new Int(5)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ThrowsOnEmptyInput()
    {
        _ = Assert.Throws<ArgumentException>(() =>
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                []
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenCollectionsHaveDifferentLengths()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(1)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenSameLengthButDifferentElements()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                    [new Int(3), new Int(4)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenCollectionsEqualAsMultiset()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2), new Int(3)],
                    [new Int(3), new Int(2), new Int(1)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenSingleCollectionProvided()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [new Int(1), new Int(2)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsTrueWhenAllEmptyExceptOneNonEmpty()
    {
        Assert.True(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [],
                    [new Int(1)],
                ]
            ).BoolValue
        );
    }

    [Fact]
    public void ReturnsFalseWhenAllCollectionsEmpty()
    {
        Assert.False(
            new NotEqualCondition<INumber<int>>(
                (a, b) => new Primitives.Number.Operations.EqualCondition<int>(a, b),
                [
                    [],
                    [],
                    [],
                ]
            ).BoolValue
        );
    }
}
