using System.Collections.Immutable;
using Pure.Primitives.Abstractions.Bool;

namespace Pure.Linq.Conditions;

public sealed record EqualCondition<T> : IBool
{
    private readonly IEnumerable<IEnumerable<T>> _values;

    private readonly Func<T, T, IBool> _equals;

    public EqualCondition(
        Func<T, T, IBool> equals,
        params IEnumerable<IEnumerable<T>> values
    )
    {
        _equals = equals;
        _values = values;
    }

    public bool BoolValue
    {
        get
        {
            ImmutableArray<ImmutableArray<T>> arrays =
            [
                .. _values.Select(x => x.ToImmutableArray()),
            ];

            return arrays.Length == 0
                ? throw new ArgumentException()
                : arrays.Select(a => a.Length).Distinct().Count() == 1
                    && arrays
                        .First()
                        .All(item =>
                            arrays
                                .Select(arr => arr.Count(c => _equals(item, c).BoolValue))
                                .Distinct()
                                .Count() == 1
                        );
        }
    }
}
