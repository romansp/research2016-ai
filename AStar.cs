using System;
using System.Collections.Generic;

namespace PudgeClient
{
    public static class AStar
    {
        public static Path<TNode> FindPath<TNode>(
            TNode start,
            TNode destination,
            Func<TNode, TNode, double> distance,
            Func<TNode, TNode, double> estimate)
            where TNode : IHasNeighbors<TNode>
        {
            var closed = new HashSet<TNode>();
            var queue = new PriorityQueue<double, Path<TNode>>();
            queue.Enqueue(0, new Path<TNode>(start));
            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                    continue;
                if (path.LastStep.Equals(destination))
                    return path;
                closed.Add(path.LastStep);
                foreach (var n in path.LastStep.Neighbors)
                {
                    double d = distance(path.LastStep, n);
                    var newPath = path.AddStep(n, d);
                    queue.Enqueue(newPath.TotalCost + estimate(n, destination), newPath);
                }
            }
            return null;
        }
    }
}