using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class GetShipsForMenuResult
    {
        public bool Success { get; set; }
        public List<GenericItemForPicklist> Ships { get; set; }
        public ErrorFromServer Error { get; set; }
    }
}
