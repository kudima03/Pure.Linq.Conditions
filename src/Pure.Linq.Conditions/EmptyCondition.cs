using Pure.Primitives.Abstractions.Bool;

namespace Pure.Linq.Conditions;

public sealed record EmptyCondition<T> : IBool
{
    private readonly IEnumerable<T> _values;

    public EmptyCondition(IEnumerable<T> values)
    {
        _values = values;
    }

    public bool BoolValue => !_values.Any();
}
