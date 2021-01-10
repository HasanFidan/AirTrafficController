using System;
using System.Collections.Generic;
using System.Threading;

namespace AirTowerController
{
    public class Tower : ITower
    {
        static Queue<AbstractAirCraft> airCrafts = new Queue<AbstractAirCraft>();
        static Thread threadC = null;

        //Simulation deterministically works until cancelation token is fired up
        public Tower(CancellationToken cts)
        {
            threadC = new Thread(RunWay);
            threadC.Start(cts);
        }

        //The Register method accepts just an object which has a AbstractAirCraft type.
        public void Register(AbstractAirCraft aircraft)
        {
            lock (airCrafts)
            {
                //QueueLimit controlled by 10 to optimize the queue.
                if(airCrafts.Count == 10)
                {
                    Console.WriteLine(Environment.NewLine + "Queue limit reached" + Environment.NewLine);
                    Monitor.Wait(airCrafts);
                }
                //Each new aircraft firstly contact the Tower to make it to register itself.
                airCrafts.Enqueue(aircraft);

                //then notify the RunWay that is waiting state about it.
                Monitor.Pulse(airCrafts);
            }
            Thread.Sleep(500);
        }

        //The Runway waits until the new plane registered  
        private static void RunWay(object token)
        {
            CancellationToken ct = (CancellationToken)token;
            while (1 == 1)
            {
                AbstractAirCraft obj = null;
                //When cancelation requested and airCraft queue is empty
                //This execution block is broken
                if (ct.IsCancellationRequested && airCrafts.Count == 0)
                {
                    break;
                }
                lock (airCrafts)
                {
                    //when the queue has not any item, the registration process should be notified about that.
                    if (airCrafts.Count == 0)
                    {
                        Console.WriteLine(Environment.NewLine + "Queue is empty" + Environment.NewLine);
                        Monitor.Pulse(airCrafts);
                        Monitor.Wait(airCrafts);
                    }

                    obj = airCrafts.Peek();
                    obj.State();
                    Thread.Sleep(100);
                    obj = airCrafts.Dequeue();
                    obj.ReceiveMessage();
                }
                Thread.Sleep(obj.Delay * 100);
            }
           
        }
    }
}
