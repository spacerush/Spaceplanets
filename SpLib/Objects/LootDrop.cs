using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class CommodityDrop
    {
        public Guid StarSystemId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public string CommodityName { get; set; }
    }
}
