using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class GenericItemForPicklist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public GenericItemForPicklist()
        {

        }

        public GenericItemForPicklist(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
