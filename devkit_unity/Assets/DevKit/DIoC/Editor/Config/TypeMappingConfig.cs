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
        [Tooltip("If checked, then the instance of this type will be singleton")]
        private bool _singleton;

        [SerializeReference]
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
                    SourceType = GetTypeName(_type),
                    TypeMappings = GetTypesNames(_implementations),
                    TypeDependencies = GetTypesNames(_dependencies)
                };
            return config;
        }

        private static string[] GetTypesNames(MonoScript[] scripts)
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

        private static string GetTypeName(MonoScript script)
        {
            if (script == null)
            {
                return null;
            }

            var implementationType = script.GetClass();
            var typeName = implementationType != null ? implementationType.FullName : script.text.GetFullTypeName();
            return typeName;
        }
    }
}
