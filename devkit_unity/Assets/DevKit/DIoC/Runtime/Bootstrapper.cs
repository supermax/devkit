using System;
using DevKit.Core.Extensions;
using DevKit.DIoC.Config;
using DevKit.Logging;
using UnityEngine;
using Logger = DevKit.Logging.Logger;

namespace DevKit.DIoC
{
    /// <summary>
    /// Responsible for loading and mapping types into <see cref="Maestro"/> container
    /// </summary>
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeReference] protected BootConfig _bootConfig;

        public BootConfig BootConfig
        {
            get
            {
                return _bootConfig;
            }
            set
            {
                _bootConfig = value;
            }
        }

        private void Awake()
        {
            Setup();
        }

        /// <summary>
        /// Read <see cref="BootConfig"/> and map types in <see cref="Maestro"/>
        /// </summary>
        /// <remarks>Or, auto-load type mappings from program's assemblies is <see cref="BootConfig"/> `AutoLoad` Flag = True</remarks>
        protected virtual void Setup()
        {
            if (_bootConfig == null)
            {
                Logger.Default.LogInfo($"{nameof(BootConfig)} is null. Skipping {nameof(Setup)}");
                return;
            }
            if (_bootConfig.AutoConfig)
            {
                AutoConfig();
            }
            LoadConfig();
        }

        private void AutoConfig()
        {
            Logger.Default.LogInfo(nameof(AutoConfig));

            // TODO implement
        }

        private void LoadConfig()
        {
            if (_bootConfig == null)
            {
                Logger.Default.LogInfo($"{nameof(BootConfig)} is null. Skipping {nameof(Setup)}");
                return;
            }
            if (_bootConfig.Assemblies.IsNullOrEmpty())
            {
                Logger.Default.LogInfo($"{nameof(_bootConfig.Assemblies)} is null. Skipping {nameof(Setup)}");
            }

            Logger.Default.LogInfo("Processing {0}...", nameof(BootConfig));

            foreach (var assemblyConfig in _bootConfig.Assemblies)
            {
                if (assemblyConfig == null)
                {
                    Logger.Default.LogInfo($"{nameof(AssemblyConfig)} is null. Skipping it.");
                    continue;
                }
                if (assemblyConfig.Types.IsNullOrEmpty())
                {
                    Logger.Default.LogInfo($"[{assemblyConfig.Name}] {nameof(AssemblyConfig)}->{nameof(assemblyConfig.Types)} is null/empty. Skipping it.");
                    continue;
                }

                foreach (var typeConfig in assemblyConfig.Types)
                {
                    if (typeConfig == null)
                    {
                        Logger.Default.LogInfo($"{nameof(TypeConfig)} is null. Skipping it.");
                        continue;
                    }

                    Map(typeConfig);
                }
            }

            Logger.Default.LogInfo("Processed {0}.", nameof(BootConfig));
        }

        private static void Map(TypeConfig typeConfig)
        {
            var maestroInterfaceType = typeof (IMaestro);
            var mapMethodInfo = maestroInterfaceType.GetMethod(nameof(Maestro.Default.Map));
            var sourceType = Type.GetType(typeConfig.SourceType);
            var genericMapMethodInfo = mapMethodInfo.MakeGenericMethod(sourceType);

            genericMapMethodInfo.Invoke(Maestro.Default, null);

            //Maestro.Default.Map<object>().Singleton<object>();
        }
    }
}
