using System;
using System.Collections.Generic;
using DevKit.Core.Extensions;
using DevKit.Core.Objects;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters;
using DevKit.Entities.Demo.Characters.API;
using DevKit.Entities.Demo.Characters.Enemies;
using DevKit.Entities.Demo.Characters.Enemies.API;
using DevKit.Entities.Demo.Characters.Players;
using DevKit.Entities.Demo.Characters.Players.API;
using DevKit.Entities.Demo.Config;
using DevKit.Entities.Demo.Config.API;
using DevKit.Entities.Demo.Engine;
using DevKit.Entities.Demo.Game;
using DevKit.Entities.Demo.Game.API;
using DevKit.Entities.Extensions;
using DevKit.Logging;
using DevKit.Logging.Extensions;
using DevKit.Serialization.Json;
using DevKit.Serialization.Json.Extensions;
using IEntityEngine = DevKit.Entities.Demo.Engine.API.IEntityEngine;
using Random = UnityEngine.Random;

namespace DevKit.Entities.Demo.Boot
{
    public class Loader : BaseMonoBehaviour
    {
        private IEntityEngine _engine;

        private IEngineConfig _config;

        private IEntityEngineConfigManager _configManager;

        protected override void OnAwake()
        {
            base.OnAwake();

            Logger.Default.Config.IsEnabled = true;

            JsonMapper.Default.RegisterImporter<IGameSettings, GameSettings>(gs =>
                new GameSettings());

            _config = new EntityEngineConfig();
            this.LogInfo($"Instantiated {nameof(EntityEngineConfig)}: {_config}");

            _engine = new EntityEngine();
            this.LogInfo($"Instantiated {nameof(EntityEngine)}: {_engine}");

            var values = GetConfig();
            _config.Init(values);
            this.LogInfo($"{_config}.{nameof(_config.Init)}: {values}");

            _engine.Init(_config);
            this.LogInfo($"{_engine}.{nameof(_engine.Init)}: {_config}");

            _engine.Register<IGameSettings, GameSettings>();
            this.LogInfo($"{_engine}.{nameof(_engine.Register)}: {typeof(IGameSettings)}");

            var gameSettings = _engine.Create<IGameSettings>();
            var gameSettingsJson = gameSettings.ToJson();
            this.LogInfo($"Created {nameof(GameSettings)}: {gameSettingsJson}");

            _engine.Register<IPlayerEntity, PlayerEntity>();
            this.LogInfo($"{_engine}.{nameof(_engine.Register)}: {typeof(IPlayerEntity)}");

            var player = _engine.Create<IPlayerEntity>();
            player.GameSettings = gameSettings;
            var playerJson = player.ToJson();
            this.LogInfo($"Created {nameof(PlayerEntity)}: {playerJson}");

            _engine.Register<IEnemyEntity, EnemyEntity>();
            this.LogInfo($"{_engine}.{nameof(_engine.Register)}: {typeof(IEnemyEntity)}");

            for (var i = 0; i < 5; i++)
            {
                var enemy = _engine.Create<IEnemyEntity>();
                var enemyJson = enemy.ToJson();
                this.LogInfo($"Created {nameof(EnemyEntity)}: {enemyJson}");
            }

            _engine.Register<IPlayerLoginProfile, PlayerLoginProfile>();
            this.LogInfo($"{_engine}.{nameof(_engine.Register)}: {typeof(IPlayerLoginProfile)}");

            var loginProfile = _engine.Create<IPlayerLoginProfile>();
            var jsonLoginProfile = loginProfile.ToJson();
            this.LogInfo($"Created {nameof(PlayerLoginProfile)}: {jsonLoginProfile}");

            _configManager = new EntityEngineConfigManager();
            _configManager.SaveConfigToFile(_config);
        }

        private Dictionary<string, EntityConfig> GetConfig()
        {
            var playerConfig = new EntityConfig();
            playerConfig.PropertyValues["typeId"] = EntityExtensions.GetTypeId<PlayerEntity>(null);
            playerConfig.PropertyValues["health"] = Random.Range(1000, 1000000);
            playerConfig.PropertyValues["damage"] = Random.Range(1000, 1000000);
            playerConfig.PropertyValues["isTargetable"] = Random.Range(0, 1) == 1;
            playerConfig.PropertyValues["canAttack"] = Random.Range(0, 1) == 1;
            playerConfig.PropertyValues[PlayerEntity.GetCanAttackTargetKey<EnemyEntity>().ToJsonPropName()] = Random.Range(0, 1) == 1;
            playerConfig.PropertyValues[PlayerEntity.GetIsTargetableByKey<EnemyEntity>().ToJsonPropName()] = Random.Range(0, 1) == 1;
            this.LogInfo($"{playerConfig}: {playerConfig.PropertyValues.ToJson()}");

            var enemyConfig = new EntityConfig();
            enemyConfig.PropertyValues["typeId"] = EntityExtensions.GetTypeId<EnemyEntity>(null);
            enemyConfig.PropertyValues["health"] = Random.Range(1000, 1000000);
            enemyConfig.PropertyValues["damage"] = Random.Range(1000, 1000000);
            enemyConfig.PropertyValues["isTargetable"] = Random.Range(0, 1) == 1;
            enemyConfig.PropertyValues["canAttack"] = Random.Range(0, 1) == 1;
            enemyConfig.PropertyValues[EnemyEntity.GetCanAttackTargetKey<PlayerEntity>().ToJsonPropName()] = Random.Range(0, 1) == 1;
            enemyConfig.PropertyValues[EnemyEntity.GetIsTargetableByKey<PlayerEntity>().ToJsonPropName()] = Random.Range(0, 1) == 1;
            this.LogInfo($"{enemyConfig}: {enemyConfig.PropertyValues.ToJson()}");

            var gameSettingsConfig = new EntityConfig();
            gameSettingsConfig.PropertyValues["typeId"] = EntityExtensions.GetTypeId<GameSettings>(null);
            gameSettingsConfig.PropertyValues["isMusicEnabled"] = Random.Range(0, 1) == 1;
            gameSettingsConfig.PropertyValues["isSfxEnabled"] = Random.Range(0, 1) == 1;
            gameSettingsConfig.PropertyValues["pushNotificationsEnabled"] = Random.Range(0, 1) == 1;
            this.LogInfo($"{gameSettingsConfig}: {gameSettingsConfig.PropertyValues.ToJson()}");

            var values = new Dictionary<string, EntityConfig>
                {
                    {nameof(IGameSettings).ToJsonPropName(), gameSettingsConfig},
                    {nameof(IPlayerEntity).ToJsonPropName(), playerConfig},
                    {nameof(IEnemyEntity).ToJsonPropName(), enemyConfig}
                };
            return values;
        }
    }
}
