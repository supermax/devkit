using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DevKit.Core.Extensions;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Config.API;
using DevKit.Serialization.Json.Extensions;

namespace DevKit.Entities.Demo.Config
{
    public class EntityEngineConfigManager : IEntityEngineConfigManager
    {
        /// <inheritdoc/>
        public Task<IEngineConfig> LoadConfigFromFile(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async void SaveConfigToFile(string filePath, IEngineConfig config)
        {
            filePath.ThrowIfNullOrEmpty(nameof(filePath));
            config.ThrowIfNull(nameof(config));

            filePath.ThrowIfNullOrEmpty(nameof(filePath));
            config.ThrowIfNull(nameof(config));

            var json = config.ToJson();
            json.ThrowIfNullOrEmpty(nameof(json));

            await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
        }

        /// <inheritdoc/>
        public Task<IEngineConfig> LoadConfigFromServer(Uri endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
