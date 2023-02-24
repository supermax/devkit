using System;
using DevKit.Core.Extensions;
using DevKit.DIoC.Config;
using UnityEditorInternal;
using UnityEngine;

namespace DevKit.DIoC.Editor.Config
{
    [Serializable]
    public class AssemblyMappingConfig : BaseMappingConfig<AssemblyConfig>
    {
        [SerializeField] private AssemblyDefinitionAsset _assembly;

        [SerializeField] private TypeMappingConfig[] _types;

        public override AssemblyConfig GetConfig()
        {
            var config = new AssemblyConfig();
            if (_assembly != null)
            {
                Name = _assembly.name;
                config.Name = _assembly.name;
            }

            if (_types.IsNullOrEmpty())
            {
                return config;
            }

            config.Types = new TypeConfig[_types.Length];
            for (var i = 0; i < _types.Length; i++)
            {
                config.Types[i] = _types[i].GetConfig();
            }
            return config;
        }
    }
}
