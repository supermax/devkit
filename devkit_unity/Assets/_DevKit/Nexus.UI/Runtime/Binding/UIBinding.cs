using DevKit.Core.Extensions;
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

        [SerializeField] private string _sourcePath;

        [SerializeField] private BaseComponentBinding _target;

        [SerializeField] private string _targetPath;

        [SerializeField] private BindingMode _bindingMode;

        [SerializeField] private bool _isBindingEnabled = true;

        protected IBinding Binding { get; set; }

        public BaseComponentBinding Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string SourcePath
        {
            get { return _sourcePath; }
            set { _sourcePath = value; }
        }

        public BaseComponentBinding Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public string TargetPath
        {
            get { return _targetPath; }
            set { _targetPath = value; }
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
                , !SourcePath.IsNullOrEmpty()
                    ? SourcePath
                    : nameof(BaseComponentBinding<Component,object>.TargetProperty)
                , Target
                , !TargetPath.IsNullOrEmpty()
                    ? TargetPath
                    : nameof(BaseComponentBinding<Component,object>.TargetProperty)
                , BindingMode);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Init();
        }
    }

}
