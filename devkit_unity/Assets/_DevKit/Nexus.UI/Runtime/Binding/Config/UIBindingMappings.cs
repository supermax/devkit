using System.Collections.Generic;
using UnityEngine;

namespace DevKit.Nexus.UI.Binding.Config
{
    [CreateAssetMenu(fileName = "UIBindingsConfig", menuName = "UI Binding/Config")]
    public class UIBindingMappings : ScriptableObject
    {
        [SerializeField] private List<UIBindingMapping> _config = new();
    }
}
