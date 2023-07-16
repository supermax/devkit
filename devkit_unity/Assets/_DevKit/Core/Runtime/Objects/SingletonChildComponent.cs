using UnityEngine;

namespace DevKit.Core.Objects
{
    public class SingletonChildComponent : MonoBehaviour
    {
        private void Awake()
        {
            var root = MonoBehaviourRoot.GetRoot();
            gameObject.transform.SetParent(root.transform);
            Debug.Log($"{name} moved under {root}");
        }
    }
}
