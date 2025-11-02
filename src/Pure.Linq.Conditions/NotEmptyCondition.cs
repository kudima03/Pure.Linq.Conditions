using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool.Operations;

namespace Pure.Linq.Conditions;

public sealed record NotEmptyCondition<T> : IBool
{
    private readonly IEnumerable<T> _values;

    public NotEmptyCondition(IEnumerable<T> values)
    {
        _values = values;
    }

    public bool BoolValue => new Not(new EmptyCondition<T>(_values)).BoolValue;
}
