using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.ServiceResponses
{
    public class GetPlayerByCookieResponse
    {
        public Player Player { get; set; }
        public bool Success { get; set; }
    }
}
