using DevKit.Core.Objects;
using DevKit.Logging.Extensions;
using UnityEngine;

namespace DevKit.PubSub.Demo
{
    public class LocalSubscriber : BaseMonoBehaviour
    {
        [SerializeField]
        private ScopedMessengerHolder _holder;

        protected override void OnStart()
        {
            base.OnStart();

            _holder.ScopedMessenger.Subscribe<ChatPayload>(OnChatPayloadCallback);

            this.LogInfo($"--- Subscribed: {nameof(_holder.ScopedMessenger)}" +
                         $".{nameof(_holder.ScopedMessenger.Subscribe)}<{nameof(ChatPayload)}>({nameof(OnChatPayloadCallback)})");
        }

        private void OnChatPayloadCallback(ChatPayload payload)
        {
            this.LogInfo($"--- Received: {nameof(ChatPayload)}: {payload}");
        }

        protected override void OnDestroy()
        {
            _holder.ScopedMessenger.Unsubscribe<ChatPayload>(OnChatPayloadCallback);

            this.LogInfo($"--- Unsubscribed: {nameof(_holder.ScopedMessenger)}" +
                         $".{nameof(_holder.ScopedMessenger.Unsubscribe)}<{nameof(ChatPayload)}>({nameof(OnChatPayloadCallback)})");

            base.OnDestroy();
        }
    }
}
