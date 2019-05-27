using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class BankedShipModule : Document
    {
        public Guid PlayerId { get; set; }
        public Guid ShipId { get; set; }
        public ShipModule ShipModule { get; set; }

        public BankedShipModule()
        {

        }

    }
}
