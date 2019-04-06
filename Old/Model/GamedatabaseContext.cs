using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpaceRushEntities.Model
{
    public partial class GamedatabaseContext : DbContext
    {
        public GamedatabaseContext()
        {
        }

        public GamedatabaseContext(DbContextOptions<GamedatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiRateLimit> ApiRateLimits { get; set; }
        public virtual DbSet<ApiUsageLogItem> ApiUsageLogItems { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharacterImplantInventoryItem> CharacterImplantInventoryItems { get; set; }
        public virtual DbSet<CharacterSlottedImplant> CharacterSlottedImplants { get; set; }
        public virtual DbSet<Cluster> Clusters { get; set; }
        public virtual DbSet<ClusterSlot> ClusterSlots { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<ComponentSlot> ComponentSlots { get; set; }
        public virtual DbSet<CrewSlot> CrewSlots { get; set; }
        public virtual DbSet<Faction> Factions { get; set; }
        public virtual DbSet<FactionOpinion> FactionOpinions { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMembershipRecord> GroupMembershipRecords { get; set; }
        public virtual DbSet<GroupPermission> GroupPermissions { get; set; }
        public virtual DbSet<Implant> Implants { get; set; }
        public virtual DbSet<ImplantBaseItem> ImplantBaseItems { get; set; }
        public virtual DbSet<ImplantCluster> ImplantClusters { get; set; }
        public virtual DbSet<ImplantSlot> ImplantSlots { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModuleBaseItem> ModuleBaseItems { get; set; }
        public virtual DbSet<ModuleComponent> ModuleComponents { get; set; }
        public virtual DbSet<ModuleSlot> ModuleSlots { get; set; }
        public virtual DbSet<MotdItem> MotdItems { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerToken> PlayerTokens { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceHub> ResourceHubs { get; set; }
        public virtual DbSet<ResourceHubMiningEvent> ResourceHubMiningEvents { get; set; }
        public virtual DbSet<ResourceProcessor> ResourceProcessors { get; set; }
        public virtual DbSet<ResourceProcessorConversionEvent> ResourceProcessorConversionEvents { get; set; }
        public virtual DbSet<ResourceProcessorInput> ResourceProcessorInputs { get; set; }
        public virtual DbSet<ResourceProcessorOutput> ResourceProcessorOutputs { get; set; }
        public virtual DbSet<ResourceProcessorType> ResourceProcessorTypes { get; set; }
        public virtual DbSet<ResourceStorage> ResourceStorages { get; set; }
        public virtual DbSet<ResourceStorageEvent> ResourceStorageEvents { get; set; }
        public virtual DbSet<ResourceStorageType> ResourceStorageTypes { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<ShipAbility> ShipAbilities { get; set; }
        public virtual DbSet<ShipCrewSlot> ShipCrewSlots { get; set; }
        public virtual DbSet<ShipTechnology> ShipTechnologies { get; set; }
        public virtual DbSet<ShipType> ShipTypes { get; set; }
        public virtual DbSet<ShipTypesCrewSlot> ShipTypesCrewSlots { get; set; }
        public virtual DbSet<ShipTypesResource> ShipTypesResources { get; set; }
        public virtual DbSet<Shipyard> Shipyards { get; set; }
        public virtual DbSet<ShipyardShipType> ShipyardShipTypes { get; set; }
        public virtual DbSet<ShipyardType> ShipyardTypes { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SpaceObject> SpaceObjects { get; set; }
        public virtual DbSet<SpaceObjectType> SpaceObjectTypes { get; set; }
        public virtual DbSet<StarSystem> StarSystems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<ApiRateLimit>(entity =>
            {
                entity.HasIndex(e => new { e.PlayerId, e.PermissionId })
                    .HasName("AK_ApiRateLimits_PlayerId_PermissionId")
                    .IsUnique();

                entity.Property(e => e.DailyLimit).HasDefaultValueSql("10000");

                entity.Property(e => e.HourlyLimit).HasDefaultValueSql("3600");

                entity.Property(e => e.MinutelyLimit).HasDefaultValueSql("60");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ApiRateLimits)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiRateLimits_PermissionId");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.ApiRateLimits)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiRateLimits_Players");
            });

            modelBuilder.Entity<ApiUsageLogItem>(entity =>
            {
                entity.HasIndex(e => e.UsageTimeUtc);

                entity.Property(e => e.UsageTimeUtc).HasDefaultValueSql("getutcdate()");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ApiUsageLogItems)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiUsageLogItems_Permissions");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.ApiUsageLogItems)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiUsageLogItems_Players");
            });

            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasIndex(e => e.CharacterName)
                    .HasName("AK_Characters_CharacterName")
                    .IsUnique();

                entity.HasIndex(e => e.PlayerId);

                entity.Property(e => e.CharacterName).IsUnicode(false);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Characters_Genders");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Characters_Players");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.ProfessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Characters_Professions");
            });

            modelBuilder.Entity<CharacterImplantInventoryItem>(entity =>
            {
                entity.HasIndex(e => e.CharacterId);

                entity.HasIndex(e => e.ImplantId)
                    .HasName("AK_CharacterImplantInventoryItems_ImplantId")
                    .IsUnique();

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.CharacterImplantInventoryItems)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacterImplantInventoryItems_Characters");

                entity.HasOne(d => d.Implant)
                    .WithOne(p => p.CharacterImplantInventoryItem)
                    .HasForeignKey<CharacterImplantInventoryItem>(d => d.ImplantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacterImplantInventoryItems_Implants");
            });

            modelBuilder.Entity<CharacterSlottedImplant>(entity =>
            {
                entity.HasIndex(e => e.CharacterId);

                entity.HasIndex(e => e.ImplantId);

                entity.HasIndex(e => new { e.CharacterId, e.ImplantSlotId })
                    .HasName("AK_CharacterSlottedImplants_CharacterId_ImplantSlotId")
                    .IsUnique();

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.CharacterSlottedImplants)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacterSlottedImplants_Characters");

                entity.HasOne(d => d.Implant)
                    .WithMany(p => p.CharacterSlottedImplants)
                    .HasForeignKey(d => d.ImplantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacterSlottedImplants_Implants");

                entity.HasOne(d => d.ImplantSlot)
                    .WithMany(p => p.CharacterSlottedImplants)
                    .HasForeignKey(d => d.ImplantSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacterSlottedImplants_ImplantSlots");
            });

            modelBuilder.Entity<Cluster>(entity =>
            {
                entity.HasIndex(e => e.ClusterSlotId);

                entity.HasIndex(e => e.ImplantBaseItemId);

                entity.HasIndex(e => new { e.ImplantBaseItemId, e.ClusterSlotId })
                    .HasName("AK_Clusters_ImplantBaseItemId_ClusterSlotId");

                entity.HasOne(d => d.ClusterSlot)
                    .WithMany(p => p.Clusters)
                    .HasForeignKey(d => d.ClusterSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clusters_ClusterSlotId");

                entity.HasOne(d => d.ImplantBaseItem)
                    .WithMany(p => p.Clusters)
                    .HasForeignKey(d => d.ImplantBaseItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clusters_ImplantBaseItems");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.Clusters)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clusters_Skills");
            });

            modelBuilder.Entity<ClusterSlot>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ClusterSlots_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasIndex(e => e.ComponentSlotId);

                entity.HasIndex(e => e.ModuleBaseItemId);

                entity.HasIndex(e => new { e.ModuleBaseItemId, e.ComponentSlotId })
                    .HasName("AK_Components_ModuleBaseItemId_ComponentSlotId");

                entity.HasOne(d => d.ComponentSlot)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.ComponentSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Components_ComponentSlotId");

                entity.HasOne(d => d.ModuleBaseItem)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.ModuleBaseItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Components_ModuleBaseItems");

                entity.HasOne(d => d.ShipAbility)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.ShipAbilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Components_ShipAbilities");
            });

            modelBuilder.Entity<ComponentSlot>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ComponentSlots_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<CrewSlot>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_CrewSlots_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Faction>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<FactionOpinion>(entity =>
            {
                entity.HasIndex(e => new { e.FactionId, e.TargetFactionId })
                    .HasName("AK_FactionOpinions_FactionId_TargetFactionId");

                entity.HasOne(d => d.Faction)
                    .WithMany(p => p.FactionOpinionFactions)
                    .HasForeignKey(d => d.FactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactionOpinions_Factions_Source");

                entity.HasOne(d => d.TargetFaction)
                    .WithMany(p => p.FactionOpinionTargetFactions)
                    .HasForeignKey(d => d.TargetFactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactionOpinions_FactionsTarget");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Genders_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Groups_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<GroupMembershipRecord>(entity =>
            {
                entity.HasIndex(e => e.GroupId);

                entity.HasIndex(e => e.PlayerId)
                    .HasName("IX_GroupMembershipRecords_ApiUserId");

                entity.HasIndex(e => new { e.ValidStart, e.ValidEnd });

                entity.Property(e => e.ValidStart).HasDefaultValueSql("getutcdate()");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembershipRecords)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMembershipRecords_Groups");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.GroupMembershipRecords)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMembershipRecords_ApiUsers");
            });

            modelBuilder.Entity<GroupPermission>(entity =>
            {
                entity.HasIndex(e => new { e.ValidStart, e.ValidEnd });

                entity.Property(e => e.ValidStart).HasDefaultValueSql("getutcdate()");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPermissions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPermissions_Groups");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.GroupPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPermissions_Permissions");
            });

            modelBuilder.Entity<Implant>(entity =>
            {
                entity.HasOne(d => d.ImplantBaseItem)
                    .WithMany(p => p.Implants)
                    .HasForeignKey(d => d.ImplantBaseItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Implants_ImplantBaseItems");
            });

            modelBuilder.Entity<ImplantBaseItem>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.ImplantSlot)
                    .WithMany(p => p.ImplantBaseItems)
                    .HasForeignKey(d => d.ImplantSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImplantBaseItems_ImplantSlots");
            });

            modelBuilder.Entity<ImplantCluster>(entity =>
            {
                entity.HasIndex(e => e.ClusterId);

                entity.HasIndex(e => e.ImplantId);

                entity.HasOne(d => d.Cluster)
                    .WithMany(p => p.ImplantClusters)
                    .HasForeignKey(d => d.ClusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImplantClusters_Clusters");

                entity.HasOne(d => d.Implant)
                    .WithMany(p => p.ImplantClusters)
                    .HasForeignKey(d => d.ImplantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImplantClusters_Implants");
            });

            modelBuilder.Entity<ImplantSlot>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ImplantSlots_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasOne(d => d.ModuleBaseItem)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.ModuleBaseItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Modules_ModuleBaseItems");
            });

            modelBuilder.Entity<ModuleBaseItem>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.ModuleSlot)
                    .WithMany(p => p.ModuleBaseItems)
                    .HasForeignKey(d => d.ModuleSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleBaseItems_ModuleSlots");
            });

            modelBuilder.Entity<ModuleComponent>(entity =>
            {
                entity.HasIndex(e => e.ComponentId)
                    .HasName("IX_ModulesModuleClusters_ClusterId");

                entity.HasIndex(e => e.ModuleId)
                    .HasName("IX_ModulesModuleClusters_ModuleId");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.ModuleComponents)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleComponents_Components");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleComponents)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleComponents_Modules");
            });

            modelBuilder.Entity<ModuleSlot>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ModuleSlots_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<MotdItem>(entity =>
            {
                entity.HasIndex(e => e.IsActive);

                entity.Property(e => e.IsActive).HasDefaultValueSql("1");

                entity.Property(e => e.MotdText).IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Permissions_Name")
                    .IsUnique();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.HasOne(d => d.StarSystem)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.StarSystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_StarSystems");
            });

            modelBuilder.Entity<PlayerToken>(entity =>
            {
                entity.HasIndex(e => e.PlayerId);

                entity.HasIndex(e => e.Token)
                    .IsUnique();

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerTokens)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Professions_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(e => e.PlayerId);

                entity.HasIndex(e => e.Token)
                    .HasName("AK_RefreshTokens_Token")
                    .IsUnique();

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ResourceHub>(entity =>
            {
                entity.HasIndex(e => new { e.SpaceObjectId, e.ResourceId })
                    .HasName("AK_ResourceSources_SpaceObjectId_ResourceId")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceHubs)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceSources_Resources");

                entity.HasOne(d => d.SpaceObject)
                    .WithMany(p => p.ResourceHubs)
                    .HasForeignKey(d => d.SpaceObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceSources_SpaceObjects");
            });

            modelBuilder.Entity<ResourceHubMiningEvent>(entity =>
            {
                entity.HasIndex(e => e.ResourceHubId)
                    .HasName("IX_ResourceHubMiningEvent_ResourceHubId");

                entity.Property(e => e.QuantityMined).HasDefaultValueSql("1");

                entity.Property(e => e.UtcDate).HasDefaultValueSql("getutcdate()");

                entity.HasOne(d => d.ResourceHub)
                    .WithMany(p => p.ResourceHubMiningEvents)
                    .HasForeignKey(d => d.ResourceHubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceHubMiningEvent_ResourceHub");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.ResourceHubMiningEvents)
                    .HasForeignKey(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceHubMiningEvent_Ships");
            });

            modelBuilder.Entity<ResourceProcessor>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ResourceProcessors_Name")
                    .IsUnique();

                entity.HasIndex(e => e.SpaceObjectId)
                    .HasName("IX_ResourceProcessors_SpaceObjects");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.ResourceProcessorType)
                    .WithMany(p => p.ResourceProcessors)
                    .HasForeignKey(d => d.ResourceProcessorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessors_ResourceProcessorTypes");

                entity.HasOne(d => d.SpaceObject)
                    .WithMany(p => p.ResourceProcessors)
                    .HasForeignKey(d => d.SpaceObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessors_SpaceObjects");
            });

            modelBuilder.Entity<ResourceProcessorConversionEvent>(entity =>
            {
                entity.HasOne(d => d.InputResource)
                    .WithMany(p => p.ResourceProcessorConversionEventInputResources)
                    .HasForeignKey(d => d.InputResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessorConversionEvents_InputResource");

                entity.HasOne(d => d.OutputResource)
                    .WithMany(p => p.ResourceProcessorConversionEventOutputResources)
                    .HasForeignKey(d => d.OutputResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessorConversionEvents_OutputResource");

                entity.HasOne(d => d.ResourceProcessor)
                    .WithMany(p => p.ResourceProcessorConversionEvents)
                    .HasForeignKey(d => d.ResourceProcessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessorConversionEvents_ResourceProcessor");
            });

            modelBuilder.Entity<ResourceProcessorInput>(entity =>
            {
                entity.Property(e => e.Quantity).HasDefaultValueSql("1");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceProcessorInputs)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessorInputs_Resources");

                entity.HasOne(d => d.ResourceProcessor)
                    .WithMany(p => p.ResourceProcessorInputs)
                    .HasForeignKey(d => d.ResourceProcessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessorInputs_ResourceProcessors");
            });

            modelBuilder.Entity<ResourceProcessorOutput>(entity =>
            {
                entity.Property(e => e.Quantity).HasDefaultValueSql("1");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceProcessorOutputs)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessorOutputs_Resources");

                entity.HasOne(d => d.ResourceProcessor)
                    .WithMany(p => p.ResourceProcessorOutputs)
                    .HasForeignKey(d => d.ResourceProcessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceProcessorOutputs_ResourceProcessors");
            });

            modelBuilder.Entity<ResourceProcessorType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ResourceProcessorTypes_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ResourceStorage>(entity =>
            {
                entity.HasOne(d => d.ResourceProcessor)
                    .WithMany(p => p.ResourceStorages)
                    .HasForeignKey(d => d.ResourceProcessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceStorage_ResourceProcessor");

                entity.HasOne(d => d.ResourceStorageType)
                    .WithMany(p => p.ResourceStorages)
                    .HasForeignKey(d => d.ResourceStorageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceStorage_ResourceStorageType");
            });

            modelBuilder.Entity<ResourceStorageEvent>(entity =>
            {
                entity.HasOne(d => d.ResourceStorage)
                    .WithMany(p => p.ResourceStorageEvents)
                    .HasForeignKey(d => d.ResourceStorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceStorageEvents_ResourceStorage");
            });

            modelBuilder.Entity<ResourceStorageType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ResourceStorageTypes_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceStorageTypes)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceStorageType_Resource");
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.HasIndex(e => new { e.IsAlive, e.PlayerId, e.StarSystemId });

                entity.HasIndex(e => new { e.IsAlive, e.StarSystemId, e.PosX, e.PosY, e.PosZ });

                entity.Property(e => e.IsAlive).HasDefaultValueSql("1");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.ShipType)
                    .WithMany(p => p.Ships)
                    .HasForeignKey(d => d.ShipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StarSystem)
                    .WithMany(p => p.Ships)
                    .HasForeignKey(d => d.StarSystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ships_StarSystems");
            });

            modelBuilder.Entity<ShipAbility>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ShipAbilities_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ShipCrewSlot>(entity =>
            {
                entity.HasIndex(e => e.ShipId)
                    .HasName("IX_ShipsCrewSlots_ShipId");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.ShipCrewSlots)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipsCrewSlots_CharacterId");

                entity.HasOne(d => d.CrewSlot)
                    .WithMany(p => p.ShipCrewSlots)
                    .HasForeignKey(d => d.CrewSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipsCrewSlots_CrewSlotId");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.ShipCrewSlots)
                    .HasForeignKey(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipsCrewSlots_ShipId");
            });

            modelBuilder.Entity<ShipTechnology>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ShipTechnologies_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ShipType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ShipTypes_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<ShipTypesCrewSlot>(entity =>
            {
                entity.HasIndex(e => new { e.ShipTypeId, e.CrewSlotId })
                    .HasName("AK_ShipTypesCrewSlots_ShipTypeId_CrewSlotId")
                    .IsUnique();

                entity.HasOne(d => d.CrewSlot)
                    .WithMany(p => p.ShipTypesCrewSlotCrewSlots)
                    .HasForeignKey(d => d.CrewSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipTypesCrewSlots_ShipTypesCrewSlots_CrewSlotId");

                entity.HasOne(d => d.ShipType)
                    .WithMany(p => p.ShipTypesCrewSlots)
                    .HasForeignKey(d => d.ShipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipTypesCrewSlots_ShipTypesCrewSlots_ShipTypeId");

                entity.HasOne(d => d.SlotDependency)
                    .WithMany(p => p.ShipTypesCrewSlotSlotDependencies)
                    .HasForeignKey(d => d.SlotDependencyId)
                    .HasConstraintName("FK_ShipTypesCrewSlots_ShipTypesCrewSlots_DependencyId");
            });

            modelBuilder.Entity<ShipTypesResource>(entity =>
            {
                entity.HasIndex(e => new { e.ShipTypeId, e.ResourceId })
                    .HasName("UQ_ShipTypesResources_ShipTypeId_ResourceId")
                    .IsUnique();

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ShipTypesResources)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipTypesResources_Resources");

                entity.HasOne(d => d.ShipType)
                    .WithMany(p => p.ShipTypesResources)
                    .HasForeignKey(d => d.ShipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipTypesResources_ShipTypes");
            });

            modelBuilder.Entity<Shipyard>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Shipyards_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.ShipyardType)
                    .WithMany(p => p.Shipyards)
                    .HasForeignKey(d => d.ShipyardTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipyards_ShipyardTypes");

                entity.HasOne(d => d.SpaceObject)
                    .WithMany(p => p.Shipyards)
                    .HasForeignKey(d => d.SpaceObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipyards_SpaceObjects");
            });

            modelBuilder.Entity<ShipyardShipType>(entity =>
            {
                entity.HasIndex(e => e.ShipTypeId);

                entity.HasIndex(e => e.ShipyardId);

                entity.HasIndex(e => new { e.ShipyardId, e.ShipTypeId })
                    .HasName("UQ_ShipyardShipTypes_ShipyardId_ShiptypeId")
                    .IsUnique();

                entity.HasOne(d => d.ShipType)
                    .WithMany(p => p.ShipyardShipTypes)
                    .HasForeignKey(d => d.ShipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipyardShipTypes_Shiptypes");

                entity.HasOne(d => d.Shipyard)
                    .WithMany(p => p.ShipyardShipTypes)
                    .HasForeignKey(d => d.ShipyardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipyardShipTypes_Shipyards");
            });

            modelBuilder.Entity<ShipyardType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ShipyardTypes_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_Skills_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<SpaceObject>(entity =>
            {
                entity.HasIndex(e => e.StarSystemId);

                entity.HasIndex(e => new { e.StarSystemId, e.PosX, e.PosY, e.PosZ });

                entity.Property(e => e.ObjectName).IsUnicode(false);

                entity.HasOne(d => d.ObjectType)
                    .WithMany(p => p.SpaceObjects)
                    .HasForeignKey(d => d.ObjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpaceObjects_ObjectTypes");

                entity.HasOne(d => d.StarSystem)
                    .WithMany(p => p.SpaceObjects)
                    .HasForeignKey(d => d.StarSystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpaceObjects_StarSystems");
            });

            modelBuilder.Entity<SpaceObjectType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_ObjectType_Name")
                    .IsUnique();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<StarSystem>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("AK_StarSystems_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}