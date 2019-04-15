using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class Implant : Document
    {
        public string Name { get; set; }
        public string Slot { get; set; }
        public int Quality { get; set; }
        public int AugmentationCapacity { get; set; }
        public List<Microcluster> Microclusters { get; set; }
        public Guid PlayerId { get; set; }
        public Guid CharacterId { get; set; }
        public bool Installed { get; set; }
        public Implant()
        {

        }

        public Implant(string name, string slot, int quality, int augmentationCapacity)
        {
            Name = name;
            Slot = slot;
            Quality = quality;
            AugmentationCapacity = augmentationCapacity;
        }

    }
}
