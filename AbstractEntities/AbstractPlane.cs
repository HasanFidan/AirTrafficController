using System;

namespace AirTowerController
{
    //The Abstract object provides a definition and some detail of implementations
    //is to generalize the concept.
    public class AbstractAirCraft
    {
        private string _predicate { get; set; }
        private int _delay { get; set; }
        private int _id { get; set; }
        private string _location { get; set; }

        public AbstractAirCraft(int id, int delay, string predicate,string location)
        {
            _predicate = predicate;
            _delay = delay;
            _id = id;
            _location = location;
        }

        public int GetID { get { return _id; } }
        public int Delay { get { return _delay; } set { _delay = value; } }
        public string GetPredicate { get { return _predicate; } }

        //According to predicate,The registered object state will be notified.
        public virtual void State() {
            Console.WriteLine("Aircraft {0} is ready to {1}",_id,_predicate);
        }

        //After the operation completed,The Message will be received.  
        public virtual void ReceiveMessage()
        {
            if(_predicate.Equals("land/s off"))
            Console.WriteLine("Aircraft {0} that comes from {1} {2}", _id,_location, _predicate);
            else
            Console.WriteLine("Aircraft {0} that goes to {1} {2}", _id, _location, _predicate);
        }
    }
}
