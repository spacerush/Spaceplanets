using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;
using SpacePlanetsMvc.Services;
using StarformCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Models.WebViewModels
{
    /// <summary>
    /// Used to retrieve galaxy information.
    /// </summary>
    public class GalaxyIndexViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IObjectService _objectService;
        private readonly Player _player;
        private readonly CasualGodComplex.Galaxy _galaxy;
        private readonly int _seed;
        public int Seed { get { return _seed; } }

        private readonly bool _success;
        public bool Success { get { return _success; } }
        public CasualGodComplex.Galaxy Galaxy { get { return _galaxy; } }

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

        public GalaxyIndexViewModel(IAuthenticationService authenticationService, IObjectService objectService, int seed, bool saveSeed, string galaxyName = "Default", string cookie = "")
        {
            _seed = seed;
            _success = false;
            _authenticationService = authenticationService;
            _objectService = objectService;
            _galaxy = CasualGodComplex.Galaxy.Generate(new CasualGodComplex.Galaxies.Spiral(), new Random(seed)).Result;
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
                    if (saveSeed == true)
                    {
                        if (_player.IsAdmin)
                        {

                            GalaxyContainer ctr = new GalaxyContainer(galaxyName);
                            ctr.SetGalaxy(_galaxy);
                            SaveGalaxyResponse response = _objectService.SaveGalaxyContainer(ctr);
                            _success = response.Success;
                            if (_success)
                            {
                                _message = "Saved galaxy as " + galaxyName;
                            }
                            else
                            {
                                _message = "Tried to save galaxy as " + galaxyName + " but failed.";
                            }
                        }
                        else {
                            _success = false;
                            _message = "Cannot save galaxy because you are not an administrator.";
                        }
                    }
                    else
                    {
                        _message = "Fill in the form to save this galaxy.";
                    }
                }
                else
                {
                    // Is this necessary?
                    _player = null;
                    _message = "You are not logged in, no attempt to save will be made.";
                }
            }
        }
    }
}