using System;
using UnityEngine;

namespace DevKit.Nexus.UI.Binding.Config
{
    [Serializable]
    public class UIBindingMapping
    {
        [SerializeField] private string _type;

        [SerializeField] private GameObject _prefab;
    }
}
