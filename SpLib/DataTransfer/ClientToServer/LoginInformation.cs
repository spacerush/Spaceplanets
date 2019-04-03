using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.DataTransfer.ClientToServer
{
    public class LoginInformation
    {
        /// <summary>
        /// Represents a username.
        /// </summary>
        public string U { get; set; }

        /// <summary>
        /// Represents a password.
        /// </summary>
        public string P { get; set; }

        public LoginInformation(string u, string p)
        {
            U = u;
            P = p;
        }
    }
}
