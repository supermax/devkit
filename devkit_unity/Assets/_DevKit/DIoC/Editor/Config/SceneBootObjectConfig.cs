using System;
using DevKit.DIoC.Config;
using UnityEditor;
using UnityEngine;

namespace DevKit.DIoC.Editor.Config
{
    [Serializable]
    public class SceneBootMappingConfig : BaseMappingConfig<SceneBootObjectConfig>
    {
        [SerializeField]
        private MonoBehaviour _instance;

        [SerializeField]
        private MonoScript _interface;

        [SerializeField]
        private MonoScript[] _dependencies;
        
        public override SceneBootObjectConfig GetConfig()
        {
            throw new NotImplementedException();
        }
    }
}
