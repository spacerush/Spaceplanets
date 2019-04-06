using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClientLib.Results
{
    public class GetAccessTokenResult
    {
        public bool Success { get; set; }
        public AccessToken Token { get; set; }
        public ErrorFromServer Error { get; set; }

        public GetAccessTokenResult(bool success, AccessToken token, ErrorFromServer error)
        {
            this.Success = success;
            this.Token = token;
            this.Error = error;
        }

        public GetAccessTokenResult()
        {

        }
    }
}
