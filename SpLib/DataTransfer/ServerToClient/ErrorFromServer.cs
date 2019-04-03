using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.DataTransfer.ServerToClient
{
    public class ErrorFromServer
    {
        public Guid ErrorId { get; set; }
        public string Message { get; set; }
        public ErrorFromServer(string message)
        {
            ErrorId = Guid.NewGuid();
            Message = message;
        }
    }
}
