using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class Microcluster : Document
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; }
        public string Slot { get; set; }
        public int Level { get; set; }
        public string StatToBuff { get; set; }
        public int AmountToBuffStat { get; set; }
    }
}
