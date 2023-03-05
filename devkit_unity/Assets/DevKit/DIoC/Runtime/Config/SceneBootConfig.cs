using System;
using System.Runtime.Serialization;
using DevKit.Core.Objects;
using UnityEngine;

namespace DevKit.DIoC.Config
{
    [Serializable]
    [DataContract]
    public class SceneBootConfig : BaseMonoBehaviour
    {
        [SerializeField]
        private SceneBootObjectConfig[] _sceneBootObjects;
    }

}
