using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.DataTransfer.ServerToClient
{
    public class GetCharacterForManagementResult
    {

        public bool Success { get; set; }
        public Character Character { get; set; }
        public ErrorFromServer Error { get; set; }

    }
}
