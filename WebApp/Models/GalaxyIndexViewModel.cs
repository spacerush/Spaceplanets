using System;
using System.Collections.Generic;
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
        public CasualGodComplex.Galaxy Galaxy { get { return _galaxy; } }
        private readonly List<StellarSystem> _stellarSystems;
        public List<StellarSystem> StellarSystems
        {
            get
            {
                return _stellarSystems;
            }
        }

        public Player player
        {
            get { return _player; }
        }

        public GalaxyIndexViewModel(IAuthenticationService authenticationService, IObjectService objectService, string cookie = "")
        {
            _authenticationService = authenticationService;
            _objectService = objectService;
            _galaxy = CasualGodComplex.Galaxy.Generate(new CasualGodComplex.Galaxies.Spiral(), new Random()).Result;

            foreach (var item in _galaxy.Stars)
            {
                StellarSystem stellarSystem = StarformCore.Generator.GenerateStellarSystem(item.Name);

            }
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