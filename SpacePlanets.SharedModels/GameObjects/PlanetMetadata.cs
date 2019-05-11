using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class PlanetMetadata : Document
    {
        public Guid SpaceObjectId { get; set; }
        public StarformCore.Data.Planet Metadata { get; set; }
    }
}
