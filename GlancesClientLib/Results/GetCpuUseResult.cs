using SpLib.DataTransfer.ServerToClient;
using SpLib.Glances;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClientLib.Results
{
    public class GetCpuUseResult
    {
        public bool Success { get; set; }
        public ErrorFromServer Error { get; set; }
        public Cpu CpuInformation { get; set; }
        public GetCpuUseResult(bool success, Cpu cpu)
        {
            this.Success = success;
            this.CpuInformation = cpu;
        }

        public GetCpuUseResult()
        {

        }
    }
}
