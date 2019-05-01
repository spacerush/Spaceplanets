using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    /// <summary>
    /// Represents a string token.
    /// </summary>
    public class AuthorizationTokenContainer
    {
        /// <summary>
        /// Represents the content of the token.
        /// </summary>
        public string Token { get; set; }
    }
}
