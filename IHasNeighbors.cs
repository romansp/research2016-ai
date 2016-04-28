using System.Collections.Generic;

namespace PudgeClient
{
    public interface IHasNeighbors<TN>
    {
        IEnumerable<TN> Neighbors { get; }
    }
}