using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Infrastructure.Extensions;

public class InitialData
{
    public static IEnumerable<WordSet> WordSets => new List<WordSet>
    {
        WordSet.Create(WordSetId.Of(new Guid("0194d661-81b1-7eb9-9542-b22efedf0d78")),WordSetName.Of("Set 1"), null),
        WordSet.Create(WordSetId.Of(new Guid("0194e577-3cc8-7b3d-b008-67bd2be0d14c")),WordSetName.Of("Set 2"), null)
    };
}