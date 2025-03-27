using DevKit.Core.Extensions;
using UnityEngine;

namespace DevKit.Core.Objects
{
    public class MonoBehaviourSingleton<TInterface, TImplementation> : BaseMonoBehaviour
        where TImplementation : MonoBehaviour, TInterface
    {
        private static TInterface _default;

        public static TInterface Default
        {
            get
            {
                var res = InvalidateInstance();
                return res;
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            InvalidateInstance();
        }

        private static TInterface InvalidateInstance()
        {
            if (!Equals(_default, default(TImplementation)))
            {
                return _default;
            }

            var typeInterface = typeof(TInterface);
            var objects = FindObjectsOfType<TImplementation>();
            if(!objects.IsNullOrEmpty())
            {
                foreach (var obj in objects)
                {
                    if(typeInterface.IsInstanceOfType(obj))
                    {
                        _default = obj;
                    }
                }
            }

            if (!Equals(_default, null))
            {
                return _default;
            }

            var typeImplementation = typeof(TImplementation);
            var go = new GameObject($"[{typeImplementation.Name}]");
            var root = MonoBehaviourRoot.GetRoot();
            go.transform.SetParent(root.transform);

            _default = go.AddComponent<TImplementation>();
            return _default;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_default != null && _default.Equals(this))
            {
                _default = default;
            }
        }
    }
}
