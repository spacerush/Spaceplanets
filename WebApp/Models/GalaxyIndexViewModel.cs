using System;
using System.Collections.Generic;
using System.Linq;
using SpacePlanetsDAL.ServiceResponses;
using SpacePlanetsDAL.Services;
using SpLib.Objects;
using StarformCore.Data;

namespace WebApp.Models
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
        private readonly CasualGodComplex.Galaxy _output;
        private readonly int _seed;
        public int Seed { get { return _seed; } }
        private List<string> _usedNames;
        private readonly bool _success;
        public bool Success { get { return _success; } }
        public CasualGodComplex.Galaxy Galaxy { get { return _galaxy; } }

        public Player player
        {
            get { return _player; }
        }

        public GalaxyIndexViewModel(IAuthenticationService authenticationService, IObjectService objectService, int seed, bool saveSeed, string cookie = "")
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
                    if (_player.IsAdmin)
                    {
                        if (saveSeed == true)
                        {
                            GalaxyContainer ctr = new GalaxyContainer("Seed " + seed.ToString());
                            ctr.SetGalaxy(_galaxy);
                            SaveGalaxyResponse response = _objectService.SaveGalaxyContainer(ctr);
                            _success = response.Success;
                        }
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