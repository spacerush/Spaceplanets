using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class ShipStatAlteration
    {
        public string Stat { get; set; }
        public int AlterationAmount { get; set; }

        public ShipStatAlteration()
        {

        }

        public ShipStatAlteration(string stat, int alterationAmount)
        {
            Stat = stat;
            AlterationAmount = alterationAmount;
        }
    }
}
