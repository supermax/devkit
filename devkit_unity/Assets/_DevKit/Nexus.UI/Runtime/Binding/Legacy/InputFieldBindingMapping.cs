using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Nexus.UI.Binding.Legacy
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldBindingMapping : BaseComponentBinding<InputField, string>
    {
        private InputField _target;

        public override InputField Target
        {
            get { return _target; }
            protected set
            {
                if (Equals(_target, value))
                {
                    return;
                }

                _target = value;
                if (_target == null)
                {
                    return;
                }

                _prevTextValue = TargetProperty;
                Target.onValueChanged.AddListener(OnValueChanged);
            }
        }

        private string _prevTextValue;

        public override string TargetProperty
        {
            get
            {
                return !Target ? null : Target.text;
            }
            set
            {
                if (!Target || Target.text == value)
                {
                    return;
                }
                Target.text = value;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (!Target)
            {
                return;
            }

            Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string text)
        {
            InvokePropertyChanged(nameof(TargetProperty), _prevTextValue, text);
            _prevTextValue = text;
        }
    }
}
