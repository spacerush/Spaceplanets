using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class MicroclusterTemplate : Document
    {
        public string Name { get; set; }
        public string Slot { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public string StatToBuff { get; set; }
        public int MinimumBuff { get; set; }
        public int MaximumBuff { get; set; }

        public MicroclusterTemplate()
        {

        }

        public MicroclusterTemplate(string name, string slot, int minimumLevel, int maximumLevel, string statToBuff, int minimumBuff, int maximumBuff)
        {
            Name = name;
            Slot = slot;
            MinimumLevel = minimumLevel;
            MaximumLevel = maximumLevel;
            StatToBuff = statToBuff;
            MinimumBuff = minimumBuff;
            MaximumBuff = maximumBuff;
        }
    }
}
