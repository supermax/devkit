#region

using UnityEngine;

#endregion

namespace DevKit.PubSub.Demo
{
    [CreateAssetMenu(fileName = "MultiThreadingToggle", menuName = "PubSub/Demo/MultiThreadingToggle")]
    public class MultiThreadingToggle : ScriptableObject
    {
        [SerializeField] private bool _isMultiThreadingOn = true;

        public bool IsMultiThreadingOn
        {
            get
            {
                return _isMultiThreadingOn;
            }
            set
            {
                _isMultiThreadingOn = value;
            }
        }
    }
}
