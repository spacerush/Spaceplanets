using System;
using System.Collections.Generic;
using System.Text;
using SpacePlanetsDAL.ServiceResponses;

namespace SpacePlanetsDAL.Services
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
        /// Get all the ship templates (often times for display)
        /// </summary>
        /// <returns>A list of ShipTemplate wrapped in a container object.</returns>
        GetAllShipTemplatesResponse GetAllShipTemplates();

        GetAllShipModulesResponse GetAllShipModules();
    }
}
