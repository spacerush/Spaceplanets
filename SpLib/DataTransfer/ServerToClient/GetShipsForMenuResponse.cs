using SpLib.DataTransfer.ServerToClient;
using SpLib.Shared;
using System.Collections.Generic;

namespace SpLib.DataTransfer.ServerToClient
{
    public class GetShipsForMenuResult
    {
        public bool Success { get; set; }
        public List<GenericItemForPicklist> Ships { get; set; }
        public ErrorFromServer Error { get; set; }
    }
}
