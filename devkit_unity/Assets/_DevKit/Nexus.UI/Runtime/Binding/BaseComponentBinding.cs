using DevKit.Core.Observables;
using DevKit.Nexus.Binding;
using DevKit.Nexus.Binding.API;
using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Nexus.UI.Binding
{
    public abstract class BaseComponentBinding<TComponent, TData>
        : ObservableComponent
        where TComponent : Object, ILayoutElement
    {
        [SerializeField] private Component _source;

        [SerializeField] private string _sourcePath;

        [SerializeField] private BindingMode _bindingMode;

        [SerializeField] private bool _isBindingEnabled = true;

        protected IBinding Binding { get; set; }

        public virtual Component Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public virtual string SourcePath
        {
            get { return _sourcePath; }
            set { _sourcePath = value; }
        }

        public virtual TComponent Target
        {
            get;
            set;
        }

        public abstract TData TargetProperty
        {
            get;
            set;
        }

        public virtual BindingMode BindingMode
        {
            get { return _bindingMode;}
            set { _bindingMode = value; }
        }

        public bool IsBindingEnabled
        {
            get { return _isBindingEnabled; }
            set { _isBindingEnabled = value; }
        }

        protected virtual void Init()
        {
            if (!Target)
            {
                Target = FindObjectOfType<TComponent>();
            }

            if (!_isBindingEnabled)
            {
                return;
            }

            Object src = Source.GetComponent<ObservableComponent>();
            if (!src)
            {
                src = Source;
            }

            Binding = BindingManager.Default.Bind(src, SourcePath, this, nameof(TargetProperty), BindingMode);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Init();
        }
    }

}
