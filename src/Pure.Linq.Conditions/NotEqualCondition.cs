using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool.Operations;

namespace Pure.Linq.Conditions;

public sealed record NotEqualCondition<T> : IBool
{
    private readonly IEnumerable<IEnumerable<T>> _values;

    private readonly Func<T, T, IBool> _equals;

    public NotEqualCondition(
        Func<T, T, IBool> equals,
        params IEnumerable<IEnumerable<T>> values
    )
    {
        _equals = equals;
        _values = values;
    }

    public bool BoolValue => new Not(new EqualCondition<T>(_equals, _values)).BoolValue;
}
