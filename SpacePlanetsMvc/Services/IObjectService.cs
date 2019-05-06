using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.Services
{
    public interface IObjectService
    {
        /// <summary>
        /// Creates ship templates when none exist in the database.
        /// </summary>
        void CreateDefaultShipTemplatesIfNecessary();

        /// <summary>
        /// Creates ship module information when none exist in the database.
        /// </summary>
        void CreateDefaultModuleTypesIfNecessary();


        /// <summary>
        /// Create templates on which to base "empty" implants.
        /// </summary>
        void CreateDefaultImplantTemplatesIfNecessary();

        /// <summary>
        /// Create templates on which to base randomly generated microcluster items.
        /// </summary>
        void CreateDefaultMicroclusterTemplatesIfNecessary();

        /// <summary>
        /// Get all the ship templates (often times for display)
        /// </summary>
        /// <returns>A list of ShipTemplate wrapped in a container object.</returns>
        GetAllShipTemplatesResponse GetAllShipTemplates();

        GetAllShipModulesResponse GetAllShipModules();

        /// <summary>
        /// Saves an entire galaxy (CasualGodComplex kind) as long as it is inside a GalaxyContainer
        /// </summary>
        /// <param name="container">The container object which inherits the Document type from Mongodb.</param>
        /// <returns>A SaveGalaxyResponse, which is just an object that has the .Id property of the galaxy container and a Success boolean.</returns>
        SaveGalaxyResponse SaveGalaxyContainer(GalaxyContainer container);

        /// <summary>
        /// Gets a container object from the database and returns it.
        /// </summary>
        /// <param name="galaxyName">The name of the galaxy desired for retrieval.</param>
        /// <returns>A container object which has a galaxy inside.</returns>
        GetGalaxyResponse GetGalaxyContainer(string galaxyName);

        /// <summary>
        /// Return the first created galaxy
        /// </summary>
        /// <returns>A service response</returns>
        GetGalaxyResponse GetFirstGalaxy();

        /// <summary>
        /// Create a default galaxy
        /// </summary>
        void CreateDefaultGalaxyIfNecessary();

        /// <summary>
        /// Return the galaxy named Default
        /// </summary>
        GetGalaxyResponse GetDefaultGalaxy();



        /// <summary>
        /// Simply saves a space object.
        /// </summary>
        /// <param name="spaceObject">Object to save.</param>
        bool SaveNewSpaceObject(SpaceObject spaceObject);


        /// <summary>
        /// Return every space object anywhere
        /// </summary>
        /// <returns>All objects</returns>
        List<SpaceObject> GetAllSpaceObjects();
    }
}
