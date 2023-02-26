using System;
using DevKit.Core.Extensions;
using DevKit.DIoC.Config;
using DevKit.DIoC.Extensions;
using UnityEditor;
using UnityEngine;

namespace DevKit.DIoC.Editor.Config
{
    [Serializable]
    public class TypeMappingConfig : BaseMappingConfig<TypeConfig>
    {
        [SerializeField]
        private TypeInitTrigger _initTrigger;

        [SerializeField]
        private bool _singleton;

        [SerializeField]
        private MonoScript _type;

        [SerializeField]
        private MonoScript[] _implementations;

        [SerializeField]
        private MonoScript[] _dependencies;

        public override TypeConfig GetConfig()
        {
            var config = new TypeConfig
                {
                    Name = Name,
                    InitTrigger = _initTrigger,
                    IsSingleton = _singleton,
                    SourceType = _type.text.GetFullTypeName(),
                };

            if (!_implementations.IsNullOrEmpty())
            {
                config.TypeMappings = new string[_implementations.Length];
                for (var i = 0; i < _implementations.Length; i++)
                {
                    if (_implementations[i] == null)
                    {
                        continue;
                    }
                    config.TypeMappings[i] = _implementations[i].text.GetFullTypeName();
                }
            }
            if (_dependencies.IsNullOrEmpty())
            {
                return config;
            }

            config.TypeDependencies = new string[_dependencies.Length];
            for (var i = 0; i < _dependencies.Length; i++)
            {
                if (_dependencies[i] == null)
                {
                    continue;
                }
                config.TypeDependencies[i] = _dependencies[i].text.GetFullTypeName();
            }
            return config;
        }
    }
}
