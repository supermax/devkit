using System.Collections;
using DevKit.Core.Objects;
using DevKit.Logging.Extensions;
using UnityEngine;

namespace DevKit.PubSub.Demo
{
    public class LocalPublisher : BaseMonoBehaviour
    {
        [SerializeField]
        private ScopedMessengerHolder _holder;

        private IEnumerator Start()
        {
            yield return PublishAsync();
        }

        private IEnumerator PublishAsync()
        {
            yield return null;

            var payload = new ChatPayload {UserId = Application.productName, Text = Application.companyName};
            this.LogInfo($"--- Published: {nameof(_holder.ScopedMessenger)}" +
                         $".{nameof(_holder.ScopedMessenger.Publish)}<{nameof(ChatPayload)}>({payload})");

            _holder.ScopedMessenger.Publish(payload);
        }
    }
}
