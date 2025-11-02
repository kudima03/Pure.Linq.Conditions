using System.Collections;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;

namespace Pure.Linq.Conditions.Tests.Fakes;

public sealed record IntRange : IEnumerable<INumber<int>>
{
    private readonly INumber<int> _from;

    private readonly INumber<int> _to;

    public IntRange()
        : this(new Zero<int>(), new Int(10)) { }

    public IntRange(INumber<int> from, INumber<int> to)
    {
        _from = from;
        _to = to;
    }

    public IEnumerator<INumber<int>> GetEnumerator()
    {
        return Enumerable
            .Range(_from.NumberValue, _to.NumberValue)
            .Select(x => new Int(x))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
