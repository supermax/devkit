using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Nexus.UI.Binding.Legacy
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldBindingMapping : BaseComponentBinding<InputField, string>
    {
        public override InputField Target
        {
            get { return base.Target; }
            protected set
            {
                if (Equals(base.Target, value))
                {
                    return;
                }

                if (base.Target != null && Target.onValueChanged != null)
                {
                    Target.onValueChanged.RemoveListener(OnValueChanged);
                }

                base.Target = value;
                if (base.Target == null || !Target)
                {
                    return;
                }

                _prevTextValue = TargetProperty;
                Target.onValueChanged?.AddListener(OnValueChanged);
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
