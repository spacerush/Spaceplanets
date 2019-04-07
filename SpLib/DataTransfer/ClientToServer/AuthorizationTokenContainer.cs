using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.DataTransfer.ClientToServer
{
    /// <summary>
    /// Represents a bearer token for json encoding.
    /// </summary>
    public class AuthorizationTokenContainer
    {
        /// <summary>
        /// Represents the content of the token.
        /// </summary>
        public string Content { get; set; }
    }
}
