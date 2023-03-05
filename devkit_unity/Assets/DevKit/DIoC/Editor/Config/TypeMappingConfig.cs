using System;
using System.Reflection;
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
        [Tooltip("If checked, then the instance of this type will be singleton")]
        private bool _singleton;

        [SerializeReference]
        private MonoScript _type;

        [SerializeField]
        private MonoScript[] _implementations;

        [SerializeField]
        private MonoScript[] _dependencies;

        public Assembly Assembly { get; set; }

        public override TypeConfig GetConfig()
        {
            var config = new TypeConfig
                {
                    Name = Name,
                    InitTrigger = _initTrigger,
                    IsSingleton = _singleton,
                    SourceType = GetTypeName(_type),
                    TypeMappings = GetTypesNames(_implementations),
                    TypeDependencies = GetTypesNames(_dependencies)
                };
            if (Name.IsNullOrEmpty())
            {
                Name = config.SourceType;
            }
            if (config.Name.IsNullOrEmpty())
            {
                config.Name = Name;
            }
            return config;
        }

        private string[] GetTypesNames(MonoScript[] scripts)
        {
            if (scripts.IsNullOrEmpty())
            {
                return null;
            }

            var typeNames = new string[scripts.Length];
            for (var i = 0; i < scripts.Length; i++)
            {
                typeNames[i] = GetTypeName(scripts[i]);
            }
            return typeNames;
        }

        private string GetTypeName(MonoScript script)
        {
            script.ThrowIfNull(nameof(script));
            script.text.ThrowIfNullOrEmpty($"Empty {nameof(script)}.{nameof(script.text)}");

            var implementationType = script.GetClass();
            var typeName = implementationType != null ? implementationType.FullName : script.text.GetFullTypeName();

            if (typeName.IsNullOrEmpty())
            {
                throw new OperationCanceledException($"Script `{script.name}` has empty `{nameof(typeName)}`.");
            }
            typeName = typeName.ToLowerInvariant();

            Debug.LogFormat("Validating type: `{0}`...", typeName);

            var types = Assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.FullName.IsNullOrEmpty()
                    || !type.FullName.ToLowerInvariant().Contains(typeName))
                {
                    continue;
                }
                Debug.LogFormat("Validated type: `{0}`...", type.FullName);
                return type.FullName;
            }

            throw new OperationCanceledException($"Cannot get type `{typeName}`.");
        }
    }
}
