using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class ImplantTemplate : Document
    {
        public string Name { get; set; }
        public string Slot { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public int MaxAugmentations { get; set; }

        public ImplantTemplate()
        {

        }

        public ImplantTemplate(string name, string slot, int minimumLevel, int maximumLevel, int maxAugmentations)
        {
            Name = name;
            Slot = slot;
            MinimumLevel = minimumLevel;
            MaximumLevel = maximumLevel;
            MaxAugmentations = maxAugmentations;
        }

    }
}
