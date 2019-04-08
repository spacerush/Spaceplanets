using System.Collections.Generic;
using SpacePlanetsDAL.ServiceResponses;
using SpacePlanetsDAL.Services;
using SpLib.Objects;

namespace WebApp.Models
{
    /// <summary>
    /// Used to retrieve information for use on index page of items database section of website.
    /// </summary>
    public class ItemsIndexViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IObjectService _objectService;
        private readonly Player _player;
        private readonly List<ShipTemplate> _shipTemplates;
        
        public Player player
        {
            get { return _player; }
        }

        public List<ShipTemplate> ShipTemplates
        {
            get { return _shipTemplates; }
        }

        public ItemsIndexViewModel(IAuthenticationService authenticationService, IObjectService objectService, string cookie = "")
        {
            _authenticationService = authenticationService;
            _objectService = objectService;
            _shipTemplates = new List<ShipTemplate>();
            GetAllShipTemplatesResponse templatesResponse = _objectService.GetAllShipTemplates();
            if (templatesResponse.Success == true)
            {
                _shipTemplates = templatesResponse.ShipTemplates;
            }
            else
            {
                _shipTemplates = new List<ShipTemplate>();
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