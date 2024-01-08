using DevKit.Core.Observables;
using DevKit.Nexus.UI.Binding.API;
using UnityEngine;

namespace DevKit.Nexus.UI.Binding
{
    public abstract class BaseComponentBinding
        : ObservableComponent
            , IComponentBinding
    {
    }

    public abstract class BaseComponentBinding<TT, TP>
        : BaseComponentBinding
        where TT : Component
    {
        public virtual TT Target { get; protected set; }

        public abstract TP TargetProperty { get; set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            if (Target)
            {
                return;
            }
            Target = GetComponent<TT>();
        }
    }
}
