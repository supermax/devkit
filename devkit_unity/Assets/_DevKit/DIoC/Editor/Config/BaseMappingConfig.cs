using UnityEngine;

namespace DevKit.DIoC.Editor.Config
{
    public abstract class BaseMappingConfig<T>
    {
        [SerializeField]
        protected string Name;

        public abstract T GetConfig();
    }
}
