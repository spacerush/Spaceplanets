using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
