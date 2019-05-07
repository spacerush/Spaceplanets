using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    /// <summary>
    /// Contains a username and password
    /// </summary>
    public class CredentialsContainer
    {
        /// <summary>
        /// Identify which account the credentials are for
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Identify the password
        /// </summary>
        public string Password { get; set; }

        public CredentialsContainer()
        {

        }
        public CredentialsContainer(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
