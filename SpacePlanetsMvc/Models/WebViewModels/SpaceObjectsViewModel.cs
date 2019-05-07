using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;
using SpacePlanetsMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Models.WebViewModels
{
    public class SpaceObjectsViewModel
    {


        private readonly IAuthenticationService _authenticationService;
        private readonly IObjectService _objectService;
        private readonly Player _player;

        public List<SpaceObject> AllSpaceObjects { get; set; }

        public int X { get; set; }
        public int Z { get; set; }
        public int Y { get; set; }

        public string ObjectType { get; set; }
        public string ObjectName { get; set; }

        private readonly string _message;
        public string Message
        {
            get
            {
                return _message;
            }
        }

        public Player player
        {
            get { return _player; }
        }

        public SpaceObjectsViewModel(IAuthenticationService authenticationService, IObjectService objectService, string objectType, string objectName, int X, int Y, int Z, string cookie = "")
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.ObjectName = objectName;
            this.ObjectType = objectType;

            _authenticationService = authenticationService;
            _objectService = objectService;

            AllSpaceObjects = new List<SpaceObject>();
            AllSpaceObjects.AddRange(_objectService.GetAllSpaceObjects());

            // For personalization, load in some player details.
            if (!string.IsNullOrEmpty(cookie))
            {
                // Try to look up the player using their session Identifier (just a basic cookie)
                GetPlayerByCookieResponse playerByWebCookie = _authenticationService.GetPlayerByWebCookie(cookie);
                // The method returns a container object that has .Success property set to TRUE when the
                // player was found via a cookie that was not expired.
                if (playerByWebCookie.Success == true)
                {
                    _player = playerByWebCookie.Player;
                    if (!string.IsNullOrEmpty(objectName) && !string.IsNullOrEmpty(objectType))
                    {
                        if (_player.IsAdmin)
                        {
                            SpaceObject newSpaceObject = new SpaceObject(objectType, objectName);
                            newSpaceObject.X = X;
                            newSpaceObject.Y = Y;
                            newSpaceObject.Z = Z;
                            if (_objectService.SaveNewSpaceObject(newSpaceObject))
                            {
                                _message = "Saved new space object: " + newSpaceObject.Name;
                            }
                            else
                            {
                                _message = "Was unable to save object. Try saving a 'Moon', 'Asteroid', or 'Planet'?";
                            }
                        }
                        else
                        {
                            _message = "You are not an administrator and can't save.";
                        }
                    }
                    else
                    {
                        _message = "You may create a new space object here.";
                    }
                }
                else
                {
                    // Is this necessary?
                    _player = null;
                }
            }
        }




    }
}
