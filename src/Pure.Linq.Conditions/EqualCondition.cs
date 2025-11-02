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

    public bool BoolValue => throw new NotImplementedException();
}
