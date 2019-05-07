using SpacePlanets.SharedModels.GameObjects;
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

        public GetShipsForMenuResult()
        {
            this.Ships = new List<GenericItemForPicklist>();
        }

        public GetShipsForMenuResult(List<Ship> ships)
        {
            this.Success = true;
            this.Error = null;
            this.Ships = new List<GenericItemForPicklist>();
            foreach (var item in ships)
            {
                this.Ships.Add(new GenericItemForPicklist() { Id = item.Id, Name = item.Name + ", a " + item.Type });
            }
        }
    }
}
