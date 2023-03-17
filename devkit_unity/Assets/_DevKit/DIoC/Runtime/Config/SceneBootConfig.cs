using System;
using DevKit.Core.Objects;
using UnityEngine;

namespace DevKit.DIoC.Config
{
    [Serializable]
    public class SceneBootConfig : BaseMonoBehaviour
    {
        [SerializeField]
        private SceneBootObjectConfig[] _sceneBootObjects;
    }

}
