using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class Player : Document
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; }
        public List<Character> Characters { get; set; }
        public List<AccessToken> AccessTokens { get; set; }
        public bool IsAdmin { get; set; }
        public Player()
        {
            Characters = new List<Character>();
            AccessTokens = new List<AccessToken>();
            IsAdmin = false;
            Version = 1;
        }

    }
}
