using System;
using DevKit.IOC.Config;
using UnityEditorInternal;
using UnityEngine;

namespace DevKit.Editor.IOC.Config
{
    [Serializable]
    public class AssemblyMappingConfig : BaseMappingConfig<AssemblyConfig>
    {
        [SerializeField] private AssemblyDefinitionAsset _assembly;

        [SerializeField] private TypeMappingConfig[] _types;

        public override AssemblyConfig GetConfig()
        {
            Name = _assembly.name;
            var config = new AssemblyConfig
                {
                    Name = _assembly.name,
                    Types = new TypeConfig[_types.Length]
                };
            for (var i = 0; i < _types.Length; i++)
            {
                config.Types[i] = _types[i].GetConfig();
            }
            return config;
        }
    }
}
