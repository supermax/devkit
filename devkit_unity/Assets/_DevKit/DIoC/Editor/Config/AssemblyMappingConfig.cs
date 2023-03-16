using System;
using System.Reflection;
using DevKit.Core.Extensions;
using DevKit.DIoC.Config;
using DevKit.Serialization.Json.Extensions;
using UnityEditorInternal;
using UnityEngine;

namespace DevKit.DIoC.Editor.Config
{
    [Serializable]
    public class AssemblyMappingConfig : BaseMappingConfig<AssemblyConfig>
    {
        [SerializeField]
        private AssemblyDefinitionAsset _assemblyDefinition;

        [SerializeField]
        private TypeMappingConfig[] _typeMappings;

        public override AssemblyConfig GetConfig()
        {
            var config = new AssemblyConfig();
            if (_assemblyDefinition != null)
            {
                var info = _assemblyDefinition.text.ToObject<AssemblyInfo>();
                Name = info.Name;
                config.Name = Name;
            }
            if (Name.IsNullOrEmpty())
            {
                return config;
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assemblyName = Name.ToLowerInvariant();
            Assembly selectedAssembly = null;
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.IsNullOrEmpty()
                    || !assembly.FullName.ToLowerInvariant().Contains(assemblyName))
                {
                    continue;
                }
                selectedAssembly = assembly;
            }

            if (selectedAssembly == null || _typeMappings.IsNullOrEmpty())
            {
                return config;
            }

            config.Types = new TypeConfig[_typeMappings.Length];
            for (var i = 0; i < _typeMappings.Length; i++)
            {
                _typeMappings[i].Assembly = selectedAssembly;
                config.Types[i] = _typeMappings[i].GetConfig();
            }
            return config;
        }
    }

}
