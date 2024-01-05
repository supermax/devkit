using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Nexus.UI.Binding.Legacy
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldBinding : BaseComponentBinding<InputField, string>
    {
        private string _prevTextValue;

        public override string TargetProperty
        {
            get { return Target.text; }
            set
            {
                if (Target.text == value)
                {
                    return;
                }

                Target.text = value;
                OnValueChanged(value);
            }
        }

        protected override void Init()
        {
            base.Init();

            _prevTextValue = Target.text;
            Target.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (Target)
            {
                Target.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        private void OnValueChanged(string text)
        {
            InvokePropertyChanged(nameof(TargetProperty), _prevTextValue, text);
            _prevTextValue = text;
        }
    }
}
