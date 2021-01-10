using System;

namespace AirTowerController
{
    // This is an example of the specialization of abstract class 
    // so we can create different concrete items by using abstract class.
    public class ConcretePlane : AbstractAirCraft
    {
        private string _from = string.Empty;

        public ConcretePlane(int id, int delay, string predicate, string location)
            : base(id, delay, predicate,location)
        {
        }

        //The Virtual method at AbstractPlane is overridden here to customize the behaviour.
        public override void ReceiveMessage()
        {
           Console.WriteLine("Plane number {0} {1}",GetID,GetPredicate);
        }
    }
}
