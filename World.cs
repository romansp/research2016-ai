using System;
using System.Collections.Generic;
using System.Linq;

namespace PudgeClient
{
    public static class Rules
    {
        public const int PudgeRadius = 20 / 2;
        public const int TreeDiameter = 16;
    }

    public static class World
    {
        #region Trees

        private static readonly HashSet<Node> Trees = new HashSet<Node>
        {
            new Node(-160, 160, false),
            new Node(-150, 160, false),
            new Node(-140, 160, false),
            new Node(-130, 160, false),
            new Node(-120, 160, false),
            new Node(-110, 160, false),
            new Node(-100, 160, false),
            new Node(-90, 160, false),
            new Node(-80, 160, false),
            new Node(-70, 160, false),
            new Node(-60, 160, false),
            new Node(-50, 160, false),
            new Node(-40, 160, false),
            new Node(-30, 160, false),
            new Node(-20, 160, false),
            new Node(-10, 160, false),
            new Node(0, 160, false),
            new Node(10, 160, false),
            new Node(20, 160, false),
            new Node(30, 160, false),
            new Node(40, 160, false),
            new Node(50, 160, false),
            new Node(60, 160, false),
            new Node(70, 160, false),
            new Node(80, 160, false),
            new Node(90, 160, false),
            new Node(100, 160, false),
            new Node(110, 160, false),
            new Node(120, 160, false),
            new Node(130, 160, false),
            new Node(140, 160, false),
            new Node(150, 160, false),
            new Node(-160, 160, false),
            new Node(-160, 150, false),
            new Node(-160, 140, false),
            new Node(-160, 130, false),
            new Node(-160, 120, false),
            new Node(-160, 110, false),
            new Node(-160, 100, false),
            new Node(-160, 90, false),
            new Node(-160, 80, false),
            new Node(-160, 70, false),
            new Node(-160, 60, false),
            new Node(-160, 50, false),
            new Node(-160, 40, false),
            new Node(-160, 30, false),
            new Node(-160, 20, false),
            new Node(-160, 10, false),
            new Node(-160, 0, false),
            new Node(-160, -10, false),
            new Node(-160, -20, false),
            new Node(-160, -30, false),
            new Node(-160, -40, false),
            new Node(-160, -50, false),
            new Node(-160, -60, false),
            new Node(-160, -70, false),
            new Node(-160, -80, false),
            new Node(-160, -90, false),
            new Node(-160, -100, false),
            new Node(-160, -110, false),
            new Node(-160, -120, false),
            new Node(-160, -130, false),
            new Node(-160, -140, false),
            new Node(-160, -150, false),

            new Node(-160, -160, false), // self-added

            new Node(160, -160, false),
            new Node(150, -160, false),
            new Node(140, -160, false),
            new Node(130, -160, false),
            new Node(120, -160, false),
            new Node(110, -160, false),
            new Node(100, -160, false),
            new Node(90, -160, false),
            new Node(80, -160, false),
            new Node(70, -160, false),
            new Node(60, -160, false),
            new Node(50, -160, false),
            new Node(40, -160, false),
            new Node(30, -160, false),
            new Node(20, -160, false),
            new Node(10, -160, false),
            new Node(0, -160, false),
            new Node(-10, -160, false),
            new Node(-20, -160, false),
            new Node(-30, -160, false),
            new Node(-40, -160, false),
            new Node(-50, -160, false),
            new Node(-60, -160, false),
            new Node(-70, -160, false),
            new Node(-80, -160, false),
            new Node(-90, -160, false),
            new Node(-100, -160, false),
            new Node(-110, -160, false),
            new Node(-120, -160, false),
            new Node(-130, -160, false),
            new Node(-140, -160, false),
            new Node(-150, -160, false),
            new Node(160, -160, false),
            new Node(160, -150, false),
            new Node(160, -140, false),
            new Node(160, -130, false),
            new Node(160, -120, false),
            new Node(160, -110, false),
            new Node(160, -100, false),
            new Node(160, -90, false),
            new Node(160, -80, false),
            new Node(160, -70, false),
            new Node(160, -60, false),
            new Node(160, -50, false),
            new Node(160, -40, false),
            new Node(160, -30, false),
            new Node(160, -20, false),
            new Node(160, -10, false),
            new Node(160, 0, false),
            new Node(160, 10, false),
            new Node(160, 20, false),
            new Node(160, 30, false),
            new Node(160, 40, false),
            new Node(160, 50, false),
            new Node(160, 60, false),
            new Node(160, 70, false),
            new Node(160, 80, false),
            new Node(160, 90, false),
            new Node(160, 100, false),
            new Node(160, 110, false),
            new Node(160, 120, false),
            new Node(160, 130, false),
            new Node(160, 140, false),
            new Node(160, 150, false),

            new Node(160, 160, false), // self-added

            new Node(-60, 0, false),
            new Node(-70, -10, false),
            new Node(-70, -20, false),
            new Node(-80, -30, false),
            new Node(-90, -40, false),
            new Node(-100, -50, false),
            new Node(-100, -60, false),
            new Node(0, -60, false),
            new Node(-10, -70, false),
            new Node(-20, -70, false),
            new Node(-30, -80, false),
            new Node(-40, -90, false),
            new Node(-50, -100, false),
            new Node(-60, -100, false),
            new Node(60, 0, false),
            new Node(70, 10, false),
            new Node(70, 20, false),
            new Node(80, 30, false),
            new Node(90, 40, false),
            new Node(100, 50, false),
            new Node(100, 60, false),
            new Node(0, 60, false),
            new Node(10, 70, false),
            new Node(20, 70, false),
            new Node(30, 80, false),
            new Node(40, 90, false),
            new Node(50, 100, false),
            new Node(60, 100, false),
            new Node(40, -80, false),
            new Node(30, -70, false),
            new Node(20, -70, false),
            new Node(10, -60, false),
            new Node(-40, 80, false),
            new Node(-30, 70, false),
            new Node(-20, 70, false),
            new Node(-10, 60, false),
            new Node(80, -40, false),
            new Node(70, -30, false),
            new Node(70, -20, false),
            new Node(60, -10, false),
            new Node(-80, 40, false),
            new Node(-70, 30, false),
            new Node(-70, 20, false),
            new Node(-60, 10, false),
            new Node(0, -150, false),
            new Node(0, -140, false),
            new Node(0, -130, false),
            new Node(0, 150, false),
            new Node(0, 140, false),
            new Node(0, 130, false),
            new Node(150, 0, false),
            new Node(140, 0, false),
            new Node(130, 0, false),
            new Node(-150, 0, false),
            new Node(-140, 0, false),
            new Node(-130, 0, false),
            new Node(140, -80, false),
            new Node(130, -80, false),
            new Node(120, -80, false),
            new Node(80, -140, false),
            new Node(80, -130, false),
            new Node(80, -120, false),
            new Node(-140, 80, false),
            new Node(-130, 80, false),
            new Node(-120, 80, false),
            new Node(-80, 140, false),
            new Node(-80, 130, false),
            new Node(-80, 120, false)
        };

        #endregion

        private const int Width = 340;
        private const int Height = 340;
        private const int PudgeSize = 20;

        public static HashSet<Node> Nodes = new HashSet<Node>();

        static World()
        {
            const int halfWidth = Width / 2;
            const int halfHeight = Height / 2;

            for (int i = -halfWidth; i <= halfWidth; i += Rules.PudgeRadius)
            {
                for (int j = -halfHeight; j <= halfHeight; j += Rules.PudgeRadius)
                {
                    var node = new Node(i, j);
                    Nodes.Add(node);
                }
            }

            foreach (var tree in Trees)
            {
                var xFrom = tree.X - Rules.TreeDiameter;
                var xTo = tree.X + Rules.TreeDiameter;
                var yFrom = tree.Y - Rules.TreeDiameter;
                var yTo = tree.Y + Rules.TreeDiameter;

                var notAccessibleNodes = Nodes.Where(c => c.X >= xFrom && c.X <= xTo && c.Y >= yFrom && c.Y <= yTo);

                foreach (var notAccessibleNode in notAccessibleNodes)
                {
                    notAccessibleNode.IsWalkable = false;
                }
            }

            foreach (var node in Nodes)
            {
                var xFrom = node.X - Rules.PudgeRadius;
                var xTo = node.X + Rules.PudgeRadius;
                var yFrom = node.Y - Rules.PudgeRadius;
                var yTo = node.Y + Rules.PudgeRadius;

                var neighbors = Nodes.Where(c => c.IsWalkable && c.X >= xFrom && c.X <= xTo && c.Y >= yFrom && c.Y <= yTo).ToList();
                neighbors.Remove(node);
                node.SetNeighbors(neighbors);
            }
        }

        public static Node GetNode(double actualX, double actualY)
        {
            var x = Math.Round(actualX / Rules.PudgeRadius) * Rules.PudgeRadius;
            var y = Math.Round(actualY / Rules.PudgeRadius) * Rules.PudgeRadius;
            return Nodes.First(c => c.X == x && c.Y == y);
        }
    }

    public class Node : IEquatable<Node>, IHasNeighbors<Node>
    {
        public int X;
        public int Y;
        public bool IsWalkable;
        private readonly List<Node> _neighbors = new List<Node>(8);

        public IEnumerable<Node> Neighbors
        {
            get { return _neighbors; }
        }

        public Node(int x, int y, bool isWalkable = true)
        {
            X = x;
            Y = y;
            IsWalkable = isWalkable;
        }

        public void SetNeighbors(IEnumerable<Node> neighbors)
        {
            foreach (var neighbor in neighbors)
            {
                _neighbors.Add(neighbor);
            }
        }

        public bool Equals(Node other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Node && Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {nameof(IsWalkable)}: {IsWalkable}";
        }
    }
}