using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Nexus.UI.Binding.Legacy
{
    [RequireComponent(typeof(Text))]
    public class TextBindingMapping : BaseComponentBinding<Text, string>
    {
        public override string TargetProperty
        {
            get { return Target.text; }
            set
            {
                if (Target.text == value)
                {
                    return;
                }

                var prevTextValue = Target.text;
                Target.text = value;

                InvokePropertyChanged(nameof(TargetProperty), prevTextValue, value);
            }
        }
    }
}
