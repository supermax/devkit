using System;
using System.Threading.Tasks;
using DevKit.Entities.API;
using DevKit.Entities.Demo.Characters.API;

namespace DevKit.Entities.Demo.Config.API
{
    /// <summary>
    /// Interface for Entity Engine Config Manager
    /// </summary>
    public interface IEntityEngineConfigManager
    {
        /// <summary>
        /// Loads config from file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>Config Instance</returns>
        Task<IEngineConfig> LoadConfigFromFile(string filePath);

        /// <summary>
        /// Save config to file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="config"></param>
        /// <returns>Config Instance</returns>
        void SaveConfigToFile(string filePath, IEngineConfig config);

        /// <summary>
        /// Loads config from server
        /// </summary>
        /// <param name="endpoint">Server's endpoint to fetch config</param>
        /// <returns>Config Instance</returns>
        Task<IEngineConfig> LoadConfigFromServer(Uri endpoint);
    }
}
