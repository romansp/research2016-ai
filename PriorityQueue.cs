using System.Collections.Generic;
using System.Linq;

namespace PudgeClient
{
    public class PriorityQueue<TP, TV>
    {
        private readonly SortedDictionary<TP, Queue<TV>> _list = new SortedDictionary<TP, Queue<TV>>();
        public void Enqueue(TP priority, TV value)
        {
            Queue<TV> q;
            if (!_list.TryGetValue(priority, out q))
            {
                q = new Queue<TV>();
                _list.Add(priority, q);
            }
            q.Enqueue(value);
        }
        public TV Dequeue()
        {
            // will throw if there isn’t any first element!
            var pair = _list.First();
            var v = pair.Value.Dequeue();
            if (pair.Value.Count == 0) // nothing left of the top priority.
                _list.Remove(pair.Key);
            return v;
        }
        public bool IsEmpty => !_list.Any();
    }
}