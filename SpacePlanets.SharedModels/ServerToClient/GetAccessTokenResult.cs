using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
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
