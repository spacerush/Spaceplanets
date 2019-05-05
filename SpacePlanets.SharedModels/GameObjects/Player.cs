using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class Player : Document
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }

        public int CameraY { get; set; }
        public int CameraX { get; set; }
        public int CameraZ { get; set; }

        public Player()
        {
            IsAdmin = false;
            Version = 1;
        }

        

    }
}
