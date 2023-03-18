using System;
using System.Collections.Generic;
using DevKit.Core.Objects;
using DevKit.Entities.Demo.Characters;
using DevKit.Entities.Demo.Characters.API;
using DevKit.Entities.Demo.Engine;
using DevKit.Logging;
using DevKit.Logging.Extensions;
using DevKit.Serialization.Json.Extensions;

namespace DevKit.Entities.Demo.Boot
{
    public class Loader : BaseMonoBehaviour
    {
        private IEntityEngine _engine;

        private IEntityEngineConfig _config;

        protected override void OnAwake()
        {
            base.OnAwake();

            Logger.Default.Config.IsEnabled = true;

            _config = new EntityEngineConfig();
            this.LogInfo($"Instantiated {_config}");

            _engine = new EntityEngine();
            this.LogInfo($"Instantiated {_engine}");

            _engine.Init(_config);
            this.LogInfo($"{_engine}.{nameof(_engine.Init)}: {_config}");

            var values = GetConfig();
            _config.Init(values);
            this.LogInfo($"{_config}.{nameof(_config.Init)}: {values}");

            _engine.Register<IPlayerEntity, PlayerEntity>();
            this.LogInfo($"{_engine}.{nameof(_engine.Register)}: {typeof(IPlayerEntity)}");


        }

        private Dictionary<Type, EntityConfig> GetConfig()
        {
            var playerConfig = new EntityConfig();
            playerConfig.PropertyValues["health"] = new PropertyValueHolder {Number = 1000000};
            playerConfig.PropertyValues["damage"] = new PropertyValueHolder {Number = 1000};
            playerConfig.PropertyValues["isTargetable"] = new PropertyValueHolder {Bool = true};
            playerConfig.PropertyValues["canAttack"] = new PropertyValueHolder {Bool = true};
            this.LogInfo($"{playerConfig}: {playerConfig.PropertyValues.ToJson()}");

            var values = new Dictionary<Type, EntityConfig>
                {
                    {typeof(PlayerEntity), playerConfig}
                };
            return values;
        }
    }
}
