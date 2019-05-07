using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.Models.ServiceResponses.Map;
using SpacePlanetsMvc.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.Services
{
    public interface IMapService
    {
        GetMapAtShipByShipIdResponse GetMapAtShipByShipId(Guid shipId, int viewWidth, int viewHeight);
    }
}
