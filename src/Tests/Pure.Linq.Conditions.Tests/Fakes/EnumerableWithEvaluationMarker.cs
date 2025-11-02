using System.Collections;

namespace Pure.Linq.Conditions.Tests.Fakes;

public sealed record EnumerableWithEvaluationMarker : IEnumerable<int>
{
    public bool Evaluated { get; private set; }

    public IEnumerator<int> GetEnumerator()
    {
        Evaluated = true;
        return Enumerable.Range(0, 10).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
