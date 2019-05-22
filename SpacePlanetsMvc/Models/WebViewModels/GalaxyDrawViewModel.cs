using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Models.WebViewModels
{
    public class GalaxyDrawViewModel
    {
        public GalaxyContainer GalaxyContainer { get; set; }

        private readonly IObjectService _objectService;

        public GalaxyDrawViewModel(IObjectService objectService, string galaxyName = "Default")
        {
            _objectService = objectService;
            this.GalaxyContainer = _objectService.GetDefaultGalaxy().GalaxyContainer;
        }

    }
}
