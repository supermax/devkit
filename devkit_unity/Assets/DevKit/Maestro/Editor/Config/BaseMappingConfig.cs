using UnityEngine;

namespace DevKit.Editor.IOC.Config
{
    public abstract class BaseMappingConfig<T>
    {
        [SerializeField]
        protected string Name;

        public abstract T GetConfig();
    }
}
