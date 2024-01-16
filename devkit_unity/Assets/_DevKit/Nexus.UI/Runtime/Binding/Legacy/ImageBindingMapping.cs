using UnityEngine;
using UnityEngine.UI;

namespace DevKit.Nexus.UI.Binding.Legacy
{
    [RequireComponent(typeof(Image))]
    public class ImageBindingMapping : BaseComponentBinding<Image, Sprite>
    {
        public override Sprite TargetProperty
        {
            get { return Target != null && Target ? Target.sprite : null; }
            set
            {
                if (Target == null || !Target)
                {
                    return;
                }

                if (Equals(Target.sprite, value))
                {
                    return;
                }

                var prevSprite = Target.sprite;
                Target.sprite = value;
                InvokePropertyChanged(nameof(TargetProperty), prevSprite, value);
            }
        }
    }
}
