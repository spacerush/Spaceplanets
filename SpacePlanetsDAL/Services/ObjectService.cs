using MongoDB.Driver;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using SpacePlanetsDAL.ServiceResponses;

namespace SpacePlanetsDAL.Services
{
    /// <summary>
    /// Default Implementation of the IObjectService.
    /// The purpose of this is to create default objects when missing.
    /// </summary>
    public class ObjectService : IObjectService
    {
        private readonly Repositories.IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;
        private readonly Random _random;

        public ObjectService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new Repositories.RepositoryWrapper(_mongoClient);
            _random = new Random();
        }


        /// <inheritdoc />
        public void CreateDefaultShipTemplatesIfNecessary()
        {
            long count = _wrapper.ShipTemplateRepository.Count<ShipTemplate>(f => f.Id != null);
            if (count == 0)
            {
                // Create a list to hold the new Bson documents.
                List<ShipTemplate> shipTemplatesToAdd = new List<ShipTemplate>();

                // Starseeker - the lowest of the low light explorers.
                ShipTemplate temp1 = new ShipTemplate("Starseeker", "Light Explorer");
                temp1.ModuleSlots = new List<ShipModuleSlot>();
                temp1.ModuleSlots.Add(new ShipModuleSlot(1, 2, "ForwardGun"));
                temp1.ModuleSlots.Add(new ShipModuleSlot(1, 2, "ForwardGun"));
                temp1.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightFighterPowerplant"));
                temp1.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightFighterShieldGenerator"));
                temp1.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                shipTemplatesToAdd.Add(temp1);

                // Starfinder
                ShipTemplate temp2 = new ShipTemplate("Starfinder", "Light Explorer");
                temp2.ModuleSlots = new List<ShipModuleSlot>();
                temp2.ModuleSlots.Add(new ShipModuleSlot(1, 3, "ForwardGun"));
                temp2.ModuleSlots.Add(new ShipModuleSlot(1, 3, "ForwardGun"));
                temp2.ModuleSlots.Add(new ShipModuleSlot(1, 3, "LightFighterPowerplant"));
                temp2.ModuleSlots.Add(new ShipModuleSlot(1, 3, "LightFighterShieldGenerator"));
                temp2.ModuleSlots.Add(new ShipModuleSlot(1, 3, "LightCargoStorage"));
                shipTemplatesToAdd.Add(temp2);

                // Startracer
                ShipTemplate temp3 = new ShipTemplate("Startracer", "Medium Explorer");
                temp3.ModuleSlots = new List<ShipModuleSlot>();
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 4, "ForwardGun"));
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 4, "ForwardGun"));
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 4, "ForwardGun"));
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 3, "Turret"));
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 5, "MediumFighterPowerplant"));
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 5, "MediumFighterShieldGenerator"));
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                temp3.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                shipTemplatesToAdd.Add(temp3);

                // Startracer
                ShipTemplate temp4 = new ShipTemplate("Mule", "Light Freighter");
                temp4.ModuleSlots = new List<ShipModuleSlot>();
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "ForwardGun"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "ForwardGun"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "AftGun"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "AftGun"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "Turret"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "Turret"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "Turret"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 1, "Turret"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightFreighterPowerplant"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightFreighterShieldGenerator"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                temp4.ModuleSlots.Add(new ShipModuleSlot(1, 2, "LightCargoStorage"));
                shipTemplatesToAdd.Add(temp4);
                _wrapper.ShipTemplateRepository.AddMany<ShipTemplate>(shipTemplatesToAdd);
            }

        }

        public void CreateDefaultModuleTypesIfNecessary()
        {
            long count = _wrapper.ShipModuleRepository.Count<ShipModule>(f => f.Id != null);
            if (count == 0)
            {
                // Create a list to hold the new Bson documents.
                List<ShipModule> modulesToAdd = new List<ShipModule>();
                ShipModule mod1 = new ShipModule(1, "Gun", "Plasma Gun MK1");
                mod1.ShipStatAlterations = new List<ShipStatAlteration>();
                mod1.ShipStatAlterations.Add(new ShipStatAlteration("PlasmaDps", 1));
                mod1.ShipStatAlterations.Add(new ShipStatAlteration("PowerDrain", 1));
                modulesToAdd.Add(mod1);
                ShipModule mod2 = new ShipModule(1, "Turret", "Plasma Turret MK1");
                mod2.ShipStatAlterations = new List<ShipStatAlteration>();
                mod2.ShipStatAlterations.Add(new ShipStatAlteration("PlasmaDps", 1));
                mod2.ShipStatAlterations.Add(new ShipStatAlteration("PowerDrain", 1));
                modulesToAdd.Add(mod2);
                ShipModule mod3 = new ShipModule(1, "Scanner", "Comestible Locator, Kiko D");
                mod3.ShipStatAlterations = new List<ShipStatAlteration>();
                mod3.ShipStatAlterations.Add(new ShipStatAlteration("CommodityScanRange", 15));
                mod3.ShipStatAlterations.Add(new ShipStatAlteration("EnemyDetectionRange", 6));
                modulesToAdd.Add(mod3);
                ShipModule mod4 = new ShipModule(1, "Scanner", "Food Finder, Marley X1");
                mod4.ShipStatAlterations = new List<ShipStatAlteration>();
                mod4.ShipStatAlterations.Add(new ShipStatAlteration("CommodityScanRange", 12));
                mod4.ShipStatAlterations.Add(new ShipStatAlteration("EnemyDetectionRange", 8));
                modulesToAdd.Add(mod4);
                ShipModule mod5 = new ShipModule(1, "LightFighterPowerplant", "Basic Fighter Powerplant, Small Size");
                mod5.ShipStatAlterations = new List<ShipStatAlteration>();
                mod5.ShipStatAlterations.Add(new ShipStatAlteration("PowerGeneration", 10));
                modulesToAdd.Add(mod5);
                ShipModule mod6 = new ShipModule(1, "MediumFighterPowerplant", "Basic Fighter Powerplant, Medium Size");
                mod6.ShipStatAlterations = new List<ShipStatAlteration>();
                mod6.ShipStatAlterations.Add(new ShipStatAlteration("PowerGeneration", 18));
                modulesToAdd.Add(mod6);
                ShipModule mod7 = new ShipModule(1, "LightFighterShieldGenerator", "Basic Shield, Small Size");
                mod7.ShipStatAlterations = new List<ShipStatAlteration>();
                mod7.ShipStatAlterations.Add(new ShipStatAlteration("PowerDrain", 8));
                mod7.ShipStatAlterations.Add(new ShipStatAlteration("DpsMitigation", 4));
                modulesToAdd.Add(mod7);
                ShipModule mod8 = new ShipModule(1, "MediumFighterShieldGenerator", "Basic Shield, Medium Size");
                mod8.ShipStatAlterations = new List<ShipStatAlteration>();
                mod8.ShipStatAlterations.Add(new ShipStatAlteration("PowerDrain", 12));
                mod8.ShipStatAlterations.Add(new ShipStatAlteration("DpsMitigation", 6));
                modulesToAdd.Add(mod8);
                ShipModule mod9 = new ShipModule(1, "LightFreighterPowerplant", "Basic Freighter Powerplant, Small Size");
                mod9.ShipStatAlterations = new List<ShipStatAlteration>();
                mod9.ShipStatAlterations.Add(new ShipStatAlteration("PowerGeneration", 28));
                modulesToAdd.Add(mod9);
                ShipModule mod10 = new ShipModule(1, "LightFreighterShieldGenerator", "Basic Freighter Shield, Small Size");
                mod10.ShipStatAlterations = new List<ShipStatAlteration>();
                mod10.ShipStatAlterations.Add(new ShipStatAlteration("PowerDrain", 16));
                mod10.ShipStatAlterations.Add(new ShipStatAlteration("DpsMitigation", 7));
                modulesToAdd.Add(mod10);
                ShipModule mod11 = new ShipModule(1, "LightCargoStorage", "Basic Cargo Pod, Small Size");
                mod11.ShipStatAlterations = new List<ShipStatAlteration>();
                mod11.ShipStatAlterations.Add(new ShipStatAlteration("Cargo", 5));
                modulesToAdd.Add(mod11);
                ShipModule mod12 = new ShipModule(1, "LightCargoStorage", "Refrigerated Cargo Pod, Small Size");
                mod12.ShipStatAlterations = new List<ShipStatAlteration>();
                mod12.ShipStatAlterations.Add(new ShipStatAlteration("RefrigeratedCargo", 5));
                mod12.ShipStatAlterations.Add(new ShipStatAlteration("Cargo", 5));
                mod12.ShipStatAlterations.Add(new ShipStatAlteration("PowerDrain", 1));
                modulesToAdd.Add(mod12);
                _wrapper.ShipModuleRepository.AddMany<ShipModule>(modulesToAdd);
            }
        }

        public GetAllShipTemplatesResponse GetAllShipTemplates()
        {
            var result = new GetAllShipTemplatesResponse();
            result.ShipTemplates = _wrapper.ShipTemplateRepository.GetAll<ShipTemplate>(f => f.Id != null);
            if (result.ShipTemplates.Count > 0)
            {
                result.Success = true;
            }
            return result;
        }
        
        public GetAllShipModulesResponse GetAllShipModules()
        {
            var result = new GetAllShipModulesResponse();
            result.ShipModules = _wrapper.ShipModuleRepository.GetAll<ShipModule>(f => f.Id != null);
            if (result.ShipModules.Count > 0)
            {
                result.Success = true;
            }
            return result;
        }

        public SaveGalaxyResponse SaveGalaxyContainer(GalaxyContainer container)
        {
            var result = new SaveGalaxyResponse();
            _wrapper.GalaxyContainerRepository.AddOne<GalaxyContainer>(container);
            result.GalaxyContainerId = container.Id;
            result.Success = true;
            return result;
        }

        public GetGalaxyResponse GetGalaxyContainer(string galaxyName)
        {
            var result = new GetGalaxyResponse();
            result.GalaxyContainer = _wrapper.GalaxyContainerRepository.GetOne<GalaxyContainer>(o => o.Name == galaxyName);
            result.Success = true;
            return result;
        }
    }
}
