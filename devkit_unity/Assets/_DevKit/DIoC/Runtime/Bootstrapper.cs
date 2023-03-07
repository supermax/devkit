using System;
using DevKit.Core.Extensions;
using DevKit.Core.Objects;
using DevKit.DIoC.Config;
using DevKit.Logging.Extensions;
using UnityEngine;
using Logger = DevKit.Logging.Logger;

namespace DevKit.DIoC
{
    /// <summary>
    /// Responsible for loading and mapping types into <see cref="Maestro"/> container
    /// </summary>
    public class Bootstrapper : BaseMonoBehaviour
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
                this.LogInfo($"{nameof(BootConfig)} is null. Skipping {nameof(Setup)}");
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
                this.LogInfo($"{nameof(BootConfig)} is null. Skipping {nameof(Setup)}");
                return;
            }
            if (_bootConfig.Assemblies.IsNullOrEmpty())
            {
                this.LogInfo($"{nameof(_bootConfig.Assemblies)} is null. Skipping {nameof(Setup)}");
            }

            this.LogInfo("Processing {0}...", nameof(BootConfig));

            foreach (var assemblyConfig in _bootConfig.Assemblies)
            {
                if (assemblyConfig == null)
                {
                    this.LogInfo($"{nameof(AssemblyConfig)} is null. Skipping it.");
                    continue;
                }
                if (assemblyConfig.Types.IsNullOrEmpty())
                {
                    this.LogInfo($"[{assemblyConfig.Name}] {nameof(AssemblyConfig)}->{nameof(assemblyConfig.Types)} is null/empty. Skipping it.");
                    continue;
                }

                foreach (var typeConfig in assemblyConfig.Types)
                {
                    if (typeConfig == null)
                    {
                        this.LogInfo($"{nameof(TypeConfig)} is null. Skipping it.");
                        continue;
                    }

                    Map(assemblyConfig, typeConfig);
                }
            }

            this.LogInfo("Processed {0}.", nameof(BootConfig));
        }

        private static void Map(AssemblyConfig assemblyConfig, TypeConfig typeConfig)
        {
            //TODO move assembly name to type map

            assemblyConfig.ThrowIfNull(nameof(assemblyConfig));
            typeConfig.ThrowIfNull(nameof(typeConfig));

            var maestroInterfaceType = typeof (IMaestro);
            var mapMethodInfo = maestroInterfaceType.GetMethod(nameof(Maestro.Default.Map));
            var sourceType = Type.GetType($"{typeConfig.SourceType}, {assemblyConfig.Name}", true, true);
            var genericMapMethodInfo = mapMethodInfo.MakeGenericMethod(sourceType);

            var typeMapObject = genericMapMethodInfo.Invoke(Maestro.Default, null);
            var typeMapObjectType = typeMapObject.GetType();
            var mapToTypeMethodInfo = typeMapObjectType.GetMethod("To");
            var targetTypeName = typeConfig.TypeMappings[0];
            var targetType = Type.GetType($"{targetTypeName}, {assemblyConfig.Name}", true, true);
            var genericMapToMethodInfo = mapToTypeMethodInfo.MakeGenericMethod(targetType);
            typeMapObject = genericMapToMethodInfo.Invoke(typeMapObject, new object[]{string.Empty});

            Debug.LogWarning(typeMapObject);

            //Maestro.Default.Map<object>().Singleton<object>();
        }
    }
}
