using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace DevKit.DIoC.Config
{
    [Serializable]
    [DataContract]
    public class SceneBootObjectConfig : BaseConfig
    {
        [SerializeField]
        private MonoBehaviour _instance;

        public MonoBehaviour Instance
        {
            get { return _instance;}
            set { _instance = value; }
        }

        public Type SourceType { get; set; }

        public Type[] TypeMappings { get; set; }

        public Type[] TypeDependencies { get; set; }
    }
}
