using System;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DevKit.DIoC.Config
{
    [Serializable]
    [DataContract]
    public class SceneBootObjectConfig : BaseConfig
    {
        [SerializeField]
        private MonoBehaviour _instance;

        [SerializeField]
        private MonoScript _interface;

        [SerializeField]
        private MonoScript[] _dependencies;
    }
}
