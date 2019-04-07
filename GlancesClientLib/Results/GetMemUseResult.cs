using SpLib.DataTransfer.ServerToClient;
using SpLib.Glances;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClientLib.Results
{
    public class GetMemUseResult
    {
        public bool Success { get; set; }
        public ErrorFromServer Error { get; set; }
        public Mem MemoryInformation { get; set; }
        public GetMemUseResult(bool success, Mem mem)
        {
            this.Success = success;
            this.MemoryInformation = mem;
        }

        public GetMemUseResult()
        {

        }
    }
}
