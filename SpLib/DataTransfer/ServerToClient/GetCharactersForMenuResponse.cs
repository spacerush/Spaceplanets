using SpLib.DataTransfer.ServerToClient;
using SpLib.Shared;
using System.Collections.Generic;

namespace SpLib.DataTransfer.ServerToClient
{
    public class GetCharactersForMenuResult
    {
        public bool Success { get; set; }
        public List<GenericItemForPicklist> Characters { get; set; }
        public ErrorFromServer Error { get; set; }
    }
}
