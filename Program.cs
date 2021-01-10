using System;
using System.Threading;
using System.Collections.Generic;

namespace AirTowerController
{
    class Program
    {
        static readonly Queue<ConcreteAirCraft> numbers = new Queue<ConcreteAirCraft>();
        static Random rnd = new Random();
        static readonly string[] predicate = new string[2] { "land/s off", "take/s off" };
        static readonly string[] fromArray = new string[4] { "izmir", "ankara", "malatya", "antalya" };
        //We must control the critical section of code block so that it allows just one thread available in there.
        static readonly object _lock = new object();
        //We can control the generation processes with different i values.
        static int i = 0;

        static void Main(string[] args)
        {
            //We can control the execution of The RunWay by using cancelation token.
            CancellationTokenSource cts = new CancellationTokenSource();

            Tower t = new Tower(cts.Token);
            //These generator threads produce new concrete object for feeding to Tower.
            Thread threadA = new Thread(GenerateAircraft);
            threadA.Start(t);
            Thread threadB = new Thread(GeneratePlane);
            threadB.Start(t);
            //Blocks the main thread until the threads terminated
            threadA.Join();
            threadB.Join();

            //The Cancellation signal is fired up.
            cts.Cancel();
            //The next line will be shown on console but The Runway thread will work to complete all tasks in the queue.
            Console.WriteLine(Environment.NewLine + "Simulation Done!!!" + Environment.NewLine);

        }
        //while(i<30) condition is just a random value.
        static void GenerateAircraft(object tower)
        {
            Tower t = tower as Tower;
           
            while (i<40)
            {
                int delay = rnd.Next(10, 25);
                string selected = predicate[rnd.Next(0, 2)];
                string from = fromArray[rnd.Next(0, 4)];
                lock (_lock)
                {
                    i++;
                    t.Register(new ConcreteAirCraft(i, delay, selected , from));
                }
            }
        }

        static void GeneratePlane(object tower)
        {
            Tower t = tower as Tower;

            while (i<25)
            {
                int delay = rnd.Next(5, 20);
                string selected = predicate[rnd.Next(0, 2)];
               
                lock (_lock)
                {
                    i++;
                    t.Register(new ConcretePlane(i, delay, selected, "Not Identify"));
                }
            }
        }

    }
}

