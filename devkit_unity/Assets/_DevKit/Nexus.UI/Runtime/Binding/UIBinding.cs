using DevKit.Core.Observables;
using DevKit.Logging.Extensions;
using DevKit.Nexus.Binding;
using DevKit.Nexus.Binding.API;
using UnityEngine;

namespace DevKit.Nexus.UI.Binding
{
    public class UIBinding : ObservableComponent
    {
        [SerializeField] private BaseComponentBinding _source;

        [SerializeField] private BaseComponentBinding _target;

        [SerializeField] private BindingMode _bindingMode;

        [SerializeField] private bool _isBindingEnabled = true;

        protected IBinding Binding { get; set; }

        public BaseComponentBinding Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public BaseComponentBinding Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public BindingMode BindingMode
        {
            get { return _bindingMode;}
            set { _bindingMode = value; }
        }

        public bool IsBindingEnabled
        {
            get { return _isBindingEnabled; }
            set { _isBindingEnabled = value; }
        }

        private void Init()
        {
            if (!IsBindingEnabled)
            {
                this.LogInfo($"{nameof(IsBindingEnabled)} = {false}, skipping binding");
                return;
            }

            Binding = BindingManager.Default.Bind(
                Source
                , nameof(BaseComponentBinding<Component,object>.TargetProperty)
                , Target
                , nameof(BaseComponentBinding<Component,object>.TargetProperty)
                , BindingMode);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Init();
        }
    }

}
