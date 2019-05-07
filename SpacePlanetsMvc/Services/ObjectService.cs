using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;

namespace SpacePlanetsMvc.Services
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
        
        /// <summary>
        /// Creates a default galaxy if needed.
        /// </summary>
        public void CreateDefaultGalaxyIfNecessary()
        {
            if (this.GetDefaultGalaxy().Success == false)
            {
                CasualGodComplex.Galaxy galaxy = CasualGodComplex.Galaxy.Generate(new CasualGodComplex.Galaxies.Spiral(), new Random(12345)).Result;
                GalaxyContainer ctr = new GalaxyContainer("Default");
                ctr.SetGalaxy(galaxy);
                SaveGalaxyResponse response = this.SaveGalaxyContainer(ctr);
            }
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

        public void CreateDefaultImplantTemplatesIfNecessary()
        {
            long count = _wrapper.ImplantTemplateRepository.Count<ImplantTemplate>(f => f.Id != null);
            if (count == 0)
            {
                List<ImplantTemplate> implantTemplatesToAdd = new List<ImplantTemplate>();
                implantTemplatesToAdd.Add(new ImplantTemplate("Cerebrum Implant - Left Hemisphere", "Left Cerebrum", 1, 300, 4));
                implantTemplatesToAdd.Add(new ImplantTemplate("Cerebrum Implant - Right Hemisphere", "Right Cerebrum", 1, 300, 4));
                implantTemplatesToAdd.Add(new ImplantTemplate("Cerebellum Implant", "Cerebellum", 1, 300, 3));
                implantTemplatesToAdd.Add(new ImplantTemplate("Brainstem Implant", "Brainstem", 1, 300, 2));
                implantTemplatesToAdd.Add(new ImplantTemplate("Right Shoulder Implant", "Right Shoulder", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Left Shoulder Implant", "Left Shoulder", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Upper Back Implant", "Upper Back", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Left Arm Implant", "Left Arm", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Right Arm Implant", "Right Arm", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Chest Implant", "Chest", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Mid Back Implant", "Mid Back", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Stomach Implant", "Stomach", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Left Hand Implant", "Left Hand", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Right Hand Implant", "Right Hand", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Pelvic Implant", "Pelvis", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Waist Implant", "Waist", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Gluteal Implant", "Gluteas", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Right Leg Implant", "Right Leg", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Left Leg Implant", "Left Leg", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Left Foot Implant", "Left Foot", 1, 300, 1));
                implantTemplatesToAdd.Add(new ImplantTemplate("Right Foot Implant", "Right Foot", 1, 300, 1));
                _wrapper.ImplantTemplateRepository.AddMany<ImplantTemplate>(implantTemplatesToAdd);
            }
        }

        public void CreateDefaultMicroclusterTemplatesIfNecessary()
        {
            long count = _wrapper.MicroclusterTemplateRepository.Count<MicroclusterTemplate>(f => f.Id != null);
            if (count == 0)
            {
                List<MicroclusterTemplate> microclusterTemplatesToAdd = new List<MicroclusterTemplate>();
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Martial Arts", "Left Cerebrum", 1, 300, "Martial Arts", 1, 90));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Martial Arts", "Right Cerebrum", 1, 300, "Martial Arts", 1, 90));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Martial Arts", "Cerebellum", 1, 300, "Martial Arts", 1, 140));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Pistol", "Cerebellum", 1, 300, "Pistol", 1, 140));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Rifle", "Cerebellum", 1, 300, "Rifle", 1, 140));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Alertness", "Brainstem", 1, 300, "Alertness", 1, 140));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Pistol", "Right Shoulder", 1, 300, "Pistol", 1, 35));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Pistol", "Left Shoulder", 1, 300, "Pistol", 1, 35));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Plating - Reflection", "Upper Back", 1, 300, "Reflect All Damage", 1, 10));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Adjustment - Agility", "Left Arm", 1, 300, "Agility", 1, 75));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Adjustment - Agility", "Right Arm", 1, 300, "Agility", 1, 75));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Augmentation - Beauty", "Chest", 1, 300, "Beauty", 1, 140));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Plating - Reflection", "Chest", 1, 300, "Reflect All Damage", 1, 10));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Plating - Reflection", "Mid Back", 1, 300, "Reflect All Damage", 1, 8));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Poison Absorption", "Stomach", 1, 300, "Poison Resistance", 1, 75));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("First Aid", "Left Hand", 1, 300, "First Aid", 1, 125));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("First Aid", "Left Hand", 1, 300, "First Aid", 1, 125));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Strength Augmentation", "Pelvis", 1, 300, "Strength", 1, 125));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Stamina Augmentation", "Pelvis", 1, 300, "Stamina", 1, 125));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Strength Augmentation", "Waist", 1, 300, "Strength", 1, 125));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Stamina Augmentation", "Waist", 1, 300, "Stamina", 1, 125));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Beauty Boost", "Gluteas", 1, 300, "Beauty", 1, 125));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Martial Arts", "Right Leg", 1, 300, "Martial Arts", 1, 50));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Martial Arts", "Left Leg", 1, 300, "Martial Arts", 1, 50));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Martial Arts", "Left Foot", 1, 300, "Martial Arts", 1, 50));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Modification - Martial Arts", "Right Foot", 1, 300, "Martial Arts", 1, 50));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Enhanced Speed", "Left Foot", 1, 300, "Runspeed", 1, 50));
                microclusterTemplatesToAdd.Add(new MicroclusterTemplate("Enhanced Speed", "Right Foot", 1, 300, "Runspeed", 1, 50));
                _wrapper.MicroclusterTemplateRepository.AddMany<MicroclusterTemplate>(microclusterTemplatesToAdd);
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

        public GetGalaxyResponse GetFirstGalaxy()
        {
            var result = new GetGalaxyResponse();
            result.GalaxyContainer = _wrapper.GalaxyContainerRepository.GetAll<GalaxyContainer>(f => f.Id != null).OrderBy(o => o.AddedAtUtc).SingleOrDefault();
            if (result.GalaxyContainer != null)
            {
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }
            return result;
        }

        public GetGalaxyResponse GetDefaultGalaxy()
        {
            var result = new GetGalaxyResponse();
            result.GalaxyContainer = _wrapper.GalaxyContainerRepository.GetOne<GalaxyContainer>(f => f.Name == "Default");
            if (result.GalaxyContainer != null)
            {
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }
            return result;
        }


        public bool SaveNewSpaceObject(SpaceObject spaceObject)
        {
            bool result = false;
            // make sure it is of valid type first.
            if (spaceObject.ObjectType == "Moon" || spaceObject.ObjectType == "Planet" || spaceObject.ObjectType == "Asteroid")
            {
                result = true;
                _wrapper.SpaceObjectRepository.AddOne<SpaceObject>(spaceObject);
            }
            return result;
        }

        public List<SpaceObject> GetAllSpaceObjects()
        {
            return _wrapper.SpaceObjectRepository.GetAll<SpaceObject>(f => f.Id != null).ToList();
        }

        public void CreateDefaultSpaceObjectsForAllStarsInDefaultGalaxyIfNecessary()
        {
            long count = _wrapper.SpaceObjectRepository.Count<SpaceObject>(f => f.ObjectType == "Planet");
            if (count == 0)
            {
                GetGalaxyResponse galaxyResponse = this.GetDefaultGalaxy();
                if (galaxyResponse.Success)
                {
                    List<Star> stars = galaxyResponse.GalaxyContainer.Galaxy.Stars;
                    foreach (var star in stars)
                    {

                        var generationOptions = StarformCore.SystemGenerationOptions.DefaultOptions;
                        var accrete = new StarformCore.Accrete(generationOptions.CloudEccentricity, generationOptions.GasDensityRatio);
                        StarformCore.Data.Star starformStar = new StarformCore.Data.Star(star.Name, star.AgeYears, star.Life, star.EcosphereRadiusAU, star.Luminosity, star.Mass, star.BinaryMass, star.SemiMajorAxisAU, star.Eccentricity);

                        double outer_planet_limit = StarformCore.Generator.GetOuterLimit(starformStar);
                        double outer_dust_limit = StarformCore.Generator.GetStellarDustLimit(star.Mass);
                        var seedSystem = accrete.GetPlanetaryBodies(star.Mass, star.Luminosity, 0.0, outer_dust_limit, outer_planet_limit, generationOptions.DustDensityCoeff, null, true);

                        var planets = StarformCore.Generator.GeneratePlanets(starformStar, seedSystem, false, StarformCore.SystemGenerationOptions.DefaultOptions);
                        var stellarSystem = new StarformCore.Data.StellarSystem()
                        {
                            Options = StarformCore.SystemGenerationOptions.DefaultOptions,
                            Planets = planets,
                            Name = star.Name,
                            Star = starformStar
                        };

                        // seed for generating planet locations
                        Random random = new Random(1);
                        foreach (var planet in stellarSystem.Planets)
                        {
                            int minX = star.X - 80;
                            int maxX = star.X + 80;
                            int minY = star.Y - 80;
                            int maxY = star.Y + 80;
                            int minZ = star.Z - 80;
                            int maxZ = star.Z + 80;
                            int planetX = random.Next(minX, maxX);
                            int planetY = random.Next(minX, maxX);
                            int planetZ = random.Next(minX, maxX);

                            SpaceObject planetObject = new SpaceObject("Planet", Guid.NewGuid().ToString());
                            planetObject.X = planetX;
                            planetObject.Y = planetY;
                            planetObject.Z = planetZ;
                            planetObject.PlanetMetadata = planet;
                            _wrapper.SpaceObjectRepository.AddOne<SpaceObject>(planetObject);
                        }
                    }
                }
            }
        }
    }
}
