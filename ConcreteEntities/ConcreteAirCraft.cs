﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AirTowerController
{
    public class ConcreteAirCraft : AbstractAirCraft
    {
        public ConcreteAirCraft(int id, int delay, string predicate,string location)
            : base(id, delay, predicate,location)
        {  }

    }
       
}
