﻿using System;
using System.Threading;
using Pudge;
using Pudge.Player;

namespace PudgeClient
{
    class Program
    {
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
                args = new[] {"127.0.0.1", "14000"};
            var ip = args[0];
            var port = int.Parse(args[1]);

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

                // Пудж узнает о всех событиях, происходящих в мире, с помощью сенсоров.
                // Для передачи и представления данных с сенсоров служат объекты класса PudgeSensorsData.
                Print(sensorData);

                // Каждое действие возвращает новые данные с сенсоров.
                sensorData = client.Move();
                Print(sensorData);

                // Для удобства, можно подписать свой метод на обработку всех входящих данных с сенсоров.
                // С этого момента любое действие приведет к отображению в консоли всех данных
                client.SensorDataReceived += Print;

                // Угол поворота указывается в градусах, против часовой стрелки.
                // Для поворота по часовой стрелке используйте отрицательные значения.
                client.Rotate(-42);

                client.Move(25);
                client.Move(25);
                client.Move(25);

                client.Rotate(30);

                client.Move(25);
                client.Move(25);
                client.Move(25);
                client.Wait(0.25);
                client.Wait(0.25);


                // Так можно хукать.
                //client.Hook();

                client.Rotate(-35);

                client.Wait(0.5);
                client.Wait(0.5);

                client.Hook();

                // Пример длинного движения. Move(100) лучше не писать. Мало ли что произойдет за это время ;) 
                //for (int i = 0; i < 5; i++)
                //    client.Move(15);
                client.Wait(1);
                // Корректно завершаем работу
                client.Exit();

                Thread.Sleep(1000);
            }
        }
    }
}
