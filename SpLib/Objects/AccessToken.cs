using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class AccessToken : Document
    {
        public Guid PlayerId { get; set; }
        public DateTime Expiry { get; set; }
        public string Content { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public AccessToken()
        {
            Version = 1;
        }
    }
}
