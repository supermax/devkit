using System.IO;
using System.Linq;
using DevKit.Core.Extensions;
using DevKit.DIoC.Config;
using DevKit.Serialization.Json.Extensions;
using UnityEditor;
using UnityEngine;

namespace DevKit.DIoC.Editor.Config
{
    [CreateAssetMenu(fileName = "BootConfig", menuName = "Bootstrapper/Boot Config")]
    public class BootMappingConfig : BootConfig
    {
        [SerializeField] private bool _autoConfig;

        [SerializeField] private AssemblyMappingConfig[] _assemblies;

        private void OnValidate()
        {
            Debug.Log(nameof(OnValidate));

            AutoConfig = _autoConfig;
            Assemblies = _assemblies.Select(assemblyMapping => assemblyMapping.GetConfig()).ToArray();
            var json = Assemblies.ToJson();
            Debug.Log(json);

            var configFilePath = AssetDatabase.GetAssetPath(this);
            configFilePath = Path.ChangeExtension(configFilePath, "json");

            // TODO add checks if file writing is in progress and don't call this function again until it's execution is done
            File.WriteAllTextAsync(configFilePath, json);
        }
    }
}
