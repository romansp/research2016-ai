using System;
using System.Linq;
using System.Threading;
using Pudge;
using Pudge.Player;

namespace PudgeClient
{
    class Program
    {
        private const double TurnAngleThreshold = 10;
        const string CvarcTag = "Получи кварк-тэг на сайте";

        // Пример визуального отображения данных с сенсоров при отладке.
        // Если какая-то информация кажется вам лишней, можете закомментировать что-нибудь.
        static void Print(PudgeSensorsData data)
        {
            Console.WriteLine("---------------------------------");
            if (data.IsDead)
            {
                // Правильное обращение со смертью.
                Console.WriteLine("Ooops, i'm dead :(");
                return;
            }
            Console.WriteLine("I'm here: " + data.SelfLocation);
            Console.WriteLine("My score now: {0}", data.SelfScores);
            Console.WriteLine("Current time: {0:F}", data.WorldTime);
            foreach (var rune in data.Map.Runes)
                Console.WriteLine("Rune! Type: {0}, Size = {1}, Location: {2}", rune.Type, rune.Size, rune.Location);
            foreach (var heroData in data.Map.Heroes)
                Console.WriteLine("Enemy! Type: {0}, Location: {1}, Angle: {2:F}", heroData.Type, heroData.Location, heroData.Angle);
            foreach (var eventData in data.Events)
                Console.WriteLine("I'm under effect: {0}, Duration: {1}", eventData.Event,
                    eventData.Duration - (data.WorldTime - eventData.Start));
            Console.WriteLine("---------------------------------");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
                args = new[] { "127.0.0.1", "14000" };
            var ip = args[0];
            var port = int.Parse(args[1]);

            Node previousNode = null;
            //foreach (var node in World.Nodes.OrderByDescending(c => c.Y).ThenBy(c => c.X))
            //{
            //    if (previousNode != null && node.Y < previousNode.Y)
            //    {
            //        Console.WriteLine();
            //    }
            //    if (node.IsWalkable)
            //    {
            //        Console.Write("·");
            //    }
            //    else
            //    {
            //        Console.Write("†");
            //    }

            //    previousNode = node;
            //}
            Console.WriteLine();

            while (true)
            {
                var client = new PudgeClientLevel3();

                // У метода Configurate так же есть необязательные аргументы:
                // timeLimit -- время в секундах, сколько будет идти матч (по умолчанию 90)
                // operationalTimeLimit -- время в секундах, отображающее ваш лимит на операции в сумме за всю игру
                // По умолчанию -- 1000. На турнире будет использоваться значение 5. Подробнее про это можно прочитать в правилах.
                // isOnLeftSide -- предпочитаемая сторона. Принимается во внимание во время отладки. По умолчанию true.
                // seed -- источник энтропии для случайного появления рун. По умолчанию -- 0. 
                // При изменении руны будут появляться в другом порядке
                // speedUp -- ускорение отладки в два раза. Может вызывать снижение FPS на слабых машинах
                var sensorData = client.Configurate(ip, port, CvarcTag);

                var targetNode = World.GetNode(0, 90);

                while (true)
                {
                    Print(sensorData);
                    var nodeX = sensorData.SelfLocation.X;
                    var nodeY = sensorData.SelfLocation.Y;

                    var currentNode = World.GetNode(nodeX, nodeY);

                    if (currentNode == targetNode)
                    {
                        sensorData = client.Wait(Strategy.DefaultWaitTime);
                        continue;
                    }

                    var path = AStar.FindPath(currentNode, targetNode,
                        (previous, possibleNext) => Distance(previous, possibleNext),
                        (current, destination) => Heuristic(current, destination))?.ToList();

                    if (path == null)
                    {
                        sensorData = client.Wait(Strategy.DefaultWaitTime);
                        continue;
                    }

                    if (path.Count < 2)
                    {
                        sensorData = client.Wait(Strategy.DefaultWaitTime);
                        continue;
                    }
                        
                    var nextNode = path[path.Count - 2];
                    var turnAngle = CalculateTurnAngle(currentNode, nextNode, sensorData.SelfLocation.Angle);

                    if (Math.Abs(turnAngle) > TurnAngleThreshold)
                    {
                        sensorData = client.Rotate(turnAngle);
                        // wait rotation
                        client.Wait(Math.Abs(turnAngle/PudgeRules.Current.RotationVelocity));
                    }
                    else
                    {
                        sensorData = client.Move(Strategy.DefaultStepSize);
                    }
                }

                client.Wait(1);
                // Корректно завершаем работу
                client.Exit();

                Thread.Sleep(1000);
            }
        }

        private static double CalculateTurnAngle(Node currentNode, Node targetNode, double currentAngle)
        {
            //return 0;
            var xChange = currentNode.X - targetNode.X;
            var yChange = currentNode.Y - targetNode.Y;

            var targetAngle = 0;

            if (xChange == 0 && yChange == 0) return 0;

            if (xChange == 0)
            {
                if (yChange < 0)
                    targetAngle = 0;
                else
                    targetAngle = 180;
            }
            else if (yChange == 0)
            {
                if (xChange < 0)
                    targetAngle = 90;
                else
                    targetAngle = -90;
            }

            else if (xChange < 0 && yChange < 0)
                targetAngle = 45;

            else if (xChange < 0 && yChange > 0)
                targetAngle = 135;

            else if(xChange > 0 && yChange < 0)
                targetAngle = 135;

            else if(xChange > 0 && yChange > 0)
                targetAngle = -135;

            return targetAngle - currentAngle;
        }

        private static double Distance(Node currentNode, Node targetNode)
        {
            const int diagonalCost = 14;
            const int straightCost = 10;

            if (currentNode.X == targetNode.X ^ currentNode.Y == targetNode.Y)
            {
                return straightCost;
            }

            return diagonalCost;
        }

        private static double Heuristic(Node current, Node target)
        {
            var xDistance = Math.Abs(current.X - target.X);
            var yDistance = Math.Abs(current.Y - target.Y);
            if (xDistance > yDistance)
                return 14 * yDistance + 10 * (xDistance - yDistance);
            return 14 * xDistance + 10 * (yDistance - xDistance);
        }
    }
}
