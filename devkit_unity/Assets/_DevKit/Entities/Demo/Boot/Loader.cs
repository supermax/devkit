using System;
using System.Collections.Generic;
using DevKit.Core.Objects;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters;
using DevKit.Entities.Demo.Characters.API;
using DevKit.Entities.Demo.Config;
using DevKit.Entities.Demo.Config.API;
using DevKit.Entities.Demo.Engine;
using DevKit.Logging;
using DevKit.Logging.Extensions;
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

            _config = new EntityEngineConfig();
            this.LogInfo($"Instantiated {_config}");

            _engine = new EntityEngine();
            this.LogInfo($"Instantiated {_engine}");

            var values = GetConfig();
            _config.Init(values);
            this.LogInfo($"{_config}.{nameof(_config.Init)}: {values}");

            _engine.Init(_config);
            this.LogInfo($"{_engine}.{nameof(_engine.Init)}: {_config}");

            _engine.Register<IPlayerEntity, PlayerEntity>();
            this.LogInfo($"{_engine}.{nameof(_engine.Register)}: {typeof(IPlayerEntity)}");

            var player = _engine.Create<IPlayerEntity>();
            var playerJson = player.ToJson();
            this.LogInfo($"Created {playerJson}");

            _engine.Register<IEnemyEntity, EnemyEntity>();
            this.LogInfo($"{_engine}.{nameof(_engine.Register)}: {typeof(IEnemyEntity)}");

            for (var i = 0; i < 5; i++)
            {
                var enemy = _engine.Create<IEnemyEntity>();
                var enemyJson = enemy.ToJson();
                this.LogInfo($"Created {enemyJson}");
            }

            _configManager = new EntityEngineConfigManager();
            _configManager.SaveConfigToFile("engine_config.json", _config);
        }

        private Dictionary<Type, EntityConfig> GetConfig()
        {
            var playerConfig = new EntityConfig();
            playerConfig.PropertyValues["typeId"] = nameof(PlayerEntity);
            playerConfig.PropertyValues["health"] = Random.Range(1000, 1000000);
            playerConfig.PropertyValues["damage"] = Random.Range(1000, 1000000);
            playerConfig.PropertyValues["isTargetable"] = Random.Range(0, 1) == 1;
            playerConfig.PropertyValues["canAttack"] = Random.Range(0, 1) == 1;
            playerConfig.PropertyValues[CharacterEntity<PlayerEntity>.GetCanAttackTargetKey<EnemyEntity>()] = Random.Range(0, 1) == 1;
            playerConfig.PropertyValues[CharacterEntity<PlayerEntity>.GetIsTargetableByKey<EnemyEntity>()] = Random.Range(0, 1) == 1;
            this.LogInfo($"{playerConfig}: {playerConfig.PropertyValues.ToJson()}");

            var enemyConfig = new EntityConfig();
            enemyConfig.PropertyValues["typeId"] = nameof(EnemyEntity);
            enemyConfig.PropertyValues["health"] = Random.Range(1000, 1000000);
            enemyConfig.PropertyValues["damage"] = Random.Range(1000, 1000000);
            enemyConfig.PropertyValues["isTargetable"] = Random.Range(0, 1) == 1;;
            enemyConfig.PropertyValues["canAttack"] = Random.Range(0, 1) == 1;;
            enemyConfig.PropertyValues[CharacterEntity<EnemyEntity>.GetCanAttackTargetKey<PlayerEntity>()] = Random.Range(0, 1) == 1;;
            enemyConfig.PropertyValues[CharacterEntity<EnemyEntity>.GetIsTargetableByKey<PlayerEntity>()] = Random.Range(0, 1) == 1;;
            this.LogInfo($"{enemyConfig}: {enemyConfig.PropertyValues.ToJson()}");

            var values = new Dictionary<Type, EntityConfig>
                {
                    {typeof(IPlayerEntity), playerConfig},
                    {typeof(IEnemyEntity), enemyConfig}
                };
            return values;
        }
    }
}
