using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class RefreshToken : Document
    {
        public DateTime Expiry { get; set; }
        public string Content { get; set; }
        public bool Used { get; set; }
        public RefreshToken()
        {
            Version = 1;
            Used = false;
        }
    }
}
