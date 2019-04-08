using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.ServiceResponses
{
    public class GetAllShipTemplatesResponse
    {
        public List<ShipTemplate> ShipTemplates { get; set; }
        public bool Success { get; set; }
    }
}
