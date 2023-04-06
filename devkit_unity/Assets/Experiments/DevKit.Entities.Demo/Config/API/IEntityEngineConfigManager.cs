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
        /// <returns>Config Instance</returns>
        Task<IEngineConfig> LoadConfigFromFile();

        /// <summary>
        /// Save config to file
        /// </summary>
        /// <param name="config"></param>
        /// <returns>Config Instance</returns>
        void SaveConfigToFile(IEngineConfig config);

        /// <summary>
        /// Loads config from server
        /// </summary>
        /// <returns>Config Instance</returns>
        Task<IEngineConfig> LoadConfigFromServer();

        /// <summary>
        /// Saves config to server
        /// <param name="config"></param>
        /// </summary>
        void SaveConfigToServer(IEngineConfig config);
    }
}
