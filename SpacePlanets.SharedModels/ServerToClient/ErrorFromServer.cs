using SpacePlanets.SharedModels.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class ErrorFromServer
    {
        public string ErrorId { get; set; }
        public string Message { get; set; }
        public ErrorFromServer(string message)
        {
            this.ErrorId = GenerationHelper.CreateRandomString(true, true, false, 6);
            this.Message = message;
        }
    }
}
