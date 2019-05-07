using System;
using MongoDbGenericRepository.Models;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class WebSession : Document
    {
        public Guid UserId { get; set; }
        public string SessionCookie { get; set; }
        public string ForUsername { get; set; }
        public DateTime Expiry { get; set; }

        public WebSession()
        {

        }
    }
}