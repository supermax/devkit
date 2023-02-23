using UnityEngine;

namespace DevKit.Core.Objects
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {

        }
    }
}
