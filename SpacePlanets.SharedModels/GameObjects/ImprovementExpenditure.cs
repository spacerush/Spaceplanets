using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class ImprovementExpenditure
    {
        public int ImprovementQuantity { get; set; }
        public string Skill { get; set; }
        public DateTime TrainDate { get; set; }
    }
}
