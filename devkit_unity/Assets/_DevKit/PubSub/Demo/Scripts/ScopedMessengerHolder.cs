using DevKit.Core.Objects;
using DevKit.Logging.Extensions;
using DevKit.PubSub.API;

namespace DevKit.PubSub.Demo
{
    public class ScopedMessengerHolder : BaseMonoBehaviour
    {
        public IMessenger ScopedMessenger { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            ScopedMessenger = new Messenger();
            this.LogInfo($"--- {nameof(ScopedMessenger)}.{nameof(ScopedMessenger.GetHashCode)}(): {ScopedMessenger.GetHashCode()}");
        }

        protected override void OnDestroy()
        {
            var messenger = (Messenger)ScopedMessenger;
            messenger.Dispose();
            this.LogInfo($"--- {nameof(ScopedMessenger)}.{nameof(messenger.Dispose)}()");

            base.OnDestroy();
        }
    }
}
